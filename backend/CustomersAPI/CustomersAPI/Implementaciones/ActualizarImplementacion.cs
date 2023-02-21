using CustomersAPI.Dtos;
using CustomersAPI.Repositorios;

namespace CustomersAPI.Implementaciones
{
    
    public interface InterfazActualizarImple
    {
        Task<ClienteDto?> Execute(ClienteDto cliente);
    }

    public class ActualizarImplementacion : InterfazActualizarImple
    {
        private readonly ClienteBasedatosContenido _clienteBasedatosContenido;

        public ActualizarImplementacion(ClienteBasedatosContenido clienteBasedatosContenido)
        {
            _clienteBasedatosContenido = clienteBasedatosContenido;
        }

        public async Task<ClienteDto?> Execute(ClienteDto cliente)
        {
            // obtenemos el cliente que estamos intentando actualizar
            var entidad = await _clienteBasedatosContenido.Obtener(cliente.Id);

            // verificamos que no sea nulo
            if (entidad == null)
                return null;

            // actualizamos los datos a los nuevos ingresados
            entidad.Nombre=cliente.Nombre;
            entidad.Apellido = cliente.Apellidos;
            entidad.Correo= cliente.Correo;
            entidad.Telefono= cliente.Telefono;
            entidad.Direccion= cliente.Direccion;

            // actualizamos en la tabla clientes con la nueva entidad
            await _clienteBasedatosContenido.Actualizar(entidad);
            // devolvemos la informacion del cliente actualizada
            return entidad.ToDto();
        }
    }
}
