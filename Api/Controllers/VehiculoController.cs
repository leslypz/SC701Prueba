using Abstracciones.Interfaces.Api;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase, IVehiculoController
    {
        private IVehiculoFlujo _vehiculoFlujo;
        private ILogger<VehiculoController> _logger;

        public VehiculoController(IVehiculoFlujo vehiculoFlujo, ILogger<VehiculoController> logger)
        {
            _vehiculoFlujo = vehiculoFlujo;
            _logger = logger;
        }

        #region Operaciones
        [HttpPost]
        //el from body significa que lo agarra de un modelo
        //cuando viene de modelo es FromBody
        public async Task<IActionResult> Agregar([FromBody]VehiculoRequest vehiculo)
        {
            var resultado =  await _vehiculoFlujo.Agregar(vehiculo);
            return CreatedAtAction(nameof(Obtener), new {IdVehiculo = resultado}, null);
        }
        [HttpPut("{IdVehiculo}")]
        // el from route especifica que el guid viene del url del api, es una buena practica hacerlo
        public async Task<IActionResult> Editar([FromRoute] Guid IdVehiculo,[FromBody] VehiculoRequest vehiculo) //FromBody: Cualquier objeto que sea un modelo
        {
            if (!await VerficarVehiculoExiste(IdVehiculo))
                return NotFound("El vehiculo no existe");
            var resultado = await _vehiculoFlujo.Editar(IdVehiculo, vehiculo);
            return Ok(resultado);
        }


        [HttpDelete("{IdVehiculo}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid IdVehiculo)
        {
            if (!await VerficarVehiculoExiste(IdVehiculo))
                return NotFound("El vehiculo no existe");
            var resultado = await _vehiculoFlujo.Eliminar(IdVehiculo);
            return Ok(resultado);
        }
        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _vehiculoFlujo.Obtener();
            if (!resultado.Any())
            {
                return NoContent();
            }
            return Ok(resultado);
        }
        [HttpGet("{IdVehiculo}")] //lo del IdVehiculo es el parametro
        public async Task<IActionResult> Obtener([FromRoute] Guid IdVehiculo)
        {
            var resultado = await _vehiculoFlujo.Obtener(IdVehiculo);
            return Ok(resultado);
        }
        #endregion

        #region Helpers

        private async Task<bool> VerficarVehiculoExiste(Guid IdVehiculo)
        {
            var resultadoValidacion = false;
            var resultadoVehiculoExiste = await _vehiculoFlujo.Obtener(IdVehiculo);
            if (resultadoVehiculoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }

        #endregion
    }
}
