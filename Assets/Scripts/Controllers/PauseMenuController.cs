using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] public static bool GameIsPaused = false;
    [SerializeField] public GameObject pauseMenuUI;
    [SerializeField] public GameObject resumeMenuUI;
    private void Start()
    {
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
