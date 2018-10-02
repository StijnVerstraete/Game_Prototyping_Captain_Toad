using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SnapScript : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!Application.isPlaying)
            {
            transform.localScale = new Vector3(Mathf.Round(transform.localScale.x), Mathf.Round(transform.localScale.y), Mathf.Round(transform.localScale.z));
            transform.position = new Vector3((Mathf.Round(transform.position.x * 2)) / 2, (Mathf.Round(transform.position.y * 2)) / 2, (Mathf.Round(transform.position.z * 2)) / 2);
            }
	}
}
