using System;

namespace RestauranteSimulacion
{
    // Aquí represento una mesa del restaurante (objeto reciclable del pool)
    public class Mesa
    {
        public int NumeroMesa { get; private set; } // Aquí guardo mi número de mesa
        public bool EstaOcupada { get; private set; } // Aquí guardo si estoy ocupada o no

        // Aquí me crean con mi número de mesa
        public Mesa(int numeroMesa)
        {
            NumeroMesa = numeroMesa; // Guardo el número que me asignan
            EstaOcupada = false; // Empiezo disponible
        }

        // Aquí me ocupan con un cliente
        public void Ocupar(string cliente)
        {
            EstaOcupada = true; // Me marco como ocupada
            Console.WriteLine($"  [MESA] Mesa {NumeroMesa} → OCUPADA por {cliente}");
        }

        // Aquí me reinician para que pueda ser usada de nuevo
        public void Reiniciar()
        {
            EstaOcupada = false; // Me libero para el siguiente cliente
            Console.WriteLine($"  [MESA] Mesa {NumeroMesa} → LIMPIADA y lista para reusar");
        }
    }
}
