using System;
using CalcuEXP;

namespace CalcuEXP
{
    // Clase para iniciar el servidor
    public class CalculatorServerProgram
    {
        public static void StartServer(int port = 5000)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║         SERVIDOR DE CALCULADORA DE EXPRESIONES             ║");
            Console.WriteLine("║                                                            ║");
            Console.WriteLine($"║  Escuchando en puerto {port}                              ║");
            Console.WriteLine("║  Presione 'salir' para detener                            ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

            // Crear servidor
            CalculatorServer server = new CalculatorServer(port);

            // Suscribirse a eventos
            server.OnClientConnected += (s, msg) => Console.WriteLine($"[CLIENTE] {msg}");
            server.OnClientDisconnected += (s, msg) => Console.WriteLine($"[CLIENTE] {msg}");
            server.OnExpressionReceived += (s, msg) => Console.WriteLine($"[EXPRESIÓN] {msg}");

            // Iniciar servidor
            server.Start();

            while (true)
            {
                string comando = Console.ReadLine() ?? "";
                if (comando.ToLower() == "salir")
                {
                    server.Stop();
                    Console.WriteLine("\n¡Servidor detenido!");
                    break;
                }
            }
        }
    }
}
