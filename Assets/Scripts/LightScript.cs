using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour {

    [HideInInspector] public float Timer;
    [SerializeField] private float _timeStep;
    [SerializeField] private bool _autoLight;
    [SerializeField] private float _autoLightFrequency;

    private MeshRenderer[] _platforms;
    private Light _light;
    private float _intensityStart;


    private void Start()
    {
        //find all platforms with Platform tag
        List<MeshRenderer> meshes = new List<MeshRenderer>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Platform"))
            {
            meshes.Add(obj.GetComponent<MeshRenderer>());
            }
        _platforms = meshes.ToArray();

        _light = this.GetComponent<Light>();
        _intensityStart = _light.intensity;
    }

    private void Update()
    {
        if (_autoLight)
            {
            //automatically fade light in and out
            Timer = Mathf.Sin(Time.frameCount * _autoLightFrequency);
            }
        else
            {
            //fade light out
            Timer = Mathf.MoveTowards(Timer, 0, _timeStep * Time.deltaTime);
            }


        //if light is on, change opacity of all platforms to 0, and start timer
        foreach (MeshRenderer platform in _platforms)
            {

            platform.material.color = new Color(1f, 1f, 1f, Timer);
            if (Timer <= 0.2f)
                {
                platform.enabled = false;
                }
            else
                {
                platform.enabled = true;
                }
            }
        //change actual light component
        _light.intensity = Mathf.Lerp(0, _intensityStart, Timer);

        //DEBUG
        if (Input.GetKeyDown(KeyCode.Joystick1Button6))
            {
            LightOn();
            }
    }

    [ContextMenu("TestSwitch")]
    public void LightOn()
    {
        Timer = 1;
    }
}
