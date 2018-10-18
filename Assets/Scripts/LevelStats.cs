using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelStats : MonoBehaviour {

    //gems collected per levl
    public int[] GemsCollected;

    //coins collecter per level
    public int[] CoinsCollected;

    //levels completed
    public bool[] LevelCompleted;

    //levelselect script
    public LevelSelect LevelSelectScripts;

    //keeps track if a levelstats already exists
    static public GameObject StatsInstance;

	void Start ()
    {
        //don't destroy on load if a statsinstance doesn't already exist
        if (StatsInstance)
            {
            Destroy(gameObject);
            }
        else
            {
            StatsInstance = gameObject;
            DontDestroyOnLoad(gameObject);
            }
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelSelect") && LevelSelectScripts == null)
        {
            LevelSelectScripts = GameObject.FindGameObjectWithTag("LevelSelectScript").GetComponent<LevelSelect>();
        }
	}
}
