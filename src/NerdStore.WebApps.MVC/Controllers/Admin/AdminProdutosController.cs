﻿using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Application.ViewModels;
using NerdStore.Catalogo.Domain;

namespace NerdStore.WebApps.MVC.Controllers.Admin
{
    
    public class AdminProdutosController : Controller
    {
        private readonly IProdutoAppService _produtoAppService;

        public AdminProdutosController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }
        [HttpGet]
        [Route("admin-produtos")]
        public async Task<IActionResult> Index()
        {
            return View(await _produtoAppService.ObterTodos());
        }
        [Route("novo-produto")]
        public async Task<IActionResult> NovoProduto()
        {
            return View(await PopularCategorias(new ProdutoViewModel()));
        }
        [HttpPost]
        [Route("novo-produto")]       
        public async Task<IActionResult> NovoProduto(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return View(await PopularCategorias(produtoViewModel));
            await _produtoAppService.AdicionarProduto(produtoViewModel);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("produtos-atualizar-estoque")]  
        public async Task<IActionResult> AtualizarEstoque(Guid id)
        {
            return View("Estoque", await _produtoAppService.ObterPorId(id));
        }
        [HttpPost]
        [Route("Produtos-atualizar-estoque")]
        public async Task<IActionResult> AtualizarEstoque(Guid id,int quantidade)
        {
            if(quantidade > 0)
            {
                await _produtoAppService.ReporEstoque(id, quantidade);  
            }
            else
            {
                await _produtoAppService.DebitarEstoque(id,quantidade);
            }
            return View("Index", await _produtoAppService.ObterTodos());
        }


        private async Task<ProdutoViewModel> PopularCategorias(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel.Categorias = await _produtoAppService.ObterPorCategorias();
            return produtoViewModel;
        }

    }
}
