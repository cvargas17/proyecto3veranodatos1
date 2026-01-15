@echo off
REM Script para iniciar el servidor de calculadora

echo.
echo ╔════════════════════════════════════════════════════════════╗
echo ║         SERVIDOR DE CALCULADORA DE EXPRESIONES             ║
echo ║                    Puerto: 5000                            ║
echo ╚════════════════════════════════════════════════════════════╝
echo.

cd /d "%~dp0CalcuEXP.Server"

REM Compilar si es necesario
echo Compilando proyecto servidor...
dotnet build --configuration Release -q

REM Ejecutar el servidor
echo.
echo Iniciando servidor...
echo.

dotnet run --no-build --configuration Release

pause
