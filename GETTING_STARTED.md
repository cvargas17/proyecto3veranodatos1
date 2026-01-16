# 🚀 GETTING STARTED - Primeros Pasos

## ⏱️ Tiempo Total: 5 minutos

---

## Paso 1: Verificar Requisitos (1 minuto)

✅ Tener .NET 10.0 instalado:
```bash
dotnet --version
```

Debería mostrar algo como: `10.0.101`

✅ Windows 7 o superior (para GUI)

---

## Paso 2: Ubicarse en el Directorio (1 minuto)

```bash
cd Proyecto3VeranoDatos1
```

---

## Paso 3: Compilar (2 minutos)

### Opción A: Línea de Comandos
```bash
dotnet build
```

### Opción B: Visual Studio Code
- Abrir carpeta en VS Code
- Terminal → Nueva terminal
- Escribir: `dotnet build`

**Resultado esperado:**
```
Build succeeded in X,Xs
```

---

## Paso 4: Ejecutar (5 minutos)

### Terminal 1 - Servidor
```bash
cd CalcuEXP.Server
dotnet run
```

Verás:
```
╔════════════════════════════════════════════════════════════╗
║         SERVIDOR DE CALCULADORA DE EXPRESIONES             ║
║                                                            ║
║  Escuchando en puerto 5000                                 ║
║  Presione 'salir' para detener                            ║
╚════════════════════════════════════════════════════════════╝
```

### Terminal 2 - Cliente
```bash
cd CalcuEXP
dotnet run
```

Se abrirá una ventana gráfica.

---

## Paso 5: Usar la Aplicación (2 minutos)

En la ventana gráfica:

1. **Conectar al servidor:**
   - Servidor: `127.0.0.1` (ya está)
   - Puerto: `5000` (ya está)
   - Click: `[Conectar]`
   - Esperar: Pasa a verde ✓

2. **Calcular una expresión:**
   - En el campo "Expresión" escribir: `2 + 3`
   - Click: `[Evaluar]`
   - Ver resultado: `5`

3. **Probar otra:**
   - Escribir: `(2 + 3) * 4`
   - Click: `[Evaluar]`
   - Ver resultado: `20`

4. **Desconectar:**
   - Click: `[Desconectar]`
   - Estado pasa a rojo ✗

---

## ✅ ¡Listo!

Ya tienes funcionando el sistema cliente-servidor.

---

## 🎯 Próximos Pasos

### Para Experimentar Más
- Ver [GUIA_RAPIDA.md](GUIA_RAPIDA.md) para más ejemplos
- Probar con múltiples clientes abiertos
- Intentar conexiones remotas

### Para Entender el Código
- Abrir `CalcuEXP/Program.cs` en VS Code
- Abrir `CalcuEXP/Network.cs` para ver la lógica TCP
- Abrir `CalcuEXP/Interfaz.cs` para ver la GUI

### Para Profundizar
- Leer [ARQUITECTURA.md](ARQUITECTURA.md)
- Examinar los diagramas
- Revisar los flujos de comunicación

---

## 🐛 Si Algo No Funciona

### "Error: No puedo conectar"
```
1. Verifica que el servidor esté corriendo en Terminal 1
2. Verifica que dice "Escuchando en puerto 5000"
3. Intenta hacer click en [Conectar] nuevamente
```

### "Error de compilación"
```
1. Verifica: dotnet --version → debe ser 10.0+
2. Ejecuta: dotnet restore
3. Intenta: dotnet build --verbosity detailed
```

### "Puerto 5000 en uso"
```
1. Detén el servidor con 'salir'
2. Cierra todas las instancias de cliente
3. Espera 10 segundos
4. Vuelve a intentar
```

---

## 📝 Expresiones para Probar

```
Simples:
2 + 3 = 5
10 - 4 = 6

Complejas:
(2 + 3) * 4 = 20
2 ** 3 = 8

Lógicas:
5 and 3 = 1
0 or 1 = 1
```

---

## 🎓 ¿Qué está Pasando?

```
Usuario escribe "2 + 3" en Cliente
         ↓
Cliente se conecta a Servidor (TCP, puerto 5000)
         ↓
Cliente envía "2 + 3" como UTF-8
         ↓
Servidor recibe y parsea
         ↓
Servidor evalúa: 2 + 3 = 5
         ↓
Servidor envía "5" de vuelta
         ↓
Cliente recibe y muestra "5"
         ↓
Usuario ve el resultado ✓
```

---

## 💡 Tips

- 💾 **Guardar**: Los datos de conexión se recuerdan
- 🔄 **Reconectar**: Puedes desconectar y reconectar sin reiniciar
- 📊 **Log**: Mira el log negro para ver toda la comunicación
- 🚀 **Rápido**: La evaluación es instantánea (<100ms)

---

## 📚 Documentación Completa

Para más detalles, consulta:
- [README.md](README.md) - Documentación completa
- [GUIA_RAPIDA.md](GUIA_RAPIDA.md) - Guía más detallada
- [ARQUITECTURA.md](ARQUITECTURA.md) - Cómo funciona internamente

---

## ✨ ¡Disfruta!

Ahora tienes una calculadora cliente-servidor funcional.

**Próximo paso recomendado**: Abre [GUIA_RAPIDA.md](GUIA_RAPIDA.md) para explorar más características.

---

*¿Preguntas? Consulta [COMANDOS.md](COMANDOS.md) para solucionar problemas.*
