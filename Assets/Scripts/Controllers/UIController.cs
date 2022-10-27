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
        PlayerScoreDisplay.text = String.Format("{0:0000}", PlayerScore);
    }
    void Update()
    {

    }
    public void gainPoints(float points)
    {
        PlayerScore += points;
        updatePoints();
    }
    private void gainCoinPoints()
    {
        gainPoints(10);
    }

    private void updatePoints()
    {
        PlayerScoreDisplay.text = String.Format("{0:0000}", PlayerScore);
    }
    private void updateHealth()
    {
        switch (SnakeController.PlayerHealth)
        {
            case <1:
                Console.WriteLine("GAME OVER");
                DisplayHealth(0);
                break;
            case 1:
                Console.WriteLine("01 Heart");
                DisplayHealth(1);
                break;
            case 2:
                Console.WriteLine("02 Hearts");
                DisplayHealth(2);
                break;
            case 3:
                Console.WriteLine("03 Hearts");
                DisplayHealth(3);
                break;
            case 4:
                Console.WriteLine("04 Hearts");
                DisplayHealth(4);
                break;
            case 5:
                Console.WriteLine("05 Hearts");
                DisplayHealth(5);
                break;
            case 6:
                Console.WriteLine("06 Hearts");
                DisplayHealth(6);
                break;
            case 7:
                Console.WriteLine("07 Hearts");
                DisplayHealth(7);
                break;
            case 8:
                Console.WriteLine("08 Hearts");
                DisplayHealth(8);
                break;
            case 9:
                Console.WriteLine("09 Hearts");
                DisplayHealth(9);
                break;
            case 10:
                Console.WriteLine("10 Hearts");
                DisplayHealth(10);
                break;
            case >10:
                Console.WriteLine("10 Hearts is max! you are rewarded +100 points instead!");
                DisplayHealth(10);
                break;
        }
    }
    private void HideHearts()
    {
        for (int i = 0; i < Hearts.Count; i++)
        {
            Hearts[i].SetActive(true);
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
        CoinController.OnCoinCollectable += gainCoinPoints;
        PowerUPController.OnHeartsCollectable += updateHealth;
    }
    private void OnDisable()
    {
        CoinController.OnCoinCollectable -= gainCoinPoints;
        PowerUPController.OnHeartsCollectable -= updateHealth;
    }
}
