using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyWaveSet waveSet;
    private int currentWave = 0;
    public bool allWavesSpawned = false;

    public GameObject winCanvas;
    public GameObject loseCanvas;

    private bool bossSpawned = false;

    void Start()
    {
        StartCoroutine(SpawnAllWaves());
    }

    IEnumerator SpawnAllWaves()
    {
        while (currentWave < waveSet.waves.Length)
        {
            var wave = waveSet.waves[currentWave];

            // Kiểm tra nếu là wave boss
            bool isBossWave = wave.enemyPrefab.name.Contains("Boss");
            if (isBossWave)
                bossSpawned = true;

            SpawnWave(wave);

            // Nếu không phải boss, đợi enemy tiêu diệt hết
            if (!isBossWave)
            {
                yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
                yield return new WaitForSeconds(wave.nextDelay);
            }
            else
            {
                // Nếu là boss, đợi đến khi boss bị tiêu diệt hoặc rời màn hình
                yield return new WaitUntil(() => GameObject.FindGameObjectWithTag("Boss") == null);

                yield return new WaitForSeconds(0.5f); // Delay nhỏ sau boss chết

                if (GameObject.FindGameObjectWithTag("Boss") == null)
                {
                    Debug.Log("Boss defeated! You win!");
                    if (winCanvas != null) winCanvas.SetActive(true);
                }
                else
                {
                    Debug.Log("Boss survived! Game Over!");
                    if (loseCanvas != null) loseCanvas.SetActive(true);
                }

                yield break; // Kết thúc coroutine tại boss wave
            }

            currentWave++;
        }

        allWavesSpawned = true;
    }

    private void SpawnWave(EnemyWaveData wave)
    {
        Transform flyPath = GameObject.Find(wave.flyPathName)?.transform;

        if (flyPath == null)
        {
            Debug.LogError("FlyPath not found: " + wave.flyPathName);
            return;
        }

        Vector3 spawnPos = flyPath.GetChild(0).position;

        for (int i = 0; i < wave.number; i++)
        {
            GameObject enemy = Instantiate(wave.enemyPrefab, spawnPos, Quaternion.identity);

            // Phân loại tag
            if (wave.enemyPrefab.name.Contains("Boss"))
                enemy.tag = "Boss";
            else
                enemy.tag = "Enemy";

            var agent = enemy.GetComponent<FlyPathAgent>();
            if (agent != null)
            {
                agent.flyPath = flyPath;
                agent.flySpeed = wave.speed;
            }

            spawnPos += wave.formationOffset;
        }
    }
}
