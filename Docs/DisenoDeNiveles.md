# Venta de Bebidas — Diseño de Niveles

Juego 2D en Unity sobre una venta/minimarket de bebidas. El jugador puede jugar
como **Cajero** (atiende clientes, cobra y da vuelto) o como **Comprador**
(entra a la tienda con un presupuesto y debe comprar lo que le piden gastando
lo menos posible o sin pasarse del dinero).

Todos los niveles se definen como datos (`LevelData` ScriptableObject), así que
agregar/editar niveles no requiere tocar código, solo crear un asset nuevo
desde el menú `Assets > Create > VentaDeBebidas > Level Data`.

## Estructura general
- 10 niveles mínimo, agrupados en 3 dificultades.
- Cada nivel define: Rol (Cajero/Comprador), dinero inicial/caja, catálogo de
  bebidas disponibles, lista de pedidos, tiempo límite (si aplica) y objetivo
  de puntaje para 1/2/3 estrellas.

## Nivel 1 — "Primeras Ventas" (Fácil · Cajero)
- Caja inicial: 20.000 Gs.
- 3 clientes, piden 1 bebida cada uno, pago siempre exacto o con billete
  "redondo" (ej. paga 5.000 por algo de 3.000 → vuelto fácil de 2.000).
- Sin límite de tiempo. Objetivo: cobrar correctamente los 3 pedidos.

## Nivel 2 — "Primeras Compras" (Fácil · Comprador)
- Presupuesto: 10.000 Gs.
- Lista de compra: 2 bebidas puntuales, precios visibles en las góndolas.
- Sin límite de tiempo. Objetivo: comprar exactamente lo pedido sin pasarse
  del presupuesto.

## Nivel 3 — "Variedad en el Mostrador" (Fácil · Cajero)
- Caja inicial: 20.000 Gs.
- 5 clientes, catálogo sube a 6 bebidas distintas, pagos con vuelto simple
  (múltiplos de 1.000).
- Objetivo: 5/5 cobros correctos.

## Nivel 4 — "El Vuelto Justo" (Medio · Comprador)
- Presupuesto: 15.000 Gs.
- Lista de 3 bebidas; el jugador debe calcular cuánto le sobra y decidir si
  puede agregar un ítem extra sin excederse.
- Objetivo: gastar el máximo posible sin pasarse del presupuesto.

## Nivel 5 — "Cajero con Vuelto" (Medio · Cajero)
- Caja inicial: 15.000 Gs.
- 6 clientes, pagos con billetes que generan vuelto no redondo
  (ej. pagar 10.000 por algo de 6.500).
- Penalización por vuelto incorrecto. Objetivo: vuelto correcto en 5/6.

## Nivel 6 — "Lista de Compras" (Medio · Comprador)
- Presupuesto ajustado: 12.000 Gs. para 4 ítems pedidos por un "cliente NPC".
- Algunas bebidas tienen 2 tamaños (precio distinto); hay que elegir bien.
- Objetivo: completar la lista completa sin excederse.

## Nivel 7 — "Hora Pico" (Medio · Cajero)
- Caja inicial: 15.000 Gs.
- 8 clientes con **tiempo límite por cliente** (ej. 20s), catálogo de 8
  bebidas. Introduce cola de clientes (más de uno esperando).
- Objetivo: atender 7/8 dentro del tiempo con cobro correcto.

## Nivel 8 — "Turno Difícil" (Difícil · Cajero)
- Caja inicial: 15.000 Gs.
- 10 clientes, tiempo límite global (ej. 3 min), pedidos de 2-3 bebidas por
  cliente, vueltos no redondos.
- Penalización fuerte por error de vuelto. Objetivo: 8/10 correctos dentro
  del tiempo.

## Nivel 9 — "Presupuesto Ajustado + Combos" (Difícil · Comprador)
- Presupuesto: 18.000 Gs.
- Lista de 5 bebidas + promos tipo "2x1" o combos que conviene detectar para
  ahorrar. Tiempo límite (ej. 90s) para decidir.
- Objetivo: comprar la lista completa gastando lo menos posible.

## Nivel 10 — "Jornada Completa" (Difícil · Cajero, nivel final)
- Caja inicial: 20.000 Gs.
- 12 clientes mixtos: algunos con prisa (tiempo corto), pedidos grandes,
  catálogo completo (10+ bebidas), posibilidad de que un cliente pague con
  billete falso o de más (detectar error). Combina todos los mecanismos
  anteriores.
- Objetivo: terminar el turno con la caja cuadrada (± margen chico) y 10/12
  clientes bien atendidos.

## Sistema de estrellas (sugerido, aplica a todos los niveles)
- ★ Completar el nivel.
- ★★ Completar sin ningún error de cobro/vuelto o sin pasarse del presupuesto.
- ★★★ Completar dentro del tiempo objetivo y sin errores.
