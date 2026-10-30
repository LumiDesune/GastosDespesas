using System.ComponentModel.DataAnnotations;

namespace GastosDespesas.API.Models;

public class Pessoa {
    [Key]
    public int Id {get; set;}
    public string Nome {get; set;} = string.Empty;
    public DateOnly? DataNascimento {get; set;}
    public ICollection<Despesa> Despesas {get; set;} = new List<Despesa>();
}