using GastosDespesas.API.Data;
using GastosDespesas.API.DTOs;
using GastosDespesas.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GastosDespesas.API.Controllers;

[ApiController]
[Route("api/pessoas")]
public class PessoasController : ControllerBase {

    private readonly AppDbContext _context;

    public PessoasController(AppDbContext context){
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetResult(){

        var pessoas = await _context.Pessoas.ToListAsync();
        return Ok(pessoas);

    }

    [HttpGet("{id}")]
    public IActionResult GetResultId(int id){

        var pessoa =  _context.Pessoas.FirstOrDefault(p => p.Id == id);

        if (pessoa is null) {
            return NotFound("Não foi encontrada essa pessoa cadastrada!");
        }

        return Ok(pessoa);

    }

    [HttpPost]
    public async Task<IActionResult> PostPessoa([FromBody] PessoaDto pessoaDto){
        
        var pessoa = new Pessoa {
            Nome = pessoaDto.Nome,
            DataNascimento = pessoaDto.DataNascimento
        };

        var dataAtual = DateOnly.FromDateTime(DateTime.Today);
        var dataLimite = dataAtual.AddYears(-18);

        if (pessoaDto.DataNascimento > dataLimite){
            return BadRequest("A pessoa precisa ter pelo menos 18 anos!");
        }

        _context.Pessoas.Add(pessoa);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetResult),
            new {
                id = pessoa.Id
            },
            pessoa
        );

    }

}