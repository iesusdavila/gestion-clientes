using CustomersAPI.Dtos;
using CustomersAPI.Implementaciones;
using CustomersAPI.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using MySqlX.XDevAPI;

namespace CustomersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : Controller
    {
        // la instancia _clienteBasedatosContenido solo se podra usar de lectura
        private readonly ClienteBasedatosContenido _clienteBasedatosContenido;
        // la instancia _actualizarImplementacion solo se podra usar de lectura
        private readonly InterfazActualizarImple _actualizarImplementacion;


        // constructor de la clase ClientesController
        public ClientesController(ClienteBasedatosContenido clienteBasedatosContenido, InterfazActualizarImple actualizarImplementacion)
        {
            _clienteBasedatosContenido = clienteBasedatosContenido;
            _actualizarImplementacion = actualizarImplementacion;
        }

        // ---- METODO GET CLIENTES ----

        [HttpGet()] // ruta: api/customer => devuelve la lista de clientes
        // Status 200 si no hubo ningun problema
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ClienteDto>))]
        public async Task<IActionResult> GetClientes()
        {
            // llamamos a la funcion Obtener de la clase ClienteBasedatosContenido, devuelve la lista de ClienteDto
            var result = _clienteBasedatosContenido.clientes.Select(c=>c.ToDto()).ToList();

            // creamos la ruta de los resultados
            return new OkObjectResult(result);
        }

        // ---- METODO GET UN CLIENTE ----

        [HttpGet("{id}")] // ruta: api/customer/{id} => devuelve un cliente especificando su ID
        // Status 200 si no hubo ningun problema
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClienteDto))]
        // Status 404 si el cliente no fue encontrado
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCliente(long id)
        {
            // llamamos a la funcion Obtener de la clase ClienteBasedatosContenido, devuelve una ClienteDto
            ClienteEntity result = await _clienteBasedatosContenido.Obtener(id);

            // creamos la ruta de los resultados
            return new OkObjectResult(result.ToDto());
        }

        // ---- METODO DELETE ----

        [HttpDelete("{id}")] // devuelve un booleano cuando se borre un cliente por su ID
        // Status 200 si no hubo ningun problema
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        // Status 404 si el cliente a actualizar no fue encontrado
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCliente(long id)
        {
            // llamamos a la funcion Borrar de la clase ClienteBasedatosContenido, devuelve true una vez que haya borrado
            var result = await _clienteBasedatosContenido.Borrar(id);

            // creamos la ruta de los resultados
            return new OkObjectResult(result);
        }

        // ---- METODO POST ----

        // se usa CrearClienteDto y no ClienteDto porque el Id no lo tiene que ingresar
        [HttpPost] // devuelve un cliente (ClienteDto) y recibe un objeto para crear cliente (CrearClienteDto)
        // Status 200 si no hubo ningun problema
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClienteDto))]
        public async Task<IActionResult> CrearCliente(CrearClienteDto cliente)
        {
            // llamamos a la funcion Agregar de la clase ClienteBasedatosContenido, devuelve una ClienteEntity
            ClienteEntity result = await _clienteBasedatosContenido.Agregar(cliente);

            // creamos la ruta de los resultados
            return new CreatedResult($"https://localhost:7143/api/clientes/{result.Id}", null);
        }

        // ---- METODO PUT ----

        /* hay dos formas de actualizar un cliente:
           1. [HttpPut("{id}")] devuelve un booleano cuando se actualice un cliente por su ID
           2. [HttpPut] devolver un objeto cliente e ingresar un por argumento un cliente*/
        [HttpPut]
        // Status 200 si no hubo ningun problema
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClienteDto))]
        // Status 404 si el cliente a actualizar no fue encontrado
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizarCliente(ClienteDto cliente)
        {
            // llamamos a la implementacion de actualizar para poder actualizar un cliente
            ClienteDto? result = await _actualizarImplementacion.Execute(cliente);
            
            // si no se encontro el cliente se devuelve un error
            if(result == null)
                return new NotFoundResult();

            // creamos la ruta de los resultados
            return new OkObjectResult(result);
        }

    }
}
