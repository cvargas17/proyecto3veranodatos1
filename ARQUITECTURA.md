# 🏗️ Arquitectura del Sistema

## Diagrama General

```
┌──────────────────────────────────────────────────────────────────┐
│                        ARQUITECTURA TCP                           │
└──────────────────────────────────────────────────────────────────┘

                    MÁQUINA LOCAL o REMOTA
                              │
                    ┌─────────┴──────────┐
                    │                    │
            ┌───────▼─────────┐   ┌─────▼────────────┐
            │  CLIENTE (GUI)  │   │  SERVIDOR (CLI)  │
            │  Port: Dinámico │   │  Port: 5000      │
            └───────┬─────────┘   └─────▬────────────┘
                    │                    │
                    │    TCP Socket      │
                    └────────────────────┘

```

## Componentes Principales

```
┌─────────────────────────────────────────────────────────────────┐
│                          CLIENTE                                 │
│  ┌────────────────────────────────────────────────────────────┐ │
│  │                    INTERFAZ GRÁFICA                        │ │
│  │                   (Windows Forms)                          │ │
│  ├────────────────────────────────────────────────────────────┤ │
│  │  [Servidor][Puerto]              [Conectar/Desconectar]  │ │
│  │  [Expresión             ]         [Evaluar] [Limpiar]    │ │
│  │  [Resultado                    ]                          │ │
│  │  ┌──────────────────────────────────────────────────────┐│ │
│  │  │            LOG DE COMUNICACIÓN                      ││ │
│  │  │  [HH:MM:SS] [CONEXIÓN] Conectado a 127.0.0.1:5000 ││ │
│  │  │  [HH:MM:SS] [ENVÍO] 2 + 3 * 4                     ││ │
│  │  │  [HH:MM:SS] [RESPUESTA] 14                        ││ │
│  │  └──────────────────────────────────────────────────────┘│ │
│  └────────────────────────────────────────────────────────────┘ │
│  ┌────────────────────────────────────────────────────────────┐ │
│  │              CALCULATORCLIENT (Network.cs)               │ │
│  │  • TcpClient connection                                  │ │
│  │  • NetworkStream I/O                                    │ │
│  │  • Async connection/send/receive                        │ │
│  │  • Event handling                                       │ │
│  └────────────────────────────────────────────────────────────┘ │
│  ┌────────────────────────────────────────────────────────────┐ │
│  │          EXPRESSIONCALCULATOR (Program.cs)              │ │
│  │  • Tokenization                                         │ │
│  │  • Parsing (Recursive Descent)                          │ │
│  │  • AST Construction                                     │ │
│  │  • Evaluation                                           │ │
│  └────────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────────┘
                              │
                    (TCP/IP Port 5000)
                              │
┌─────────────────────────────────────────────────────────────────┐
│                         SERVIDOR                                │
│  ┌────────────────────────────────────────────────────────────┐ │
│  │         CONSOLA INTERACTIVA (ServerProgram.cs)           │ │
│  │  ╔════════════════════════════════════════╗              │ │
│  │  ║  Escuchando en puerto 5000             ║              │ │
│  │  ║  [CLIENTE] Cliente conectado...        ║              │ │
│  │  ║  [EXPRESIÓN] Expresión recibida...     ║              │ │
│  │  ║  Presione 'salir' para detener         ║              │ │
│  │  ╚════════════════════════════════════════╝              │ │
│  └────────────────────────────────────────────────────────────┘ │
│  ┌────────────────────────────────────────────────────────────┐ │
│  │              CALCULATORSERVER (Network.cs)               │ │
│  │  • TcpListener on port 5000                              │ │
│  │  • Accept incoming connections                           │ │
│  │  • Multi-threaded client handling                        │ │
│  │  • Expression evaluation                                 │ │
│  │  • Result transmission                                   │ │
│  └────────────────────────────────────────────────────────────┘ │
│  ┌────────────────────────────────────────────────────────────┐ │
│  │    EXPRESSIONCALCULATOR (Shared with Client)            │ │
│  │  • Evaluates mathematical expressions                    │ │
│  │  • Returns string result to client                       │ │
│  └────────────────────────────────────────────────────────────┘ │
│  ┌────────────────────────────────────────────────────────────┐ │
│  │            CLIENT CONNECTION HANDLER                     │ │
│  │  Per client thread:                                      │ │
│  │  1. Accept TCP connection                                │ │
│  │  2. Read expression bytes                                │ │
│  │  3. Decode UTF-8 string                                  │ │
│  │  4. Evaluate expression                                  │ │
│  │  5. Send result bytes                                    │ │
│  │  6. Close connection                                     │ │
│  └────────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────────┘
```

