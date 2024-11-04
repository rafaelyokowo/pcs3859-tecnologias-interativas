using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialController : MonoBehaviour
{

    [SerializeField]
    private Image imageNeedle;
    [SerializeField]
    private Slider pressureSlider;

    private float currentPressure = 0;
    private float targetPressure = 0;
    private float needlePressure = 150.0f;

    // Update is called once per frame
    void Update()
    {
        if (targetPressure != currentPressure)
        {
            UpdatePressure();
        }
    }

    public void SetPressureFromSlider()
    {
        targetPressure = pressureSlider.value;
    }

    // Usar quando outro script chama
    public void SetPressure(float amt)
    {
        targetPressure = amt;
    }

    void UpdatePressure()
    {
        if (targetPressure > currentPressure)
        {
            currentPressure += Time.deltaTime * needlePressure;
            currentPressure = Mathf.Clamp(currentPressure, 0.0f, targetPressure);
        }
        else if (targetPressure < currentPressure)
        {
            currentPressure -= Time.deltaTime * needlePressure;
            currentPressure = Mathf.Clamp(currentPressure, targetPressure, 300.0f);
        }
        SetNeedle();
    }

    void SetNeedle()
    {
        imageNeedle.transform.localEulerAngles = new Vector3(0, 0, (currentPressure / 300.0f * 240.0f - 120.0f) * -1.0f );
    }
}
