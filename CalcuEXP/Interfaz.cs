using System;
using System.Windows.Forms;
using System.Drawing;

namespace CalcuEXP
{
    public class Interfaz : Form
    {
        private CalculatorClient? client;
        private TextBox? txtExpresion;
        private TextBox? txtResultado;
        private RichTextBox? txtLog;
        private Label? lblEstado;
        private Button? btnConectar;
        private Label? lblConexion;
        private TextBox? txtServidor;
        private TextBox? txtPuerto;

        public Interfaz()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Calculadora Remota - Cliente TCP";
            this.Width = 1000;
            this.Height = 750;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.FormClosing += (s, e) => { client?.Disconnect(); };

            // Panel de conexión
            Panel panelConexion = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.LightSkyBlue,
                Padding = new Padding(10)
            };

            Label lblServLabel = new Label { Text = "Server:", Left = 15, Top = 12, Width = 60, Font = new Font("Arial", 10, FontStyle.Bold) };
            txtServidor = new TextBox { Left = 80, Top = 10, Width = 150, Height = 25, Font = new Font("Arial", 10), Text = "127.0.0.1" };

            Label lblPuertoLabel = new Label { Text = "Port:", Left = 250, Top = 12, Width = 50, Font = new Font("Arial", 10, FontStyle.Bold) };
            txtPuerto = new TextBox { Left = 300, Top = 10, Width = 80, Height = 25, Font = new Font("Arial", 10), Text = "5000" };

            btnConectar = new Button
            {
                Text = "Conectar",
                Left = 390,
                Top = 10,
                Width = 100,
                Height = 25,
                BackColor = Color.RoyalBlue,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnConectar.Click += (s, e) => ConectarServidor();

            lblConexion = new Label
            {
                Text = "Desconectado",
                Left = 500,
                Top = 15,
                Width = 400,
                Height = 25,
                Font = new Font("Arial", 10),
                ForeColor = Color.Red,
                AutoSize = false
            };

            panelConexion.Controls.AddRange(new Control[] { lblServLabel, txtServidor, lblPuertoLabel, txtPuerto, btnConectar, lblConexion });

            // Panel superior - Entrada
            Panel panelEntrada = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.LightGreen,
                Padding = new Padding(10)
            };

