using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectAttachDetector : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnObjectGrabbed);
        grabInteractable.selectExited.AddListener(OnObjectReleased);
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        grabInteractable.selectEntered.RemoveListener(OnObjectGrabbed);
        grabInteractable.selectExited.RemoveListener(OnObjectReleased);
    }

    private void OnObjectGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log($"Attached!");
    }

    private void OnObjectReleased(SelectExitEventArgs args)
    {
        Debug.Log($"Released!");
    }

}
