namespace CacheApi.Models;

public class Produto
{
    public Produto()
    {
        
    }

    public Produto(string nome, decimal valor)
    {
        IdProduto = Guid.NewGuid();
        Nome = nome;
        Valor = valor;
    }

    public Guid IdProduto { get; set; }
    public string Nome { get; set; }
    public decimal Valor { get; set; }
}
