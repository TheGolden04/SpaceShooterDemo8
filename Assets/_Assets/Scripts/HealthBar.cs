using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public RectTransform mask;
    public Health health;

    private float originalWidth;

    // Start is called before the first frame update
    void Start()
    {
        originalWidth = mask.sizeDelta.x;

        // Đăng ký lắng nghe sự kiện khi máu của đối tượng thay đổi
        if (health != null)
        {
            health.onHealthChanged += UpdateHealthValue;
        }
        // Cập nhật giá trị máu lần đầu
        // Lệnh này vẫn cần thiết phòng trường hợp script HealthBar chạy trước script Health
        UpdateHealthValue();
    }
    // Hàm này sẽ được gọi khi đối tượng chứa HealthBar bị phá hủy
    private void OnDestroy()
    {
        // Rất QUAN TRỌNG: Hủy đăng ký sự kiện để tránh lỗi và rò rỉ bộ nhớ
        if (health != null)
        {
            health.onHealthChanged -= UpdateHealthValue;
        }
    }
    private void UpdateHealthValue()
    {
        // Thêm kiểm tra health và mask để tránh lỗi khi đối tượng health đã bị hủy
        if (health == null || mask == null) return;

        float scale = (float)health.healthPoint / health.defaultHealthPoint;
        mask.sizeDelta = new Vector2(scale * originalWidth, mask.sizeDelta.y);
    }
}
