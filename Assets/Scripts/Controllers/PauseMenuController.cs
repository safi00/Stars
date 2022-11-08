using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] public static bool GameIsPaused = false;
    [SerializeField] public static bool gameStart = false;
    [SerializeField] public GameObject pauseMenuUI;
    [SerializeField] public GameObject resumeMenuUI;
    [SerializeField] public GameObject startUI;
    private void Start()
    {
        showStartinfo();
        pauseMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameStart)
            {
                hideStartinfo();
            }
        }
    }
    public void hideStartinfo()
    {
        gameStart = false;
        GameIsPaused = false;
        Time.timeScale = 1f;
        startUI.SetActive(false);
    }
    private void showStartinfo()
    {
        gameStart = true;
        GameIsPaused = true;
        Time.timeScale = 0f;
        startUI.SetActive(true);
    }
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        resumeMenuUI.SetActive(true);
        GameIsPaused = false;
        Time.timeScale = 1f;
    }
    private void PauseGame()
    {
        resumeMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        Time.timeScale = 0f;
    }
    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        Loader.Load(Loader.Scene.MainMenu);
    }
    public void Quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
