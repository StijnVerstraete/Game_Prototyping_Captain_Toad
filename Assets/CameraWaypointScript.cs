using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWaypointScript : MonoBehaviour {

    [SerializeField] private Vector2 _rotationSpeed;
    [SerializeField] private Vector2 _clamp;
    [SerializeField] private bool _isWorld;
    private CameraScript _cam;
    private Transform _xAxis;
    private Transform _player;

	// Use this for initialization
	void Start () {
        _xAxis = transform.GetChild(0);
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _cam = Camera.main.GetComponent<CameraScript>();

        if (_isWorld)
            {
            Vector3 newRot = Quaternion.LookRotation(_player.forward).eulerAngles;
            newRot.x = 0;
            newRot.z = 0;
            transform.eulerAngles = newRot;
            }
	}
	
	// Update is called once per frame
	void Update ()
        {
        //is camera active
        if ((_isWorld && _cam.State == CameraScript.CameraState.World) || (!_isWorld && _cam.State == CameraScript.CameraState.Player))
            {
            if (_isWorld)
                {
                //world
                if (Vector3.Distance(Vector3.zero, _player.position) < 8)
                    {
                    transform.position = Vector3.Lerp(transform.position, Vector3.zero + (Vector3.Scale(_player.position, new Vector3(1, 0, 1)) / 3f), Time.deltaTime * 5);
                    }
                else
                    {
                    transform.position = Vector3.Lerp(transform.position, Vector3.zero + (Vector3.Scale(_player.position, new Vector3(1, 0, 1)) / 1.5f), Time.deltaTime * 5);
                    //transform.position = Vector3.zero + (Vector3.Scale(_player.position, new Vector3(1, 0, 1)));
                    }
                }
            }
        else //inactive
            {

            //player
            //if (!_isWorld)
            //    {
            //    transform.rotation = Quaternion.identity;
            //    }
            }

        transform.Rotate(Vector3.up, Input.GetAxisRaw("HorizontalCam") * _rotationSpeed.x);

        //clamp rotation
        Vector3 newRot = _xAxis.localEulerAngles;
        newRot.x += Input.GetAxisRaw("VerticalCam") * _rotationSpeed.y;
        newRot.x = ClampAngle(newRot.x, _clamp.x, _clamp.y);
        _xAxis.localEulerAngles = newRot;

        }

    public static float ClampAngle(float angle, float min, float max)
        {
        angle = Mathf.Repeat(angle, 360);
        min = Mathf.Repeat(min, 360);
        max = Mathf.Repeat(max, 360);
        bool inverse = false;
        var tmin = min;
        var tangle = angle;
        if (min > 180)
            {
            inverse = !inverse;
            tmin -= 180;
            }
        if (angle > 180)
            {
            inverse = !inverse;
            tangle -= 180;
            }
        var result = !inverse ? tangle > tmin : tangle < tmin;
        if (!result)
            angle = min;

        inverse = false;
        tangle = angle;
        var tmax = max;
        if (angle > 180)
            {
            inverse = !inverse;
            tangle -= 180;
            }
        if (max > 180)
            {
            inverse = !inverse;
            tmax -= 180;
            }

        result = !inverse ? tangle < tmax : tangle > tmax;
        if (!result)
            angle = max;
        return angle;
        }
    }
