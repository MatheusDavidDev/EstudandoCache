using CacheApi.Controllers.Models;
using CacheApi.Models;
using CacheApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CacheApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutoController(IProdutoService produtoService, ICacheService cacheService) : ControllerBase
{
    private readonly IProdutoService _produtoService = produtoService;
    private readonly ICacheService _cacheService = cacheService;

    [HttpGet]
    public IActionResult GetAll()
    {
        var key = "produtos";
        var produtosCache = _cacheService.Get(key) as List<Produto>;

        if (produtosCache != null)
        {
            return Ok(produtosCache);
        }

        var produtos = _produtoService.GetAll();

        _cacheService.Set(key, produtos);

        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var produtoCache = _cacheService.Get(id.ToString()) as Produto;

        if (produtoCache != null) 
        {
            return Ok(produtoCache);
        }

        var produto = _produtoService.GetById(id);

        _cacheService.Set(id.ToString(), produto);

        return Ok(produto);
    }

    [HttpPost]
    public IActionResult Set([FromBody] ProdutoModel produto)
    {
        var produtoNovo = _produtoService.Set(produto.NomeProduto, produto.Valor);

        var produtosCache = _cacheService.Get("produtos") as List<Produto>;

        if (produtosCache != null)
        {
            produtosCache.Add(produtoNovo);
            _cacheService.Set("produtos", produtosCache);
        }
        else
        {
            _cacheService.Remove("produtos");
        }

        _cacheService.Set(produtoNovo.IdProduto.ToString(), produtoNovo);

        return Ok();
    }
}
