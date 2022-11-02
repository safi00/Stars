using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class QuestionsController : MonoBehaviour
{
    [SerializeField]
    public class Question
    {
        public string questionname { get; set; }
        public string answer { get; set; }
        public List<string> falseAnswers { get; set; }
    }
    [SerializeField]
    public class Root
    {
        public List<Question> questions { get; set; }
    }
    // Start is called before the first frame update
    void Start()
    {
        string testtxt = File.ReadAllText(Application.dataPath + "/Resources/test.json");             

        Question test = JsonUtility.FromJson<Question>(testtxt);
        Debug.Log(testtxt);

        //Root root = new Root();

        //questionMaker(100, root);
        Question question = new Question()
        {
            questionname = "x",
            answer = "x",
            falseAnswers = new List<string>(),
        };

        Root root = new Root()
        {
            questions = new List<Question>(),
        };
        root.questions.Add(question);
        string JSONtext = JsonUtility.ToJson(root);
        File.WriteAllText(Application.dataPath + "/Resources/questionsz.json", JSONtext);
    }

    private void questionMaker(int amount, Root root) 
    {
        for (int i = 0; i < amount; i++)
        {
            Question question = new Question();
            question.questionname = "What's 2x3?";
            question.answer = "6";
            question.falseAnswers.Add("5");
            question.falseAnswers.Add("7");
            question.falseAnswers.Add("8");
            root.questions.Add(question);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
