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
[System.Serializable]
public class RandomizableQuestion
{
    public Question[] possibleQuestions;
    public int selectedQuestion;

    public void RandomizeSelectedQuestion()
    {
        selectedQuestion = Random.Range(0, possibleQuestions.Length - 1);
    }

    public Question GetSelectedQuestion() { return possibleQuestions[selectedQuestion]; }

    public RandomizableQuestion(Question question) {
        possibleQuestions = new Question[] { question };
    }

    public RandomizableQuestion(Question[] questions)
    {
        possibleQuestions = questions;
    }
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

    [SerializeField] private TMP_Text previousButtonText;

    private int currentQuestionIndex = 0;

    private bool randomSelectionStarted = false;

    private RandomizableQuestion[] randomizableQuestions;

    private void Start()
    {
        randomizableQuestions = new RandomizableQuestion[]
        {
            new RandomizableQuestion(new Question
            {
                questionText = "Antes da consulta, qual o primeiro procedimento a ser feito?",
                description = "Escolha a opção correta abaixo.",
                options = new string[] { "Realizar a higienização das mãos",
                "Cumprimentar o paciente com um aperto de mão",
                "Fotografar o paciente para o registro",
                "Confirmar o nome do paciente" },
                correctAnswerIndex = 0
            }),
            new RandomizableQuestion(new Question
            {
                questionText = "Durante a higienização, quais materiais devem ser desinfetados e de que forma?",
                description = "Escolha a opção correta abaixo.",
                options = new string[] { "Cadeira, maca e mesa de atendimento com produtos de limpeza",
                "Campânula/diafragma e olivas do estetoscópio com algodão embebido em álcool 70%",
                "Máscaras, bisturi e outros materiais de instrumentação com algodão embebido em álcool 70%",
                "Não há necessidade de desinfetar materiais nesta etapa do procedimento" },
                correctAnswerIndex = 1
            }),
            new RandomizableQuestion(new Question
            {
                questionText = "O paciente entra no consultório. Qual é a primeira coisa a se fazer?",
                description = "Escolha a opção correta abaixo.",
                options = new string[] { "Apresentar-se ao paciente",
                "Pedir para que se deite",
                "Colocar o manguito no paciente",
                "Aferir a pressão do paciente" },
                correctAnswerIndex = 0
            }),
            new RandomizableQuestion(new Question
            {
                questionText = "Você se apresenta ao paciente. O que você usa para confirmar a identidade do mesmo?",
                description = "Escolha a opção correta abaixo.",
                options = new string[] { "CPF ou RG",
                "Apelido",
                "Nome e data de nascimento",
                "Somente nome" },
                correctAnswerIndex = 2
            }),
            new RandomizableQuestion(new Question
            {
                questionText = "Antes de dar o início ao procedimento de aferição de pressão, alguns pré-requisitos devem ser atendidos, eles estão relacionados à:",
                description = "Escolha a opção correta abaixo.",
                options = new string[] { "Características do paciente como altura, peso e orientação sexual",
                "Situação emocional e psicológica para evitar vieses na medida da pressão arterial",
                "Hábitos de sono, de alimentação e de exercícios físicos",
                "Alimentação ou prática de atividade física na última hora, ingestão de bebidas alcoólicas ou café e consumo de tabaco." },
                correctAnswerIndex = 3
            }),
            new RandomizableQuestion(new Question[]
            {
                new Question
                {
                    questionText = "O paciente menciona que fumou 15 minutos atrás. O que deve ser feito?",
                    description = "Escolha a opção correta abaixo.",
                    options = new string[] { "Pedir para o paciente aguardar pelo menos 15 minutos. O procedimento não pode continuar até então.", "Nada. O procedimento pode continuar normalmente.", "Pedir para o paciente retornar no dia seguinte. O paciente não pode ter fumado nas últimas 8 horas.", "Pedir para o paciente aguardar pelo menos 1 hora. O procedimento não pode continuar até então." },
                    correctAnswerIndex = 0
                },
                new Question
                {
                    questionText = "O paciente menciona que tomou café 2 horas atrás. O que deve ser feito?",
                    description = "Escolha a opção correta abaixo.",
                    options = new string[] { "Pedir para o paciente retornar no dia seguinte. O paciente não pode ter tomado café nas últimas 6 horas.", "Pedir para o paciente tomar água. O procedimento pode continuar após isso.", "Pedir para o paciente aguardar pelo menos 1 hora. O procedimento não pode continuar até então.", "Nada. O procedimento pode continuar normalmente." },
                    correctAnswerIndex = 3
                },
                new Question
                {
                    questionText = "O paciente diz que fez uma extensa caminhada para chegar ao consultório. O que deve ser feito?",
                    description = "Escolha a opção correta abaixo.",
                    options = new string[] { "Se o paciente for jovem, continuar o procedimento normalmente. Caso contrário, aguardar 1 hora.", "Se o paciente julgar o exercício como intenso, interromper o procedimento. O paciente não pode realizar esforço no mesmo dia.", "Nada. O procedimento pode continuar normalmente.", "Pedir para que o paciente aguarde 1 hora ou retorne outro dia sem fazer exercício. O procedimento não pode continuar até então." },
                    correctAnswerIndex = 3
                },
                new Question
                {
                    questionText = "O paciente menciona que urinou 5 minutos atrás. O que deve ser feito?",
                    description = "Escolha a opção correta abaixo.",
                    options = new string[] { "Nada. O procedimento pode continuar normalmente.", "Pedir para que o paciente tome água e aguarde 10 minutos.", "Pedir para que o paciente volte outro dia de bexiga cheia.", "Pedir para que o paciente não urine no consultório." },
                    correctAnswerIndex = 0
                },
                new Question
                {
                    questionText = "Questão 6.5 - O paciente menciona que almoçou uma feijoada há menos de 10 minutos. O que deve ser feito?",
                    description = "Escolha a opção correta abaixo.",
                    options = new string[] { "Nada. O procedimento continua como normal.", "Pedir para o paciente aguardar 2 horas. Até então, o procedimento não pode continuar.", "Pedir para o paciente voltar outro dia. O paciente deve estar de jejum.", "Pedir para o paciente aguardar 30 minutos. Até então, o procedimento não pode continuar." },
                    correctAnswerIndex = 3
                },
                new Question
                {
                    questionText = "O paciente menciona que bebeu vodka no dia anterior. O que deve ser feito?",
                    description = "Escolha a opção correta abaixo.",
                    options = new string[] { "Pedir para o paciente aguardar 2 horas. Até então, o procedimento não pode continuar.", "Pedir para o paciente tomar água e esperar 10 minutos. Até então, o procedimento não pode continuar.", "Pedir para o paciente voltar outro dia. O paciente não pode ter bebido nas últimas 24 horas.", "Nada. O procedimento continua como normal." },
                    correctAnswerIndex = 3
                },
            }),
            new RandomizableQuestion(new Question
            {
                questionText = "Parabéns, você completou o questionário sobre anamnese!",
                description = "Deixe o tablet sobre a mesa e inicie o procedimento de aferição de pressão.",
                options = new string[] {"","","",""},
                correctAnswerIndex = 5
            })
        };

        foreach (var randomizableQuestion in randomizableQuestions)
        {
            randomizableQuestion.RandomizeSelectedQuestion();
        }

        if (randomizableQuestions.Length > 0)
        {
            DisplayCurrentQuestion();
        }
        else
        {
            Debug.LogError("No questions have been added to the QuestionManager.");
        }

        closeButton.onClick.AddListener(CloseFeedbackWindow);
        feedbackWindow.SetActive(false); 

        // Add listeners for the Next and Previous buttons
        nextButton.onClick.AddListener(NextQuestion);
        previousButton.onClick.AddListener(PreviousQuestion);
    }

