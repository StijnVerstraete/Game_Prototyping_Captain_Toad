using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarPickup : MonoBehaviour {

    public LocalLevelStats LocalStats;
    private LevelStats _levelStats;
    private void Start()
    {
        _levelStats = GameObject.FindGameObjectWithTag("Stats").GetComponent<LevelStats>();
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_levelStats.GemsCollected[SceneManager.GetActiveScene().buildIndex - 1] < LocalStats.GemsCollected)
        {
            _levelStats.GemsCollected[SceneManager.GetActiveScene().buildIndex - 1] = LocalStats.GemsCollected;
        }
        if (_levelStats.CoinsCollected[SceneManager.GetActiveScene().buildIndex - 1] < LocalStats.CoinsCollected)
        {
            _levelStats.CoinsCollected[SceneManager.GetActiveScene().buildIndex - 1] = LocalStats.CoinsCollected;
        }
        _levelStats.LevelCompleted[SceneManager.GetActiveScene().buildIndex-1] = true;

        SceneManager.LoadScene(0);
    }
}
