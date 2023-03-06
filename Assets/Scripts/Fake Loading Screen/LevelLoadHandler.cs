using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadHandler : MonoBehaviour
{
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public static LevelLoadHandler Instance { get; private set; }
    private int _levelToLoad = 1;
    private float _loadTimer = 1.0f;
    private float _loadTimerReset = 2.58f;


    public void NewLevelToLoad(int level)
    {
        _levelToLoad = level;
        SceneManager.LoadSceneAsync(0);
        _loadTimer = _loadTimerReset;
    }

    private void Update()
    {
        if (_levelToLoad >= 1)
        {
            if (_loadTimer < 0)
            {
                SceneManager.LoadSceneAsync(_levelToLoad);
                _levelToLoad = -1;
                _loadTimer = _loadTimerReset;
            }
            else
            {
                _loadTimer -= Time.deltaTime;
            }
        }
    }

}
