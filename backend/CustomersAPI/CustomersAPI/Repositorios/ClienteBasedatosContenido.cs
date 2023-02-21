using CustomersAPI.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomersAPI.Repositorios
{
    // DbContext es una clase Entity Framework para operaciones CRUD
    public class ClienteBasedatosContenido : DbContext
    {
        /* DbContextOptions es una clase genérica que se utiliza para 
           configurar la forma en que se crean las instancias de un objeto 
           DbContext. */
        public ClienteBasedatosContenido(DbContextOptions<ClienteBasedatosContenido> options) : base(options){
            
        }


        // referencia a la tabla "clientes" de nuestra base de datos
        public DbSet<ClienteEntity> clientes { get; set; }

        // metodos para diferentes procesos de nuestra Base de Datos
        // usaremos async - await porque son peticiones asincronas como JS

        // ---- METODO PARA OBTENER CLIENTES ----
        public async Task<ClienteEntity> Obtener (long id)
        {
            // se devuelve el primer cliente encontrado con dicha id
            return await clientes.FirstOrDefaultAsync(c => c.Id == id);
        }

        // ---- METODO PARA AGREGAR CLIENTES ----
        public async Task<ClienteEntity> Agregar(CrearClienteDto cliente)
        {
            // creacion de un objeto de ClienteEntity
            ClienteEntity entidad = new ClienteEntity()
            {
                Id = null,
                Nombre= cliente.Nombre,
                Apellido= cliente.Apellidos,
                Correo= cliente.Correo,
                Telefono= cliente.Telefono,
                Direccion= cliente.Direccion,

            };

            // EntityEntry<ClienteEntity> => la respuesta sera una entidad de Cliente
            // await porque si colocamos async, tendremos que responder con await
            // clientes.AddAsync(entidad) => a la tabla clientes se le agrega una entidad de forma asincrona
            EntityEntry<ClienteEntity> response = await clientes.AddAsync(entidad);
            // guardamos los cambios a la Base de Datos de manera asincrona            
            await SaveChangesAsync();

            // usamos el metodo Obtener para devolver el cliente que hemos creado, en caso se error mandamos un mensaje
            return await Obtener(response.Entity.Id ?? throw new Exception("No se pudo guardar"));
        }        
    }


    // ClienteEntity es igual a ClienteDto, porque ClienteEntity se relaciona directamente con la Base de Datos
    /* una entidad corresponde una fila de informacion de la Base de Datos, si
       creamos 5 objetos de esta entidad podremos agregar 5 filas a nuestra
       tabla de la Base de Datos */
    public class ClienteEntity
    {
        // long? permite que esa propiedad pueda ser nula
        public long? Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        // convertir un ClienteEntity a una Cliente normal
        public ClienteDto ToDto()
        {
            return new ClienteDto()
            {
                Nombre = Nombre,
                Apellidos = Apellido,
                Correo = Correo,
                Telefono = Telefono,
                Direccion = Direccion,
                Id = Id ?? throw new Exception("El id no puede ser null")
            };
        }
    }
}
