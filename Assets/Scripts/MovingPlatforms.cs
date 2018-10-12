using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour {

    private GameObject _player;
    public Transform[] WayPoints;
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private int _targetIndex = 0;

    private float _speed = 1f;
	void Start ()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _target = WayPoints[1];

        transform.position = WayPoints[0].position;
	}
    private void Update()
    {
        
        if (transform.position == _target.position)
        {
            _targetIndex += 1;
            if (_targetIndex > 1) { _targetIndex = 0; }
            _target = WayPoints[_targetIndex];
        }
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.fixedDeltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            _player.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
        {
            _player.transform.parent = null;
        }
    }
}
