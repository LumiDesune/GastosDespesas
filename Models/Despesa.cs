namespace GastosDespesas.API.Models;

public class Despesa {
    public int Id { get; set;}
    public string Descricao { get; set;} = string.Empty;
    public decimal Valor { get; set;}
    public DateOnly DataVencimento { get; set;}
    public DateOnly? DataPagamento { get; set;}
    // Referenciando pessoa na despesa (chave estrangeira)
    public int PessoaId {get; set;}
    public Pessoa Pessoa {get; set;} = null!;
}