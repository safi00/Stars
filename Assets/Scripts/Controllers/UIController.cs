using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

    // Start is called before the first frame update
    void Start()
    {
        Heart04.SetActive(false);
        Heart05.SetActive(false);
        Heart06.SetActive(false);
        Heart07.SetActive(false);
        Heart08.SetActive(false);
        Heart09.SetActive(false);
        Heart10.SetActive(false);
        PlayerScoreDisplay.text = String.Format("{0:0000}", PlayerScore);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void gainPoints(float points)
    {
        PlayerScore += points;
        updatePoints();
    }
    private void updatePoints()
    {
        PlayerScoreDisplay.text = String.Format("{0:0000}", PlayerScore);
    }
}
