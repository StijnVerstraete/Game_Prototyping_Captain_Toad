using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LocalLevelStats : MonoBehaviour {

    public int CoinsCollected = 0;
    public int GemsCollected = 0;
    private LevelStats _stats;
    [SerializeField] private Text _coinText;

	// Use this for initialization
	void Start () {
        _stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<LevelStats>();
	}
	
	// Update is called once per frame
	void Update () {
        _coinText.text = CoinsCollected.ToString();
	}

    public void LevelCompletion()
        {
        if (_stats)
            {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
            //update coins
            if (_stats.CoinsCollected[sceneIndex] < CoinsCollected)
                {
                _stats.CoinsCollected[sceneIndex] = CoinsCollected;
                }
            //update gems
            if (_stats.GemsCollected[sceneIndex] < GemsCollected)
                {
                _stats.GemsCollected[sceneIndex] = GemsCollected;
                }
            }
        }
}
