# 🔧 Comandos Útiles

## Compilación

### Compilar todo
```bash
cd Proyecto3VeranoDatos1
dotnet build
```

### Compilar específicamente Cliente
```bash
cd CalcuEXP
dotnet build
```

### Compilar específicamente Servidor
```bash
cd CalcuEXP.Server
dotnet build
```

### Compilar en Release
```bash
dotnet build --configuration Release
```

### Limpiar compilados
```bash
dotnet clean
```

---

## Ejecución

### Ejecutar Cliente
```bash
cd CalcuEXP
dotnet run
```

### Ejecutar Servidor
```bash
cd CalcuEXP.Server
dotnet run
```

### Ejecutar con parámetros (futura expansión)
```bash
dotnet run -- --port 6000
```

---

## Debugging

### Ejecutar en modo debug
```bash
dotnet run --configuration Debug
```

### Ver información de compilación
```bash
dotnet build --verbosity detailed
```

### Listar assemblies
```bash
dotnet list package
```

---

## Pruebas de Conexión

### Verificar puerto 5000 en uso (Windows)
```powershell
netstat -ano | findstr :5000
```

### Matar proceso en puerto (Windows)
```powershell
taskkill /PID <PID> /F
```

### Verificar puerto en Linux
```bash
lsof -i :5000
```

### Telnet para probar manualmente
```bash
telnet 127.0.0.1 5000
```

---

## Gestión de Archivos

### Ver estructura del proyecto
```powershell
tree /F
```

### Listar archivos principales
```powershell
Get-ChildItem -Recurse -Include "*.cs", "*.csproj"
```

### Buscar en archivos
```powershell
Select-String -Path "*.cs" -Pattern "CalculatorServer"
```

---

## Git (si aplica)

### Ver cambios
```bash
git status
```

### Agregar archivos
```bash
git add .
```

### Commit
```bash
git commit -m "Mensaje descriptivo"
```

### Push
```bash
git push origin main
```

---

## Solución de Problemas

### Restaurar paquetes NuGet
```bash
dotnet restore
```

### Limpiar NuGet cache
```bash
dotnet nuget locals all --clear
```

### Ver logs detallados
```bash
dotnet build --verbosity diagnostic
```

### Verificar .NET version
```bash
dotnet --version
```

### Listar SDKs instalados
```bash
dotnet --list-sdks
```

---

## Optimización

### Publicar en Release
```bash
dotnet publish -c Release
```

### Medir tiempo de compilación
```bash
$start = Get-Date
dotnet build
$end = Get-Date
($end - $start).TotalSeconds
```

---

## Documentación

### Generar documentación XML
```bash
dotnet build /p:GenerateDocumentationFile=true
```

### Abrir README
```powershell
Start-Process README.md
```

---

## Atajos Útiles (Visual Studio Code)

### Generar clase
```
ctrl+shift+p > C#: Generate Constructor
```

### Ir a definición
```
F12 o ctrl+Click
```

### Buscar referencias
```
Shift+F12
```

### Refactorizar (renombrar)
```
F2
```

### Formatear código
```
Shift+Alt+F
```

### Ver problemas
```
Ctrl+Shift+M
```

---

## Monitoreo

### Monitor de recursos (Windows)
```powershell
Get-Process | Where-Object {$_.ProcessName -like "*dotnet*"}
```

### Ver memoria usada
```powershell
(Get-Process dotnet).WorkingSet / 1MB
```

### Monitoreo en tiempo real (Windows)
```
tasklist /FI "IMAGENAME eq dotnet.exe"
```

---

## Ejemplos de Expresiones para Probar

### Aritméticas
```
2 + 3
10 - 4
3 * 4
20 / 4
10 % 3
2 ** 3
```

### Con Paréntesis
```
(2 + 3) * 4
2 + (3 * 4)
((10 + 5) * 2)
```

### Lógicas
```
5 and 3
0 or 1
3 xor 1
not 0
5 and 3 or 1
```

### Complejas
```
(5 + 3) * 2 - 4
10 / 2 + 3 * 4
2 ** 3 + 1 - 2
```

### Errores Esperados
```
10 / 0          → Error: División por cero
10 %            → Error: Token inesperado
(5 + 3          → Error: Paréntesis no balanceados
5 ++            → Error: Sintaxis inválida
```

---

## Variables de Entorno

### Ver variables de entorno
```powershell
Get-ChildItem Env:
```

### Establecer variable
```powershell
$env:MYVAR = "value"
```

### Variable permanente (Windows)
```
setx MYVAR "value"
```

---

## Empaquetar para Distribución

### Publicar ejecutable standalone
```bash
dotnet publish -c Release -r win-x64 --self-contained
```

### Crear ZIP
```powershell
Compress-Archive -Path "./bin/Release/net10.0-windows/publish" -DestinationPath "CalcuEXP.zip"
```

---

## Comandos de Ayuda

### Ver opciones de dotnet
```bash
dotnet --help
```

### Ver opciones de build
```bash
dotnet build --help
```

### Ver opciones de run
```bash
dotnet run --help
```

---

## Checklist de Verificación Rápida

```powershell
# 1. Verificar compilación
dotnet build ✓

# 2. Ejecutar servidor en background
Start-Process dotnet -ArgumentList "run" -WorkingDirectory "CalcuEXP.Server" ✓

# 3. Ejecutar cliente
cd CalcuEXP && dotnet run ✓

# 4. Probar expresión: "2 + 3"
# Resultado esperado: 5 ✓

# 5. Probar expresión: "(5 + 3) * 2"
# Resultado esperado: 16 ✓

# 6. Desconectar y cerrar
# Status: OK ✓
```

---

## Tips Productivos

💡 Usar scripts batch para automatizar inicio:
```batch
@echo off
start "Servidor" cmd /k "cd CalcuEXP.Server && dotnet run"
timeout /t 2
start "Cliente" cmd /k "cd CalcuEXP && dotnet run"
```

💡 Monitorear cambios automáticamente:
```bash
dotnet watch run
```

💡 Crear alias en PowerShell:
```powershell
Set-Alias calc-server "cd CalcuEXP.Server && dotnet run"
Set-Alias calc-client "cd CalcuEXP && dotnet run"
```

💡 Ver logs en tiempo real:
```bash
dotnet run --verbosity minimal
```

---

## Referencias Rápidas

| Comando | Función |
|---------|---------|
| `dotnet new console` | Crear proyecto console |
| `dotnet add package` | Agregar NuGet package |
| `dotnet test` | Ejecutar pruebas |
| `dotnet format` | Formatear código |
| `dotnet tool list` | Listar herramientas globales |

---

*Última actualización: Enero 2026*
