# 📋 Resumen de la Infraestructura TCP Implementada

## ✅ Lo que se ha completado

### 1. **Infraestructura de Sockets TCP**
- ✅ Servidor TCP multihilo que acepta múltiples conexiones simultáneas
- ✅ Cliente TCP con interfaz gráfica (Windows Forms)
- ✅ Protocolo de comunicación texto UTF-8 simple y eficiente
- ✅ Manejo robusto de errores de conexión

### 2. **Componentes Implementados**

#### **Network.cs**
- `CalculatorServer`: Clase servidor que maneja conexiones TCP
  - Escucha en puerto 5000 (configurable)
  - Procesa múltiples clientes simultáneamente
  - Evalúa expresiones usando ExpressionCalculator
  - Eventos para monitorear conexiones

- `CalculatorClient`: Clase cliente TCP
  - Conecta a servidor remoto
  - Envía expresiones y recibe resultados
  - Manejo asincrónico de I/O de red
  - Eventos para UI updates

#### **Program.cs**
- Interfaz gráfica Windows Forms como cliente
- Campos para especificar servidor y puerto
- Botones para conectar/desconectar
- Display de resultados en tiempo real
- Log de comunicación con timestamps

#### **ServerProgram.cs**
- Clase `CalculatorServerProgram` para iniciar el servidor
- Interfaz de consola para administrador

#### **CalcuEXP.Server\Program.cs**
- Punto de entrada del ejecutable servidor
- Inicia el servidor en puerto 5000

### 3. **Características de la Aplicación**

#### **Cliente (GUI)**
```
┌─────────────────────────────────────────────────────────────┐
│ CONEXIÓN                                                     │
│ Servidor: 127.0.0.1    Puerto: 5000    [Conectar]          │
│ Estado: ✓ Conectado a 127.0.0.1:5000                       │
├─────────────────────────────────────────────────────────────┤
│ ENTRADA                                                      │
│ Expresión: [2 + 3 * 4         ]  [Evaluar] [Limpiar]       │
├─────────────────────────────────────────────────────────────┤
│ RESULTADO                                                    │
│ Resultado: [14            ]                                 │
│ Estado: ✓ Expresión evaluada correctamente                 │
├─────────────────────────────────────────────────────────────┤
│ LOG DE COMUNICACIÓN                                          │
│ [10:30:45] [CONEXIÓN] Conectado a 127.0.0.1:5000          │
│ [10:30:47] [ENVÍO] 2 + 3 * 4                              │
│ [10:30:47] [RESPUESTA] 14                                  │
└─────────────────────────────────────────────────────────────┘
```

#### **Servidor (Consola)**
```
╔════════════════════════════════════════════════════════════╗
║         SERVIDOR DE CALCULADORA DE EXPRESIONES             ║
║                                                            ║
║  Escuchando en puerto 5000                                 ║
║  Presione 'salir' para detener                            ║
╚════════════════════════════════════════════════════════════╝

[CLIENTE] Cliente conectado desde 127.0.0.1
[EXPRESIÓN] Expresión recibida: 2 + 3 * 4
[CLIENTE] Cliente desconectado
```

## 🚀 Cómo Ejecutar

### **Opción 1: Scripts Batch (Recomendado para Windows)**
```batch
REM En Terminal 1
iniciar_servidor.bat

REM En Terminal 2
iniciar_cliente.bat
```

### **Opción 2: Línea de Comandos**
```bash
# Terminal 1 - Servidor
cd CalcuEXP.Server
dotnet run

# Terminal 2 - Cliente
cd CalcuEXP
dotnet run
```

## 📡 Flujo de Comunicación

```
CLIENTE                              SERVIDOR
   │                                    │
   ├─ Conectar (TCP)                   │
   ├─────────────────────────────────➤ │
   │                                    │ (Acepta conexión)
   │ ◀─────────────────────────────────┤
   │ (Conexión establecida)             │
   │                                    │
   ├─ "2 + 3 * 4" (UTF-8)             │
   ├─────────────────────────────────➤ │
   │                                    │ (Parse & Evaluate)
   │ ◀──────────── "14" ────────────────┤
   │ (Muestra resultado)                │
   │                                    │
   ├─ Desconectar                       │
   ├─────────────────────────────────➤ │
   │                                    │ (Cierra conexión)
```

