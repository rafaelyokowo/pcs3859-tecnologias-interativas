using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogueObject", menuName = "ScriptableObjects/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] public DialogueObject previousDialogue;
    [SerializeField] public DialogueObject nextDialogue;
    [SerializeField] public string dialoguePrompt;
    [SerializeField] public List<ResponseObject> responses;
    [SerializeField] public List<int> indexOrder = new List<int> { 0, 1, 2, 3 };
    private void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void RandomizeResponseOrder()
    {
        ShuffleList(indexOrder);
    }
    public ResponseObject GetResponseAtIndex(int index)
    {
        return responses[indexOrder[index]];
    }

    public List<ResponseObject> GetResponses()
    {
        List<ResponseObject> randomizedResponses = new List<ResponseObject>();
        for(int i = 0; i < 4; i++) {
            randomizedResponses.Add(responses[indexOrder[i]]);
        }
        return randomizedResponses;
    }

    public List<string> GetResponseTexts()
    {
        List<string> responseTexts = new List<string>();
        foreach (int index in indexOrder)
        {
            responseTexts.Add(responses[index].response);
        }
        return responseTexts;

    }
}