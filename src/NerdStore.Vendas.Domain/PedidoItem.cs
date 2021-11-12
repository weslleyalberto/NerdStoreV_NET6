﻿using NerdStore.Core.DomainObjects;

namespace NerdStore.Vendas.Domain
{
    public class PedidoItem :Entity
    {
        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string ProdutoNome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        //EF relation
        public Pedido Pedido { get; set; }

    }
}
