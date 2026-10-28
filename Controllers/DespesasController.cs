using GastosDespesas.API.DTOs;
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
    public IActionResult PostDespesa([FromBody] CriarDespesaDto dto){

        // Se assumir o valor padrão, ele dará Bad Request
        if (dto.DataVencimento == default) {
            return BadRequest("A data de vencimento é obrigatória");
        }

        if (dto.DataVencimento < DateOnly.FromDateTime(DateTime.Today)){
            return BadRequest("Não possível cadastrar uma despesa com data anterior!");
        }

        if (dto.Valor <= 0) {
            return BadRequest("Ocorreu um erro! Defina um valor acima de 0!");
        }

        if (string.IsNullOrWhiteSpace(dto.Descricao)) {
            return BadRequest("Defina uma descrição para a despesa!");
        }

        var despesa = new Despesa {
            Id = _despesas.Max(d => d.Id) + 1,
            Descricao = dto.Descricao,
            Valor = dto.Valor,
            DataVencimento = dto.DataVencimento,
            DataPagamento = dto.DataPagamento
        };

        _despesas.Add(despesa);

        return CreatedAtAction(
            nameof(GetResultId),
            new {
                id = despesa.Id
            },
            despesa
        );
    }

    // Atualizando despesas por completo
    [HttpPut("{id}")]
    public IActionResult PutDespesa(int id, [FromBody] CriarDespesaDto dto){

        var despesa = _despesas.FirstOrDefault(d => d.Id == id);

        if (despesa is null){
            return NotFound();
        }

        if (dto.DataVencimento == default) {
            return BadRequest("A data de vencimento é obrigatória");
        }

        if (dto.DataVencimento < DateOnly.FromDateTime(DateTime.Today)){
            return BadRequest("Não possível cadastrar uma despesa com data anterior!");
        }

        if (dto.Valor <= 0) {
            return BadRequest("Ocorreu um erro! Defina um valor acima de 0!");
        }

        if (string.IsNullOrWhiteSpace(dto.Descricao)) {
            return BadRequest("Defina uma descrição para a despesa!");
        }

        despesa.Descricao = dto.Descricao;
        despesa.Valor = dto.Valor;
        despesa.DataVencimento = dto.DataVencimento;
        despesa.DataPagamento = dto.DataPagamento;

        return Ok(despesa);

    }

    // Atualizando somente o valor da despesa
    [HttpPatch("{id}/valor")]
    public IActionResult AtualizaValorDespesa(int id, [FromBody] AtualizaValorDespesaDto dto){

        var despesa = _despesas.FirstOrDefault(d => d.Id == id);

        if (despesa is null){
            return NotFound();
        }

        despesa.Valor = dto.Valor;

        return Ok(despesa);

    }

}