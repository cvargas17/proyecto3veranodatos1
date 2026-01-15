using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

// Nodo del árbol de expresión binaria
public class ExpressionNode
{
    public string Value { get; set; }
    public ExpressionNode? Left { get; set; }
    public ExpressionNode? Right { get; set; }
    public bool IsOperator { get; set; }
    public bool IsOperand { get; set; }

    public ExpressionNode(string value, bool isOperator = false, bool isOperand = false)
    {
        Value = value;
        IsOperator = isOperator;
        IsOperand = isOperand;
        Left = null;
        Right = null;
    }
}

// Clase principal de la calculadora
public class ExpressionCalculator
{
    private string expression;
    private List<string> tokens;
    private int tokenIndex;

    // Operadores soportados
    private static readonly Dictionary<string, int> Precedence = new Dictionary<string, int>
    {
        { "or", 1 },
        { "xor", 2 },
        { "and", 3 },
        { "+", 4 },
        { "-", 4 },
        { "*", 5 },
        { "/", 5 },
        { "%", 5 },
        { "**", 6 },
        { "not", 7 }
    };

    private static readonly HashSet<string> BinaryOperators = new HashSet<string>
    {
        "+", "-", "*", "/", "%", "**", "and", "or", "xor"
    };

    private static readonly HashSet<string> UnaryOperators = new HashSet<string>
    {
        "not"
    };

    private static readonly HashSet<string> LogicalOperators = new HashSet<string>
    {
        "and", "or", "xor", "not"
    };

    public ExpressionCalculator(string expr)
    {
        expression = expr.Trim();
        tokens = new List<string>();
        tokenIndex = 0;
    }

    // Tokenizar la expresión
    private void Tokenize()
    {
        tokens.Clear();
        string pattern = @"(\d+\.?\d*|\+|-|\*{1,2}|/|%|and|or|xor|not|[\(\)])";
        MatchCollection matches = Regex.Matches(expression, pattern, RegexOptions.IgnoreCase);

        foreach (Match match in matches)
        {
            tokens.Add(match.Value.ToLower());
        }

        // Validar tokens
        if (tokens.Count == 0)
            throw new ArgumentException("Expresión vacía o inválida");
    }

    // Obtener el siguiente token
    private string? PeekToken()
    {
        return tokenIndex < tokens.Count ? tokens[tokenIndex] : null;
    }

    // Consumir el siguiente token
    private string? GetToken()
    {
        return tokenIndex < tokens.Count ? tokens[tokenIndex++] : null;
    }

    // Parser recursivo descendente
    public ExpressionNode Parse()
    {
        Tokenize();
        tokenIndex = 0;
        var result = ParseOrExpression();

        if (PeekToken() != null)
            throw new ArgumentException("Tokens adicionales después de la expresión");

        return result;
    }

    private ExpressionNode ParseOrExpression()
    {
        var left = ParseXorExpression();

        while (PeekToken() == "or")
        {
            string op = GetToken();
            var right = ParseXorExpression();
            var node = new ExpressionNode(op, isOperator: true);
            node.Left = left;
            node.Right = right;
            left = node;
        }

        return left;
    }

    private ExpressionNode ParseXorExpression()
    {
        var left = ParseAndExpression();

        while (PeekToken() == "xor")
        {
            string op = GetToken();
            var right = ParseAndExpression();
            var node = new ExpressionNode(op, isOperator: true);
            node.Left = left;
            node.Right = right;
            left = node;
        }

        return left;
    }

    private ExpressionNode ParseAndExpression()
    {
        var left = ParseAddSubtractExpression();

        while (PeekToken() == "and")
        {
            string op = GetToken();
            var right = ParseAddSubtractExpression();
            var node = new ExpressionNode(op, isOperator: true);
            node.Left = left;
            node.Right = right;
            left = node;
        }

        return left;
    }

    private ExpressionNode ParseAddSubtractExpression()
    {
        var left = ParseMultiplyDivideExpression();

        while (PeekToken() == "+" || PeekToken() == "-")
        {
            string op = GetToken();
            var right = ParseMultiplyDivideExpression();
            var node = new ExpressionNode(op, isOperator: true);
            node.Left = left;
            node.Right = right;
            left = node;
        }

        return left;
    }

    private ExpressionNode ParseMultiplyDivideExpression()
    {
        var left = ParseModuloExpression();

        while (PeekToken() == "*" || PeekToken() == "/")
        {
            string op = GetToken();
            var right = ParseModuloExpression();
            var node = new ExpressionNode(op, isOperator: true);
            node.Left = left;
            node.Right = right;
            left = node;
        }

        return left;
    }

    private ExpressionNode ParseModuloExpression()
    {
        var left = ParseExponentExpression();

        while (PeekToken() == "%")
        {
            string op = GetToken();
            var right = ParseExponentExpression();
            var node = new ExpressionNode(op, isOperator: true);
            node.Left = left;
            node.Right = right;
            left = node;
        }

        return left;
    }

