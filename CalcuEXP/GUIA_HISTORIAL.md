# 📖 Guía de Uso - Sistema de Historial de Evaluaciones

## 🎯 ¿Qué se implementó?

Se creó un **sistema completo de historial de evaluaciones** que permite:
- ✅ Guardar automáticamente cada cálculo realizado en un archivo CSV
- ✅ Acceder al historial desde los clientes remotos
- ✅ Exportar el historial como archivo CSV descargable

## 🚀 Cómo funciona

### En el Servidor

1. Cuando se inicia el servidor, se crea automáticamente un archivo `evaluations.csv` en la carpeta de ejecución
2. Cada vez que un cliente envía una expresión para evaluar:
   - El servidor evalúa la expresión
   - Guarda el registro con: fecha, hora, expresión, resultado, IP del cliente

### En el Cliente

1. **Ver Historial**: Haz click en el botón "Historial"
2. Se abre una ventana emergente con todos los registros del servidor
3. **Exportar**: Click en "Exportar CSV" para guardar una copia local

## 📊 Estructura del CSV

El archivo `evaluations.csv` contiene:

```
Fecha,Hora,Expresión,Resultado,Cliente
2025-01-16,14:30:45,"2+2","4",127.0.0.1
2025-01-16,14:31:12,"10*5","50",127.0.0.1
2025-01-16,14:32:00,"100/2","50",127.0.0.1
```

| Campo | Descripción |
|-------|-------------|
| **Fecha** | Día en que se evaluó (YYYY-MM-DD) |
| **Hora** | Hora exacta (HH:MM:SS) |
| **Expresión** | La expresión matemática/lógica |
| **Resultado** | El resultado del cálculo |
| **Cliente** | Dirección IP del cliente que lo solicitó |

## 🔧 Archivos Modificados

### Nuevo Archivo: `EvaluationHistory.cs`
Clase encargada de:
- Crear y mantener el archivo CSV
- Guardar nuevas evaluaciones
- Leer el historial completo
- Proteger el acceso (thread-safe)

### Modificado: `Network.cs`
- Servidor ahora instancia `EvaluationHistory`
- Nuevo comando: `GET_HISTORY` para descargar el CSV
- Buffer aumentado a 4096 bytes para datos más grandes
- Cliente ahora tiene método `ObtenerHistorial()`

### Modificado: `Interfaz.cs`
- Nuevo botón "Historial" en el panel de entrada
- Nuevo método `VerHistorial()` que:
  - Obtiene el CSV del servidor
  - Muestra una ventana emergente
  - Permite exportar a archivo local

## 💡 Casos de Uso

### Caso 1: Ver todas las evaluaciones realizadas
```
1. Click en "Historial"
2. Se abre ventana con todos los registros
3. Ver fecha, hora, expresión y resultado de cada cálculo
```

### Caso 2: Exportar el historial para análisis
```
1. Click en "Historial"
2. Click en "Exportar CSV"
3. Seleccionar ubicación
4. Archivo CSV guardado localmente
```

### Caso 3: Revisar evaluaciones de clientes específicos
```
1. Obtener el archivo evaluations.csv del servidor
2. Abrir en Excel o LibreOffice
3. Filtrar por la columna "Cliente"
```

## 🔒 Seguridad y Confiabilidad

- **Thread-Safe**: Múltiples clientes pueden usar el sistema simultáneamente
- **Sincronización**: Acceso al archivo protegido con locks
- **Escalable**: Soporta archivos grandes con buffers aumentados
- **Persistencia**: Los datos se guardan en disco permanentemente

## 📁 Ubicación del Archivo

El archivo `evaluations.csv` se crea en:
- **Servidor**: En la carpeta desde donde se ejecuta el servidor
- **Cliente**: En la ubicación que elijas al exportar

## ⚙️ Configuración

El archivo CSV se crea automáticamente con el nombre: `evaluations.csv`

Para cambiar el nombre, modifica en `Network.cs`:
```csharp
history = new EvaluationHistory("mi_historial.csv");
```

## 🧪 Prueba Rápida

1. Inicia el servidor
2. Conecta un cliente
3. Evalúa: `2+2`
4. Click en "Historial"
5. Verás el registro con fecha, hora, "2+2", "4", y tu IP

## ❓ Preguntas Frecuentes

**P: ¿Dónde se guarda el CSV?**
R: En la carpeta donde se ejecuta el servidor (generalmente la raíz del proyecto)

**P: ¿Se puede limpiar el historial?**
R: Sí, manualmente borrando el archivo `evaluations.csv` (se recrea al siguiente registro)

**P: ¿Funciona con múltiples clientes?**
R: Sí, cada cliente puede ver todo el historial de todos los clientes

**P: ¿Qué pasa si hay errores en la evaluación?**
R: Se guarda: `ERROR:descripción del error` en el campo de resultado

## 📝 Ejemplo de Historial Completo

```
Fecha,Hora,Expresión,Resultado,Cliente
2025-01-16,14:30:45,"2+2","4",127.0.0.1
2025-01-16,14:31:12,"10*5","50",127.0.0.1
2025-01-16,14:32:00,"not 0","1",127.0.0.1
2025-01-16,14:32:30,"100/2","50",127.0.0.1
2025-01-16,14:33:15,"2**8","256",127.0.0.1
2025-01-16,14:34:00,"1 and 1","1",192.168.1.100
2025-01-16,14:34:45,"1 or 0","1",192.168.1.100
2025-01-16,14:35:20,"5 % 3","2",192.168.1.100
2025-01-16,14:36:00,"10/0","ERROR:División por cero",192.168.1.100
```

---

✅ Sistema implementado y compilado exitosamente
