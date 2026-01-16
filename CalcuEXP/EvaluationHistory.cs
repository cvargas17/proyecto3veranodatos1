using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CalcuEXP
{
    // Clase para manejar el historial de evaluaciones en CSV
    public class EvaluationHistory
    {
        private readonly string filePath;
        private readonly object lockObject = new object();

        public EvaluationHistory(string path = "evaluations.csv")
        {
            filePath = path;
            InitializeFile();
        }

        // Inicializar archivo CSV si no existe
        private void InitializeFile()
        {
            lock (lockObject)
            {
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, "Fecha,Hora,Expresión,Resultado,Cliente\n");
                }
            }
        }

        // Guardar una evaluación
        public void SaveEvaluation(string expression, string result, string clientAddress = "127.0.0.1")
        {
            lock (lockObject)
            {
                try
                {
                    DateTime now = DateTime.Now;
                    string date = now.ToString("yyyy-MM-dd");
                    string time = now.ToString("HH:mm:ss");

                    // Escapar comillas en la expresión y resultado
                    string escapedExpression = expression.Replace("\"", "\"\"");
                    string escapedResult = result.Replace("\"", "\"\"");

                    string line = $"{date},{time},\"{escapedExpression}\",\"{escapedResult}\",{clientAddress}";

                    File.AppendAllText(filePath, line + "\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error guardando evaluación: {ex.Message}");
                }
            }
        }

        // Obtener todas las evaluaciones
        public List<string> GetAllEvaluations()
        {
            lock (lockObject)
            {
                try
                {
                    if (!File.Exists(filePath))
                        return new List<string>();

                    var lines = File.ReadAllLines(filePath);
                    // Retornar todas las líneas excepto el encabezado
                    return lines.Skip(1).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error leyendo evaluaciones: {ex.Message}");
                    return new List<string>();
                }
            }
        }

        // Obtener el contenido del archivo CSV como string
        public string GetCSVContent()
        {
            lock (lockObject)
            {
                try
                {
                    if (!File.Exists(filePath))
                        return "Fecha,Hora,Expresión,Resultado,Cliente\n";

                    return File.ReadAllText(filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error leyendo CSV: {ex.Message}");
                    return "";
                }
            }
        }

        // Limpiar el historial
        public void ClearHistory()
        {
            lock (lockObject)
            {
                try
                {
                    File.WriteAllText(filePath, "Fecha,Hora,Expresión,Resultado,Cliente\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error limpiando historial: {ex.Message}");
                }
            }
        }

        // Obtener el número de evaluaciones
        public int GetCount()
        {
            lock (lockObject)
            {
                try
                {
                    if (!File.Exists(filePath))
                        return 0;

                    var lines = File.ReadAllLines(filePath);
                    return Math.Max(0, lines.Length - 1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error contando evaluaciones: {ex.Message}");
                    return 0;
                }
            }
        }
    }
}
