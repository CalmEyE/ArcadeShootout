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
            if (GameObject.FindGameObjectWithTag("LevelHandler") != null)
            {
                LevelLoadHandler.Instance.NewLevelToLoad(1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }


}
