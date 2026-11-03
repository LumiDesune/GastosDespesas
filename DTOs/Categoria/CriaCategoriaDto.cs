using System.ComponentModel.DataAnnotations;

namespace GastosDespesas.API.DTOs;

public class CriaCategoriaDto {
    [Required(ErrorMessage = "O nome da categoria é obrigatório!")]
    [StringLength(100, ErrorMessage = "A categoria deverá ter até 100 caracteres!")]
    public string Nome {get; set;} = string.Empty;
}