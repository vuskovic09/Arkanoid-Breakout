using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    public GameObject gameOverScreen;

    public GameObject victoryScreen;

    public int AvailableLives = 3;

    public int Lives { get; set; }

    public bool IsGameStarted { get; set; }

    public static event Action<int> OnLifeLost;

    public AudioSource[] sounds;
    public AudioSource music;
    public AudioSource gameOverMusic;
    public AudioSource victoryMusic;

    private void Start() 
    {
        sounds = GetComponents<AudioSource>();
        music = sounds[0];
        gameOverMusic = sounds[1];
        victoryMusic = sounds[2];
        music.Play();
        this.Lives = this.AvailableLives;
        Screen.SetResolution(540, 960, false);
        Ball.OnBallDeath += OnBallDeath;
        Brick.OnBrickDestruction += OnBrickDestruction;
    }

    private void OnBrickDestruction(Brick obj)
    {
        if (BricksManager.Instance.RemainingBricks.Count <= 0)
        {
            BallsManager.Instance.ResetBalls();
            GameManager.Instance.IsGameStarted = false;
            BricksManager.Instance.LoadNextLevel();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnBallDeath(Ball obj)
    {
        if (BallsManager.Instance.Balls.Count <= 0)
        {
            this.Lives--;

            if(this.Lives < 1)
            {
                //show gameover screen
                gameOverScreen.SetActive(true);
                music.Stop();
                gameOverMusic.Play();
               
            }
            else
            {
                OnLifeLost?.Invoke(this.Lives);

                //reset balls
                //stop game
                //reload level

                BallsManager.Instance.ResetBalls();
                IsGameStarted = false;
                BricksManager.Instance.LoadLevel(BricksManager.Instance.CurrentLevel);
            }
        }
    }

    public void ShowVictoryScreen()
    {
        music.Stop();
        victoryMusic.Play();
        victoryScreen.SetActive(true);
    }

    private void OnDisable()
    {
        Ball.OnBallDeath -= OnBallDeath;
    }
}
