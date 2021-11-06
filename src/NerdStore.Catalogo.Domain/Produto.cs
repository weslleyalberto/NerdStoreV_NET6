using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain
{
    public class Produto : Entity , IAggragateRoot
    {
       
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get;private set; }
        public decimal Valor { get; private set; }
        public Guid CategoriaId { get; private set; }
        public DateTime DataCadastro { get;private set; }
        public string Imagem { get;private set; }
        public int QuantidadeEstoque { get; private set; }
        public Categoria Categoria { get; private set; }

        public Produto( string nome, string descricao, bool ativo, decimal valor, DateTime dataCadastro, string imagem)
        {
           
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Valor = valor;
            DataCadastro = dataCadastro;
            Imagem = imagem;
          
        }
        public void Ativar() => Ativo = true;
        public void Desativar()=> Ativo = false;
        public void AlterarCategoria(Categoria categoria)
        {
            Categoria = categoria;
            CategoriaId = categoria.Id;
        }
        public void AlterarDescricao(string descricao)
        {
            Descricao = descricao;
        }
        public void DebitarEstoque(int quantidade)
        {
            if (quantidade < 0) quantidade *= -1;
            QuantidadeEstoque -= quantidade;
        }
        public void ReporEstoque(int quantidade)
        {
            QuantidadeEstoque += quantidade;
        }
        public bool Possuiestoque(int quantidade)
        {
            return QuantidadeEstoque >= quantidade;
        }
        public void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome do produto não pode estar vazio");
            Validacoes.ValidarSeVazio(Descricao, "O campo descrição não pode estar vazio");
            Validacoes.ValidarSeDiferente(CategoriaId, Guid.Empty, "O campo categoriaId do produto não pode ser vazio");
            Validacoes.ValidarSeMenorIgualMinimo(Valor,0, "O campo valor do produto não pode ser menor igual a 0");
            Validacoes.ValidarSeVazio(Imagem, "O campo imagem do produto não pode estar vazio");

        }
    }
    public class Categoria : Entity
    {
        public string Nome { get; private set; }
        public int Codigo { get;private set; }

        public Categoria(string nome, int codigo)
        {
            Nome = nome;
            Codigo = codigo;
        }
        public override string ToString()
        {
            return $"{Nome} - {Codigo}";
        }
    }
}
