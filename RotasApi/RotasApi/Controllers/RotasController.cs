using Microsoft.AspNetCore.Mvc;
using RotasApi.Repository;
using RotasApi.Model;
using RotasApi.Services;
using Microsoft.EntityFrameworkCore;

namespace PedidosApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RotasController : ControllerBase
{    

    private readonly ILogger<RotasController> _logger;
    private ServiceRotas Service;

    public RotasController(ILogger<RotasController> logger, Context context)
    {
        _logger = logger;        
        Service = new ServiceRotas(context);
    }

    [HttpGet(Name = "GetRotas")]
    public async Task<IActionResult> Get(string origem, string destino)
    {    
        decimal menorCusto = -1;     
        var melhorCaminho = Service.BuscarMelhorRoda(origem, destino, ref menorCusto);

        // Exibe o resultado
        if (melhorCaminho != null)
        {            
            return Ok("Melhor rota: " + string.Join(" - ", melhorCaminho) + " ao custo de $" + menorCusto);
        }
        else
        {
            return BadRequest("Nenhuma rota encontrada.");            
        }      
    }

    [HttpPost()]    
    public async Task<IActionResult> Post(string origem, string destino, decimal valor)
    {
        try
        {
            return Ok(Service.SalvarRota(origem, destino, valor));            
        }
        catch (Exception ex)
        {
            var erro = ex.Message;
            var result = StatusCodes.Status500InternalServerError.ToString(erro);
            throw;
        }
    }      
}
