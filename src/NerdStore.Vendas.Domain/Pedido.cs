using NerdStore.Core.DomainObjects;

namespace NerdStore.Vendas.Domain
{
    public class Pedido : Entity, IAggragateRoot
    {
        public int Codigo { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid? VoucherId { get; set; }
        public bool VoucherUtilizado { get; set; }
        public decimal Desconto { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }
        private readonly List<PedidoItem> _pedidoItems;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;
        //EF Relaciotion
        public virtual Voucher Voucher { get; private set; }

        protected Pedido()
        {
            _pedidoItems = new List<PedidoItem>();
        }

        public Pedido(Guid clienteId, bool voucherUtilizado, decimal desconto, decimal valorTotal)
        {
            ClienteId = clienteId;
            VoucherUtilizado = voucherUtilizado;
            Desconto = desconto;
            ValorTotal = valorTotal;
            _pedidoItems = new List<PedidoItem>();
        }

        public void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(p => p.CalcularValor());
            CalcularValorTotalDesconto();
        }

        public void CalcularValorTotalDesconto()
        {
            if (!VoucherUtilizado) return;
            decimal desconto = 0;
            var valor = ValorTotal;
            if(Voucher.TipoDescontoVoucher == TipoDescontoVoucher.Porcentagem)
            {
                if (Voucher.Percentual.HasValue)
                {
                    desconto = (valor * Voucher.Percentual.Value) / 100;
                    valor -= desconto;
                }
            }
            else
            {
                if (Voucher.ValorDesconto.HasValue)
                {
                    desconto = Voucher.ValorDesconto.Value;
                    valor -= desconto;
                }
            }
            ValorTotal = valor < 0 ? 0 : valor;
            Desconto = desconto;
        }
        public bool PedidoItemExistente(PedidoItem item) => _pedidoItems.Any(p => p.ProdutoId == item.ProdutoId);
        public void AdicionarItem(PedidoItem item)
        {
            if (!item.EhValido()) return;
            item.AssociarPedido(Id);
            if (PedidoItemExistente(item))
            {
                var itemsExistente = _pedidoItems.FirstOrDefault(p=> p.ProdutoId == item.ProdutoId);
                itemsExistente.AdicionarUnidades(item.Quantidade);
                item = itemsExistente;
                _pedidoItems.Remove(itemsExistente);
            }
            item.CalcularValor();
            _pedidoItems.Add(item);
            CalcularValorPedido();

        }
        public void RemoverItem(PedidoItem item)
        {
            if(!item.EhValido()) return;
            var itemExistente = PedidoItems.FirstOrDefault(p=> p.ProdutoId == item.ProdutoId);
            if (itemExistente is null)
                throw new DomainException("o item não pertece ao pedido");
            _pedidoItems.Remove(itemExistente);
            CalcularValorPedido();
        }
        public void AtualizarItem(PedidoItem item)
        {
            if(!item.EhValido()) return ;
            item.AssociarPedido(Id);
            var itemExistente = PedidoItems.FirstOrDefault(p=> p.ProdutoId ==item.ProdutoId);
            if (itemExistente is null)
                throw new DomainException("O item não pertece ao pedido");
            _pedidoItems.Remove(itemExistente);
            _pedidoItems.Add(item);
            CalcularValorPedido();
        }
        public void AtualizarUnidades(PedidoItem item, int unidades)
        {
            item.AtualizarUnidades(unidades);
            AtualizarItem(item);
        }
        public void TornarRascunho()
        {
            PedidoStatus = PedidoStatus.Rascunho;
        }
        public void IniciarPedido()
        {
            PedidoStatus = PedidoStatus.Iniciado;
        }
        public void FinalizarPedido()
        {
            PedidoStatus = PedidoStatus.Pago;
        }
        public void CancelarPedido()
        {
            PedidoStatus = PedidoStatus.Cancelado;
        }
        public static class PedidoFactor
        {
            public static Pedido NovoPedidoRascunho(Guid clienteId)
            {
                Pedido pedido = new()
                {
                    ClienteId = clienteId,
                };
                pedido.TornarRascunho();
                return pedido;
            }
        }
    }
}
