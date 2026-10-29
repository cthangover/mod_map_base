# World Map Engine——在画好的地图上往来于各地之间

World Map Engine 显示一幅全屏的游戏世界图景，上面标着各个地点。玩家打开地图，选中一个地点，然后前往那里：游戏切换到该地点的场景。剧本可以随剧情推进打开、关闭、隐藏或重新显示地点，游戏会保存这些变化。

Cthangover 用的就是它。它不同于 RPG World 的图块地图：世界地图是一张带可点击区域的图，前往即切换场景；图块地图则是一张格子，英雄一格一格地走。

## 游戏中的地图

点击工具栏上的地图图标即可打开。

![Cthangover 的世界地图：群山与河流之间的一条山谷里有两座城镇；标签 "Villa" 和 "Town entrance" 标出两个地点，阴影斜线显示它们的位置；左下角是图例（你在这里、可前往、已关闭），底部有一行提示。](assets/world-map.jpg)

- **地点**是画在图片之上的区域；斜线阴影标出它们。有些地点还有带图标的圆形徽章。
- **图例**：绿色——你在这里，金色——可前往，灰色——已关闭。
- **点击一个地点**可以打开它的卡片：状态、描述和 **前往** 按钮。**Esc** 或 **×** 关闭地图。

![选中了 Town entrance：它的区域以金色描边，右侧是卡片——Available、"The northern entrance to the town"、Transition、Travel。](assets/zone-selected.jpg)

**前往**会把游戏切换到该地点的场景：

![前往之后：进入城镇入口的场景，有开场一行和两个选项。](assets/after-travel.jpg)

一个地点处于关闭状态——卡片显示“道路尚未开放。”且没有“前往”按钮——的情况有：剧本把它关了、它没有场景，或者它引用的场景在当前玩的游戏里不存在。

## 给游戏添加世界地图

游戏需要在它的 mod 里有两样东西：

1. **图片**——一个名为 `map` 的背景（例如 `backgrounds/map.png`）。
2. **地点**——`map/` 文件夹里的 JSON 文件。所有位置都是图片宽高的比例，从 0 到 1，因此地图在任何尺寸下都能用。

这是 Cthangover 的城镇入口（精简版）：

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

- `points`——可点击区域的轮廓。
- `scene`——“前往”要切换到的逻辑场景。
- `title`、`description`——翻译键；title 是地点在卡片和徽章上显示的名字。
- `image` 和 `image_pos`——图标及其位置，用来在地点上显示圆形徽章。
- `label_pos`——直接摆在地图上的名字。只有 title 和标签位置（如 `town_entry_label`）的区域是一个名字，不是地点：点不了。
- `locked`——开局时关闭；`hidden`——开局时不显示。

Cthangover 把地点名字放在单独的标签区域里，地点自己的 `title` 留空——所以上面这张卡片顶部没有名字。

世界地图也可以用 Editor Tools 的地图编辑器来画和改。

## 用剧本改变地点

```scenario
action map.open_zone zone_id=town_entry
action map.close_zone zone_id=town_entry
action map.show_zone zone_id=town_entry
action map.hide_zone zone_id=town_entry
```

改动会存进存档并在读档后保留；新游戏则从 JSON 里的值开始。

## 出问题了？

1. **"地图不可用"。** 游戏的任何 mod 都没有名为 `map` 的背景。
2. **进不去一个地点。** 它的 `scene` 为空或拼写有误，或者剧本把它关了。
3. **地点位置不对。** 位置是图片的比例（0–1），不是像素。
