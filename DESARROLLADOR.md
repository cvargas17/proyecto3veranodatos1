# 🛠️ Documentación Técnica - CalcuEXP

## Descripción General
Sistema cliente-servidor para evaluación de expresiones algebraicas y lógicas usando Sockets TCP en C#. Incluye interfaz gráfica para el cliente y servidor multihilo.

---

## Arquitectura
- **Servidor:**
  - Escucha en puerto 5000 (configurable)
  - Multihilo, acepta múltiples clientes
  - Evalúa expresiones y guarda historial en CSV
- **Cliente:**
  - Interfaz Windows Forms
  - Permite conectar/desconectar, enviar expresiones, ver historial
- **Comunicación:**
  - Protocolo TCP, mensajes en texto UTF-8

---

## Componentes Principales
- `Network.cs`: Clases `CalculatorServer` y `CalculatorClient` para manejo de sockets
- `EvaluationHistory.cs`: Manejo de historial en CSV (guardar, leer, exportar)
- `Interfaz.cs`: Lógica de la interfaz gráfica
- `ServerProgram.cs`: Entrada del servidor
- `Program.cs`: Entrada del cliente

---

## Detalles de Implementación
- Guardado automático de cada evaluación en `evaluations.csv`
- Comando especial `GET_HISTORY` para obtener historial desde el cliente
- Buffer de red aumentado para soportar datos grandes
- Métodos para exportar historial y limpiar registros

---

## Checklist de Verificación
- Compila sin errores ni warnings críticos
- El servidor acepta múltiples conexiones
- El cliente se conecta y muestra resultados correctamente
- El historial se guarda y puede exportarse

---

## Comandos Útiles
- Compilar todo: `dotnet build`
- Ejecutar cliente: `cd CalcuEXP && dotnet run`
- Ejecutar servidor: `cd CalcuEXP && dotnet run --project ServerProgram.cs`
- Limpiar compilados: `dotnet clean`

---

## Historial de Cambios
- Implementación de historial CSV
- Mejoras en manejo de errores y buffers
- Nuevas funciones en la interfaz para historial y exportación

---

## Estructura del Proyecto
- `CalcuEXP/` (Cliente y lógica principal)
- `CalcuEXP.Server/` (Servidor CLI)
- Documentación y scripts en la raíz

---

## Créditos
Equipo Proyecto3VeranoDatos1, 2026.
