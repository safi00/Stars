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
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }
    private void resumeGame()
    {
        pauseMenuUI.SetActive(false);
        resumeMenuUI.SetActive(true);
        GameIsPaused = false;
        Time.timeScale = 1f;
    }
    private void pauseGame()
    {
        resumeMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        Time.timeScale = 0f;
    }
}
