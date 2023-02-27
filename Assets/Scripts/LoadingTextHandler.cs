using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingTextHandler : MonoBehaviour
{

    private TextMeshProUGUI textComp;

    private string _currentText;
    public string textToWrite = "L O A D I N G . . .";
    private float _timer = 0f;
    private float _resetTimer = 0.1f;

    void Start()
    {
        textComp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_timer <= 0 && textToWrite != "")
        {
            _currentText += textToWrite[0];
            textToWrite = textToWrite.Substring(1);
            _timer = _resetTimer;
            textComp.text = _currentText;
        }
        else if (_timer <= 0 && textToWrite == "")
        {
            textToWrite = _currentText;
            _currentText = "";
            _timer = _resetTimer;
            textComp.text = _currentText;
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }
}