    private ExpressionNode ParseExponentExpression()
    {
        var left = ParseUnaryExpression();

        while (PeekToken() == "**")
        {
            string op = GetToken();
            var right = ParseUnaryExpression();
            var node = new ExpressionNode(op, isOperator: true);
            node.Left = left;
            node.Right = right;
            left = node;
        }

        return left;
    }

    private ExpressionNode ParseUnaryExpression()
    {
        if (PeekToken() == "not")
        {
            string op = GetToken();
            var right = ParseUnaryExpression();
            var node = new ExpressionNode(op, isOperator: true);
            node.Right = right;
            return node;
        }

        return ParsePrimaryExpression();
    }

    private ExpressionNode ParsePrimaryExpression()
    {
        string token = PeekToken();

        if (token == "(")
        {
            GetToken(); // consumir '('
            var expr = ParseOrExpression();
            if (PeekToken() != ")")
                throw new ArgumentException("Paréntesis no balanceados");
            GetToken(); // consumir ')'
            return expr;
        }

        if (double.TryParse(token, out _))
        {
            GetToken();
            return new ExpressionNode(token, isOperand: true);
        }

        throw new ArgumentException($"Token inesperado: {token}");
    }

    // Evaluar el árbol de expresión
    public double Evaluate(ExpressionNode? node)
    {
        if (node == null)
            throw new ArgumentException("Nodo nulo en el árbol");

        // Si es un operando
        if (node.IsOperand)
        {
            if (double.TryParse(node.Value, out double value))
                return value;
            throw new ArgumentException($"Operando inválido: {node.Value}");
        }

        // Si es un operador unario
        if (node.Value == "not")
        {
            double unaryRight = Evaluate(node.Right);
            return (unaryRight == 0) ? 1 : 0;
        }

        // Si es un operador binario
        if (node.Left == null || node.Right == null)
            throw new ArgumentException($"Operador binario {node.Value} sin operandos");

        double leftValue = Evaluate(node.Left);
        double rightValue = Evaluate(node.Right);

        return node.Value switch
        {
            // Operaciones algebraicas
            "+" => leftValue + rightValue,
            "-" => leftValue - rightValue,
            "*" => leftValue * rightValue,
            "/" => rightValue != 0 ? leftValue / rightValue : throw new DivideByZeroException("División por cero"),
            "%" => rightValue != 0 ? leftValue % rightValue : throw new DivideByZeroException("Módulo por cero"),
            "**" => Math.Pow(leftValue, rightValue),

            // Operaciones lógicas (0 = falso, 1 = verdadero)
            "and" => (leftValue != 0 && rightValue != 0) ? 1 : 0,
            "or" => (leftValue != 0 || rightValue != 0) ? 1 : 0,
            "xor" => ((leftValue != 0) != (rightValue != 0)) ? 1 : 0,

            _ => throw new ArgumentException($"Operador desconocido: {node.Value}")
        };
    }

    // Método de evaluación directa
    public double Calculate()
    {
        var ast = Parse();
        return Evaluate(ast);
    }

    // Imprimir el árbol en formato legible
    public void PrintTree(ExpressionNode? node, int level = 0)
    {
        if (node == null)
            return;

        string indent = new string(' ', level * 2);
        if (node.IsOperand)
        {
            Console.WriteLine($"{indent}[Operando: {node.Value}]");
        }
        else
        {
            Console.WriteLine($"{indent}[Operador: {node.Value}]");
            if (node.Left != null)
            {
                Console.WriteLine($"{indent}  Izquierda:");
                PrintTree(node.Left, level + 2);
            }
            if (node.Right != null)
            {
                Console.WriteLine($"{indent}  Derecha:");
                PrintTree(node.Right, level + 2);
            }
        }
    }
}

// Programa principal
class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║         CALCULADORA DE EXPRESIONES BINARIAS                ║");
        Console.WriteLine("║                                                            ║");
        Console.WriteLine("║  Operaciones algebraicas: +, -, *, /, %, **               ║");
        Console.WriteLine("║  Operaciones lógicas: and, or, xor, not                   ║");
        Console.WriteLine("║  Escribe 'salir' para terminar                            ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

        while (true)
        {
            Console.Write("Ingrese una expresión: ");
            string input = Console.ReadLine();

            if (input.ToLower() == "salir")
            {
                Console.WriteLine("\n¡Hasta luego!");
                break;
            }

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Expresión vacía. Intente de nuevo.\n");
                continue;
            }

            try
            {
                var calculator = new ExpressionCalculator(input);
                var ast = calculator.Parse();

                Console.WriteLine("\n📊 Árbol de Expresión:");
                calculator.PrintTree(ast);

                double result = calculator.Evaluate(ast);
                Console.WriteLine($"\n✓ Resultado: {result}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Error: {ex.Message}\n");
            }
        }
    }
}
