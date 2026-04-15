using CacheApi.Models;

namespace CacheApi.Services;

public class ProdutoService : IProdutoService
{

    private readonly string _filePath;

    public ProdutoService()
    {
        var basePath = Directory.GetCurrentDirectory();

        _filePath = Path.Combine(basePath, "Repositories", "produtos.csv");

        ArquivoExiste();
    }

    private void ArquivoExiste()
    {
        var directory = Path.GetDirectoryName(_filePath);

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory!);

        if (!File.Exists(_filePath))
            File.WriteAllText(_filePath, "IdProduto;Nome;Valor\n");
    }

    public List<Produto> GetAll()
    {
        if (!File.Exists(_filePath))
            return new List<Produto>();

        var linhas = File.ReadAllLines(_filePath);

        return linhas
            .Skip(2) // pula o header
            .Select(x =>
            {
                var colunas = x.Split(";");
                return new Produto
                {
                    IdProduto = Guid.Parse(colunas[0]),
                    Nome = colunas[1],
                    Valor = decimal.Parse(colunas[2])
                };
            }).ToList();
    }

    public Produto GetById(Guid idProduto)
    {
        if (!File.Exists(_filePath))
            return new Produto();

        var linhas = File.ReadAllLines(_filePath);
        var produto = linhas
            .Skip(2) // pula o header
            .Select(x =>
            {
                var colunas = x.Split(";");
                return new Produto
                {
                    IdProduto = Guid.Parse(colunas[0].Trim()),
                    Nome = colunas[1],
                    Valor = decimal.Parse(colunas[2])
                };
            }).FirstOrDefault(p => p.IdProduto == idProduto);

        return linhas.Any() ? produto : new Produto();
    }

    public Produto Set(string nomeProduto, decimal valor)
    {
        var produto = new Produto(nomeProduto, valor); 
        var linha = $"{produto.IdProduto};{produto.Nome};{produto.Valor}";

        // cria header se não existir
        if (!File.Exists(_filePath))
        {
            File.WriteAllLines(_filePath, new List<string> { "IdProduto;Nome;Valor", linha });
            return produto;
        }

        File.AppendAllText(_filePath, Environment.NewLine + linha);

        return produto;
    }
}
