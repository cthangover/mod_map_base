# World Map Engine — 그려진 맵 위에서 장소 사이를 이동

World Map Engine은 게임 세계를 담은 전체 화면 그림에 장소를 표시해 보여 줍니다. 플레이어는 지도를 열고 장소를 고른 뒤 그곳으로 떠납니다: 게임은 그 장소의 장면으로 바뀝니다. 시나리오는 이야기가 진행됨에 따라 장소를 열고, 닫고, 숨기고, 다시 드러낼 수 있고, 게임은 그 변화를 저장합니다.

Cthangover가 쓰는 것입니다. RPG World의 타일 맵과는 다른 것입니다: 월드 맵은 클릭할 수 있는 영역을 가진 한 장의 그림이고, 이동이 장면을 바꿉니다. 타일 맵은 영웅이 한 칸씩 걷는 격자입니다.

## 게임 속의 지도

도구 모음의 지도 아이콘으로 엽니다.

![Cthangover의 월드 맵: 산과 강 사이, 두 마을이 있는 계곡. 라벨 "Villa"와 "Town entrance"가 두 장소를 표시하고, 빗금 칠한 영역이 그 위치를 보여 줍니다. 왼쪽 아래에 범례(현재 위치, 갈 수 있음, 닫힘), 아래에 힌트.](assets/world-map.jpg)

- **장소**는 그림 위에 그려진 영역이고, 빗금으로 표시됩니다. 일부 장소에는 아이콘을 넣은 둥근 엠블럼도 있습니다.
- **범례**: 초록 — 현재 위치, 금색 — 갈 수 있음, 회색 — 닫힘.
- **장소를 클릭**하면 카드가 열립니다: 상태, 설명, **이동** 버튼. **Esc**나 **×**가 지도를 닫습니다.

![Town entrance 선택: 그 영역이 금색 테두리로 둘러졌고 오른쪽에 카드 — Available, "The northern entrance to the town", Transition, Travel.](assets/zone-selected.jpg)

**이동**은 게임을 그 장소의 장면으로 바꿉니다:

![이동 후: 마을 입구 장면이 첫 줄과 두 개의 답과 함께 열립니다.](assets/after-travel.jpg)

장소가 닫혀 있으면 — 카드에는 «아직 길이 열리지 않았습니다.»가 나오고 이동 버튼은 없습니다 — 시나리오가 닫았거나, 장면이 없거나, 플레이 중인 게임에 그 장면이 존재하지 않는 경우입니다.

## 게임에 월드 맵 추가하기

게임은 자기 mod 안에 두 가지를 필요로 합니다:

1. **그림** — `map`이라는 이름의 배경(예: `backgrounds/map.png`).
2. **장소** — `map/` 폴더의 JSON 파일. 모든 위치는 그림 너비와 높이에 대한 비율(0부터 1)이라서, 지도는 어떤 크기에서도 동작합니다.

다음은 Cthangover의 마을 입구입니다(요약판):

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

- `points` — 클릭할 수 있는 영역의 윤곽.
- `scene` — 이동이 전환할 논리 장면.
- `title`, `description` — 번역 키. title은 장소의 이름으로 카드와 엠블럼에 나옵니다.
- `image`와 `image_pos` — 아이콘과 그 위치로, 장소에 둥근 엠블럼을 보여 줍니다.
- `label_pos` — 지도에 홀로 놓이는 이름. title과 라벨 위치만 가진 구역(`town_entry_label` 같은)은 이름이지 장소가 아니므로 클릭할 수 없습니다.
- `locked` — 시작 때 닫혀 있음. `hidden` — 시작 때 표시되지 않음.

Cthangover는 장소 이름을 별도의 라벨 구역에 두고 장소 자체의 `title`은 비워 둡니다. 그래서 위의 카드에는 상단에 이름이 없습니다.

월드 맵은 Editor Tools의 맵 편집기로 그리거나 고칠 수도 있습니다.

## 시나리오에서 장소 바꾸기

```scenario
action map.open_zone zone_id=town_entry
action map.close_zone zone_id=town_entry
action map.show_zone zone_id=town_entry
action map.hide_zone zone_id=town_entry
```

변경은 저장 파일에 기록되어 불러온 뒤에도 남습니다. 새 게임은 JSON의 값에서 시작합니다.

## 문제가 생겼을 때

1. **«지도를 쓸 수 없음».** 게임의 어느 mod에도 `map`이라는 이름의 배경이 없습니다.
2. **장소에 들어갈 수 없습니다.** 그 `scene`이 비었거나 철자가 틀렸거나, 시나리오가 닫았습니다.
3. **장소가 엉뚱한 곳에 있습니다.** 위치는 그림에 대한 비율(0–1)이지 픽셀이 아닙니다.
