# 📖 Guía de Usuario - CalcuEXP

## ¿Qué es CalcuEXP?
Aplicación cliente-servidor para evaluar expresiones algebraicas y lógicas usando Sockets TCP en C#. El cliente es una interfaz gráfica (Windows Forms) y el servidor procesa las expresiones.

---

## Instalación y Requisitos
- Tener .NET 10.0 instalado (`dotnet --version`)
- Windows 7 o superior

---

## Inicio Rápido

### Opción 1: Usar scripts (recomendado)
1. Ejecuta `iniciar_servidor.bat` para iniciar el servidor
2. Ejecuta `iniciar_cliente.bat` para abrir el cliente

### Opción 2: Manual
- Abre dos terminales
- En una: `cd CalcuEXP` y `dotnet run --project ServerProgram.cs`
- En otra: `cd CalcuEXP` y `dotnet run`

---

## Uso de la Aplicación
- Ingresa la IP del servidor (por defecto: 127.0.0.1) y puerto (por defecto: 5000)
- Haz click en "Conectar"
- Escribe una expresión y presiona "Evaluar"
- El resultado aparecerá en pantalla y se guardará en el historial
- Puedes ver el historial y exportarlo a CSV desde el cliente

---

## Operadores Soportados
- Suma: `+`, Resta: `-`, Multiplicación: `*`, División: `/`, Módulo: `%`, Exponenciación: `**`
- Lógicos: `and`, `or`, `xor`

---

## Ejemplo de Uso
1. Conectar al servidor
2. Evaluar: `2+2` → Resultado: `4`
3. Consultar historial y exportar si lo deseas

---

## Solución de Problemas
- Si no conecta, revisa que el servidor esté corriendo y el puerto/IP sean correctos
- Si hay errores de compilación, asegúrate de tener la versión correcta de .NET

---

## Créditos y Contacto
Desarrollado por el equipo de Proyecto3VeranoDatos1.
