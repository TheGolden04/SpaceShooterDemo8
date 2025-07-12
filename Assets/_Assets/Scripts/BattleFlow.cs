using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleFlow : MonoBehaviour
{
    public BossHealth bossHealth;
    public GameObject gameOverUI;
    public GameObject gameWinUI;
    public PlayerHealth playerHealth;
    public GameObject bgMusic;

    private void Start()
    {
        gameWinUI.SetActive(false);
        gameOverUI.SetActive(false);

        playerHealth.onDead += OnGameOver; // Khi player chết
    }

    private void OnGameOver()
    {
        gameOverUI.SetActive(true);
        bgMusic.SetActive(false);
        this.enabled = false;
    }

    public void OnGameWin()
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
