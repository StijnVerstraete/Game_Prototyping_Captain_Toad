using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    private int _levelSelected;

    private bool _inputDelayed = false;

    //relevant UI elements
    public Text LevelNumber;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        //switch level screens
		if (Input.GetAxis("Horizontal") == -1 && _inputDelayed == false)
        {
            _levelSelected -= 1;
            _inputDelayed = true;
        }
        else if (Input.GetAxis("Horizontal") == 1 && _inputDelayed == false)
        {
            _levelSelected += 1;
            _inputDelayed = true;
        }
        else if (Input.GetAxis("Horizontal") == 0)
        {
            _inputDelayed = false;
        }
        //check level boundaries
        if (_levelSelected == -1)
        {
            _levelSelected = 2;
        }
        if (_levelSelected == 3)
        {
            _levelSelected = 0;
        }
        //assign the correct text
        TextAssign();
        LoadLevel();
	}
    private void TextAssign()
    {
        LevelNumber.text = "" + _levelSelected;
    }
    private void LoadLevel()
    {
        if (Input.GetButtonDown("joystick button 2"))
        {
            SceneManager.LoadScene(_levelSelected);
        }
    }
}
