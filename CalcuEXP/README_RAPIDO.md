# ⚡ INICIO RÁPIDO - Sistema de Historial CSV

## 3️⃣ Pasos para Usar

### 1️⃣ Iniciar Servidor
```bash
cd f:\repos\Proyecto3VeranoDatos1\CalcuEXP
dotnet run ServerProgram.cs
```
✅ Se crea automáticamente: `evaluations.csv`

### 2️⃣ Ejecutar Cliente (Interfaz)
```bash
# Otra ventana/terminal
dotnet run
```
✅ Se abre la GUI del cliente

### 3️⃣ Usar el Sistema
```
1. Conectar al servidor
   • Servidor: 127.0.0.1
   • Puerto: 5000
   • Click "Conectar"

2. Evaluar expresiones
   • Ingresa: 2+2
   • Click "Evaluar"
   • Resultado: 4
   • ✅ Se guardó en CSV automáticamente

3. Ver Historial
   • Click "Historial"
   • Se abre ventana con todos los registros
   • Click "Exportar CSV" para guardar
```

## 📊 Qué se Guarda

```
Fecha,Hora,Expresión,Resultado,Cliente
2025-01-16,14:30:45,"2+2","4",127.0.0.1
```

## 🎯 Ubicaciones Importantes

| Elemento | Ubicación |
|----------|-----------|
| **Servidor CSV** | `evaluations.csv` (carpeta del servidor) |
| **Cliente CSV** | Donde elijas al exportar |
| **Código Servidor** | `Network.cs` (CalculatorServer) |
| **Código Cliente** | `Network.cs` (CalculatorClient) |
| **Interfaz** | `Interfaz.cs` |
| **Historial** | `EvaluationHistory.cs` |

## 🔧 Cambios en la Interfaz

✅ **Nuevo Botón**: "Historial" en panel entrada
✅ **Nueva Ventana**: Muestra CSV con botón exportar
✅ **Nuevas Funcionalidades**: ObtenerHistorial(), VerHistorial()

## 📝 Ejemplo Completo

```
1. Evalúas: 10*5
   CSV: 2025-01-16,14:31:12,"10*5","50",127.0.0.1

2. Evalúas: 100/2
   CSV: 2025-01-16,14:32:00,"100/2","50",127.0.0.1

3. Clickeas "Historial"
   ↓ Ves ambos registros en ventana emergente

4. Clickeas "Exportar CSV"
   ↓ Archivo guardado en tu PC con ambos registros
```

## ✅ Verificación

```bash
# Ver el archivo CSV creado
cat evaluations.csv

# Salida esperada:
# Fecha,Hora,Expresión,Resultado,Cliente
# 2025-01-16,14:31:12,"10*5","50",127.0.0.1
```

---

**¡Listo! El sistema está completamente implementado y funcionando.**
