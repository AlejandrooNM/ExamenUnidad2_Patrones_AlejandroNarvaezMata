using System;

namespace RestauranteSimulacion
{
    // Aquí implemento el patrón Singleton - Solo puedo existir una vez
    public sealed class GerenteRestaurante
    {
        private static GerenteRestaurante _instancia = null!; // Aquí guardo mi única instancia
        private static readonly object _lock = new object(); // Aquí controlo el acceso de múltiples hilos
        
        public int ClientesAtendidos { get; private set; } // Aquí cuento los clientes que atiendo

        // Constructor privado - Solo yo puedo crearme
        private GerenteRestaurante()
        {
            ClientesAtendidos = 0; // Empiezo sin clientes atendidos
            Console.WriteLine("\n[SINGLETON]  INSTANCIA ÚNICA CREADA - Gerente del restaurante");
        }

        // Aquí entrego mi única instancia (o me creo si no existo)
        public static GerenteRestaurante ObtenerInstancia()
        {
            if (_instancia == null) // Verifico si ya existo
            {
                lock (_lock) // Bloqueo para que solo un hilo entre
                {
                    if (_instancia == null) // Verifico de nuevo dentro del bloqueo
                    {
                        _instancia = new GerenteRestaurante(); // Me creo por primera vez
                    }
                }
            }
            else
            {
                Console.WriteLine("  [SINGLETON] → Reutilizando instancia única existente");
            }
            return _instancia; // Me devuelvo a quien me llamó
        }

        // Aquí abro el restaurante
        public void AbrirRestaurante()
        {
            Console.WriteLine("\n[RESTAURANTE] El gerente ha abierto el restaurante\n");
        }

        // Aquí cierro el restaurante
        public void CerrarRestaurante()
        {
            Console.WriteLine($"\n[RESTAURANTE] El gerente ha cerrado el restaurante - Clientes atendidos: {ClientesAtendidos}\n");
        }

        // Aquí recibo a los clientes en la entrada
        public void RecibirCliente(string nombreCliente)
        {
            Console.WriteLine($"  [SINGLETON-HOST] Gerente recibe a {nombreCliente} en la entrada");
        }

        // Aquí asigno una mesa al cliente
        public void AsignarMesa(string nombreCliente, int numeroMesa)
        {
            Console.WriteLine($"  [SINGLETON-HOST] Gerente asigna Mesa {numeroMesa} a {nombreCliente}");
        }

        // Aquí registro cada cliente que atiendo
        public void RegistrarClienteAtendido()
        {
            ClientesAtendidos++; // Sumo uno al contador
            Console.WriteLine($"  [SINGLETON-HOST] Gerente registra cliente #{ClientesAtendidos} atendido");
        }

        // Aquí me despido de los clientes
        public void DespedirCliente(string nombreCliente)
        {
            Console.WriteLine($"  [SINGLETON-HOST] Gerente despide a {nombreCliente}");
        }
    }
}
