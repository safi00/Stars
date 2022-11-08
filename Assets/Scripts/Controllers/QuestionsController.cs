using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class QuestionsController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {        
    }
    private List<Question> getQuestionList()
    {
        string jsontxt = File.ReadAllText(Application.dataPath + "/Resources/questions.json");
        List<Question> questionList = JsonConvert.DeserializeObject<List<Question>>(jsontxt);
        return questionList;
    }
    public class Question
    {
        public string questionname { get; set; }
        public string answer { get; set; }
        public List<string> falseAnswers { get; set; }
    } 
}
