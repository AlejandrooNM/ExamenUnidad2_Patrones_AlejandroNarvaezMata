using System;
using System.Collections.Generic;

namespace RestauranteSimulacion
{
    // Aquí represento un pedido de comida (objeto reciclable del pool)
    public class Pedido
    {
        public int NumeroPedido { get; private set; } // Aquí guardo mi número de pedido
        public decimal Total { get; private set; } // Aquí guardo el total del pedido

        // Aquí me crean con mi número de pedido
        public Pedido(int numeroPedido)
        {
            NumeroPedido = numeroPedido; // Guardo el número que me asignan
            Total = 0; // Empiezo con total en cero
        }

        // Aquí inicio un pedido para un cliente
        public void IniciarPedido(string cliente)
        {
            Console.WriteLine($"  [PEDIDO] Pedido {NumeroPedido} → INICIADO para {cliente}");
        }

        // Aquí agrego items al pedido
        public void AgregarItem(string item, decimal precio)
        {
            Total += precio; // Sumo el precio al total
            Console.WriteLine($"    + {item} (${precio})");
        }

        // Aquí finalizo el pedido y muestro el total
        public void FinalizarPedido()
        {
            Console.WriteLine($"  [PEDIDO] → FINALIZADO - Total: ${Total}");
        }

        // Aquí me reinician para que pueda ser usado de nuevo
        public void Reiniciar()
        {
            Total = 0; // Limpio el total
            Console.WriteLine($"  [PEDIDO] Pedido {NumeroPedido} → LIMPIADO y listo para reusar");
        }
    }
}
