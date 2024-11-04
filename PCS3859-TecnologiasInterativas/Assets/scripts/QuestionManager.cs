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
                questionText = "Antes da consulta, qual o primeiro procedimento a ser feito?",
                description = "Escolha a opção correta abaixo.",
                options = new string[] { "Realizar a higienização das mãos", 
                "Cumprimentar o paciente com um aperto de mão", 
                "Fotografar o paciente para o registro", 
                "Confirmar o nome do paciente" },
                correctAnswerIndex = 0
            },
            new Question
            {
                questionText = "Durante a higienização, quais materiais devem ser desinfetados e de que forma?",
                description = "Escolha a opção correta abaixo.",
                options = new string[] { "Cadeira, maca e mesa de atendimento com produtos de limpeza", 
                "Campânula/diafragma e olivas do estetoscópio com algodão embebido em álcool 70%", 
                "Máscaras, bisturi e outros materiais de instrumentação com algodão embebido em álcool 70%", 
                "Não há necessidade de desinfetar materiais nesta etapa do procedimento" },
                correctAnswerIndex = 1
            },
            new Question
            {
                questionText = "O paciente entra no consultório. Qual é a primeira coisa a se fazer?",
                description = "Escolha a opção correta abaixo.",
                options = new string[] { "Apresentar-se ao paciente", 
                "Pedir para que se deite", 
                "Colocar o manguito no paciente", 
                "Aferir a pressão do paciente" },
                correctAnswerIndex = 0
            },
            new Question
            {
                questionText = "Você se apresenta ao paciente. O que você usa para confirmar a identidade do mesmo?",
                description = "Escolha a opção correta abaixo.",
                options = new string[] { "CPF ou RG", 
                "Apelido", 
                "Nome e data de nascimento", 
                "Somente nome" },
                correctAnswerIndex = 2
            },
            new Question
            {
                questionText = "Antes de dar o início ao procedimento de aferição de pressão, alguns pré-requisitos devem ser atendidos, eles estão relacionados à:",
                description = "Escolha a opção correta abaixo.",
                options = new string[] { "Características do paciente como altura, peso e orientação sexual", 
                "Situação emocional e psicológica para evitar vieses na medida da pressão arterial", 
                "Hábitos de sono, de alimentação e de exercícios físicos", 
                "Alimentação ou prática de atividade física na última hora, ingestão de bebidas alcoólicas ou café e consumo de tabaco." },
                correctAnswerIndex = 3
            },
            new Question
            {
                questionText = "Which planet is known as the Red Planet?",
                description = "Select the right option.",
                options = new string[] { "Earth", "Mars", "Jupiter", "Saturn" },
                correctAnswerIndex = 1 // Mars
            },
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
