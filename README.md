# Calculadora de Expresiones TCP

## Descripción
Aplicación cliente-servidor de calculadora de expresiones usando Sockets TCP en C#. El servidor evalúa expresiones algebraicas y lógicas, y los clientes se conectan remotamente para usar la calculadora.

## Arquitectura

### Servidor (ServerProgram.cs)
- **Puerto**: 5000 (configurable)
- **Protocolo**: TCP
- **Funcionalidad**: Escucha conexiones de clientes, recibe expresiones y envía resultados

### Cliente (Program.cs - GUI)
- **Conexión**: Interfaz gráfica con Windows Forms
- **Funcionalidad**: Se conecta al servidor y envía expresiones para evaluar
- **Características**:
  - Campo para ingresar servidor y puerto
  - Botón para conectar/desconectar
  - Log de comunicación en tiempo real
  - Visualización de resultados

### Network.cs
Contiene dos clases principales:
- **CalculatorServer**: Servidor TCP que maneja múltiples clientes
- **CalculatorClient**: Cliente TCP para conectarse al servidor

## Operadores Soportados

### Algebraicos
- `+` : Suma
- `-` : Resta
- `*` : Multiplicación
- `/` : División
- `%` : Módulo
- `**` : Exponenciación

### Lógicos
- `and` : AND bit a bit (0 = falso, 1 = verdadero)
- `or` : OR bit a bit
- `xor` : XOR bit a bit
- `not` : NOT lógico (unario)

## Instrucciones de Uso

### 1. Compilar los Proyectos
```bash
# Cliente (GUI)
cd CalcuEXP
dotnet build

# Servidor
cd ../CalcuEXP.Server
dotnet build
```

### 2. Iniciar el Servidor
```bash
cd CalcuEXP.Server
dotnet run
```

El servidor mostrará:
```
╔════════════════════════════════════════════════════════════╗
║         SERVIDOR DE CALCULADORA DE EXPRESIONES             ║
║                                                            ║
║  Escuchando en puerto 5000                                 ║
║  Presione 'salir' para detener                            ║
╚════════════════════════════════════════════════════════════╝
```

### 3. Iniciar el Cliente
En otra terminal:
```bash
cd CalcuEXP
dotnet run
```

### 4. Usar la Aplicación
1. En la interfaz gráfica, ingresar:
   - **Servidor**: `127.0.0.1` (o la IP del servidor)
   - **Puerto**: `5000`
2. Hacer clic en **"Conectar"**
3. Ingresar una expresión (ej: `2 + 3 * 4`)
4. Hacer clic en **"Evaluar"**
5. Ver el resultado y el log de comunicación

## Ejemplos de Expresiones

```
2 + 3 * 4          → 14
(2 + 3) * 4        → 20
2 ** 3             → 8
10 / 2 + 3         → 8
5 and 3            → 1 (verdadero)
0 or 1             → 1 (verdadero)
not 0              → 1 (verdadero)
not 5              → 0 (falso)
3 xor 1            → 1
(5 + 3) * 2 - 4    → 12
```

## Estructura de Carpetas

```
CalcuEXP/
├── Program.cs          (Cliente GUI + ExpressionCalculator)
├── Interfaz.cs         (Interfaz gráfica del cliente)
├── Network.cs          (CalculatorServer y CalculatorClient)
├── ServerProgram.cs    (Clase CalculatorServerProgram)
├── CalcuEXP.csproj     (Configuración del proyecto cliente)
└── bin/obj/            (Compilados y objetos)

CalcuEXP.Server/
├── Program.cs          (Punto de entrada del servidor)
├── CalcuEXP.Server.csproj (Configuración del proyecto servidor)
└── bin/obj/            (Compilados y objetos)
```

## Características Técnicas

### Manejo de Concurrencia
- El servidor acepta múltiples clientes simultáneamente
- Cada cliente se maneja en una tarea separada
- Thread-safe con eventos para actualizar la UI

### Protocolo de Comunicación
1. **Cliente envía**: Expresión como string UTF-8
2. **Servidor procesa**: Parsea y evalúa la expresión
3. **Servidor responde**: Resultado como string o error
4. **Cliente recibe**: Muestra el resultado en la interfaz

### Manejo de Errores
- Errores de conexión
- Expresiones inválidas
- División por cero
- Tokens inesperados

## Requisitos
- .NET 8.0 o superior
- Windows Forms (incluido en .NET)
- Sockets TCP (parte del framework)

## Notas Importantes
- El servidor escucha en `0.0.0.0:5000` (todas las interfaces)
- El cliente se conecta a `127.0.0.1:5000` por defecto
- Para conectarse desde otra máquina, cambiar la IP del servidor
- El protocolo es texto UTF-8 sin delimitadores especiales
- El servidor espera una expresión por conexión
