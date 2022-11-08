using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardTable : MonoBehaviour
{
    //UI Canvas
    [SerializeField] public GameObject GameOverCanvas;
    //UI Panels
    [SerializeField] public GameObject UIGameOver;
    [SerializeField] public GameObject UILeadboard;
    [SerializeField] public GameObject UILeadboardInput;
    // player health / hearts
    [SerializeField] public TMP_Text Row0Date;
    [SerializeField] public TMP_Text Row0Score;
    [SerializeField] public TMP_Text Row0Name;
    [SerializeField] public TMP_Text Row1Date;
    [SerializeField] public TMP_Text Row1Score;
    [SerializeField] public TMP_Text Row1Name;
    [SerializeField] public TMP_Text Row2Date;
    [SerializeField] public TMP_Text Row2Score;
    [SerializeField] public TMP_Text Row2Name;
    [SerializeField] public TMP_Text Row3Date;
    [SerializeField] public TMP_Text Row3Score;
    [SerializeField] public TMP_Text Row3Name;
    [SerializeField] public TMP_Text Row4Date;
    [SerializeField] public TMP_Text Row4Score;
    [SerializeField] public TMP_Text Row4Name;
    [SerializeField] public TMP_Text Row5Date;
    [SerializeField] public TMP_Text Row5Score;
    [SerializeField] public TMP_Text Row5Name;
    [SerializeField] public TMP_Text Row6Date;
    [SerializeField] public TMP_Text Row6Score;
    [SerializeField] public TMP_Text Row6Name;
    [SerializeField] public TMP_Text Row7Date;
    [SerializeField] public TMP_Text Row7Score;
    [SerializeField] public TMP_Text Row7Name;
    [SerializeField] public TMP_Text Row8Date;
    [SerializeField] public TMP_Text Row8Score;
    [SerializeField] public TMP_Text Row8Name;
    [SerializeField] public TMP_Text Row9Date;
    [SerializeField] public TMP_Text Row9Score;
    [SerializeField] public TMP_Text Row9Name;

    [SerializeField] public GameObject inputField;
    [SerializeField] public Text inputText;
    [SerializeField] public TMP_Text SaveInput;
    [SerializeField] public GameObject SaveButton;
    [SerializeField] public bool SavedOnce;
    // Start is called before the first frame update
    void Start()
    {
        SavedOnce = false;
    }
    // Update is called once per frame
    void Update()
    {
    }    
    private void Awake()
    {
        updateLeaderboard();
    }
    public void updateLeaderboard()
    {
        string jsontxt = File.ReadAllText(Application.dataPath + "/Resources/scores.json");
        List<Score> scorelist = JsonConvert.DeserializeObject<List<Score>>(jsontxt);
        List<Score> SortedList = scorelist.OrderByDescending(o => o.score).ToList();
        for (int i = 0; i < 10; i++)
        {
            switch (i)
            {
                case 0:
                    Row0Date.text = SortedList[i].date;
                    Row0Score.text = removeFloatDecimal(SortedList[i].score);
                    Row0Name.text = SortedList[i].name;
                    break;
                case 1:
                    Row1Date.text = SortedList[i].date;
                    Row1Score.text = removeFloatDecimal(SortedList[i].score);
                    Row1Name.text = SortedList[i].name;
                    break;
                case 2:
                    Row2Date.text = SortedList[i].date;
                    Row2Score.text = removeFloatDecimal(SortedList[i].score);
                    Row2Name.text = SortedList[i].name;
                    break;
                case 3:
                    Row3Date.text = SortedList[i].date;
                    Row3Score.text = removeFloatDecimal(SortedList[i].score);
                    Row3Name.text = SortedList[i].name;
                    break;
                case 4:
                    Row4Date.text = SortedList[i].date;
                    Row4Score.text = removeFloatDecimal(SortedList[i].score);
                    Row4Name.text = SortedList[i].name;
                    break;
                case 5:
                    Row5Date.text = SortedList[i].date;
                    Row5Score.text = removeFloatDecimal(SortedList[i].score);
                    Row5Name.text = SortedList[i].name;
                    break;
                case 6:
                    Row6Date.text = SortedList[i].date;
                    Row6Score.text = removeFloatDecimal(SortedList[i].score);
                    Row6Name.text = SortedList[i].name;
                    break;
                case 7:
                    Row7Date.text = SortedList[i].date;
                    Row7Score.text = removeFloatDecimal(SortedList[i].score);
                    Row8Name.text = SortedList[i].name;
                    break;
                case 8:
                    Row8Date.text = SortedList[i].date;
                    Row8Score.text = removeFloatDecimal(SortedList[i].score);
                    Row8Name.text = SortedList[i].name;
                    break;
                case 9:
                    Row9Date.text = SortedList[i].date;
                    Row9Score.text = removeFloatDecimal(SortedList[i].score);
                    Row9Name.text = SortedList[i].name;
                    break;
            }
        }
    }
    public void save()
    {
        string jsontxt = File.ReadAllText(Application.dataPath + "/Resources/scores.json");
        List<Score> scoreList = JsonConvert.DeserializeObject<List<Score>>(jsontxt);
        int listSize = scoreList.Count;
        Score score = new Score();

        score.name = SaveInput.text;
        score.date = "" + DateTime.Now.ToString("MM/dd/yyyy");
        score.score = float.Parse(UIController.getPlayerScore());
        scoreList.Add(score);

        jsontxt = JsonConvert.SerializeObject(scoreList, Formatting.Indented);

        if (scoreList.Count > listSize)
        {
            File.WriteAllText(Application.dataPath + "/Resources/scores.json", jsontxt);
            Debug.Log("saved");
        }
        hideLeaderboardInput();
        SavedOnce = true;
    }
    public void showLeaderboard() 
    {
        UILeadboard.SetActive(true);
    }
    public void hideLeaderboard()
    {
        UILeadboard.SetActive(false);
    }
    public void showLeaderboardInput()
    {
        string savedOnceText = "Write your name for the leaderboard";
        inputText.text = savedOnceText;
        UILeadboardInput.SetActive(true);
        if (SavedOnce)
        {
            savedOnceText = "You've already saved!";
            inputText.text = savedOnceText;
            inputField.SetActive(false);
            SaveButton.SetActive(false);
        }
    }
    public void hideLeaderboardInput()
    {
        UILeadboardInput.SetActive(false);
    }
    private string removeFloatDecimal(float number)
    {
        string stringWdecimal = "" + number;
        string[] numberString = stringWdecimal.Split('.');
        return numberString[0];
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
    public class Score
    {
        public string name { get; set; }
        public float score { get; set; }
        public string date { get; set; }
    }
}
