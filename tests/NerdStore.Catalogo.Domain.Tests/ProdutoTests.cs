using NerdStore.Core.DomainObjects;
using System;
using Xunit;

namespace NerdStore.Catalogo.Domain.Tests
{
    public class ProdutoTests
    {
        [Fact]
        public void Produto_Validar_ValidacoesDevemRetornarExceptions()
        {
            var ex = Assert.Throws<DomainException>(() => new Produto(nome: "Nome do produto", "Descricao 54654 56654 464", true, 100, DateTime.Now,Guid.NewGuid(), "46465465454", new Dimensoes(15.0M, 6.0M, 5.0M)
               ));
            Assert.Equal("O campo nome do produto não pode estar vazio",ex.Message);
   
          
        }
    }
}