## 🔧 Configuración

### **Cambiar Puerto**
```csharp
// Servidor
new CalculatorServer(6000);  // En ServerProgram.cs

// Cliente
new CalculatorClient("127.0.0.1", 6000);  // En Interfaz.cs
```

### **Conectar Remoto**
Cliente: Cambiar IP de "127.0.0.1" a la IP del servidor
```
Servidor: 192.168.1.100
Puerto: 5000
```

## 📊 Estadísticas del Proyecto

| Aspecto | Detalle |
|---------|---------|
| Líneas de código | ~1000+ |
| Archivos fuente | 5 (.cs) |
| Proyectos | 2 (Cliente + Servidor) |
| Target Framework | .NET 10.0-windows |
| Protocolo | TCP/IP |
| Puerto | 5000 (configurable) |

## 🎯 Operadores Soportados

### Aritméticos
- `+` Suma
- `-` Resta  
- `*` Multiplicación
- `/` División
- `%` Módulo
- `**` Exponenciación

### Lógicos
- `and` AND bit a bit
- `or` OR bit a bit
- `xor` XOR bit a bit
- `not` NOT lógico

## ✨ Características Especiales

✅ **Multihilo**: Servidor maneja múltiples clientes simultáneamente  
✅ **Thread-Safe**: Uso de locks para operaciones concurrentes  
✅ **Eventos**: OnConnected, OnDisconnected, OnResponseReceived, OnErrorOccurred  
✅ **Log en Tiempo Real**: Visualización de toda la comunicación  
✅ **Manejo de Errores**: Try-catch robusto en cliente y servidor  
✅ **Interfaz Intuitiva**: GUI con colores y feedback visual  
✅ **Sin Dependencias Externas**: Solo .NET Framework  

## 📝 Ejemplo de Uso

```
1. Ejecutar servidor (Terminal 1)
   $ cd CalcuEXP.Server
   $ dotnet run
   ✓ Servidor escuchando en puerto 5000

2. Ejecutar cliente (Terminal 2)
   $ cd CalcuEXP
   $ dotnet run
   ✓ Se abre la interfaz gráfica

3. En la interfaz:
   - Servidor: 127.0.0.1
   - Puerto: 5000
   - Click [Conectar]
   - Expresión: 10 + 5 * 2
   - Click [Evaluar]
   - Resultado: 20 ✓

4. Ver en servidor:
   [CLIENTE] Cliente conectado desde 127.0.0.1
   [EXPRESIÓN] Expresión recibida: 10 + 5 * 2
```

## 🔐 Notas de Seguridad

⚠️ Este es un sistema educativo sin autenticación  
⚠️ No usar en producción sin mejoras de seguridad  
⚠️ Agregar validación de entrada adicional si es necesario  
⚠️ Considerar encriptación TLS para datos sensibles  

## 🐛 Solución de Problemas

### "No puede conectar"
- Verificar que el servidor está corriendo
- Verificar IP correcta (127.0.0.1 para local)
- Verificar puerto (5000 por defecto)
- Revisar firewall

### "Expresión inválida"
- Revisar sintaxis
- Verificar paréntesis balanceados
- Usar espacios alrededor de operadores

### Error de compilación
- Verificar .NET 10.0 instalado: `dotnet --version`
- Restaurar NuGet: `dotnet restore`

## 📚 Referencias

- [Sockets TCP en C#](https://learn.microsoft.com/es-es/dotnet/api/system.net.sockets.tcpclient)
- [Windows Forms](https://learn.microsoft.com/es-es/dotnet/desktop/winforms/)
- [Async/Await](https://learn.microsoft.com/es-es/dotnet/csharp/asynchronous-programming/async-await)

---

**Proyecto completado exitosamente** ✅  
**Fecha**: Enero 2026  
**Versión**: 1.0
