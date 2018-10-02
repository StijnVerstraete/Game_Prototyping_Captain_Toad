using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    [SerializeField] private Vector2 _rotationSpeed;
    [SerializeField] private Vector2 _clamp;
    private Transform _xAxis;

	// Use this for initialization
	void Start () {
        _xAxis = transform.GetChild(0);	
	}
	
	// Update is called once per frame
	void Update ()
        {
        transform.Rotate(Vector3.up, Input.GetAxisRaw("HorizontalCam") * _rotationSpeed.x);
        _xAxis.Rotate(Vector3.right, Input.GetAxisRaw("VerticalCam") * _rotationSpeed.y);

        //clamp rotation
        Vector3 newRot = _xAxis.eulerAngles;
        newRot.x = ClampAngle(newRot.x, _clamp.x, _clamp.y);
        _xAxis.eulerAngles = newRot;

        //fix weird angle bug

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
