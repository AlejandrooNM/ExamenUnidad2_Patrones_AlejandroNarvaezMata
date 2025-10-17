using System;
using System.Collections.Generic;

namespace RestauranteSimulacion
{
    // Implementación del patrón Object Pool para reutilización de objetos
    public class ObjectPool<T> where T : class
    {
        private readonly List<T> _disponibles; // Aquí guardo los objetos listos para usar
        private readonly List<T> _enUso; // Aquí guardo los objetos que están en uso
        public string NombrePool { get; private set; } // Aquí está el nombre del pool

        // Constructor: aquí inicializo el pool
        public ObjectPool(string nombrePool)
        {
            NombrePool = nombrePool; // Guardo el nombre que me dan
            _disponibles = new List<T>(); // Creo la lista de disponibles vacía
            _enUso = new List<T>(); // Creo la lista de en uso vacía
            Console.WriteLine($"[POOL] Pool '{nombrePool}' creado"); // Aviso que ya estoy listo
        }

        // Aquí recibo objetos nuevos para el pool
        public void AgregarObjeto(T objeto)
        {
            _disponibles.Add(objeto); // Pongo el objeto en disponibles
            Console.WriteLine($"[POOL-PUSH] ✓ Objeto agregado al pool '{NombrePool}' (Total disponibles: {_disponibles.Count})");
        }

        // Aquí entrego un objeto cuando me lo piden
        public T Obtener()
        {
            if (_disponibles.Count > 0) // Verifico si tengo objetos disponibles
            {
                T objeto = _disponibles[0]; // Tomo el primer objeto que tengo
                _disponibles.RemoveAt(0); // Lo saco de disponibles
                _enUso.Add(objeto); // Lo pongo en la lista de en uso
                Console.WriteLine($"[POOL-POP] ← Objeto SACADO del pool '{NombrePool}' (Disponibles: {_disponibles.Count} | En uso: {_enUso.Count})");
                return objeto; // Se lo devuelvo al que lo pidió
            }
            Console.WriteLine($"[POOL-ERROR] ✗ Pool '{NombrePool}' vacío - No hay objetos disponibles");
            return null!; // No tengo nada disponible, devuelvo null
        }

        // Aquí recibo de vuelta los objetos que ya terminaron de usar
        public void Devolver(T objeto)
        {
            if (objeto == null) return; // Si me dan null, no hago nada
            
            if (_enUso.Remove(objeto)) // Lo busco y saco de la lista en uso
            {
                ReiniciarObjeto(objeto); // Lo reinicio para que esté listo de nuevo
                _disponibles.Add(objeto); // Lo pongo de vuelta en disponibles
                Console.WriteLine($"[POOL-RETURN] → Objeto DEVUELTO al pool '{NombrePool}' (Disponibles: {_disponibles.Count} | En uso: {_enUso.Count})");
            }
        }

        // Aquí reinicio el objeto según su tipo
        private void ReiniciarObjeto(T objeto)
        {
            if (objeto is Mesa mesa) mesa.Reiniciar(); // Si es mesa, la reinicio
            else if (objeto is Mesero mesero) mesero.Reiniciar(); // Si es mesero, lo reinicio
            else if (objeto is Pedido pedido) pedido.Reiniciar(); // Si es pedido, lo reinicio
        }

        // Aquí muestro cuántos objetos tengo
        public void MostrarEstadisticas()
        {
            Console.WriteLine($"\n[{NombrePool}] Disponibles: {_disponibles.Count} | En uso: {_enUso.Count}"); // Muestro el resumen
        }
    }
}
