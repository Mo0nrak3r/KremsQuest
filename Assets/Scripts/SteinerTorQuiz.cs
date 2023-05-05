using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static UnityEngine.ParticleSystem;


public class SteinerTorQuiz : MonoBehaviour
{

    public GameObject prefabButton;
    public Text questionText;
    
    //find Panel in scene
    public RectTransform _uipanel;
    private bool _hasAnswered = false;
    public ScoreScript scoreScript;

    public GameObject parent;
    static float dT = 0;

    // make the questions as Dict at some point - from .txt file. Key = number, value = array of answers, 
    // where the first element is the question itself and correct answers are marked with an astrisk, last element is the question tag.
    //Dictionary<string, string[]> questions = new Dictionary<string, string[]>();

    // Instanciate buttons from prefab with answers from database. 
    // Instances should be children of the AnswerPanel
    void InstancieateButtons(string[] answers, int correctAnswer){
        questionText.text = answers[0];
        for (int i = 0; i < 4 ; i++){
            GameObject cloneButton = Instantiate(prefabButton);
            cloneButton.transform.SetParent(_uipanel);
            cloneButton.GetComponentInChildren<TMP_Text>().text = answers[i+1];
            if(i == correctAnswer-1){
                cloneButton.GetComponent<Button>().onClick.AddListener(QuizCorrect);
                var colors = cloneButton.GetComponent<Button> ().colors;
                colors.selectedColor = Color.green;
                colors.disabledColor = Color.green;
                cloneButton.GetComponent<Button> ().colors = colors;
            }
            else{
                cloneButton.GetComponent<Button>().onClick.AddListener(QuizWrong);
                var colors = cloneButton.GetComponent<Button> ().colors;
                colors.selectedColor = Color.red;
                colors.disabledColor = Color.grey;
                cloneButton.GetComponent<Button> ().colors = colors;
            }
        }
    }

    void QuizCorrect(){
        _hasAnswered = true;
        Debug.Log("Correct!");
        scoreScript.addGrapes(40);
        // add points.

    }

    void QuizWrong(){
        _hasAnswered = true;
        Debug.Log("Wrong!");
    }

    // Start is called before the first frame update
    void Start()
    {
        string[] questions = {"When was the Steiner Tor built?",
                              "During the Roman Empire in 100 BC",
                              "In the 20th century during World War II",
                              "In the late 15th century, specifically between 1480 and 1485.",
                              "In the 14th century by Vikings who invaded Austria." };
        int correctAnswer = 3;
        InstancieateButtons(questions, correctAnswer);
    }
    // Update is called once per frame
    void Update()
    {
        if (_hasAnswered){
            // disable buttons
            foreach (Button child in _uipanel.GetComponentsInChildren<Button>())
            {
                child.interactable = false;
            }

            dT += Time.deltaTime;

            if (dT > 3)
            {
                Destroy(gameObject);
            }
        }
    }
}
