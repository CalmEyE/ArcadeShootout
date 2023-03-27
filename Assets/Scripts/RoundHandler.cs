using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundHandler : MonoBehaviour
{
    public RoundMode currentMode = RoundMode.Idle;
    public List<Character> players = new List<Character>();

    //Idle
    private float _currentIdleTimer = 5.0f;
    public GameObject idleScreen;
    public TextMeshProUGUI countdownTimer;

    //Showdown
    public GameObject showdownScreen;
    private float _currentShowdownTimer = 0.0f;
    private float _currentShowdownTimerMax = 0.0f; //For use in round time indicator
    private Vector2 _showdownTimerMinMax = new Vector2(10.0f, 15.0f);
    public List<RectTransform> _yScale = new List<RectTransform>();

    //Draw
    public GameObject drawScreen;
    public GameObject drawObject;
    private float _currentDrawTimer = 0.0f;
    private float _currentDrawTimerMax = 0.0f;
    private bool _allowShoot = false;
    public bool allowShoot
    {
        get
        {
            return _allowShoot;
        }
    }
    public float shotTimer
    {
        get
        {
            return _shootTimer;
        }
    }
    public bool canHit
    {
        get
        {
            return _canHit;
        }
    }
    private bool _canHit = false;
    private float _shootTimer = 0.0f;
    private Vector2 _drawMinMax = new Vector2(2.0f, 6.0f);
    private int _playersFired = 0;
    

    //Resolution

    private void Start()
    {
        showdownScreen.SetActive(false);
        drawScreen.SetActive(false);
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
                Draw();
                break;
            case RoundMode.Resolution:
                Resolution();
                break;
            case RoundMode.EndScreen:
                break;
        }


    }

    #region Idle
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
    #endregion
    #region Showdown
    private void StartShowdown()
    {
        showdownScreen.SetActive(true);
        _currentShowdownTimer = Random.Range(_showdownTimerMinMax.x, _showdownTimerMinMax.y);
        _currentShowdownTimerMax = _currentShowdownTimer;
        for (int i = 0; i < players.Count; i++)
        {
            players[i].EnterShowdown();
        }
        currentMode = RoundMode.Showdown;

        for (int i = 0; i < _yScale.Count; i++)
        {
            _yScale[i].localScale = Vector3.zero;
        }
    }
    private void Showdown()
    {
        _currentShowdownTimer -= Time.deltaTime;

        for (int i = 0; i < _yScale.Count; i++)
        {
            _yScale[i].localScale = new Vector3(1.0f, 1 - (_currentShowdownTimer / _currentShowdownTimerMax), 1.0f);
        }

        if (_currentShowdownTimer < 0)
        {
            showdownScreen.SetActive(false);
            StartDraw();
        }
    }
    #endregion
    #region Draw
    private void StartDraw()
    {
        _currentDrawTimerMax = Random.Range(_drawMinMax.x, _drawMinMax.y);

        for (int i = 0; i < players.Count; i++)
        {
            players[i].EnterDraw();
        }
        drawScreen.SetActive(true);
        drawObject.SetActive(false);
        _allowShoot = false;
        _canHit = false;
        _playersFired = 0;
        currentMode = RoundMode.Draw;
    }
    private void Draw()
    {
        if (_canHit)
        {
            _shootTimer += Time.deltaTime;
        }

        if (_currentDrawTimer < _currentDrawTimerMax)
        {
            _currentDrawTimer += Time.deltaTime;
            if (_currentDrawTimer > 0.5f)
            {
                _allowShoot = true;
            }
        }
        else if (_currentDrawTimer > _currentDrawTimerMax && !_canHit)
        {

            //TODO:Enable draw-text and sound
            drawObject.SetActive(true);
            _canHit = true;
        }

        if(_playersFired >= players.Count)
        {
            drawScreen.SetActive(false);
            StartResolution();
        }
    }
    public void Fired()
    {
        _playersFired++;
    }
    #endregion
    #region Resolution
    private void StartResolution()
    {

    }
    private void Resolution()
    {

    }
    #endregion
}

public enum RoundMode
{
    Idle,
    Showdown,
    Draw,
    Resolution,
    EndScreen
}