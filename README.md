# 🥤 Venta de Bebidas — Juego 2D (Unity)

Juego 2D hecho en Unity donde el jugador vende y compra bebidas en un
minimarket. Incluye dos roles jugables — **Cajero** y **Comprador** — y un
mínimo de 10 niveles divididos en **Fácil**, **Medio** y **Difícil**.

Ver el diseño completo de los 10 niveles en [`Docs/DisenoDeNiveles.md`](Docs/DisenoDeNiveles.md).

## Requisitos
- Unity 2022 LTS o superior (2D URP recomendado, pero funciona con Built-in).
- Git / cuenta de GitHub para el repositorio.

## Cómo abrir el proyecto
1. Cloná este repo o descomprimí el zip.
2. Abrí Unity Hub → `Add project from disk` → seleccioná la carpeta
   `VentaDeBebidas`.
3. Abrí la escena `Assets/Scenes/MainMenu.unity` (crearla como se indica
   abajo) y presioná Play.

> Nota: este paquete trae todo el **código (C#) y los datos de los 10
> niveles** ya armados. Las escenas (.unity), sprites y prefabs visuales hay
> que crearlos dentro del Editor de Unity siguiendo la guía de abajo, porque
> esos archivos son binarios propios del Editor y no se pueden generar fuera
> de él.

## Estructura del proyecto
```
Assets/
  Scripts/
    Managers/
      GameManager.cs        -> persiste estado global, dinero, nivel actual
      LevelManager.cs        -> carga LevelData, controla objetivos y estrellas
      AudioManager.cs        -> sonidos de compra/venta/error
    Gameplay/
      Product.cs              -> ScriptableObject: una bebida (nombre, precio, sprite)
      Customer.cs              -> NPC cliente: pedido, paciencia, pago
      CashierController.cs    -> lógica del rol Cajero (cobrar, dar vuelto)
      BuyerController.cs      -> lógica del rol Comprador (elegir productos, pagar)
      ShopSystem.cs            -> inventario/góndola de productos disponibles
      ScoreSystem.cs           -> puntaje, errores, cálculo de estrellas
    Data/
      LevelData.cs             -> ScriptableObject: define cada nivel
      LevelDatabase.cs         -> lista de los 10 niveles + carga por índice
    UI/
      MainMenuUI.cs            -> menú de inicio
      LevelSelectUI.cs         -> selección de nivel (mapa/lista con estrellas)
      GameplayHUD.cs           -> HUD de dinero, tiempo, pedido actual
      ResultsUI.cs             -> pantalla de resultados/estrellas al final
  Resources/
    Levels/                    -> (vacío) acá van los 10 assets LevelData
Docs/
  DisenoDeNiveles.md           -> diseño detallado de los 10 niveles
```

## Cómo terminar de armar el proyecto en el Editor

### 1. Generar los 10 niveles automáticamente (recomendado)
Ya incluí `Assets/Editor/LevelDataGenerator.cs`, que crea solo los **datos**:
10 bebidas de ejemplo y los 10 niveles (`Level_01` a `Level_10`) ya
completos según `Docs/DisenoDeNiveles.md` (dinero, catálogo, clientes o
lista de compra, tiempo límite, etc).

En el menú de Unity: **VentaDeBebidas → Generar Productos y Niveles de
Ejemplo**. Después podés editar cualquier valor o asignar sprites a cada
`Product` desde el Inspector.

(Alternativa manual: click derecho en `Assets/Resources/Levels` →
`Create > VentaDeBebidas > Level Data` y completar campo por campo con la
tabla de `Docs/DisenoDeNiveles.md`.)

### 2. Crear las escenas
- `MainMenu` — un Canvas con el script `MainMenuUI` (botones: Jugar,
  Selección de nivel, Salir).
- `LevelSelect` — un Canvas con `LevelSelectUI`, que instancia un botón por
  cada nivel de `LevelDatabase` (bloqueado/desbloqueado + estrellas).
- `Gameplay` — una escena única y reutilizable: al cargarla, `LevelManager`
  lee qué nivel fue seleccionado y activa el flujo de Cajero o Comprador
  correspondiente, e instancia el HUD.
- `Results` — puede ser un Canvas superpuesto en la misma escena de
  Gameplay en vez de escena aparte (más simple).

Agregá las 3-4 escenas a `File > Build Settings` en ese orden.

### 3. Sprites y prefabs
- Prefab `Customer` con `SpriteRenderer` + `Customer.cs`.
- Prefab `ProductSlot` (botón de góndola) con `Image` + `Button` +
  referencia a un `Product`.
- Sprites: podés usar arte propio o placeholders (cuadrados de color) para
  probar la lógica primero.

### 4. Conectar todo
Cada script deja comentarios `// TODO: asignar en el Inspector` en los
campos que se completan arrastrando objetos desde la escena — no hace falta
tocar código para eso.

## Subir a GitHub
```bash
cd VentaDeBebidas
git init
git add .
git commit -m "Estructura inicial: sistema de niveles, cajero y comprador"
git branch -M main
git remote add origin https://github.com/TU_USUARIO/venta-de-bebidas.git
git push -u origin main
```

El `.gitignore` incluido ya excluye las carpetas pesadas/generadas de Unity
(`Library/`, `Temp/`, `Obj/`, builds, etc.) para no subir basura al repo.
