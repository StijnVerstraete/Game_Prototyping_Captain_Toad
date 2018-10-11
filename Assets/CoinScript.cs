using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

    private GameObject _model;
    private MeshRenderer _rend;

    [SerializeField] private bool _isInvisible;
    [SerializeField] private float _visibleStep;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _touchTurnSpeed;
    [SerializeField] private float _showTurnSpeed;
    [SerializeField, Range(0,1)] private float _slowdownTurnStep;
    [SerializeField] private float _invisibleAlpha;
    [SerializeField] private float _jump;
    [SerializeField] private float _grav;

    private Vector3 _startPos;
    private Color _initColor;
    private float _realSpeed;
    private Vector3 _vel;
    private bool _doJump;

    // Use this for initialization
    void Start () {
        _model = transform.GetChild(0).gameObject;
        _rend = _model.transform.GetChild(0).GetComponent<MeshRenderer>();
        _initColor = _rend.material.color;
        if (_isInvisible)
            {
            Color halfClear = _rend.material.color;
            halfClear.a = _invisibleAlpha;
            _rend.material.color = halfClear;
            }
        _realSpeed = _turnSpeed;
        _startPos = _model.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if (!_isInvisible && _rend.material.color.a != 1)
            {
            Color newColor = _rend.material.color;
            newColor.a = Mathf.MoveTowards(newColor.a, _initColor.a, _visibleStep);
            _rend.material.color = newColor;
            }
        if (_doJump)
            {
            _vel.y -= _grav;
            }

        if (_model.transform.localPosition.y < 0)
            {
            Destroy(gameObject);
            }

        _realSpeed = Mathf.Lerp(_realSpeed, _turnSpeed, _slowdownTurnStep);
        _model.transform.Rotate(Vector3.up, _realSpeed, Space.World);

        _model.transform.localPosition += _vel;
	}

    private void OnTriggerExit(Collider other)
        {
        if (other.CompareTag("Player") && other.GetComponent<PlayerScript>())
            {
            if (_isInvisible)
                _realSpeed = _showTurnSpeed;
            _isInvisible = false;
            }
        }

    private void OnTriggerEnter(Collider other)
        {
        if (other.CompareTag("Player") && other.GetComponent<PlayerScript>())
            {
            _realSpeed = _touchTurnSpeed;
            if (!_isInvisible && !_doJump)
                {
                _vel.y = _jump;
                _doJump = true;
                }
            }
        }
    }
