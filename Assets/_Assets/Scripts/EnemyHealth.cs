using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public static int LivingEnemyCount;
    private void Awake()
    {
        LivingEnemyCount++; // Mỗi enemy mới tạo sẽ tăng
        Debug.Log("Living enemy count: " + LivingEnemyCount);
    }

    protected override void Die()
    {
        Debug.Log("Enemy died");
        LivingEnemyCount--;  // Khi enemy chết thì giảm
        base.Die();          // Gọi Die() của lớp cha (Health)
    }
}

