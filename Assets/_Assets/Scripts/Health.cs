using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour 
{ 
    public GameObject explosionPrefab;
    public int defaultHealthPoint;
    public int healthPoint;
    public System.Action onDead; // Khai báo biến delegate onDead công khai (delegate tương tự như con trỏ trong C/C++)
                                 // onDead là 1 kiểu action, được định nghĩa là delegate: khai báo định dạng hàm sẵn.
                                 // Như 1 danh sách các con trỏ trỏ đến các hàm, không có tham số hay kiểu dữ liệu trả về
    public System.Action onHealthChanged;

    private void Start()
    {
        healthPoint = defaultHealthPoint;
        onHealthChanged?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (healthPoint <= 0) // Nếu đối tượng đã chết, thì không làm gì cả và thoát khỏi hàm.
        {
            return;
        }
        healthPoint -= damage; // Trừ đi lượng máu bằng với sát thương nhận vào.
        onHealthChanged?.Invoke();
        if (healthPoint <= 0) // Sau khi trừ máu, kiểm tra xem máu có xuống dưới hoặc bằng 0 không
        {
            Die(); // Nếu có, gọi hàm Die() để xử lý cái chết.
        }
        Debug.Log($"{gameObject.name} took damage, HP now: {healthPoint}");
    }

    // public void OnTriggerEnter2D(Collider2D collision) => Die(); Gây lỗi logic ở đây

    protected virtual void Die()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation); // Tạo ra một bản sao của prefab hiệu ứng nổ.
        Destroy(explosion, 1); // Hủy đối tượng hiệu ứng nổ sau 1 giây, để nó có thời gian hiển thị.
        Destroy(gameObject); // Hủy chính đối tượng game object đang gắn script này
        onDead?.Invoke(); // Thông báo cho các đối tượng trong danh sách onDead rằng đối tượng này đã chết.
    }
}