## Flujo de Ejecución

```
PASO 1: INICIALIZACIÓN
┌─────────────────────────────────┐       ┌──────────────────────┐
│ Cliente Inicia                  │       │ Servidor Inicia      │
├─────────────────────────────────┤       ├──────────────────────┤
│ 1. Carga interfaz GUI           │       │ 1. Crea TcpListener  │
│ 2. Inicializa CalculatorClient  │       │ 2. Comienza a       │
│ 3. Espera entrada del usuario   │       │    escuchar          │
│ 4. Muestra estado: Desconectado │       │ 3. Acepta clientes  │
└─────────────────────────────────┘       └──────────────────────┘

PASO 2: CONEXIÓN
Cliente                                     Servidor
   │                                           │
   ├─ Usuario: "Conectar"                     │
   │                                           │
   ├─ client.Connect("127.0.0.1", 5000)      │
   │                                           │
   ├──────── TCP SYN ────────────────────────➤ │
   │                                   listener.AcceptTcpClient()
   │ ◀──────── TCP SYNACK ──────────────────── │
   │                                           │
   ├──────── TCP ACK ────────────────────────➤ │
   │                                           │
   │                                    Crea nueva thread
   │                                    para cliente
   │ ◀─── OnConnected event ────────────────── │
   │                                           │
   └─ Actualiza UI: Verde ✓                  └─ Listo para recibir

PASO 3: ENVÍO DE EXPRESIÓN
Usuario escribe: "2 + 3 * 4"
   │
   ├─ Click: Evaluar
   │
   ├─ Encode: "2 + 3 * 4" → bytes UTF-8
   │
   ├─ stream.Write(bytes)
   │
   ├────────────── bytes ──────────────────────➤
   │                                           │
   │                        stream.Read(buffer)
   │                        Recibe: "2 + 3 * 4"
   │                           │
   │                        EvaluarExpresion()
   │                           │
   │                        1. Parse
   │                        2. Evaluate
   │                        3. Result: "14"
   │                           │
   │                        Encode: "14" → bytes
   │                        stream.Write(bytes)
   │
   │ ◀──────────── "14" bytes ──────────────────
   │
   ├─ Decode: bytes → "14"
   │
   ├─ Mostrar resultado: 14
   │
   └─ Mostrar log: [RESPUESTA] 14

PASO 4: DESCONEXIÓN
   │
   ├─ Usuario: "Desconectar"
   │
   ├─ stream.Close()
   │
   ├─ client.Close()
   │
   ├─────────── FIN ────────────────────────➤
   │                                   Cierra conexión
   │                                   Termina thread
   │
   └─ Actualiza UI: Rojo ✗                  │
```

## Estructura de Archivos

```
f:\repos\Proyecto3VeranoDatos1\
│
├─ CalcuEXP\                          (Proyecto Cliente)
│  ├─ Program.cs                      (GUI + ExpressionCalculator)
│  ├─ Interfaz.cs                     (Formulario Windows Forms)
│  ├─ Network.cs                      (CalculatorClient/Server)
│  ├─ ServerProgram.cs                (Clase servidor)
│  ├─ CalcuEXP.csproj                 (Config: WinExe, net10.0-windows)
│  └─ bin/obj/                        (Compilados)
│
├─ CalcuEXP.Server\                   (Proyecto Servidor)
│  ├─ Program.cs                      (Main - Punto entrada)
│  ├─ CalcuEXP.Server.csproj          (Config: Exe, net10.0-windows)
│  └─ bin/obj/                        (Compilados)
│
├─ README.md                          (Documentación principal)
├─ GUIA_RAPIDA.md                     (Guía de usuario)
├─ IMPLEMENTACION.md                  (Detalles técnicos)
├─ CHECKLIST.md                       (Verificación)
├─ ARQUITECTURA.md                    (Este archivo)
│
├─ iniciar_servidor.bat               (Script Windows servidor)
├─ iniciar_cliente.bat                (Script Windows cliente)
│
└─ CalcuEXP.sln                       (Solución)
```

