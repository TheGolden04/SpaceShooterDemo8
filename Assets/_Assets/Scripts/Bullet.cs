using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float flySpeed;
    public int damage;

    void Update()
    {
        var newPosition = transform.position;
        newPosition.y += Time.deltaTime * flySpeed;
        transform.position = newPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<EnemyHealth>(); // Thử lấy component "EnemyHealth" từ đối tượng đã va chạm.
        if (enemy != null) // Nếu "enemy" không bằng "null", nghĩa là chúng ta đã va chạm với một kẻ địch.
        {
            // Nếu đúng là kẻ địch, gọi hàm TakeDamage() của nó và truyền vào lượng sát thương của viên đạn.
            enemy.TakeDamage(damage);
        }
        // Sau khi va chạm (dù trúng địch hay không), tự hủy đối tượng viên đạn.
        Destroy(gameObject);
    }
}