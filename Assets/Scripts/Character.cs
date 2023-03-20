using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterMode current_CM;


    
    //Button and Target
    public string button1 = "";
    public Transform target1;

    public string button2 = "";
    public Transform target2;

    public string button3 = "";
    public Transform target3;

    private int _currentTarget = 0;
    private Vector3 _targetTimers = new Vector3();

    public List<GameObject> targetArrows = new List<GameObject>();


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


    private void Idle()
    {
        //Todo: remove this... obviously.
        EnterShowdown();
    }

    public void EnterShowdown()
    {
        current_CM = CharacterMode.Showdown;
        _currentTarget = Random.Range(1, 4);

        for(int i = 0; i < targetArrows.Count; i++)
        {

            targetArrows[i].SetActive(false);

            if (i == _currentTarget - 1)
            {
                targetArrows[i].SetActive(true);
            }
        }

        _targetTimers = Vector3.zero;
    }

    private void Showdown()
    {

        //Change target
        if(Input.GetButton(button1) || Input.GetButton(button2) || Input.GetButton(button3))
        {
            if (Input.GetButtonDown(button1))
            {
                _currentTarget = 1;
            }
            else if (Input.GetButtonDown(button2))
            {
                _currentTarget = 2;
            }
            else if (Input.GetButtonDown(button3))
            {
                _currentTarget = 3;
            }
        }

        for (int i = 0; i < targetArrows.Count; i++)
        {

            targetArrows[i].SetActive(false);

            if (i == _currentTarget - 1)
            {
                targetArrows[i].SetActive(true);
            }
        }

        //Increase looktimer
        switch (_currentTarget)
        {
            case 1:
                _targetTimers.x += Time.deltaTime;
                break;

            case 2:
                _targetTimers.y += Time.deltaTime;
                break;

            case 3:
                _targetTimers.z += Time.deltaTime;
                break;
        }
    }

    private void Draw()
    {

    }

    private void Resulotion()
    {

    }

}

public enum CharacterMode
{
    Idle,
    Showdown,
    Draw,
    Resolution,
}
