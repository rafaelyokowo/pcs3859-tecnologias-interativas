using SojaExiles;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonPushOpenDoor : MonoBehaviour
{
    public GameObject door;

    [SerializeField]
    EventScriptableObject pushButtonEvent;
    // public GameObject pacient;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(x => ToggleDoorOpen());
    }

    public void ToggleDoorOpen()
    {
        print("you are opening the door using VR button");
        var openDoorScript = door.GetComponent("opencloseDoor1") as opencloseDoor1;
        openDoorScript.OpenDoor();
        pushButtonEvent?.Invoke();
        // Logica para spawnar um paciente -> TODO
        //Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 11), 5, Random.Range(-10, 11));  
        //Instantiate(pacient, randomSpawnPosition, Quaternion.identity);
    }
}
