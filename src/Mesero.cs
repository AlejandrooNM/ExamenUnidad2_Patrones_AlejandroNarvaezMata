using System;

namespace RestauranteSimulacion
{
    // Aquí represento a un mesero del restaurante (objeto reciclable del pool)
    public class Mesero
    {
        public string Nombre { get; private set; } // Aquí guardo mi nombre
        public bool EstaDisponible { get; private set; } // Aquí guardo si estoy libre o ocupado

        // Aquí me crean con mi nombre
        public Mesero(int id, string nombre)
        {
            Nombre = nombre; // Guardo el nombre que me dan
            EstaDisponible = true; // Empiezo disponible para atender
        }

        // Aquí me asignan a un cliente
        public void AsignarCliente(string cliente)
        {
            EstaDisponible = false; // Me marco como ocupado
            Console.WriteLine($"  [MESERO] {Nombre} → ATENDIENDO a {cliente}");
        }

        // Aquí me reinician para que pueda atender a otro cliente
        public void Reiniciar()
        {
            EstaDisponible = true; // Me libero para el siguiente cliente
            Console.WriteLine($"  [MESERO] {Nombre} → LIBRE y listo para reusar");
        }
    }
}
