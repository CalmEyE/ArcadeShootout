using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundHandler : MonoBehaviour
{
    public RoundMode currentMode = RoundMode.Idle;
    public List<Character> players = new List<Character>();


    private float _currentIdleTimer = 5.0f;
    public GameObject idleScreen;
    public TextMeshProUGUI countdownTimer;

    private float _currentShowdownTimer = 0.0f;
    private float _currentShowdownTimerMax = 0.0f; //For use in round time indicator
    private Vector2 _showdownTimerMinMax = new Vector2(10.0f, 15.0f);

    private void Start()
    {
        StartIdle();
    }

    private void Update()
    {

        switch (currentMode)
        {
            case RoundMode.Idle:
                Idle();
                break;
            case RoundMode.Showdown:
                Showdown();
                break;
            case RoundMode.Draw:
                break;
            case RoundMode.Resolution:
                break;
            case RoundMode.EndScreen:
                break;
        }


    }

    private void StartIdle()
    {
        _currentIdleTimer = 5.0f;
        for (int i = 0; i < players.Count; i++)
        {
            players[i].EnterIdle();
        }
        idleScreen.SetActive(true);
        currentMode = RoundMode.Idle;
    }
    private void Idle()
    {
        _currentIdleTimer -= Time.deltaTime;
        countdownTimer.text = _currentIdleTimer.ToString("#.0");
        countdownTimer.transform.localScale *= (1 - 0.1f*Time.deltaTime);
        if (_currentIdleTimer < 0)
        {
            idleScreen.SetActive(false);
            StartShowdown();
        }
    }

    private void StartShowdown()
    {
        _currentShowdownTimer = Random.Range(_showdownTimerMinMax.x, _showdownTimerMinMax.y);
        _currentShowdownTimerMax = _currentShowdownTimer;
        for (int i = 0; i < players.Count; i++)
        {
            players[i].EnterShowdown();
        }
        currentMode = RoundMode.Showdown;
    }
    private void Showdown()
    {
        _currentShowdownTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        
    }

    
}

public enum RoundMode
{
    Idle,
    Showdown,
    Draw,
    Resolution,
    EndScreen
}
