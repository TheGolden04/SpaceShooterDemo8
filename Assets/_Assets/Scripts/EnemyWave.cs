using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public Transform enemyPrefab;         // Prefab của kẻ địch sẽ được sinh ra (có thể là prefab enemy)
    public int numberOfEnemy;             // Số lượng kẻ địch trong đợt này
    public Vector3 formationOffset;       // Độ lệch vị trí giữa các kẻ địch trong đội hình (xếp theo hàng/dọc/ngang)
    public FlyPath flyPath;               // Đường bay mà các kẻ địch sẽ đi theo (FlyPath là MonoBehaviour gắn vào GameObject)
    public float speed;                   // Tốc độ bay của các kẻ địch trong wave này
    public float nextWaveDelay;           // Thời gian chờ (delay) trước khi bắt đầu wave kế tiếp
}
