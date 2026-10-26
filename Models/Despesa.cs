namespace GastosDespesas.API.Models;

public class Despesa {
    public int Id { get; set;}
    public string Descricao { get; set;} = string.Empty;
    public decimal Valor { get; set;}
    public DateOnly DataVencimento { get; set;}
    public DateOnly? DataPagamento { get; set;}
}