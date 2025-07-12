using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public BossHealth health;
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null) // Kiểm tra xem đối tượng va chạm có phải là người chơi hay không.
        {
            playerHealth.TakeDamage(damage); // Nếu đúng là người chơi, hãy gọi hàm TakeDamage() của người chơi để trừ máu.
        }
    }
}