    private void DisplayCurrentQuestion()
    {
        if (currentQuestionIndex >= 0 && currentQuestionIndex < randomizableQuestions.Length)
        {
            Question currentQuestion = randomizableQuestions[currentQuestionIndex].GetSelectedQuestion();
            SetQuestion(
                currentQuestion.questionText,
                currentQuestion.description,
                currentQuestion.options,
                currentQuestion.correctAnswerIndex
            );
        }
        else
        {
            Debug.Log("Não há mais questões para apresentar.");
        }
    }

    public void SetQuestion(string question, string description, string[] options, int correctIndex)
    {
        titleText.text = question;
        descriptionText.text = description;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionTexts[i].text = options[i];
            int index = i;
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }
    }

    private void CheckAnswer(int selectedIndex)
    {
        Question currentQuestion = randomizableQuestions[currentQuestionIndex].GetSelectedQuestion();
        if (selectedIndex == currentQuestion.correctAnswerIndex)
        {
            ShowFeedback("Você acertou!", "Parabéns, você escolheu a resposta correta.");
        }
        else if (currentQuestionIndex == randomizableQuestions.Length - 1)
        {
            ShowFeedback("Você finalizou o questionário de anamnese.", "Siga para a próxima etapa.");
        }
        else
        {
            ShowFeedback("A resposta escolhida não está correta. ", "Tente novamente.");
        }
    }

    private void ShowFeedback(string title, string message)
    {
        feedbackText.text = $"{title}\n{message}";
        feedbackWindow.SetActive(true);
    }

    private void CloseFeedbackWindow()
    {
        feedbackWindow.SetActive(false);
    }

    public void NextQuestion()
    {
        if (currentQuestionIndex < randomizableQuestions.Length - 1)
        {
            currentQuestionIndex++;
        }
        DisplayCurrentQuestion();
    }

    public void PreviousQuestion()
    {
        if (currentQuestionIndex > 0 && currentQuestionIndex < randomizableQuestions.Length - 1)
        {
            currentQuestionIndex--;
            DisplayCurrentQuestion();
        }
        else
        {
            currentQuestionIndex = 0;
            previousButtonText.text = "Voltar";
            DisplayCurrentQuestion();
            Debug.Log("Você está na primeira questão.");
        }
    }
    
}
