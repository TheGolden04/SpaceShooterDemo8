using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script sinh ra các đợt kẻ địch theo thông tin cấu hình từ mảng EnemyWave
public class EnemySpawner : MonoBehaviour
{
    public EnemyWave[] enemyWaves; // Mảng các đợt kẻ địch (wave) được cấu hình sẵn trong Inspector
    private int currentWave;       // Đánh dấu wave hiện tại đang được sinh

    void Start()
    {
        SpawnEnemyWave(); // Bắt đầu sinh wave đầu tiên
    }
    // Hàm sinh wave hiện tại
    private void SpawnEnemyWave()
    {
        var waveInfo = enemyWaves[currentWave];         // Lấy thông tin wave hiện tại
        var startPosition = waveInfo.flyPath[0];        // Lấy vị trí xuất phát (điểm đầu tiên trong FlyPath)

        for (int i = 0; i < waveInfo.numberOfEnemy; i++) // Lặp theo số lượng enemy trong wave
        {
            var enemy = Instantiate(waveInfo.enemyPrefab, startPosition, Quaternion.identity); // Sinh enemy tại vị trí bắt đầu
            var agent = enemy.GetComponent<FlyPathAgent>(); // Lấy component FlyPathAgent từ enemy
            agent.flyPath = waveInfo.flyPath;               // Gán đường bay cho enemy
            agent.flySpeed = waveInfo.speed;                // Gán tốc độ bay
            startPosition += waveInfo.formationOffset;      // Cộng thêm offset để tạo đội hình (ví dụ: xếp hàng ngang)
        }
        currentWave++; // Tăng chỉ số wave hiện tại
        // Nếu còn wave kế tiếp, gọi lại hàm SpawnEnemyWave sau khoảng delay đã định
        if (currentWave < enemyWaves.Length)
        {
            Invoke(nameof(SpawnEnemyWave), waveInfo.nextWaveDelay);
        }
    }
}