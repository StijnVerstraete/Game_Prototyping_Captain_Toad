using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private Transform _waypointWorld;
    private Transform _waypointPlayer;

    [SerializeField] private float _easeSpeed = 3;

    [HideInInspector] public enum CameraState {
        World,
        Player
        }
    public CameraState State = CameraState.World;

    private bool _ease = false;
    private float _easeDistance = 0;

	// Use this for initialization
	void Start () {
        //find waypoints
        _waypointWorld = transform.parent;
        _waypointPlayer = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).GetChild(0).GetChild(0);
        //detach camera
        transform.parent = null;
	}
	
	// Update is called once per frame
	void Update ()
        {
        SetPositionRotation();
        if (Input.GetButtonDown("Switch View"))
            {
            ToggleState();
            }
        }

    private void SetPositionRotation()
        {
        //see what the current waypoint is
        Transform curWaypoint = _waypointWorld;
        Transform prevWaypoint = _waypointPlayer;
        if (State == CameraState.Player)
            {
            curWaypoint = _waypointPlayer;
            prevWaypoint = _waypointWorld;
            }

        //do ease
        if (_ease)
            {
            transform.position = Vector3.Lerp(transform.position, curWaypoint.position, _easeSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(prevWaypoint.rotation, curWaypoint.rotation, 1 - (Vector3.Distance(transform.position, curWaypoint.position) / _easeDistance));

            //stop ease
            if (Vector3.Distance(transform.position, curWaypoint.position) <= 0.2f)
                {
                _ease = false;
                }
            }
        else
            {
            if (State == CameraState.Player)
                transform.position = Vector3.Lerp(transform.position, curWaypoint.position, _easeSpeed * Time.deltaTime);
            else
                transform.position = curWaypoint.position;

            transform.rotation = curWaypoint.rotation;
            }
        }

    public void ToggleState()
        {
        //start ease
        _ease = true;
        //change state
        if (State == CameraState.Player)
            {
            State = CameraState.World;
            _easeDistance = Vector3.Distance(transform.position, _waypointWorld.position);
            }
        else
            {
            State = CameraState.Player;
            _easeDistance = Vector3.Distance(transform.position, _waypointPlayer.position);
            }
        }
}
