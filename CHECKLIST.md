# ✅ Checklist de Verificación

## Compilación
- [ ] `cd CalcuEXP && dotnet build` - ✓ Build exitoso
- [ ] `cd CalcuEXP.Server && dotnet build` - ✓ Build exitoso
- [ ] No hay errores CS (C# Compiler)
- [ ] No hay warnings críticos

## Servidor
- [ ] Ejecutar: `dotnet run` en CalcuEXP.Server
- [ ] Ver mensaje "Escuchando en puerto 5000"
- [ ] Escribir "salir" detiene el servidor
- [ ] Acepta múltiples conexiones simultáneas

## Cliente
- [ ] Ejecutar: `dotnet run` en CalcuEXP
- [ ] Se abre ventana GUI correctamente
- [ ] Campo "Servidor" tiene valor "127.0.0.1"
- [ ] Campo "Puerto" tiene valor "5000"
- [ ] Botón "Conectar" está habilitado

## Conexión
- [ ] Click en "Conectar" con servidor corriendo
- [ ] Etiqueta de estado cambia a verde "✓ Conectado"
- [ ] Botón cambia a "Desconectar"
- [ ] Log muestra "[CONEXIÓN] Conectado a 127.0.0.1:5000"

## Evaluación de Expresiones
Probar las siguientes expresiones:

### Aritméticas Básicas
- [ ] `2 + 3` → `5`
- [ ] `10 - 4` → `6`
- [ ] `3 * 4` → `12`
- [ ] `20 / 4` → `5`
- [ ] `10 % 3` → `1`
- [ ] `2 ** 3` → `8`

### Con Paréntesis
- [ ] `(2 + 3) * 4` → `20`
- [ ] `2 + 3 * 4` → `14`
- [ ] `((10 + 5) * 2)` → `30`

### Operadores Lógicos
- [ ] `5 and 3` → `1`
- [ ] `0 or 1` → `1`
- [ ] `3 xor 1` → `1`
- [ ] `not 0` → `1`
- [ ] `not 5` → `0`

### Combinadas
- [ ] `(5 + 3) * 2 - 4` → `12`
- [ ] `10 / 2 + 3` → `8`
- [ ] `5 and 1 or 0` → `1`

### Errores Esperados
- [ ] `10 / 0` → Error: "División por cero"
- [ ] `10 %` → Error: "Token inesperado"
- [ ] `(5 + 3` → Error: "Paréntesis no balanceados"

## Log de Comunicación
- [ ] Mostrar timestamps correcto [HH:mm:ss]
- [ ] "[CONEXIÓN] Conectado a..."
- [ ] "[ENVÍO] expresión"
- [ ] "[RESPUESTA] resultado"
- [ ] "[ERROR] si hay error"
- [ ] Scroll automático hacia abajo

## Desconexión
- [ ] Click en "Desconectar"
- [ ] Etiqueta pasa a rojo "✗ Desconectado"
- [ ] Log muestra "[DESCONEXIÓN]"
- [ ] Botón vuelve a "Conectar"

## Interfaz de Usuario
- [ ] Todos los campos son legibles
- [ ] Los colores están bien diferenciados
- [ ] Botones responden al click
- [ ] No hay excepciones no manejadas
- [ ] Ventana es redimensionable

## Múltiples Clientes
- [ ] Abrir 2+ instancias del cliente
- [ ] Conectar todas al servidor
- [ ] Servidor acepta todas las conexiones
- [ ] Cada cliente funciona independientemente
- [ ] Log del servidor muestra múltiples "[CLIENTE]"

## Performance
- [ ] Respuesta rápida (<100ms)
- [ ] No freezes en UI
- [ ] Memoria estable
- [ ] Sin memory leaks visibles

## Limpiar Campos
- [ ] Click en "Limpiar"
- [ ] Campo expresión se vacía
- [ ] Resultado se vacía
- [ ] Estado se limpia
- [ ] Cursor en campo expresión

## Scripts Batch (Windows)
- [ ] `iniciar_servidor.bat` abre terminal servidor
- [ ] `iniciar_cliente.bat` abre interfaz cliente
- [ ] Ambos scripts ejecutan sin errores

## Documentación
- [ ] README.md presente y legible
- [ ] GUIA_RAPIDA.md presente
- [ ] IMPLEMENTACION.md presente
- [ ] Instrucciones claras

---

## Resultado Final: _____________________

### Total Verificaciones: 60+
### Aprobadas: _____ / 60+
### Porcentaje: _____%

**Notas:**
```
_________________________________________________________________
_________________________________________________________________
_________________________________________________________________
```

---

**Fecha de Verificación**: ________________  
**Responsable**: ________________  
**Estado**: ☐ Pendiente ☐ En Progreso ☐ Completado ✓
