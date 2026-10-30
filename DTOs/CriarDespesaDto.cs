using System.ComponentModel.DataAnnotations;

namespace GastosDespesas.API.DTOs;

public class CriarDespesaDto {
    [Required(ErrorMessage = "A descrição é obrigatória!")]
    [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres")]
    public string Descricao {get; set;} = string.Empty;
    [Range(typeof(decimal), "0.01", "999999999", ErrorMessage = "O valor deve ser maior que zero!")]
    public decimal Valor {get; set;}
    [Required]
    public DateOnly DataVencimento {get; set;}
    public DateOnly? DataPagamento {get; set;}
    [Required(ErrorMessage = "Obrigatório definir o ID da pessoa para a despesa!")]
    public int PessoaId {get; set;}
}