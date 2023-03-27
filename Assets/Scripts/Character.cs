using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterMode current_CM;
    [SerializeField] private RoundHandler _rh;
    private int _currentTarget = 0;
    public List<TargetHandler> targetHandler = new List<TargetHandler>();

    private bool _hasFired = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        switch (current_CM)
        {
            case CharacterMode.Idle:
                Idle();
                break;
            case CharacterMode.Showdown:
                Showdown();
                break;
            case CharacterMode.Draw:
                Draw();
                break;
            case CharacterMode.Resolution:
                Resulotion();
                break;
        }
    }

    public void EnterIdle()
    {
        for (int i = 0; i < targetHandler.Count; i++)
        {
            targetHandler[i].indicator.SetActive(false);
        }
        current_CM = CharacterMode.Idle;
    }

    private void Idle()
    {
        
    }

    public void EnterShowdown()
    {
        current_CM = CharacterMode.Showdown;
        _currentTarget = Random.Range(0, 3);

        for(int i = 0; i < targetHandler.Count; i++)
        {

            targetHandler[i].indicator.SetActive(false);
            targetHandler[i].targetTime = 0.0f;

            if (i == _currentTarget)
            {
                targetHandler[i].indicator.SetActive(true);
            }
        }
    }

    private void Showdown()
    {

        //Change target
        if(Input.GetButton(targetHandler[0].button) || Input.GetButton(targetHandler[1].button) || Input.GetButton(targetHandler[2].button))
        {
            if (Input.GetButtonDown(targetHandler[0].button))
            {
                _currentTarget = 0;
            }
            else if (Input.GetButtonDown(targetHandler[1].button))
            {
                _currentTarget = 1;
            }
            else if (Input.GetButtonDown(targetHandler[2].button))
            {
                _currentTarget = 2;
            }
        }

        for (int i = 0; i < targetHandler.Count; i++)
        {

            targetHandler[i].indicator.SetActive(false);

            if (i == _currentTarget)
            {
                targetHandler[i].indicator.SetActive(true);
            }
        }

        //Increase looktimer
        switch (_currentTarget)
        {
            case 0:
                targetHandler[0].targetTime += Time.deltaTime;
                break;

            case 1:
                targetHandler[1].targetTime += Time.deltaTime;
                break;

            case 2:
                targetHandler[2].targetTime += Time.deltaTime;
                break;
        }
    }

    public void EnterDraw()
    {
        current_CM = CharacterMode.Draw;
        _hasFired = false;

        for (int i = 0; i < targetHandler.Count; i++)
        {
            targetHandler[i].shotAt = false;
            targetHandler[i].canHit = false;
            targetHandler[i].shotTime = 0.0f;
        }
    }

    private void Draw()
    {
        if (!_hasFired)
        {
            if (Input.GetButton(targetHandler[0].button) || Input.GetButton(targetHandler[1].button) || Input.GetButton(targetHandler[2].button))
            {
                if (Input.GetButtonDown(targetHandler[0].button))
                {
                    Shot(0);
                }
                else if (Input.GetButtonDown(targetHandler[1].button))
                {
                    Shot(1);
                }
                else if (Input.GetButtonDown(targetHandler[2].button))
                {
                    Shot(2);
                }
            }
        }
    }
    private void Shot(int index)
    {
        if (_rh.allowShoot)
        {
            _hasFired = true;
            targetHandler[index].shotAt = true;
            targetHandler[index].shotTime = _rh.shotTimer;
            targetHandler[index].canHit = _rh.canHit;
            _rh.Fired();
        }
    }

    private void Resulotion()
    {

    }

}

[System.Serializable]
public class TargetHandler
{
    public bool shotAt = false;
    public string button;
    public Transform target;
    public float targetTime;
    public float shotTime;
    public bool canHit;
    public GameObject indicator;

}

public enum CharacterMode
{
    Idle,
    Showdown,
    Draw,
    Resolution,
}
