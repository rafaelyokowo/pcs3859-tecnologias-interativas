using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabEventInvoker : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;

    [SerializeField] EventScriptableObject eventToInvoke;

    void Start()
    {
        if (grabInteractable == null)
            grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("Object grabbed");
        eventToInvoke.Invoke();
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        Debug.Log("Object released");
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }
}