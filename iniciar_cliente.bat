@echo off
REM Script para iniciar el cliente de calculadora

echo.
echo ╔════════════════════════════════════════════════════════════╗
echo ║      CLIENTE CALCULADORA - CONEXION TCP REMOTA             ║
echo ╚════════════════════════════════════════════════════════════╝
echo.

cd /d "%~dp0CalcuEXP"

REM Compilar si es necesario
echo Compilando proyecto...
dotnet build --configuration Release -q

REM Ejecutar el cliente
echo.
echo Iniciando cliente...
echo.

dotnet run --no-build --configuration Release

pause
