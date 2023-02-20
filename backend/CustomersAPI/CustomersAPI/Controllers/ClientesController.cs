using CustomersAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CustomersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : Controller
    {
        // ---- METODOS GET ----

        // devuelve la lista de clientes
        // ruta: api/customer
        [HttpGet()]
        public async Task<List<ClienteDto>> GetClientes()
        {
            throw new NotImplementedException();
        }

        // devuelve un cliente especificando su ID
        // ruta: api/customer/{id}
        [HttpGet("{id}")]
        public async Task<ClienteDto> GetCliente(long id)
        {
            throw new NotImplementedException();
        }

        // ---- METODO DELETE ----

        // devuelve un booleano cuando se borre un cliente por su ID
        [HttpDelete("{id}")]
        public async Task<bool> DeleteCliente(long id)
        {
            throw new NotImplementedException();
        }

        // ---- METODO POST ----

        // devuelve un cliente (ClienteDto) y recibe un objeto para crear cliente (CrearClienteDto)
        /* diferencia entre devolver un cliente y recibir un objeto 
            para crear un cliente es que lo segundo no tiene un ID
            debido a que eso el usuario no tiene que ingresar*/
        [HttpPost]
        public async Task<ClienteDto> CrearCliente(CrearClienteDto cliente)
        {
            throw new NotImplementedException();
        }

        // ---- METODO PUT ----

        /* hay dos formas de actualizar un cliente:
           1. [HttpPut("{id}")] devuelve un booleano cuando se actualice un cliente por su ID
           2. [HttpPut] devolver un objeto cliente e ingresar un por argumento un cliente*/
        [HttpPut]
        public async Task<ClienteDto> ActualizarCliente(ClienteDto cliente)
        {
            throw new NotImplementedException();
        }

    }
}
