using GastosDespesas.API.Data;
using GastosDespesas.API.DTOs;
using GastosDespesas.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GastosDespesas.API.Controllers;

[ApiController]
[Route("api/despesas")] // Define a rota da API
public class DespesasController : ControllerBase {

    private readonly AppDbContext _context;

    public DespesasController(AppDbContext context){
        _context = context;
    }


    [HttpGet] // Método HTTP para a rota
    public async Task<IActionResult> GetResult(){
        var despesas = await _context.Despesas
        .Select(d => new {
            d.Id,
            d.Descricao,
            d.Valor,
            d.DataVencimento,
            d.DataPagamento,

            Pessoa = new {
                d.Pessoa.Id,
                d.Pessoa.Nome
            },

            Categoria = new {
                d.Categoria.Id,
                d.Categoria.Nome
            }
        })
        .ToListAsync();

        return Ok(despesas);

    }

    /// Busca por id da despesa
    /// Busca por Parâmetro
    [HttpGet("{id}")]
    public IActionResult GetResultId(int id) {


        var despesa = _context.Despesas.FirstOrDefault(d => d.Id == id);

        if (despesa is null) {
            return NotFound("Despesa não encontrada!");
        }
        
        return Ok(despesa);

    }

    // Busca despesas por pessoa
    [HttpGet("pessoa/{pessoaId}")]
    public async Task<IActionResult> GetDespesasPorPessoa(int pessoaId){
        var despesas = await _context.Despesas.Where(d => d.PessoaId == pessoaId).ToListAsync();

        if (despesas.Count == 0) {
            return NotFound("Não foi encontrada nenhuma despesa para essa pessoa!");
        }

        return Ok(despesas);
    }

    // Cadastro de despesa
    [HttpPost]
    public async Task<IActionResult> PostDespesa([FromBody] CriarDespesaDto dto){

        var pessoaExiste = await _context.Pessoas.AnyAsync(p => p.Id == dto.PessoaId);

        if (!pessoaExiste){
            return NotFound("Não foi encontrada a pessoa especifica!");
        }

        var despesa = new Despesa {
            Descricao = dto.Descricao,
            Valor = dto.Valor,
            DataVencimento = dto.DataVencimento,
            DataPagamento = dto.DataPagamento,
            PessoaId = dto.PessoaId,
            CategoriaId = dto.CategoriaId
        };

        if (dto.DataPagamento > DateOnly.FromDateTime(DateTime.Today)){
            return BadRequest("Não é possível definir uma data de pagamento posterior");
        }

        _context.Despesas.Add(despesa);

        await _context.SaveChangesAsync();

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

      //  var despesa = _despesas.FirstOrDefault(d => d.Id == id);

       // if (despesa is null){
      //      return NotFound();
      //  }

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

       // despesa.Descricao = dto.Descricao;
       // despesa.Valor = dto.Valor;
       // despesa.DataVencimento = dto.DataVencimento;
       // despesa.DataPagamento = dto.DataPagamento;

        return Ok();

    }

    [HttpDelete("{id}")]
    // Deletando um id próprio da despesa
    public async Task<ActionResult> DeleteDespesa(int id){

        //var despesa = _despesas.FirstOrDefault(d => d.Id == id);
        var despesa = await _context.Despesas.FirstOrDefaultAsync(d => d.Id == id);

        if (despesa is null) {
            return NotFound("Despesa não encontrada!");
        }

       _context.Despesas.Remove(despesa);
       await _context.SaveChangesAsync();

        return NoContent();

    }


    // Atualizando somente o valor da despesa
    [HttpPatch("{id}/valor")]
    public async Task<IActionResult> AtualizaValorDespesa(int id, [FromBody] AtualizaValorDespesaDto dto){

        var despesa = await _context.Despesas.FirstOrDefaultAsync(d => d.Id == id);

        if (despesa is null){
            return NotFound("Despesa não encontrada!");
       }

        despesa.Valor = dto.Valor;

        await _context.SaveChangesAsync();
        return Ok();

    }

}