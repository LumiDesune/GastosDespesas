namespace GastosDespesas.API.DTOs;

public class DespesaResponseDto
{
    public int Id {get; set;}
    public string Descricao {get; set; } = string.Empty;
    public decimal Valor {get; set;}
    public DateOnly DataVencimento {get; set;}
    public DateOnly? DataPagamento {get; set;}
    public PessoaResponseDto Pessoa {get; set;} = null!;
    public CategoriaResponseDto Categoria {get; set; } = null!;
}