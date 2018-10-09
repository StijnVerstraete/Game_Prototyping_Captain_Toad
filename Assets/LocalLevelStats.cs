using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalLevelStats : MonoBehaviour {

    public int CoinsCollected;
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
}
