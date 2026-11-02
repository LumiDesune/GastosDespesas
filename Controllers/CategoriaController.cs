using GastosDespesas.API.Data;
using GastosDespesas.API.DTOs;
using GastosDespesas.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GastosDespesas.API.Controllers;

[ApiController]
[Route("api/despesas/categorias")]
public class CategoriaController : ControllerBase {

    private readonly AppDbContext _context;
    public CategoriaController(AppDbContext context){
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategorias(){
        var categorias = await _context.Categorias.ToListAsync();

        return Ok(categorias);
    }

    [HttpPost]
    public async Task<IActionResult> PostCategorias([FromBody] CriaCategoriaDto dto) {

        var categoria = new Categoria {
            Nome = dto.Nome
        };

        _context.Categorias.Add(categoria);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCategorias), new { id = categoria.Id }, categoria);

    }

}