            Label lblExpresion = new Label
            {
                Text = "Expresión:",
                Top = 15,
                Left = 10,
                Width = 70,
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            txtExpresion = new TextBox
            {
                Left = 90,
                Top = 10,
                Width = 800,
                Height = 30,
                Font = new Font("Arial", 11)
            };

            Button btnEvaluar = new Button
            {
                Text = "Evaluar",
                Left = 900,
                Top = 10,
                Width = 80,
                Height = 30,
                BackColor = Color.LimeGreen,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnEvaluar.Click += (s, e) => EvaluarExpresion();

            Button btnLimpiar = new Button
            {
                Text = "Limpiar",
                Left = 900,
                Top = 45,
                Width = 80,
                Height = 30,
                BackColor = Color.Orange,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnLimpiar.Click += (s, e) => LimpiarCampos();

            Button btnHistorial = new Button
            {
                Text = "Historial",
                Left = 810,
                Top = 45,
                Width = 80,
                Height = 30,
                BackColor = Color.CornflowerBlue,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnHistorial.Click += (s, e) => VerHistorial();

            panelEntrada.Controls.AddRange(new Control[] { lblExpresion, txtExpresion, btnEvaluar, btnHistorial, btnLimpiar });

            // Panel central - Resultado
            Panel panelResultado = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.LightYellow,
                Padding = new Padding(10)
            };

            Label lblResultado = new Label
            {
                Text = "Result:",
                Top = 12,
                Left = 15,
                Width = 70,
                Font = new Font("Arial", 11, FontStyle.Bold)
            };

            txtResultado = new TextBox
            {
                Left = 90,
                Top = 10,
                Width = 870,
                Height = 30,
                Font = new Font("Arial", 14, FontStyle.Bold),
                ReadOnly = true,
                BackColor = Color.White
            };

            lblEstado = new Label
            {
                Left = 10,
                Top = 45,
                Width = 950,
                Height = 30,
                Font = new Font("Arial", 10),
                ForeColor = Color.DarkGreen,
                AutoSize = false
            };

            panelResultado.Controls.AddRange(new Control[] { lblResultado, txtResultado, lblEstado });

            // Panel para log
            Panel panelLog = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            Label lblLog = new Label
            {
                Text = "Log de Comunicación:",
                Top = -20,
                Width = 200,
                Font = new Font("Arial", 11, FontStyle.Bold)
            };

            txtLog = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Courier New", 9),
                ReadOnly = true,
                BackColor = Color.Black,
                ForeColor = Color.Lime
            };

            panelLog.Controls.AddRange(new Control[] { lblLog, txtLog });

            this.Controls.AddRange(new Control[] { panelLog, panelResultado, panelEntrada, panelConexion });

            // Inicializar cliente
            client = new CalculatorClient();
            client.OnConnected += (s, msg) => AgregarLog($"[CONEXIÓN] {msg}");
            client.OnDisconnected += (s, msg) => AgregarLog($"[DESCONEXIÓN] {msg}");
            client.OnResponseReceived += (s, msg) => AgregarLog($"[RESPUESTA] {msg}");
            client.OnErrorOccurred += (s, msg) => AgregarLog($"[ERROR] {msg}");
        }

        private void ConectarServidor()
        {
            if (client?.IsConnected ?? false)
            {
                client?.Disconnect();
                btnConectar!.Text = "Conectar";
                btnConectar.BackColor = Color.RoyalBlue;
                lblConexion!.Text = "Desconectado";
                lblConexion.ForeColor = Color.Red;
                return;
            }

            string servidor = txtServidor!.Text.Trim();
            if (!int.TryParse(txtPuerto!.Text, out int puerto))
            {
                AgregarLog("[ERROR] Puerto inválido");
                return;
            }

            client = new CalculatorClient(servidor, puerto);
            client.OnConnected += (s, msg) => ActualizarEstadoConexion(msg, true);
            client.OnDisconnected += (s, msg) => ActualizarEstadoConexion(msg, false);
            client.OnResponseReceived += (s, msg) => AgregarLog($"[RESPUESTA] {msg}");
            client.OnErrorOccurred += (s, msg) => AgregarLog($"[ERROR] {msg}");

            if (client.Connect())
            {
                btnConectar!.Text = "Desconectar";
                btnConectar.BackColor = Color.Red;
                AgregarLog($"[CONEXIÓN] Conectado a {servidor}:{puerto}");
            }
        }

        private void ActualizarEstadoConexion(string mensaje, bool conectado)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ActualizarEstadoConexion(mensaje, conectado)));
                return;
            }

            if (conectado)
            {
                lblConexion!.Text = $"✓ {mensaje}";
                lblConexion.ForeColor = Color.DarkGreen;
                btnConectar!.Text = "Desconectar";
                btnConectar.BackColor = Color.Red;
            }
            else
            {
                lblConexion!.Text = "✗ Desconectado";
                lblConexion.ForeColor = Color.Red;
                btnConectar!.Text = "Conectar";
                btnConectar.BackColor = Color.RoyalBlue;
            }

            AgregarLog($"[ESTADO] {mensaje}");
        }

        private void EvaluarExpresion()
        {
            if (!(client?.IsConnected ?? false))
            {
                lblEstado!.Text = "❌ No está conectado al servidor";
                lblEstado.ForeColor = Color.Red;
                return;
            }

            string expresion = txtExpresion!.Text.Trim();

            if (string.IsNullOrWhiteSpace(expresion))
            {
                lblEstado!.Text = "❌ Por favor ingrese una expresión";
                lblEstado.ForeColor = Color.Red;
                return;
            }

            AgregarLog($"[ENVÍO] {expresion}");

            try
            {
                string? respuesta = client.EvaluarExpresion(expresion);

                if (respuesta != null)
                {
                    if (respuesta.StartsWith("ERROR:"))
                    {
                        txtResultado!.Text = "";
                        lblEstado!.Text = $"❌ {respuesta.Replace("ERROR:", "")}";
                        lblEstado.ForeColor = Color.Red;
                    }
                    else
                    {
                        txtResultado!.Text = respuesta;
                        lblEstado!.Text = "✓ Expresión evaluada correctamente";
                        lblEstado.ForeColor = Color.DarkGreen;
                    }
                }
            }
            catch (Exception ex)
            {
                txtResultado!.Text = "";
                lblEstado!.Text = $"❌ Error: {ex.Message}";
                lblEstado.ForeColor = Color.Red;
            }
        }

        private void AgregarLog(string mensaje)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => AgregarLog(mensaje)));
                return;
            }

            txtLog!.AppendText($"[{DateTime.Now:HH:mm:ss}] {mensaje}\n");
        }

        private void LimpiarCampos()
        {
            txtExpresion!.Clear();
            txtResultado!.Clear();
            lblEstado!.Text = "";
            txtExpresion.Focus();
        }

        private void VerHistorial()
        {
            if (!(client?.IsConnected ?? false))
            {
                lblEstado!.Text = "❌ No está conectado al servidor";
                lblEstado.ForeColor = Color.Red;
                return;
            }

            try
            {
                string? historial = client.ObtenerHistorial();

                if (historial != null && !string.IsNullOrWhiteSpace(historial))
                {
                    // Crear una nueva ventana para mostrar el historial
                    Form formHistorial = new Form
                    {
                        Text = "Historial de Evaluaciones",
                        Width = 900,
                        Height = 600,
                        StartPosition = FormStartPosition.CenterParent,
                        Owner = this
                    };

                    RichTextBox rtbHistorial = new RichTextBox
                    {
                        Dock = DockStyle.Fill,
                        Font = new Font("Courier New", 9),
                        ReadOnly = true,
                        BackColor = Color.White,
                        ForeColor = Color.Black,
                        Text = historial
                    };

                    Button btnExportar = new Button
                    {
                        Text = "Exportar CSV",
                        Dock = DockStyle.Bottom,
                        Height = 40,
                        BackColor = Color.LimeGreen,
                        ForeColor = Color.White,
                        Font = new Font("Arial", 10, FontStyle.Bold)
                    };

                    btnExportar.Click += (s, e) =>
                    {
                        SaveFileDialog saveDialog = new SaveFileDialog
                        {
                            Filter = "CSV Files (*.csv)|*.csv",
                            FileName = $"historial_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                            DefaultExt = "csv"
                        };

                        if (saveDialog.ShowDialog() == DialogResult.OK)
                        {
                            System.IO.File.WriteAllText(saveDialog.FileName, historial);
                            MessageBox.Show($"Historial exportado a:\n{saveDialog.FileName}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    };

                    formHistorial.Controls.Add(rtbHistorial);
                    formHistorial.Controls.Add(btnExportar);
                    formHistorial.ShowDialog();
                }
                else
                {
                    MessageBox.Show("El historial está vacío", "Historial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                lblEstado!.Text = $"❌ Error al obtener historial: {ex.Message}";
                lblEstado.ForeColor = Color.Red;
            }
        }
    }
}