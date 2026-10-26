using GastosDespesas.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace GastosDespesas.API.Controllers;

[ApiController]
[Route("api/despesas")] // Define a rota da API
public class DespesasController : ControllerBase {

    private static readonly List<Despesa> _despesas = new() {

        new Despesa {
                Id = 1,
                Descricao = "Conta de energia",
                Valor = 524.50m,
                DataVencimento = new DateOnly(2026, 12, 24),
                DataPagamento = null
            },

            new Despesa {
                Id = 2,
                Descricao = "Conta de água",
                Valor = 120m,
                DataVencimento = new DateOnly(2026, 11, 12),
                DataPagamento = new DateOnly(2026, 10, 26)
            },

            new Despesa {
                Id = 3,
                Descricao = "Café com leite",
                Valor = 15m,
                DataVencimento = new DateOnly(2026, 11, 09),
                DataPagamento = new DateOnly(2026, 10, 23)
            }

    };


    [HttpGet] // Método HTTP para a rota
    public IActionResult GetResult() {
        
        return Ok(_despesas);

    }

    [HttpGet("resume")] // Define uma nova rota
    public IActionResult GetResume() {
        return Ok("Resumo das despesas");
    }

    /// Busca por id da despesa
    /// Busca por Parâmetro
    [HttpGet("{id}")]
    public IActionResult GetResultId(int id) {


        var despesa = _despesas.FirstOrDefault(d => d.Id == id);

        if (despesa is null) {
            return NotFound("Despesa não encontrada!");
        }
        
        return Ok(despesa);

    }

    // Cadastro de despesa
    [HttpPost]
    public IActionResult PostDespesa([FromBody] Despesa despesa){
        int novoId = _despesas.Max(d => d.Id) + 1;

        despesa.Id = novoId;

        // Se assumir o valor padrão, ele dará Bad Request
        if (despesa.DataVencimento == default) {
            return BadRequest("A data de vencimento é obrigatória");
        }

        if (despesa.Valor <= 0) {
            return BadRequest("Ocorreu um erro! Defina um valor acima de 0!");
        }

        _despesas.Add(despesa);

        return CreatedAtAction(
            nameof(GetResultId),
            new {
                id = despesa.Id
            },
            despesa
        );
    }

}