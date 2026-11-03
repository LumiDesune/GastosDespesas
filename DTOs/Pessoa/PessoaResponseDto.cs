namespace GastosDespesas.API.DTOs;

public class PessoaResponseDto {
    public int Id {get; set;}
    public string Nome {get; set;} = string.Empty;
    public DateOnly? DataNascimento {get; set;}
}