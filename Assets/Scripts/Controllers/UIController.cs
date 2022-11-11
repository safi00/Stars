using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("UI Panel")]
    [SerializeField] public GameObject UIPanelContainer;

    [Header("player health / hearts")]
    [SerializeField] public GameObject Heart01;
    [SerializeField] public GameObject Heart02;
    [SerializeField] public GameObject Heart03;
    [SerializeField] public GameObject Heart04;
    [SerializeField] public GameObject Heart05;
    [SerializeField] public GameObject Heart06;
    [SerializeField] public GameObject Heart07;
    [SerializeField] public GameObject Heart08;
    [SerializeField] public GameObject Heart09;
    [SerializeField] public GameObject Heart10;

    [Header("Player Score & Multiplier")]
    [SerializeField] public Text PlayerScoreDisplay;
    [SerializeField] public GameObject PlayerScoreMultiplierDisplay;
    [SerializeField] public Text PlayerScoreDisplayText;
    [SerializeField] public static float PlayerScore;
    [SerializeField] public static double PlayerScoreMultiplier;

    [Header("Easy access to hearts")]
    private List<GameObject> Hearts = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        Hearts.Add(Heart01);
        Hearts.Add(Heart02);
        Hearts.Add(Heart03);
        Hearts.Add(Heart04);
        Hearts.Add(Heart05);
        Hearts.Add(Heart06);
        Hearts.Add(Heart07);
        Hearts.Add(Heart08);
        Hearts.Add(Heart09);
        Hearts.Add(Heart10);
        DisplayHealth(3);
        setPoints(0);
        PlayerScoreDisplay.text = String.Format("{0:0000}", PlayerScore);
    }
    void Update()
    {
        UpdateHealth();
        UpdatePoints();
    }
    private void setPoints(float points)
    {
        PlayerScore = points;
        Update();
    }
    public float getPlayerPoints()
    {
        return PlayerScore;
    }
    public static string getPlayerScore()
    {
        return String.Format("{0:0000}", PlayerScore);
    }
    public void GainPoints(float points)
    {
        float gainedpoints = (float)(points * PlayerScoreMultiplier);
        PlayerScore += gainedpoints;
        UpdatePoints();
    }
    /// <summary>
    /// The Method below is meant only for coins and it adds points for the player
    /// </summary>
    private void GainCoinPoints()
    {
        GainPoints(100);
    }
    private void UpdatePoints()
    {
        PlayerScoreDisplay.text = String.Format("{0:0000}", PlayerScore);
    }
    private void UpdateMultiplier()
    {
        PlayerScoreMultiplierDisplay.SetActive(true);
        PlayerScoreDisplayText.text = (100 * PlayerScoreMultiplier) + "%";
    }
    private void UpdateHealth()
    {
        switch (SnakeController.PlayerHealth)
        {
            case <1:
                Debug.Log("GAME OVER");
                DisplayHealth(0);
                break;
            case 1:
                DisplayHealth(1);
                break;
            case 2:
                DisplayHealth(2);
                break;
            case 3:
                DisplayHealth(3);
                break;
            case 4:
                DisplayHealth(4);
                break;
            case 5:
                DisplayHealth(5);
                break;
            case 6:
                DisplayHealth(6);
                break;
            case 7:
                DisplayHealth(7);
                break;
            case 8:
                DisplayHealth(8);
                break;
            case 9:
                DisplayHealth(9);
                break;
            case 10:
                DisplayHealth(10);
                break;
            case >10:
                DisplayHealth(10);
                break;
        }
    }
    private void HideHearts()
    {
        for (int i = 0; i < Hearts.Count; i++)
        {
            Hearts[i].SetActive(false);
        }
    }
    private void DisplayHealth(float health)
    {
        HideHearts();
        for (int i = 0; i < health; i++)
        {
            Hearts[i].SetActive(true);
        }
    }
    /// <summary>
    /// This methods are here to subcribe to events
    /// so when a coin gets collected all the other scripts know to tun a method
    /// </summary>
    private void OnEnable()
    {
        CoinController.OnCoinCollectable += GainCoinPoints;
        Heart.OnHeartGained += UpdateHealth;
        Hurt.OnPlayerWallHitCollision += UpdateHealth;
        Speed.OnSpeedGained += UpdateMultiplier;
        PowerUPController.OnQCoinsCollectable += GainCoinPoints;
    }
    private void OnDisable()
    {
        CoinController.OnCoinCollectable -= GainCoinPoints;
        Heart.OnHeartGained -= UpdateHealth;
        Hurt.OnPlayerWallHitCollision -= UpdateHealth;
        Speed.OnSpeedGained -= UpdateMultiplier;
        PowerUPController.OnQCoinsCollectable -= GainCoinPoints;
    }
}
