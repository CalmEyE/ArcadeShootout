using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    public bool backToMain = false;
    private void Update()
    {
        if (backToMain || Input.GetButtonDown("Submit"))
        {
            GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelLoadHandler>().NewLevelToLoad(0);
        }
    }


}
