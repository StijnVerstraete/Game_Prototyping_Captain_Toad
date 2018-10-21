using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour {


	
	// Update is called once per frame
	void Update ()
    {
	    if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelSelect") && Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            Application.Quit();
        }
	}
}
