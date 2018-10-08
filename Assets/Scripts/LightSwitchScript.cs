using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchScript : MonoBehaviour {

    GameObject[] platforms;
    public bool isLightOn = false;
    int timer;
    public int becomeVisibleTime;

    private void Start()
    {
        //find all platforms with Platform tag
        platforms = GameObject.FindGameObjectsWithTag("Platform");
    }

    private void FixedUpdate()
    {
        //if light is on, change opacity of all platforms to 0, and start timer
        if (isLightOn == true)
        {
            foreach (GameObject platform in platforms)
            {
                platform.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0f);
            }
            timer++;
        }

        //if light isn't on, change opacity of all platforms to 1
        if (isLightOn == false)
        {
            foreach (GameObject platform in platforms)
            {
                platform.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        //if timer is equal to become visible time, turn light off and reset timer
        if (timer == becomeVisibleTime)
        {
            timer = 0;
            isLightOn = false;
        }
    }

    //if player collides with istrigger collider, set light to on.
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isLightOn = true;
        }
    }
}
