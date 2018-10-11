using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour {

    GameObject[] _platforms;
    float _timer;
    Light _light;
    [SerializeField] private float _timeStep;
    private float _intensityStart;


    private void Start()
    {
        //find all platforms with Platform tag
        _platforms = GameObject.FindGameObjectsWithTag("Platform");
        _light = this.GetComponent<Light>();
        _intensityStart = _light.intensity;
    }

    private void Update()
    {
        _light.intensity = Mathf.Lerp(0, _intensityStart, _timer);

        //if light is on, change opacity of all platforms to 0, and start timer
            foreach (GameObject platform in _platforms)
            {
                platform.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, _timer);
            }
        _timer = Mathf.MoveTowards(_timer, 0, _timeStep * Time.deltaTime);
    }

    public void LightOn()
    {
        _timer = 1;
    }
}
