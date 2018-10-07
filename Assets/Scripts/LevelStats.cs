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

    //ui stat elements on main menu
    public Image Star;
    public Image[] Gem;
    public Text CoinsCollectedText;

    //possible sprites

    [SerializeField]
    private Sprite _noStar;
    [SerializeField]
    private Sprite _star;
    [SerializeField]
    private Sprite _noGem;
    [SerializeField]
    private Sprite _gem;

    //levelselect script
    public LevelSelect LevelSelectScripts;

	void Start ()
    {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelSelect"))
        {
            //enable UI elements
            Star.enabled = true;
            //CoinsCollectedText.enabled = true;
            //foreach (Image Gem in Gem)
            //{
            //    Gem.enabled = true;
            //}
            //update UI elements
            if (LevelCompleted[LevelSelectScripts.LevelSelected] == true)
            {
                Star.sprite = _star;
            }
            else if (LevelCompleted[LevelSelectScripts.LevelSelected] == false)
            {
                Star.sprite = _noStar;
            }
        }
        else
        {
            //disable UI elements
            Star.enabled = false;
            //CoinsCollectedText.enabled = false;
            //foreach (Image Gem in Gem)
            //{
            //  Gem.enabled = false;
            //}
        }
	}
}
