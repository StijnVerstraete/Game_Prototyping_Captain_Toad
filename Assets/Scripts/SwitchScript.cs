using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour {

    LightScript _light;
    bool _inCollider=false;

	// Use this for initialization
	void Start () {
        _light = GameObject.FindGameObjectWithTag("Light").GetComponent<LightScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_inCollider==true && Input.GetKeyDown(KeyCode.E))
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
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            _inCollider = false;
        }
    }
}
