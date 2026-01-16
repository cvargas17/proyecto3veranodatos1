# 🚀 Guía Rápida de Uso

## Inicio Rápido en Windows

### Opción 1: Usar los scripts batch (Recomendado)

#### Terminal 1 - Iniciar Servidor:
```bash
iniciar_servidor.bat
```

#### Terminal 2 - Iniciar Cliente:
```bash
iniciar_cliente.bat
```

---

### Opción 2: Usar línea de comandos

#### Terminal 1 - Iniciar Servidor:
```bash
cd CalcuEXP
dotnet run --project ServerProgram.cs
```

#### Terminal 2 - Iniciar Cliente:
```bash
cd CalcuEXP
dotnet run
```

---

## Uso de la Aplicación

### 1. Interfaz del Cliente
![Descripción de la interfaz]
- **Sección de Conexión** (Azul)
  - Ingresa la IP del servidor (default: 127.0.0.1)
  - Ingresa el puerto (default: 5000)
  - Botón "Conectar" para establecer conexión

- **Sección de Entrada** (Verde)
  - Campo para escribir la expresión matemática
  - Botón "Evaluar" para calcular
  - Botón "Limpiar" para limpiar campos

- **Sección de Resultado** (Amarillo)
  - Muestra el resultado de la evaluación
  - Mensajes de estado de la operación

- **Log de Comunicación** (Negro)
  - Registro en tiempo real de toda la comunicación
  - Timestamps de cada evento

### 2. Ejemplos de Uso

**Conectar al servidor:**
```
Servidor: 127.0.0.1
Puerto: 5000
→ Click en "Conectar"
→ Status: ✓ Conectado a 127.0.0.1:5000
```

**Evaluar expresiones:**
```
Expresión: 2 + 3 * 4
→ Click en "Evaluar"
→ Resultado: 14

Expresión: (2 + 3) * 4
→ Resultado: 20

Expresión: 5 and 3
→ Resultado: 1

Expresión: not 0
→ Resultado: 1
```

### 3. Log de Comunicación

El log muestra:
```
[10:30:45] [CONEXIÓN] Conectado a 127.0.0.1:5000
[10:30:47] [ENVÍO] 2 + 3 * 4
[10:30:47] [RESPUESTA] 14
[10:30:49] [ENVÍO] (2 + 3) * 4
[10:30:49] [RESPUESTA] 20
```

---

## Solución de Problemas

### Error: "No puede conectar al servidor"
- ✓ Verificar que el servidor esté ejecutándose
- ✓ Verificar la IP correcta (127.0.0.1 para máquina local)
- ✓ Verificar que el puerto sea el mismo (5000 por defecto)
- ✓ Revisar firewall

### Error: "Expresión inválida"
- ✓ Revisar la sintaxis de la expresión
- ✓ Verificar paréntesis balanceados
- ✓ Usar espacios alrededor de operadores (recomendado)

### Error: "División por cero"
- ✓ Revisar que no haya división entre cero
- ✓ Ejemplo incorrecto: `10 / 0` ❌
- ✓ Ejemplo correcto: `10 / 2` ✓

### La interfaz se ve pequeña
- Redimensionar la ventana del cliente
- La interfaz se adapta automáticamente

---

## Operadores Disponibles

### Aritméticos (Mayor precedencia)
| Símbolo | Nombre | Ejemplo |
|---------|--------|---------|
| `+` | Suma | `2 + 3 = 5` |
| `-` | Resta | `5 - 2 = 3` |
| `*` | Multiplicación | `3 * 4 = 12` |
| `/` | División | `12 / 3 = 4` |
| `%` | Módulo | `10 % 3 = 1` |
| `**` | Exponenciación | `2 ** 3 = 8` |

### Lógicos (Menor precedencia)
| Símbolo | Nombre | Ejemplo |
|---------|--------|---------|
| `and` | AND | `5 and 3 = 1` |
| `or` | OR | `0 or 1 = 1` |
| `xor` | XOR | `3 xor 1 = 1` |
| `not` | NOT | `not 0 = 1` |

---

## Características

✅ Evaluación en tiempo real  
✅ Múltiples clientes simultáneos  
✅ Log de comunicación detallado  
✅ Manejo de errores robusto  
✅ Interfaz gráfica intuitiva  
✅ Sockets TCP seguros  
✅ Timestamps en log  
✅ Indicador de estado de conexión  
✅ Expresiones con paréntesis  
✅ Precedencia correcta de operadores  

---

## Arquitectura Técnica

```
┌─────────────────────┐         ┌─────────────────────┐
│   Cliente (GUI)     │         │   Servidor TCP      │
│  ┌───────────────┐  │         │  ┌───────────────┐  │
│  │ Interfaz      │  │◄────────│─►│ Socket TCP    │  │
│  │ Windows Forms │  │ Puerto  │  │ Multihilo     │  │
│  │               │  │  5000   │  │               │  │
│  └───────────────┘  │         │  └───────────────┘  │
│ CalculatorClient    │         │ CalculatorServer    │
└─────────────────────┘         └─────────────────────┘
         │                               │
         │ Envía: "2 + 3 * 4"           │
         │──────────────────────────────►│
         │                               │ Parse & Evaluate
         │                         Resultado: 14
         │◄──────────────────────────────│
         │ Muestra: 14                   │
```

---

## Notas Importantes

🔔 **Conexión Remota**: Para conectar desde otra máquina, usar la IP del servidor (no 127.0.0.1)

🔔 **Puerto 5000**: Si está en uso, cambiar en:
- Servidor: `new CalculatorServer(PUERTO_NUEVO)`
- Cliente: `new CalculatorClient("IP", PUERTO_NUEVO)`

🔔 **Thread-Safe**: La aplicación es thread-safe y maneja múltiples conexiones

🔔 **Sintaxis**: Use operadores en minúsculas (and, or, xor, not)

---

## Contacto & Soporte

Para reportar problemas o sugerencias, revisar los logs en la sección de comunicación.

¡Que disfrute de la calculadora! 🎉
