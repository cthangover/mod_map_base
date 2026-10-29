# World Map Engine — travel between places on a drawn map

World Map Engine shows a full-screen picture of the game world with places marked on it. The player opens the map, picks a place and travels there: the game switches to that place's scene. Scenarios can open, close, hide and show places as the story goes on, and the game saves those changes.

It is used by Cthangover. It is a different thing from the tile maps of RPG World: a world map is one picture with clickable areas, and travelling changes the scene; a tile map is a grid the hero walks on cell by cell.

## The map in the game

Open it with the map icon on the toolbar.

![The world map of Cthangover: a valley with two towns between mountains and rivers; the labels "Villa" and "Town entrance" mark the two places, hatched areas show where they are; a legend at the bottom left (you are here, available, closed) and a hint at the bottom.](assets/world-map.jpg)

- **Places** are areas drawn over the picture; hatching shows them. Some places also have a round emblem with an icon.
- **The legend**: green — where you are now, gold — available, grey — closed.
- **Click a place** to see its card: state, description and the **Travel** button. **Esc** or **×** closes the map.

![The Town entrance selected: its area outlined in gold, and the card on the right — Available, "The northern entrance to the town", Transition, Travel.](assets/zone-selected.jpg)

**Travel** switches the game to the place's scene:

![After Travel: the town entrance scene with its opening line and two answers.](assets/after-travel.jpg)

A place is closed — the card says "The path is not open yet." and offers no Travel — when a scenario has closed it, when it has no scene, or when its scene does not exist in the game being played.

## Adding a world map to a game

A game needs two things in its mods:

1. **The picture** — a background named `map` (for example `backgrounds/map.png`).
2. **The places** — JSON files in a `map/` folder. All positions are fractions of the picture's width and height, from 0 to 1, so the map works at any size.

This is the town entrance of Cthangover, shortened:

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

- `points` — the outline of the area that can be clicked.
- `scene` — the logical scene Travel switches to.
- `title`, `description` — translation keys; the title is the place's name on its card and emblem.
- `image` and `image_pos` — an icon and its position, to show a round emblem on the place.
- `label_pos` — a free-standing name on the map. A zone with only a title and a label position (like `town_entry_label`) is a name, not a place: it cannot be clicked.
- `locked` — closed at the start; `hidden` — not shown at the start.

Cthangover keeps the names of its places in separate label zones and leaves the places' own `title` empty, which is why the card above has no name at the top.

The world map can also be drawn and edited in the map editor of Editor Tools.

## Changing places from a scenario

```scenario
action map.open_zone zone_id=town_entry
action map.close_zone zone_id=town_entry
action map.show_zone zone_id=town_entry
action map.hide_zone zone_id=town_entry
```

The change is kept in the save and stays after loading; a new game starts from the values in the JSON.

## If something goes wrong

1. **"Map unavailable".** No mod of the game has a background named `map`.
2. **A place cannot be entered.** Its `scene` is empty or misspelled, or a scenario closed it.
3. **A place is in the wrong spot.** Positions are fractions of the picture (0–1), not pixels.
