using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    // UI Text Components
    [Header("UI Text Components")]
    [SerializeField] private TextMeshProUGUI titleText;  // Question title
    [SerializeField] private TextMeshProUGUI optionAText;  // Option A text
    [SerializeField] private TextMeshProUGUI optionBText;  // Option B text
    [SerializeField] private TextMeshProUGUI optionCText;  // Option C text
    [SerializeField] private TextMeshProUGUI optionDText;  // Option D text

    [Header("Dialogue")]
    [SerializeField] private DialogueObject currentDialogue;

    [Header("Panels")]
    [SerializeField] private GameObject interfacePanel;  // Panel that contains the questions and options
    [SerializeField] private GameObject controlPanel;  // Panel that contains the control buttons

    [Header("Buttons")]
    [SerializeField] private Button toggleButton;  // Button to show/hide the interface
    [SerializeField] private Button nextButton;  // Button to go to next question
    [SerializeField] private Button previousButton;  // Button to go to previous question

    private bool isInterfaceVisible = true;  // Boolean to track visibility of the interface

    // Camera Reference for the floating canvas
    [SerializeField] private Transform cameraTransform;  // Reference to VR Camera (usually main camera in XR Rig)
    public float distanceFromCamera = 2.0f;  // Distance the UI will stay in front of the camera
    public float heightOffset = 0.0f;  // Height offset for UI positioning

    void Start()
    {
        // Initialize the UI
        UpdateQuestionUI();

        // Assign button listeners
        toggleButton.onClick.AddListener(ToggleInterfaceVisibility);
        nextButton.onClick.AddListener(NextStep);
        previousButton.onClick.AddListener(PreviousStep);
    }

    void Update()
    {
        // Make the canvas follow the camera position
        UpdateCanvasPosition();
    }

    // Updates the Canvas to stay in front of the camera
    private void UpdateCanvasPosition()
    {
        Vector3 newPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
        newPosition.y += heightOffset;  // Apply height offset if needed
        transform.position = newPosition;

        // Rotate the UI to face the camera
        transform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position);
    }

    // Updates the question and options text
    private void UpdateQuestionUI()
    {
        currentDialogue.RandomizeResponseOrder();
        titleText.text = currentDialogue.dialoguePrompt;
        List<ResponseObject> responses = currentDialogue.GetResponses();
        optionAText.text = responses[0].response;
        optionBText.text = responses[1].response;
        optionCText.text = responses[2].response;
        optionDText.text = responses[3].response;
    }

    // Next question
    public void NextStep()
    {
        if (currentDialogue.nextDialogue != null)
        {
            currentDialogue = currentDialogue.nextDialogue;
            UpdateQuestionUI();
        }
    }

    // Previous question
    public void PreviousStep()
    {
        if (currentDialogue.previousDialogue != null)
        {
            currentDialogue = currentDialogue.previousDialogue;
            UpdateQuestionUI();
        }
    }

    // Toggles the visibility of the interface (questionnaire part)
    private void ToggleInterfaceVisibility()
    {
        isInterfaceVisible = !isInterfaceVisible;
        interfacePanel.SetActive(isInterfaceVisible);  // Show/hide interface panel

        // Update the button text accordingly
        toggleButton.GetComponentInChildren<TextMeshProUGUI>().text = isInterfaceVisible ? "Hide" : "Show";
    }
}
