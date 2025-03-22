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
        var melhorCaminho = Service.BuscarMelhorRota(origem, destino, ref menorCusto);

        // Exibe o resultado
        if (melhorCaminho == null)
            return BadRequest("Nenhuma rota encontrada.");

        return Ok("Melhor rota: " + string.Join(" - ", melhorCaminho) + " ao custo de $" + menorCusto);

    }

    [HttpPost()]
    public async Task<IActionResult> Post(string origem, string destino, decimal valor)
    {
        try
        {
            //Evita duplicidade
            if (Service.VerificaRotaCadastrada(origem, destino, valor))
                return BadRequest("Rota já cadastrada");

            return Ok(await Service.SalvarRota(origem, destino, valor));

        }
        catch (Exception ex)
        {
            var erro = ex.Message;
            var result = StatusCodes.Status500InternalServerError.ToString(erro);
            throw;
        }
    }
}
