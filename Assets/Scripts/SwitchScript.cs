using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour {

    LightScript _light;
    bool _inCollider=false;
    private GameObject _canvas;

	// Use this for initialization
	void Start () {
        _light = GameObject.FindGameObjectWithTag("Light").GetComponent<LightScript>();
        _canvas = transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (_inCollider==true && Input.GetButtonDown("Submit"))
        {
            _light.LightOn();
        }
	}

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag=="Player")
        {
            _inCollider = true;
        }
        _canvas.SetActive(true);
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            _inCollider = false;
            }
        _canvas.SetActive(false);
        }
}
