using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizQuestion[] dailyQuiz;

    [SerializeField] private GameObject quizCanvas;

    [Header("Question Display")]

    [SerializeField] private TextMeshProUGUI questionText;

    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private TextMeshProUGUI[] optionTextArray;

    [SerializeField] private Image[] optionImageArray;

    [Header("Result Display")]

    [SerializeField] private GameObject resultDisplayObj;

    [SerializeField] private TextMeshProUGUI resultText;

    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private Button[] optionButtonArray;

    [Header("End Screen")]

    [SerializeField] private GameObject endScreenObj;

    [SerializeField] private GameObject questionObj;

    [SerializeField] private TextMeshProUGUI finalScoreText;

    [SerializeField] private TextMeshProUGUI coinText;

    [Header("Option Colors")]

    [SerializeField] private Color defaultOptionColor;

    [SerializeField] private Color correctOptionColor;

    [SerializeField] private Color incorrectOptionColor;

    private QuizQuestion[] _currentQuiz;

    private int _questionIndex;

    private int _questionTimer;

    private int _resultTimer;

    private int _score;

    public void StartDailyQuiz()
    {
        StartQuiz(dailyQuiz);
    }

    public void StartQuiz(QuizQuestion[] quiz)
    {
        _score = 0;

        _questionIndex = 0;
        _currentQuiz = quiz;

        quizCanvas.SetActive(true);

        DisplayNextQuestion();
    }

    private void DisplayNextQuestion()
    {
        if (_questionIndex >= _currentQuiz.Length)
        {
            EndQuiz();
            return;
        }

        _questionTimer = 8;
        timerText.text = _questionTimer.ToString();
        StartCoroutine(QuestionTimerDelay());

        resultDisplayObj.SetActive(false);
        questionText.text = _currentQuiz[_questionIndex].Question;

        foreach (Button button in optionButtonArray)
        {
            button.interactable = true;
        }

        for (int i = 0; i < 4; i++)
        {
            optionTextArray[i].text = _currentQuiz[_questionIndex].OptionsArray[i];
            optionImageArray[i].color = defaultOptionColor;
        }
    }

    public void SelectOption(int index)
    {
        _resultTimer = 5;
        timerText.text = _resultTimer.ToString();
        StartCoroutine(ResultTimerDelay());

        resultDisplayObj.SetActive(true);

        foreach(Button button in optionButtonArray)
        {
            button.interactable = false;
        }
        
        if (_currentQuiz[_questionIndex].CorrectAnswerIndex == index)
        {
            for(int i = 0; i < optionImageArray.Length; i++)
            {
                if(index == i)
                {
                    optionImageArray[i].color = correctOptionColor;
                }
                else
                {
                    optionImageArray[i].color = Color.grey;
                }
            }

            resultText.text = "CORRECT!";

            _score++;
            scoreText.text = "Score: " + _score.ToString();
        }
        else
        {
            for (int i = 0; i < optionImageArray.Length; i++)
            {
                if (index == i)
                {
                    optionImageArray[i].color = incorrectOptionColor;
                }
                else
                {
                    if(i == _currentQuiz[_questionIndex].CorrectAnswerIndex)
                    {
                        optionImageArray[i].color = correctOptionColor;
                    }
                    else
                    {
                        optionImageArray[i].color = Color.grey;
                    }
                }
            }

            resultText.text = "INCORRECT!";
            scoreText.text = "Score: " + _score.ToString();
        }

        _questionIndex++;
    }

    public void EndQuiz()
    {
        Debug.Log("End Quiz!");
        StopAllCoroutines();
        _questionTimer = 0;
        _resultTimer = 0;

        endScreenObj.SetActive(true);
        questionObj.SetActive(false);

        finalScoreText.text = "FINAL SCORE: " + _score.ToString() + "/" + _currentQuiz.Length.ToString();
        coinText.text = (_score * 5).ToString();

        GameManager.InventoryManager.AddItem(GameManager.InventoryManager.coinItem, _score * 5);
    }

    private IEnumerator QuestionTimerDelay()
    {
        yield return new WaitForSeconds(1);

        if(_resultTimer <= 0) //Result Display is not active, so keep decreasing the timer until we reach zero. When we reach zero, end the question and display the result (incorrect)
        {
            _questionTimer--;
            timerText.text = _questionTimer.ToString();

            if (_questionTimer <= 0)
            {
                SelectOption(4);
            }
            else
            {
                StartCoroutine(QuestionTimerDelay());
            }
        }
    }

    private IEnumerator ResultTimerDelay()
    {
        yield return new WaitForSeconds(1);
        _resultTimer--;

        timerText.text = _resultTimer.ToString();

        if(_resultTimer <= 0)
        {
            DisplayNextQuestion();
        }
        else
        {
            StartCoroutine(ResultTimerDelay());
        }
    }
}

[System.Serializable]
public class QuizQuestion
{
    [SerializeField] private string question;

    [SerializeField] private string[] optionsArray;

    [SerializeField] private int correctAnswerIndex;

    public string Question
    {
        get { return question; }
    }

    public string[] OptionsArray
    {
        get { return optionsArray; }
    }

    public int CorrectAnswerIndex
    {
        get { return correctAnswerIndex; }
    }
}