## Matriz de Responsabilidades

```
┌──────────────────┬──────────────────┬──────────────┬─────────────┐
│ Componente       │ Responsabilidad  │ Archivo      │ Tipo        │
├──────────────────┼──────────────────┼──────────────┼─────────────┤
│ ExpressionNode   │ Nodo del AST     │ Program.cs   │ Clase       │
├──────────────────┼──────────────────┼──────────────┼─────────────┤
│ Expression       │ Parser + Evaluador│ Program.cs   │ Clase       │
│ Calculator       │ de expresiones   │              │             │
├──────────────────┼──────────────────┼──────────────┼─────────────┤
│ CalculatorClient │ Cliente TCP      │ Network.cs   │ Clase       │
│                  │ Comunicación      │              │             │
├──────────────────┼──────────────────┼──────────────┼─────────────┤
│ CalculatorServer │ Servidor TCP     │ Network.cs   │ Clase       │
│                  │ Multihilo        │              │             │
├──────────────────┼──────────────────┼──────────────┼─────────────┤
│ Interfaz         │ Formulario GUI   │ Interfaz.cs  │ Clase       │
├──────────────────┼──────────────────┼──────────────┼─────────────┤
│ Program (Client) │ Entry point      │ Program.cs   │ Main()      │
├──────────────────┼──────────────────┼──────────────┼─────────────┤
│ Calculator       │ Entry point      │ Server/      │ Main()      │
│ ServerProgram    │ servidor         │ Program.cs   │             │
└──────────────────┴──────────────────┴──────────────┴─────────────┘
```

## Secuencia de Clases

```
┌─────────────────────────────────────────────────────┐
│              CLIENTE (CalcuEXP.exe)                 │
├─────────────────────────────────────────────────────┤
│                                                     │
│  Main() → Interfaz() → InitializeComponent()        │
│                           │                        │
│                      CalculatorClient               │
│                      ├─ OnConnected                │
│                      ├─ OnDisconnected            │
│                      ├─ OnResponseReceived        │
│                      └─ OnErrorOccurred           │
│                                                     │
│                      ExpressionCalculator          │
│                      ├─ Parse()                    │
│                      └─ Evaluate()                 │
│                                                     │
└─────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────┐
│           SERVIDOR (CalcuEXP.Server.exe)            │
├─────────────────────────────────────────────────────┤
│                                                     │
│  Main() → CalculatorServerProgram.StartServer()    │
│               │                                    │
│           CalculatorServer                          │
│           ├─ Start()                              │
│           ├─ AcceptConnections()                  │
│           ├─ HandleClient()                       │
│           ├─ OnClientConnected                    │
│           ├─ OnClientDisconnected                 │
│           └─ OnExpressionReceived                 │
│                  │                                │
│            ExpressionCalculator                    │
│            ├─ Parse()                             │
│            └─ Evaluate()                          │
│                                                     │
└─────────────────────────────────────────────────────┘
```

## Modelo de Datos

```
ExpressionNode
├─ Value: string          (ej: "2", "+", "*")
├─ IsOperator: bool       (ej: true para "+")
├─ IsOperand: bool        (ej: true para "2")
├─ Left: ExpressionNode?  (rama izquierda)
└─ Right: ExpressionNode? (rama derecha)

Ejemplo: (2 + 3) * 4
     
       *
      / \
     +   4
    / \
   2   3
```

## Precedencia de Operadores

```
Precedencia       Operador       Asociatividad
────────────────────────────────────────────
1 (Mayor)         or             Izquierda
2                 xor            Izquierda
3                 and            Izquierda
4                 +, -           Izquierda
5                 *, /, %        Izquierda
6                 **             Derecha
7 (Menor)         not            Derecha
```

---

**Última actualización**: Enero 2026  
**Versión**: 1.0  
**Estado**: ✅ Completado
