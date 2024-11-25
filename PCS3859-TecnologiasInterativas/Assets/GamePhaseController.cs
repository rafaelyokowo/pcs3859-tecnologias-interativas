using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class GamePhaseController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject startingPhase;
    [SerializeField]
    private GameObject anamnesisPhase;
    [SerializeField]
    private GameObject sphygmomanometerPhase;
    [SerializeField]
    private GameObject pressureMeasurementPhase;
    [SerializeField]
    private XRInteractorLineVisual rightHandRayLine;
    [SerializeField]
    private XRRayInteractor rightHandRayEnabler;


    [SerializeField]
    EventScriptableObject openDoorEvent;

    [SerializeField]
    EventScriptableObject endAnamneseEvent;

    void Start()
    {
        startingPhase.SetActive(true);
        anamnesisPhase.SetActive(false);
        sphygmomanometerPhase.SetActive(false);
        pressureMeasurementPhase.SetActive(false);

        openDoorEvent?.AddListener(StartGame);
        endAnamneseEvent?.AddListener(PressureMeasurement);
    }

    public void StartGame()
    {
        startingPhase.SetActive(false);
        anamnesisPhase.SetActive(true);
    }

    public void SphygmomanometerPlacement()
    {
        anamnesisPhase.SetActive(false);
        sphygmomanometerPhase.SetActive(true);
    }

    public void PressureMeasurement()
    {
        sphygmomanometerPhase.SetActive(false);
        pressureMeasurementPhase.SetActive(true);
        rightHandRayEnabler.enabled = true;
        rightHandRayLine.enabled = true;
    }

    public void EndGame()
    {
        pressureMeasurementPhase.SetActive(false);
        rightHandRayEnabler.enabled = false;
        rightHandRayLine.enabled = false;
        SceneManager.LoadScene("FinalScene_00");
    }
}
