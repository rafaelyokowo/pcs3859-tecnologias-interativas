using UnityEngine;
using UnityEngine.UI;
using TMPro; // Include for TextMeshPro support

[System.Serializable]
public class Question
{
    public string questionText;
    public string description;
    public string[] options; // Must be of length 4 for options A-D
    public int correctAnswerIndex; // Index of the correct answer
}

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText; // Reference to "Título" in Interface
    [SerializeField] private TMP_Text descriptionText; // Reference to "Texto" in Interface
    [SerializeField] private Button[] optionButtons; // Buttons for Option A-D
    [SerializeField] private TMP_Text[] optionTexts; // References to OptionAText, OptionBText, etc.
    [SerializeField] private GameObject feedbackWindow; // Reference to the FeedbackWindow
    [SerializeField] private TMP_Text feedbackText; // Text (TMP) component inside FeedbackWindow
    [SerializeField] private Button closeButton; // Close button in FeedbackWindow
    [SerializeField] private Button nextButton; // Next Button to go to the next question
    [SerializeField] private Button previousButton; // Previous Button to go to the previous question

    private int currentQuestionIndex = 0;

    private Question[] questions; // Array of questions

    private void Start()
    {
        // Hardcode the questions and options here
        questions = new Question[]
        {
            new Question
            {
                questionText = "What is the capital of France?",
                description = "Choose the correct answer from the options below.",
                options = new string[] { "Berlin", "Madrid", "Paris", "Rome" },
                correctAnswerIndex = 2 // Paris
            },
            new Question
            {
                questionText = "Which planet is known as the Red Planet?",
                description = "Select the right option.",
                options = new string[] { "Earth", "Mars", "Jupiter", "Saturn" },
                correctAnswerIndex = 1 // Mars
            },
            // Add more questions as needed
        };

        if (questions.Length > 0)
        {
            DisplayCurrentQuestion();
        }
        else
        {
            Debug.LogError("No questions have been added to the QuestionManager.");
        }

        // Add listener for the close button in the feedback window
        closeButton.onClick.AddListener(CloseFeedbackWindow);
        feedbackWindow.SetActive(false); // Start with the feedback window hidden

        // Add listeners for the Next and Previous buttons
        nextButton.onClick.AddListener(NextQuestion);
        previousButton.onClick.AddListener(PreviousQuestion);
    }

    private void DisplayCurrentQuestion()
    {
        if (currentQuestionIndex >= 0 && currentQuestionIndex < questions.Length)
        {
            Question currentQuestion = questions[currentQuestionIndex];
            SetQuestion(
                currentQuestion.questionText,
                currentQuestion.description,
                currentQuestion.options,
                currentQuestion.correctAnswerIndex
            );
        }
        else
        {
            Debug.Log("No more questions to display.");
        }
    }

    public void SetQuestion(string question, string description, string[] options, int correctIndex)
    {
        titleText.text = question;
        descriptionText.text = description;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionTexts[i].text = options[i]; // Set button text using TMP_Text
            int index = i; // Capture the current index for the listener
            optionButtons[i].onClick.RemoveAllListeners(); // Clear previous listeners
            optionButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }
    }

    private void CheckAnswer(int selectedIndex)
    {
        Question currentQuestion = questions[currentQuestionIndex];
        if (selectedIndex == currentQuestion.correctAnswerIndex)
        {
            ShowFeedback("Correct!", "Well done, that's the right answer.");
        }
        else
        {
            ShowFeedback("Incorrect", "That's not correct. Please try again.");
        }
    }

    private void ShowFeedback(string title, string message)
    {
        feedbackText.text = $"{title}\n{message}";
        feedbackWindow.SetActive(true); // Show the feedback window
    }

    private void CloseFeedbackWindow()
    {
        feedbackWindow.SetActive(false); // Hide the feedback window
    }

    private void NextQuestion()
    {
        if (currentQuestionIndex < questions.Length - 1)
        {
            currentQuestionIndex++;
            DisplayCurrentQuestion();
        }
        else
        {
            Debug.Log("Reached the end of the quiz.");
        }
    }

    private void PreviousQuestion()
    {
        if (currentQuestionIndex > 0)
        {
            currentQuestionIndex--;
            DisplayCurrentQuestion();
        }
        else
        {
            Debug.Log("Already at the first question.");
        }
    }
}
