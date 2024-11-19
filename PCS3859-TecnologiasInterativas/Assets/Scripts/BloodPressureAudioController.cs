using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class BloodPressureAudioController : MonoBehaviour
{
    [SerializeField]
    private Image imageNeedle;

    public InputActionReference inflatePressureReference;

    public AudioSource audioSource;
    public AudioClip higherThanMAX;    
    public AudioClip phase1;       // Som inicial
    public AudioClip phase2And3;    // Batimentos cardíacos
    public AudioClip phase4And5;      // Som final ou silêncio

    public float currentPressure;       // A pressão atual
    private float targetPressure = 0;
    public float systolicPressure = 120f;   // Valor da pressão sistólica -> logica random TODO
    public float diastolicPressure = 80f;   // Valor da pressão diastólica
    public float maxPressure = 200f;

    private bool isInflating = false;    // Controlar se o jogador está inflando o medidor

    private float needlePressure = 0.0f;

    void Start()
    {
        audioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePressure();
        if (currentPressure > maxPressure)
        {
            if (audioSource.clip != higherThanMAX)
            {
                audioSource.clip = higherThanMAX;
                audioSource.Play();
            }
        }
        else if(currentPressure > systolicPressure && currentPressure <= maxPressure)
        {
            if (audioSource.clip != phase1)
            {
                audioSource.clip = phase1;
                audioSource.Play();
            }
        }
        else if (currentPressure <= systolicPressure && currentPressure > diastolicPressure)
        {
            if (audioSource.clip != phase2And3)
            {
                audioSource.clip = phase2And3;
                audioSource.Play();
            }
        }
        else if (currentPressure <= diastolicPressure)
        {
            if (audioSource.clip != phase4And5)
            {
                audioSource.clip = phase4And5;
                audioSource.Play();
            }
        }
    }

    public void Inflate (bool _inflate)
    {
        isInflating = _inflate;
    }

    public void SetPressure(float amt)
    {
        targetPressure = amt;
    }

    void UpdatePressure()
    {
        if (isInflating || inflatePressureReference.action.ReadValue<float>() > 0.0f) // O botão que você usa para inflar
        {
            currentPressure += 30f * Time.deltaTime; // Incremento de pressão (ajuste conforme necessário)

        }
        else
        {
            currentPressure -= 10f * Time.deltaTime; // Decremento de pressão (ajuste conforme necessário)
        }
        currentPressure = Mathf.Clamp(currentPressure, 0, 360.0f);
        SetNeedle();
    }

    void SetNeedle()
    {
        imageNeedle.transform.localEulerAngles = new Vector3(0, 0, (currentPressure / 360.0f * 360.0f ) * -1.0f);
    }
}
