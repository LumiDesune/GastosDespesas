using GastosDespesas.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace GastosDespesas.API.Controllers;

[ApiController]
[Route("api/pessoas")]
public class PessoasController : ControllerBase {

    private static readonly List<Pessoa> _pessoas = new () {
        new Pessoa {
            Id = 1,
            Nome = "Ronaldo",
            Email = "ronaldo@email.com",
            DataNascimento = new DateOnly(2004, 8, 12)
        }
    };

    [HttpGet]
    public IActionResult GetResult(){
        return Ok(_pessoas);
    }

}