using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    protected override void Die()
    {
        base.Die(); // gọi đến hàm Die() gốc của lớp cha (lớp Health).
        Debug.Log("Player died");
    }
}
