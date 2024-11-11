using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAppearenceController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject dial;
    [SerializeField]
    private GameObject needle;
    [SerializeField]
    private GameObject pressureButton;
    [SerializeField]
    private GameObject guessPressureButton;
    [SerializeField]
    private GameObject secondPhase;

    private bool pressureUIvisible = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pressureUIvisible == false)
        {
            dial.SetActive(false);
            pressureButton.SetActive(false);
            needle.SetActive(false);
            guessPressureButton.SetActive(false);
        }
    }

    public void GuessPressure()
    {
        pressureUIvisible = !pressureUIvisible;
        secondPhase.SetActive(true);
    }
}
