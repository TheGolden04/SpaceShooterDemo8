using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : Health
{
    protected override void Die()
    {
        base.Die();
        Debug.Log("Boss died");

        // Gọi sự kiện win tại BattleFlow (hoặc qua delegate)
        FindObjectOfType<BattleFlow>().OnGameWin(); // ← Gọi trực tiếp
    }
}
