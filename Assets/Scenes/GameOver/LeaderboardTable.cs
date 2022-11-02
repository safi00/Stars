using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class LeaderboardTable : MonoBehaviour
{
    public class Root
    {
        public List<Score> scores { get; set; }
    }
    public class Score
    {
        public string name { get; set; }
        public string score { get; set; }
        public string date { get; set; }
    }
    private Transform entryContent;
    private Transform entryTemplate;
    private Transform entryBoard;

    // Start is called before the first frame update
    void Start()
    {
        JSONreader();
    }

    // Update is called once per frame
    void Update()
    {        
    }
    
    private void Awake()
    {
        entryBoard = transform.Find("Leaderboard");
        entryContent = entryBoard.transform.Find("LeaderboardInfo");
        entryTemplate = entryContent.transform.Find("Row");
        entryTemplate.gameObject.SetActive(false);

        for (int i = 0; i < 10; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContent);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

            entryRectTransform.anchoredPosition = new Vector2(0, 448 - (40 * i));
            entryTransform.gameObject.SetActive(true);

            int rank = i + 1;
            string rankString;
            switch (rank)
            {
                default:
                    rankString = "" + rank + "TH";
                    break;
                case 1:
                    rankString = "1ST";
                    break;
                case 2:
                    rankString = "2ND";
                    break;
                case 3:
                    rankString = "3RD";
                    break;
            }
            /*
            entryTemplate.Find("RankText").GetComponent<Text>().text = rankString;
            int score = Random.Range(0, 2000);
            entryTemplate.Find("DateText").GetComponent<Text>().text = rankString;
            entryTemplate.Find("RankText").GetComponent<Text>().text = rankString;
            entryTemplate.Find("RankText").GetComponent<Text>().text = rankString;
            */
        }
    }

    private void JSONreader()
    {
        string testtxt = File.ReadAllText(Application.dataPath + "/Resources/scores.json");

        Root test = new Root();
        test.scores = new List<Score>();

        string xxx = File.ReadAllText(Application.dataPath + "/Resources/sssss.json");

        List<Score> Scorex = JsonConvert.DeserializeObject<List<Score>>(xxx);
        Debug.Log(Scorex.Count);
        Score x = new Score();
        x.name = "x";
        x.date = "x";
        x.score = "x";

        Scorex.Add(x);
        Debug.Log(Scorex.Count);
        xxx = JsonConvert.SerializeObject(Scorex, Formatting.Indented);

        File.WriteAllText(Application.dataPath + "/Resources/sssss.json", xxx);
    }
}
