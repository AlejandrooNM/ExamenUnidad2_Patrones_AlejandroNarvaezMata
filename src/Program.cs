using System;

namespace RestauranteSimulacion
{
    class Program
    {
        // Aquí guardo los pools que voy a usar en todo el programa
        static ObjectPool<Mesa> poolMesas = null!; // Pool de mesas
        static ObjectPool<Mesero> poolMeseros = null!; // Pool de meseros
        static ObjectPool<Pedido> poolPedidos = null!; // Pool de pedidos

        // Aquí es donde empieza el programa
        static void Main(string[] args)
        {
            // Obtengo el gerente (Singleton - solo existe uno)
            var gerente = GerenteRestaurante.ObtenerInstancia();
            gerente.AbrirRestaurante(); // Abro el restaurante

            // Creo e inicializo los pools
            InicializarPools();

            // Atiendo a los clientes
            AtenderCliente("Frenkie de Jong", new[] { ("Hamburguesa", 150m), (" Bacardi con coca ", 30m) });
            AtenderCliente("Aitana Bombati", new[] { ("Pizza", 200m) });

            // Muestro las estadísticas de los pools
            poolMesas.MostrarEstadisticas();
            poolMeseros.MostrarEstadisticas();
            poolPedidos.MostrarEstadisticas();

            // Cierro el restaurante
            gerente.CerrarRestaurante();
            Console.ReadKey(); // Espero a que presionen una tecla
        }

        // Aquí creo los pools y les agrego objetos
        static void InicializarPools()
        {
            // Creo el pool de mesas y agrego 3 mesas
            poolMesas = new ObjectPool<Mesa>("Mesas");
            poolMesas.AgregarObjeto(new Mesa(1)); // Agrego mesa 1
            poolMesas.AgregarObjeto(new Mesa(2)); // Agrego mesa 2
            poolMesas.AgregarObjeto(new Mesa(3)); // Agrego mesa 3

            // Creo el pool de meseros y agrego 2 meseros
            poolMeseros = new ObjectPool<Mesero>("Meseros");
            poolMeseros.AgregarObjeto(new Mesero(1, "Gilberto")); // Agrego a Gilberto
            poolMeseros.AgregarObjeto(new Mesero(2, "Marcela")); // Agrego a Marcela

            // Creo el pool de pedidos y agrego 2 pedidos
            poolPedidos = new ObjectPool<Pedido>("Pedidos");
            poolPedidos.AgregarObjeto(new Pedido(1)); // Agrego pedido 1
            poolPedidos.AgregarObjeto(new Pedido(2)); // Agrego pedido 2
        }

        // Aquí simulo la atención de un cliente completo
        static void AtenderCliente(string cliente, (string item, decimal precio)[] items)
        {
            Console.WriteLine($"\n{new string('=', 60)}"); // Aquí creo una línea de 60 signos de igual
            Console.WriteLine($"[CLIENTE] {cliente} llego al restaurante");
            var gerente = GerenteRestaurante.ObtenerInstancia(); // Obtengo el gerente
            gerente.RecibirCliente(cliente); // El gerente recibe al cliente

            Console.WriteLine("\n--- Solicitando recursos del pool ---");
            // Paso 1: Pido una mesa del pool
            Mesa mesa = poolMesas.Obtener();
            if (mesa == null) return; // Si no hay mesas, me detengo
            
            gerente.AsignarMesa(cliente, mesa.NumeroMesa); // El gerente asigna la mesa
            
            // Paso 2: Pido un mesero del pool
            Mesero mesero = poolMeseros.Obtener();
            if (mesero == null) // Si no hay meseros
            {
                poolMesas.Devolver(mesa); // Devuelvo la mesa
                return; // Me detengo
            }

            // Paso 3: Pido un pedido del pool
            Pedido pedido = poolPedidos.Obtener();
            if (pedido == null) // Si no hay pedidos
            {
                poolMesas.Devolver(mesa); // Devuelvo la mesa
                poolMeseros.Devolver(mesero); // Devuelvo el mesero
                return; // Me detengo
            }

            Console.WriteLine("\n--- Usando los recursos ---");
            // Paso 4: Uso los recursos que obtuve
            mesa.Ocupar(cliente); // Ocupo la mesa
            mesero.AsignarCliente(cliente); // Asigno el mesero
            pedido.IniciarPedido(cliente); // Inicio el pedido

            // Paso 5: Agrego items al pedido
            foreach (var (item, precio) in items) // Recorro cada item
            {
                pedido.AgregarItem(item, precio); // Agrego el item
            }

            pedido.FinalizarPedido(); // Finalizo el pedido

            Console.WriteLine("\n--- Devolviendo recursos al pool ---");
            // Paso 6: Devuelvo los recursos al pool
            poolPedidos.Devolver(pedido); // Devuelvo el pedido
            poolMeseros.Devolver(mesero); // Devuelvo el mesero
            poolMesas.Devolver(mesa); // Devuelvo la mesa

            // Paso 7: Registro que atendí al cliente
            gerente.RegistrarClienteAtendido(); // Registro en el gerente
            gerente.DespedirCliente(cliente); // El gerente despide al cliente
            Console.WriteLine($"[CLIENTE] {cliente} se fue del restaurante");
        }
    }
}
