using System.ComponentModel.DataAnnotations;

namespace GastosDespesas.API.DTOs;

public class PessoaDto {
    [Required(ErrorMessage = "O nome é obrigatório!")]
    [StringLength(100, ErrorMessage = "O nome é obrigatorio ter até 100 caracteres!")]
    public string Nome {get; set;} = string.Empty;
    public DateOnly? DataNascimento {get; set;}
}