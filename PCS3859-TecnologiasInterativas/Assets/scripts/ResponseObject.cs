using UnityEngine;

[CreateAssetMenu(fileName = "ResponseObject", menuName = "ScriptableObjects/ResponseObject")]
public class ResponseObject : ScriptableObject
{
    [SerializeField] private bool isCorrectResponse;
    [SerializeField] public string response;
    [SerializeField] public string isCorrectMessage;
}