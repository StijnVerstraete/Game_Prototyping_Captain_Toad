using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour {

    GameObject[] platforms;
    float timer;
    [SerializeField] private float _timeStep;


    private void Start()
    {
        //find all platforms with Platform tag
        platforms = GameObject.FindGameObjectsWithTag("Platform");
    }

    private void FixedUpdate()
    {
        //if light is on, change opacity of all platforms to 0, and start timer
            foreach (GameObject platform in platforms)
            {
                platform.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, timer);
            }
        timer = Mathf.MoveTowards(timer, 0, _timeStep);
    }

    public void LightOn()
    {
        timer = 1;
    }
}
