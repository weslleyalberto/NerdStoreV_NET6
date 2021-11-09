﻿using NerdStore.Core.Data;

namespace NerdStore.Catalogo.Domain
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterTodos();
        Task<Produto> ObterPorId(Guid id);
        Task<IEnumerable<Produto>> ObterPorCategoria(int codigo);
        Task<IEnumerable<Categoria>> ObterCategorias();
        Task<Categoria> ObterCategoriasPorId(Guid id);
        void Adicionar(Produto produto);
        void Atualizar(Produto produto);    
        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categoria);
    }
}
