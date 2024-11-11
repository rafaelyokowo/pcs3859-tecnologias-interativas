using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PressureGuessController : MonoBehaviour
{
    public TMP_InputField[] digitFields; // Referência para os textos dos dígitos (Digit1, Digit2, Digit3)
    private int currentDigitIndex = 0; // Índice do dígito selecionado
    public Image[] digitBackgrounds; // Referência para os fundos dos dígitos, se cada campo tiver um componente Image
    public Color activeColor = Color.yellow;
    public Color inactiveColor = Color.white;

    [SerializeField]
    private GameObject phase2;
    [SerializeField]
    private GameObject phaseCorrect;
    [SerializeField]
    private GameObject phaseWrong;
    [SerializeField]
    private BloodPressureAudioController bloodPressureAudioController;

    // Chame essa função quando o botão numérico for pressionado
    public void OnNumberButtonPressed(int number)
    {
        digitFields[currentDigitIndex].text = number.ToString();
    }

    // Muda o dígito selecionado quando o jogador clica em "próximo" ou "anterior"
    public void SelectNextDigit()
    {
        currentDigitIndex = (currentDigitIndex + 1) % digitFields.Length;
        UpdateHighlight();
    }

    public void SelectPreviousDigit()
    {
        currentDigitIndex = (currentDigitIndex - 1 + digitFields.Length) % digitFields.Length;
        UpdateHighlight();
    }

    public void OnClickGuess()
    {
        phase2.SetActive(false);
        if (IsPressureCorrect())
        {
            phaseCorrect.SetActive(true);
        }
        else
        {
            phaseWrong.SetActive(true);
        }
    }

    // Verifica se a senha está correta (senha definida como exemplo)
    public bool IsPressureCorrect()
    {
        float systolic = float.Parse(digitFields[0].text.ToString()) * 100.0f + float.Parse(digitFields[1].text.ToString()) * 10.0f + float.Parse(digitFields[2].text.ToString());
        float diastolic = float.Parse(digitFields[3].text.ToString()) * 100.0f + float.Parse(digitFields[4].text.ToString()) * 10.0f + float.Parse(digitFields[5].text.ToString());
        if(bloodPressureAudioController.systolicPressure - 5.0f <= systolic && systolic <= bloodPressureAudioController.systolicPressure + 5.0f && bloodPressureAudioController.diastolicPressure - 5.0f <= diastolic && diastolic <= bloodPressureAudioController.diastolicPressure + 5.0f)
        {
            return true;
        }
        return false;
    }

    void Start()
    {
        UpdateHighlight();
    }

    private void UpdateHighlight()
    {
        // Atualiza a cor de todos os fundos, destacando o campo selecionado
        for (int i = 0; i < digitBackgrounds.Length; i++)
        {
            digitBackgrounds[i].color = (i == currentDigitIndex) ? activeColor : inactiveColor;
        }
    }

}
