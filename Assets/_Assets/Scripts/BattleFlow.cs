using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleFlow : MonoBehaviour
{
    public GameObject gameOverUI; 
    public GameObject gameWinUI;
    public PlayerHealth playerHealth;
    public GameObject bgMusic;

    private void Start()
    {
        gameWinUI.SetActive(false);
        gameOverUI.SetActive(false);
        playerHealth.onDead += OnGameOver; // Lắng nghe sự kiện khi người chơi chết.
                                           // Thêm vào danh sách hàm onDead
    }
    private void Update()
    {
        if (EnemyHealth.LivingEnemyCount <= 0)
        {
            OnGameWin();
        }
    }
    private void OnGameOver()
    {
        gameOverUI.SetActive(true);
        bgMusic.SetActive(false);

        this.enabled = false;
    }
    private void OnGameWin()
    {
        gameWinUI.SetActive(true);
        bgMusic.SetActive(false);
        if (playerHealth != null)
        {
            playerHealth.gameObject.SetActive(false);
        }
        this.enabled = false;
    }
    public void ReturnToMainMenu() => SceneManager.LoadScene("MainMenu");
}
