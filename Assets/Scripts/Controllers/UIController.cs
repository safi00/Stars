using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //UI Panel
    [SerializeField] public GameObject UIPanelContainer;

    // player health / hearts
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

    //player score
    [SerializeField] public Text PlayerScoreDisplay;
    [SerializeField] public float PlayerScore;

    //for easy acces to the hearts
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
        PlayerScoreDisplay.text = String.Format("{0:0000}", PlayerScore);
    }
    void Update()
    {

    }
    public void GainPoints(float points)
    {
        PlayerScore += points;
        UpdatePoints();
    }
    private void GainCoinPoints()
    {
        GainPoints(10);
    }

    private void UpdatePoints()
    {
        PlayerScoreDisplay.text = String.Format("{0:0000}", PlayerScore);
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
                Debug.Log("01 Heart");
                DisplayHealth(1);
                break;
            case 2:
                Debug.Log("02 Hearts");
                DisplayHealth(2);
                break;
            case 3:
                Debug.Log("03 Hearts");
                DisplayHealth(3);
                break;
            case 4:
                Debug.Log("04 Hearts");
                DisplayHealth(4);
                break;
            case 5:
                Debug.Log("05 Hearts");
                DisplayHealth(5);
                break;
            case 6:
                Debug.Log("06 Hearts");
                DisplayHealth(6);
                break;
            case 7:
                Debug.Log("07 Hearts");
                DisplayHealth(7);
                break;
            case 8:
                Debug.Log("08 Hearts");
                DisplayHealth(8);
                break;
            case 9:
                Debug.Log("09 Hearts");
                DisplayHealth(9);
                break;
            case 10:
                Debug.Log("10 Hearts");
                DisplayHealth(10);
                break;
            case >10:
                Debug.Log("10 Hearts is max! you are rewarded +100 points instead!");
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

    private void OnEnable()
    {
        CoinController.OnCoinCollectable += GainCoinPoints;
        PowerUPController.OnHeartsCollectable += UpdateHealth;
        Hurt.OnPlayerPainfulCollision += UpdateHealth;
    }
    private void OnDisable()
    {
        CoinController.OnCoinCollectable -= GainCoinPoints;
        PowerUPController.OnHeartsCollectable -= UpdateHealth;
        Hurt.OnPlayerPainfulCollision -= UpdateHealth;
    }
}
