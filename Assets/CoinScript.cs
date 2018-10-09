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

    private Vector3 _startPos;
    private Color _initColor;
    private float _realSpeed;

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

        _model.transform.localPosition = Vector3.Lerp(_model.transform.localPosition, _startPos, 0.2f);
        _realSpeed = Mathf.Lerp(_realSpeed, _turnSpeed, _slowdownTurnStep);
        _model.transform.Rotate(Vector3.up, _realSpeed, Space.World);
	}

    private void OnTriggerExit(Collider other)
        {
        if (other.CompareTag("Player") && other.GetComponent<PlayerScript>())
            {
            _realSpeed = _showTurnSpeed;
            if (_isInvisible)
                {
                _model.transform.localPosition = new Vector3(_startPos.x, _startPos.y + _jump, _startPos.z);
                _isInvisible = false;
                }
            }
        }

    private void OnTriggerEnter(Collider other)
        {
        if (other.CompareTag("Player") && other.GetComponent<PlayerScript>())
            {
            _realSpeed = _touchTurnSpeed;
            if (!_isInvisible)
                {
                _model.transform.localPosition = new Vector3(_startPos.x, _startPos.y + _jump, _startPos.z);
                }
            }
        }
    }
