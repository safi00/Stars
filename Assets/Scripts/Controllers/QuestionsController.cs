using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestionsController : MonoBehaviour, ICollectable
{
    [SerializeField] public GameObject QuestionUI;
    [SerializeField] public TMP_Text QuestionText;
    [SerializeField] public TMP_Text AnswerA;
    [SerializeField] public TMP_Text AnswerB;
    [SerializeField] public TMP_Text AnswerC;
    [SerializeField] public TMP_Text AnswerD;
    [SerializeField] public int questionIndex;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(getQuestionList().Count);
    }
    // Update is called once per frame
    void Update()
    {        
    }
    private void FillinQuestions()
    {
        List<Question> Questionlist = getQuestionList();
        //questionIndex = Random.Range(0,Questionlist.Count);
        Question randomQuestion = Questionlist[questionIndex];
        QuestionText.text = randomQuestion.questionid + "#" + Environment.NewLine + randomQuestion.question;
        List<string> randomAnswers = randomizeAnswers(randomQuestion.falseAnswers, randomQuestion.answer);
        AnswerA.text = randomAnswers[0];
        AnswerB.text = randomAnswers[1];
        AnswerC.text = randomAnswers[2];
        AnswerD.text = randomAnswers[3];
    }
    private List<string> randomizeAnswers(List<string> falseAnswers, string rightAnswer)
    {
        List<string> answers = new List<string>();
        int randomPlacement = Random.Range(0, 4);
        for (int i = 0; i < 3; i++)
        {
            answers.Add(falseAnswers[i]);
        }
        answers.Add(falseAnswers[randomPlacement]);
        answers[randomPlacement] = rightAnswer;
        return answers;
    }
    private List<string> getStringAnswers(List<string> falseAnswers, string rightAnswer)
    {
        return null;
    }
    private List<int> getIntAnswers()
    {
        return null;
    }
    private List<float> getFloatAnswers()
    {
        return null;
    }
    private List<Question> getQuestionList()
    {
        string jsontxt = File.ReadAllText(Application.dataPath + "/Resources/questions.json");
        List<Question> questionList = JsonConvert.DeserializeObject<List<Question>>(jsontxt);
        return questionList;
    }
    public void AnswerAButton()
    {

    }
    public void AnswerBButton()
    {

    }
    public void AnswerCButton()
    {

    }
    public void AnswerDButton()
    {

    }
    public void QuetionPOP()
    {
        QuestionUI.SetActive(true);
        FillinQuestions();
    }
    private void OnEnable()
    {
        PowerUPController.OnQCoinsCollectable += QuetionPOP;
    }
    private void OnDisable()
    {
        PowerUPController.OnQCoinsCollectable -= QuetionPOP;
    }
    public void Collect(string Collectable)
    {
        throw new System.NotImplementedException();
    }
    public class Question
    {
        public int questionid { get; set; }
        public string answertype { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public List<string> falseAnswers { get; set; }
    }
}
