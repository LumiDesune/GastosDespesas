using System.ComponentModel.DataAnnotations;

namespace GastosDespesas.API.DTOs;

public class AtualizaValorDespesaDto {
    [Range(typeof(decimal), "0.01", "999999999", ErrorMessage = "O valor precisa ser maior que zero!")]
    public decimal Valor {get; set;}
}