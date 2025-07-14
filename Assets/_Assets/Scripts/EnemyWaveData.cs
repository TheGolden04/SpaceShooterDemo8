using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWaveData", menuName = "ScriptableObjects/EnemyWaveData", order = 1)]
public class EnemyWaveData : ScriptableObject
{
    public GameObject enemyPrefab;
    public int number = 5;
    public Vector3 formationOffset;
    public string flyPathName; // ← dùng tên GameObject thay vì Transform
    public float speed = 2f;
    public float nextDelay = 5f;
}
