using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    public int LevelSelected;

    private bool _inputDelayed = false;

    //relevant UI elements
    public Image LevelImage;

    //level text assets
    public Sprite[] LevelImageSprites;

    //script
    public LevelStats LevelStatsScript;

    [SerializeField]
    private Sprite _noGem;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        //switch level screens
		if (Input.GetAxis("Horizontal") == -1 && _inputDelayed == false)
        {
            LevelSelected -= 1;
            _inputDelayed = true;
            foreach (Image Gem in LevelStatsScript.Gem)
            {
                Gem.sprite = _noGem;
            }
        }
        else if (Input.GetAxis("Horizontal") == 1 && _inputDelayed == false)
        {
            LevelSelected += 1;
            _inputDelayed = true;
            foreach (Image Gem in LevelStatsScript.Gem)
            {
                Gem.sprite = _noGem;
            }
        }
        else if (Input.GetAxis("Horizontal") == 0)
        {
            _inputDelayed = false;
        }
        //check level boundaries
        if (LevelSelected == 3)
        {
            LevelSelected = 2;
        }
        if (LevelSelected == -1)
        {
            LevelSelected = 0;
        }
        //assign the correct text
        TextAssign();
        LoadLevel();
	}
    private void TextAssign()
    {
        LevelImage.sprite = LevelImageSprites[LevelSelected];
    }
    private void LoadLevel()
    {
        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene(LevelSelected + 1);
        }
    }
}
