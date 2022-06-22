using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameState gameState = GameState.GameStart;
    private static GameManager instance;
    public Action GameWin;
    public Action GameFail;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject NextButton;

    public static GameManager Instance { get => instance; set => instance = value; }
    public GameState GameState { get => gameState; set => gameState = value; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void NextLevel()
    {
        LevelManager.Instance.SetLevel();
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void Start()
    {
        GameWin += GameWinFunction;
        GameFail += Retry;
    }
    private void GameWinFunction()
    {
        NextButton.SetActive(true);
    }
    public void GameStartFunction()
    {
        gameState = GameState.GameRunning;
        playButton.SetActive(false);
    }

}
public enum GameState
{
    GameStart,
    GameOver,
    GameRunning,
    GameWon
}