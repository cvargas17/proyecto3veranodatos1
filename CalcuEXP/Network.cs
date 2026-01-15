using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalcuEXP
{
    // Servidor TCP para la calculadora
    public class CalculatorServer
    {
        private TcpListener listener;
        private bool isRunning;
        private int port;
        private readonly object lockObject = new object();

        public event EventHandler<string>? OnClientConnected;
        public event EventHandler<string>? OnClientDisconnected;
        public event EventHandler<string>? OnExpressionReceived;

        public CalculatorServer(int port = 5000)
        {
            this.port = port;
            this.isRunning = false;
            listener = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            if (isRunning)
                return;

            try
            {
                listener.Start();
                isRunning = true;
                OnClientConnected?.Invoke(this, $"Servidor iniciado en puerto {port}");

                // Iniciar tarea para aceptar conexiones
                Task.Run(() => AcceptConnections());
            }
            catch (Exception ex)
            {
                OnClientDisconnected?.Invoke(this, $"Error al iniciar servidor: {ex.Message}");
            }
        }

        public void Stop()
        {
            if (!isRunning)
                return;

            try
            {
                isRunning = false;
                listener?.Stop();
                OnClientDisconnected?.Invoke(this, "Servidor detenido");
            }
            catch (Exception ex)
            {
                OnClientDisconnected?.Invoke(this, $"Error al detener servidor: {ex.Message}");
            }
        }

        private void AcceptConnections()
        {
            while (isRunning)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    var remoteEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                    string clientAddress = remoteEndPoint?.Address.ToString() ?? "Desconocido";
                    OnClientConnected?.Invoke(this, $"Cliente conectado desde {clientAddress}");

                    // Manejar cliente en una tarea separada
                    Task.Run(() => HandleClient(client));
                }
                catch (SocketException)
                {
                    // Excepción esperada cuando se detiene el servidor
                    break;
                }
                catch (Exception ex)
                {
                    if (isRunning)
                        OnClientDisconnected?.Invoke(this, $"Error aceptando cliente: {ex.Message}");
                }
            }
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = new byte[1024];

                    while (client.Connected && isRunning)
                    {
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);

                        if (bytesRead == 0)
                            break;

                        string expresion = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                        OnExpressionReceived?.Invoke(this, $"Expresión recibida: {expresion}");

                        string respuesta = EvaluarExpresion(expresion);
                        byte[] responseBytes = Encoding.UTF8.GetBytes(respuesta);

                        stream.Write(responseBytes, 0, responseBytes.Length);
                        stream.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                OnClientDisconnected?.Invoke(this, $"Error en cliente: {ex.Message}");
            }
            finally
            {
                client?.Close();
                client?.Dispose();
            }
        }

        private string EvaluarExpresion(string expresion)
        {
            try
            {
                var calculator = new ExpressionCalculator(expresion);
                var ast = calculator.Parse();
                double resultado = calculator.Evaluate(ast);
                return resultado.ToString();
            }
            catch (Exception ex)
            {
                return $"ERROR:{ex.Message}";
            }
        }
    }

    // Cliente TCP para la calculadora
    public class CalculatorClient
    {
        private TcpClient client;
        private NetworkStream? stream;
        private string serverAddress;
        private int port;
        private bool isConnected;

        public event EventHandler<string>? OnConnected;
        public event EventHandler<string>? OnDisconnected;
        public event EventHandler<string>? OnResponseReceived;
        public event EventHandler<string>? OnErrorOccurred;

        public CalculatorClient(string address = "127.0.0.1", int port = 5000)
        {
            this.serverAddress = address;
            this.port = port;
            this.isConnected = false;
            this.client = new TcpClient();
        }

        public bool Connect()
        {
            try
            {
                if (isConnected)
                    return true;

                client = new TcpClient();
                client.Connect(serverAddress, port);
                stream = client.GetStream();
                isConnected = true;

                OnConnected?.Invoke(this, $"Conectado a {serverAddress}:{port}");
                return true;
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke(this, $"Error de conexión: {ex.Message}");
                isConnected = false;
                return false;
            }
        }

        public void Disconnect()
        {
            try
            {
                if (!isConnected)
                    return;

                stream?.Close();
                client?.Close();
                isConnected = false;
                OnDisconnected?.Invoke(this, "Desconectado del servidor");
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke(this, $"Error al desconectar: {ex.Message}");
            }
        }

        public string? EvaluarExpresion(string expresion)
        {
            if (!isConnected)
            {
                OnErrorOccurred?.Invoke(this, "No conectado al servidor");
                return null;
            }

            try
            {
                if (stream == null)
                {
                    OnErrorOccurred?.Invoke(this, "Stream de red no disponible");
                    return null;
                }

                // Enviar expresión
                byte[] requestBytes = Encoding.UTF8.GetBytes(expresion);
                stream.Write(requestBytes, 0, requestBytes.Length);
                stream.Flush();

                // Recibir respuesta
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string respuesta = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                OnResponseReceived?.Invoke(this, respuesta);
                return respuesta;
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke(this, $"Error al enviar expresión: {ex.Message}");
                isConnected = false;
                return null;
            }
        }

        public bool IsConnected => isConnected;
    }
}
