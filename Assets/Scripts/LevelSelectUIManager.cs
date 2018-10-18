using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectUIManager : MonoBehaviour {

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

    //levelstats
    [SerializeField]
    private LevelStats _levelStats;

    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_levelStats == null)
        {
            _levelStats = GameObject.FindGameObjectWithTag("Stats").GetComponent<LevelStats>();
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelSelect"))
        {
            //enable UI elements
            Star.enabled = true;
            CoinsCollectedText.enabled = true;
            foreach (Image Gem in Gem)
            {
                Gem.enabled = true;
            }
            //update UI elements
            if (_levelStats.LevelCompleted[_levelStats.LevelSelectScripts.LevelSelected] == true)
            {
                Star.sprite = _star;
                CoinsCollectedText.text = "Best Coin Run:" + _levelStats.CoinsCollected[_levelStats.LevelSelectScripts.LevelSelected];
                for (int i = 0; i < _levelStats.GemsCollected[_levelStats.LevelSelectScripts.LevelSelected]-1; i++)
                {
                    Gem[i].sprite = _gem;
                }
            }
            else if (_levelStats.LevelCompleted[_levelStats.LevelSelectScripts.LevelSelected] == false)
            {
                Star.sprite = _noStar;
                CoinsCollectedText.text = "";
                foreach (Image Gem in Gem)
                {
                    Gem.sprite = _noGem;
                }
            }
        }
    }
}
