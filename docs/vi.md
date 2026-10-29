# World Map Engine — đi lại giữa các địa điểm trên một bản đồ vẽ sẵn

World Map Engine hiển thị một bức tranh toàn màn hình về thế giới của trò chơi, với các địa điểm được đánh dấu trên đó. Người chơi mở bản đồ, chọn một địa điểm và lên đường tới đó: trò chơi chuyển sang cảnh của địa điểm ấy. Kịch bản có thể mở, đóng, ẩn và hiện lại các địa điểm theo tiến triển câu chuyện, và trò chơi lưu lại những thay đổi đó.

Cthangover dùng nó. Nó khác với các bản đồ ô vuông (tile) của RPG World: bản đồ thế giới là một bức tranh với những vùng bấm được, và đi lại là đổi cảnh; bản đồ ô vuông là một lưới mà nhân vật đi từng ô một.

## Bản đồ trong trò chơi

Mở bằng biểu tượng bản đồ trên thanh công cụ.

![Bản đồ thế giới của Cthangover: một thung lũng có hai thị trấn giữa núi và sông; các nhãn "Villa" và "Town entrance" đánh dấu hai địa điểm, vùng gạch chéo cho biết vị trí của chúng; chú giải ở góc dưới bên trái (bạn ở đây, có thể đến, đã đóng) và một gợi ý ở dưới.](assets/world-map.jpg)

- **Địa điểm** là những vùng vẽ đè lên tranh; nét gạch chéo cho thấy chúng. Một số địa điểm còn có huy hiệu tròn kèm biểu tượng.
- **Chú giải**: xanh lục — nơi bạn đang ở, vàng kim — có thể đến, xám — đã đóng.
- **Bấm vào một địa điểm** để xem thẻ của nó: trạng thái, mô tả và nút **Di chuyển**. **Esc** hay **×** đều đóng bản đồ.

![Town entrance đang được chọn: vùng của nó được viền vàng kim, bên phải là thẻ — Available, "The northern entrance to the town", Transition, Travel.](assets/zone-selected.jpg)

**Di chuyển** chuyển trò chơi sang cảnh của địa điểm đó:

![Sau khi di chuyển: cảnh cổng thị trấn với dòng mở đầu và hai lựa chọn.](assets/after-travel.jpg)

Một địa điểm bị đóng — thẻ ghi «Con đường chưa mở.» và không đưa ra Di chuyển — khi một kịch bản đã đóng nó, khi nó không có cảnh, hoặc khi cảnh của nó không tồn tại trong trò chơi đang chơi.

## Thêm bản đồ thế giới vào một trò chơi

Một trò chơi cần hai thứ trong các mod của nó:

1. **Bức tranh** — một phông nền tên là `map` (ví dụ `backgrounds/map.png`).
2. **Các địa điểm** — các tệp JSON trong thư mục `map/`. Mọi vị trí đều là tỉ lệ so với chiều rộng và chiều cao của tranh, từ 0 đến 1, nên bản đồ chạy ở mọi kích thước.

Đây là cổng thị trấn của Cthangover, bản rút gọn:

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

- `points` — đường viền của vùng có thể bấm.
- `scene` — cảnh logic mà Di chuyển chuyển sang.
- `title`, `description` — các khóa dịch; title là tên của địa điểm trên thẻ và huy hiệu của nó.
- `image` và `image_pos` — một biểu tượng và vị trí của nó, để hiện huy hiệu tròn trên địa điểm.
- `label_pos` — một cái tên đứng riêng trên bản đồ. Vùng chỉ có title và vị trí nhãn (như `town_entry_label`) là một cái tên chứ không phải địa điểm: không thể bấm.
- `locked` — đã đóng lúc đầu; `hidden` — không hiển thị lúc đầu.

Cthangover giữ tên các địa điểm trong những vùng nhãn riêng và để trống `title` của chính các địa điểm — vì thế thẻ ở trên không có tên ở đỉnh.

Bản đồ thế giới cũng có thể vẽ và chỉnh trong trình soạn thảo bản đồ của Editor Tools.

## Thay đổi địa điểm từ kịch bản

```scenario
action map.open_zone zone_id=town_entry
action map.close_zone zone_id=town_entry
action map.show_zone zone_id=town_entry
action map.hide_zone zone_id=town_entry
```

Thay đổi được ghi vào save và còn đó sau khi tải lại; trò chơi mới bắt đầu với các giá trị trong JSON.

## Khi có gì đó không ổn

1. **"Bản đồ không khả dụng".** Không mod nào của trò chơi có phông nền tên `map`.
2. **Không vào được một địa điểm.** `scene` của nó rỗng hoặc gõ sai, hoặc một kịch bản đã đóng nó.
3. **Địa điểm nằm sai chỗ.** Vị trí là tỉ lệ của bức tranh (0–1), không phải điểm ảnh.
