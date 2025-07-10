using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyHealth health;
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null) // Kiểm tra xem đối tượng va chạm có phải là người chơi hay không.
        {
            playerHealth.TakeDamage(damage); // Nếu đúng là người chơi, hãy gọi hàm TakeDamage() của người chơi để trừ máu.
            health.TakeDamage(1000); // Sẽ chết nếu Enemy va chạm với Player
        }
    }
}
