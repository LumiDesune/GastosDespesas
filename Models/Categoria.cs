using System.ComponentModel.DataAnnotations;

namespace GastosDespesas.API.Models;

public class Categoria {
    [Key]
    public int Id {get; set;}
    public string Nome {get; set;} = string.Empty;
    public ICollection<Despesa> Despesas {get; set;} = new List<Despesa>();
}