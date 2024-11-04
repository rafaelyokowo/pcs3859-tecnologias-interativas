using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonHandler : MonoBehaviour
{
    public void OnButtonPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
