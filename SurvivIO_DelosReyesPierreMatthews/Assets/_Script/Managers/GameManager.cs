using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingletonClass<GameManager>
{
    [field: SerializeField] public Player Player { get; set; }
    public void BeginPlay()
    {
        Debug.Log("Play game scene initiated");
        SceneManager.LoadScene("GameScene");
    }

    public void ReturnToMenu()
    {
        Debug.Log("Menu scene initiated");
        SceneManager.LoadScene("MainMenuScene");
    }

    public void GameOver()
    {
        Debug.Log("Game over scene initiated");
        SceneManager.LoadScene("GameOverScene");
    }

    public void Winner()
    {
        Debug.Log("Win scene initiated");
        SceneManager.LoadScene("WinScene");
    }
}