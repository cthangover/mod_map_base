# World Map Engine — viajar entre lugares sobre un mapa dibujado

World Map Engine muestra una imagen a pantalla completa del mundo del juego con lugares marcados en ella. El jugador abre el mapa, elige un lugar y viaja hasta allí: el juego cambia a la escena de ese lugar. Los escenarios pueden abrir, cerrar, ocultar y volver a mostrar lugares a medida que avanza la historia, y el juego guarda esos cambios.

Lo usa Cthangover. No es lo mismo que los mapas de tiles de RPG World: un mapa del mundo es una sola imagen con zonas en las que hacer clic, y viajar cambia la escena; un mapa de tiles es una cuadrícula por la que el héroe camina casilla a casilla.

## El mapa en el juego

Se abre con el icono del mapa en la barra de herramientas.

![El mapa del mundo de Cthangover: un valle con dos pueblos entre montañas y ríos; las etiquetas "Villa" y "Town entrance" marcan los dos lugares, las zonas rayadas muestran dónde están; una leyenda abajo a la izquierda (estás aquí, disponible, cerrado) y una pista abajo.](assets/world-map.jpg)

- **Lugares**: zonas dibujadas sobre la imagen; el rayado las delata. Algunos lugares tienen además un emblema redondo con un icono.
- **La leyenda**: verde — donde estás ahora, dorado — disponible, gris — cerrado.
- **Haz clic en un lugar** para ver su ficha: estado, descripción y el botón **Viajar**. **Esc** o **×** cierran el mapa.

![La entrada del pueblo (Town entrance) seleccionada: su zona contorneada en dorado, y la ficha a la derecha — Available, "The northern entrance to the town", Transition, Travel.](assets/zone-selected.jpg)

**Viajar** cambia el juego a la escena del lugar:

![Tras viajar: la escena de la entrada del pueblo con su primera línea y dos respuestas.](assets/after-travel.jpg)

Un lugar está cerrado —la ficha dice «El camino todavía no está abierto.» y no ofrece Viajar— cuando un escenario lo cerró, cuando no tiene escena o cuando su escena no existe en el juego al que se juega.

## Añadir un mapa del mundo a un juego

Un juego necesita dos cosas en sus mods:

1. **La imagen** — un fondo llamado `map` (por ejemplo `backgrounds/map.png`).
2. **Los lugares** — archivos JSON en una carpeta `map/`. Todas las posiciones son fracciones del ancho y el alto de la imagen, de 0 a 1, así el mapa funciona a cualquier tamaño.

Esta es la entrada del pueblo de Cthangover, abreviada:

```json
{
  "Items": [
    {
      "ID": "town_entry",
      "points": [[0.7099, 0.2493], [0.7334, 0.2630], [0.7348, 0.2906], [0.7091, 0.3429], [0.6974, 0.3264]],
      "title": "",
      "scene": "town_entry",
      "description": "mapzone/town_entry/info",
      "locked": false,
      "hidden": false
    },
    {
      "ID": "town_entry_label",
      "points": [],
      "title": "mapzone/town_entry",
      "label_pos": [0.7246, 0.2163]
    }
  ]
}
```

- `points` — el contorno de la zona en la que se puede hacer clic.
- `scene` — la escena lógica a la que cambia Viajar.
- `title`, `description` — claves de traducción; el título es el nombre del lugar en su ficha y emblema.
- `image` e `image_pos` — un icono y su posición, para mostrar un emblema redondo en el lugar.
- `label_pos` — un nombre suelto sobre el mapa. Una zona con solo un título y una posición de etiqueta (como `town_entry_label`) es un nombre, no un lugar: no se puede pulsar.
- `locked` — cerrado al empezar; `hidden` — no se muestra al empezar.

Cthangover guarda los nombres de sus lugares en zonas de etiqueta aparte y deja vacío el `title` de los lugares; por eso la ficha de arriba no lleva nombre arriba.

El mapa del mundo también se puede dibujar y editar en el editor de mapas de Editor Tools.

## Cambiar los lugares desde un escenario

```scenario
action map.open_zone zone_id=town_entry
action map.close_zone zone_id=town_entry
action map.show_zone zone_id=town_entry
action map.hide_zone zone_id=town_entry
```

El cambio se guarda en la partida y sigue ahí tras cargar; una partida nueva arranca con los valores del JSON.

## Si algo falla

1. **«Mapa no disponible».** Ningún mod del juego tiene un fondo llamado `map`.
2. **No se puede entrar en un lugar.** Su `scene` está vacía o mal escrita, o un escenario lo cerró.
3. **Un lugar está fuera de sitio.** Las posiciones son fracciones de la imagen (0–1), no píxeles.
