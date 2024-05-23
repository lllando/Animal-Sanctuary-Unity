using UnityEngine;

public class QuizManager : MonoBehaviour
{
    private QuizQuestion[] _currentQuiz;

    private int _questionIndex;

    public void StartQuiz(QuizQuestion[] quiz)
    {
        _questionIndex = 0;
        _currentQuiz = quiz;

        DisplayNextQuestion();
    }

    private void DisplayNextQuestion()
    {
        //Update Display

        _questionIndex++;

        if(_questionIndex >= _currentQuiz.Length)
        {
            EndQuiz();
        }
    }

    public void SelectOption(int index)
    {
        if (_currentQuiz[_questionIndex].CorrectAnswerIndex == index)
        {

        }
        else
        {

        }
    }

    public void EndQuiz()
    {

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
