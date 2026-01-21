using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
        { "or", 1 }, { "|", 1 },
        { "xor", 2 }, { "^", 2 },
        { "and", 3 }, { "&", 3 },
        { "+", 4 },
        { "-", 4 },
        { "*", 5 },
        { "/", 5 },
        { "%", 5 },
        { "**", 6 },
        { "not", 7 }, { "~", 7 }
    };

    private static readonly HashSet<string> BinaryOperators = new HashSet<string>
    {
        "+", "-", "*", "/", "%", "**", "and", "or", "xor", "&", "|", "^"
    };

    private static readonly HashSet<string> UnaryOperators = new HashSet<string>
    {
        "not", "~"
    };

    private static readonly HashSet<string> LogicalOperators = new HashSet<string>
    {
        "and", "or", "xor", "not", "&", "|", "^", "~"
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
        // Agregar los símbolos lógicos: & (and), | (or), ^ (xor), ~ (not)
        // Cada símbolo lógico como alternativa separada para evitar problemas con la clase de caracteres
        string pattern = @"(\d+\.?\d*|\+|-|\*{1,2}|/|%|and|or|xor|not|&|\||\^|~|[\(\)])";
        MatchCollection matches = Regex.Matches(expression, pattern, RegexOptions.IgnoreCase);

        foreach (Match match in matches)
        {
            string val = match.Value.ToLower();
            // Normalizar símbolos a palabras clave
            switch (val)
            {
                case "&": val = "and"; break;
                case "|": val = "or"; break;
                case "^": val = "xor"; break;
                case "~": val = "not"; break;
            }
            tokens.Add(val);
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

    // Validar que un token no sea nulo
    private string ValidateToken(string? token)
    {
        if (token == null)
            throw new ArgumentException("Token inesperado: nulo");
        return token;
    }

    // Parser recursivo descendente
    public ExpressionNode Arbol()
    {
        Tokenize();
        tokenIndex = 0;
        var result = ArbolOrExpression();

        if (PeekToken() != null)
            throw new ArgumentException("Tokens adicionales después de la expresión");

        return result;
    }

    private ExpressionNode ArbolOrExpression()
    {
        var left = ArbolXorExpression();

        while (PeekToken() == "or")
        {
            string op = ValidateToken(GetToken());
            var right = ArbolXorExpression();
            var node = new ExpressionNode(op, isOperator: true);
            node.Left = left;
            node.Right = right;
            left = node;
        }

        return left;
    }

    private ExpressionNode ArbolXorExpression()
    {
        var left = ArbolAndExpression();

        while (PeekToken() == "xor")
        {
            string op = ValidateToken(GetToken());
            var right = ArbolAndExpression();
            var node = new ExpressionNode(op, isOperator: true);
            node.Left = left;
            node.Right = right;
            left = node;
        }

        return left;
    }

    private ExpressionNode ArbolAndExpression()
    {
        var left = ArbolAddSubtractExpression();

        while (PeekToken() == "and")
        {
            string op = ValidateToken(GetToken());
            var right = ArbolAddSubtractExpression();
            var node = new ExpressionNode(op, isOperator: true);
            node.Left = left;
            node.Right = right;
            left = node;
        }

        return left;
    }

    private ExpressionNode ArbolAddSubtractExpression()
    {
        var left = ArbolMultiplyDivideExpression();

        while (PeekToken() == "+" || PeekToken() == "-")
        {
            string op = ValidateToken(GetToken());
            var right = ArbolMultiplyDivideExpression();
            var node = new ExpressionNode(op, isOperator: true);
            node.Left = left;
            node.Right = right;
            left = node;
        }

        return left;
    }

    private ExpressionNode ArbolMultiplyDivideExpression()
    {
        var left = ArbolModuloExpression();

        while (PeekToken() == "*" || PeekToken() == "/")
        {
            string op = ValidateToken(GetToken());
            var right = ArbolModuloExpression();
            var node = new ExpressionNode(op, isOperator: true);
            node.Left = left;
            node.Right = right;
            left = node;
        }

        return left;
    }

    private ExpressionNode ArbolModuloExpression()
    {
        var left = ArbolExponentExpression();

        while (PeekToken() == "%")
        {
            string op = ValidateToken(GetToken());
            var right = ArbolExponentExpression();
            var node = new ExpressionNode(op, isOperator: true);
            node.Left = left;
            node.Right = right;
            left = node;
        }

        return left;
    }

    private ExpressionNode ArbolExponentExpression()
    {
        var left = ArbolUnaryExpression();

        while (PeekToken() == "**")
        {
            string op = ValidateToken(GetToken());
            var right = ArbolUnaryExpression();
            var node = new ExpressionNode(op, isOperator: true);
            node.Left = left;
            node.Right = right;
            left = node;
        }

        return left;
    }

    private ExpressionNode ArbolUnaryExpression()
    {
        if (PeekToken() == "not")
        {
            string op = ValidateToken(GetToken());
            var right = ArbolUnaryExpression();
            var node = new ExpressionNode(op, isOperator: true);
            node.Right = right;
            return node;
        }

        return ArbolPrimaryExpression();
    }

    private ExpressionNode ArbolPrimaryExpression()
    {
        string? token = PeekToken();

        if (token == "(")
        {
            GetToken(); // consumir '('
            var expr = ArbolOrExpression();
            if (PeekToken() != ")")
                throw new ArgumentException("Paréntesis no balanceados");
            GetToken(); // consumir ')'
            return expr;
        }

        if (token != null && double.TryParse(token, out _))
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
        var ast = Arbol();
        return Evaluate(ast);
    }
}

class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new CalcuEXP.Interfaz());
    }
}
