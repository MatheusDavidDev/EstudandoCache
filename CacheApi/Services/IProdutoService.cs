using CacheApi.Models;

namespace CacheApi.Services;

public interface IProdutoService
{
    List<Produto> GetAll();
    Produto GetById(Guid idProduto);
    Produto Set(string nomeProduto, decimal valor);
}
