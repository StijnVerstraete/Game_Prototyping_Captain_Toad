using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    [SerializeField] private float _acceleration;
    [SerializeField] private float _drag;
    [SerializeField] private float _maximumXZVelocity = (30 * 1000) / (60 * 60); //[m/s] 30km/h

    [Space, SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;
    [SerializeField] private float _deathHeight = -10;

    private Transform _absoluteTransform;
    private CharacterController _char;
    private MeshRenderer _rend;
    private Transform _model;
    private HashSet<GameObject> _floors = new HashSet<GameObject>();

    private Vector3 _velocity = Vector3.zero; // [m/s]
    private Vector3 _inputMovement;
    private bool _trueAired;


    void Start()
        {
        //attach components
        _char = GetComponent<CharacterController>();
        _rend = transform.GetChild(0).GetComponent<MeshRenderer>();
        _absoluteTransform = Camera.main.transform;
        _model = transform.GetChild(0);

        //dependency error
#if DEBUG
        Assert.IsNotNull(_char, "DEPENDENCY ERROR: CharacterController missing from PlayerScript");
#endif

        }

    private void Update()
        {
        //get movement
        _inputMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        Death();
        }

    private void Death()
        {
        //death
        if (transform.position.y < _deathHeight)
            {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

    void FixedUpdate()
        {
        //call movement-related functions
        ApplyGround();
        ApplyGravity();
        ApplyMovement();
        ApplyDragOnGround();
        LimitXZVelocity();
        FallStraightDown();
        RotateModel();

        //move the character controller according to velocity
        DoMovement();
        }

    //check true aired
    private void OnTriggerEnter(Collider other)
        {
        if (other.gameObject != gameObject)
            {
            _floors.Add(other.gameObject);
            _trueAired = false;
            }
        }
    private void OnTriggerExit(Collider other)
        {
        if (_floors.Remove(other.gameObject))
            {
            _trueAired = _floors.Count <= 0;
            }
        }

    #region MovementFunctions
    private void ApplyGround()
        {
        if (_char.isGrounded)
            {
            //ground velocity
            _velocity -= Vector3.Project(_velocity, Physics.gravity);
            }
        }
    private void ApplyGravity()
        {
        if (!_char.isGrounded)
            {
            //apply gravity
            _velocity += Physics.gravity * Time.deltaTime;
            }
        }
    private void ApplyMovement()
        {
        if (_char.isGrounded)
            {
            //get relative rotation from camera
            Vector3 xzForward = Vector3.Scale(_absoluteTransform.forward, new Vector3(1, 0, 1));
            Quaternion relativeRot = Quaternion.LookRotation(xzForward);

            //move in relative direction
            Vector3 relativeMov = relativeRot * _inputMovement;
            _velocity += relativeMov * _acceleration * Time.deltaTime;
            }

        }
    private void LimitXZVelocity()
        {
        Vector3 yVel = Vector3.Scale(_velocity, Vector3.up);
        Vector3 xzVel = Vector3.Scale(_velocity, new Vector3(1, 0, 1));

        xzVel = Vector3.ClampMagnitude(xzVel, _maximumXZVelocity);

        _velocity = xzVel + yVel;
        }
    private void ApplyDragOnGround()
        {
        if (_char.isGrounded)
            {
            //drag
            _velocity = _velocity * (1 - _drag * Time.deltaTime); //same as lerp
            }
        }
    private void FallStraightDown()
        {
        if (_trueAired)
            {
            _velocity.x = 0;
            _velocity.z = 0;
            }
        }
    private void DoMovement()
        {
        //do velocity / movement on character controller
        Vector3 movement = _velocity * Time.deltaTime;
        _char.Move(movement);
        }
    #endregion

    private void RotateModel()
        {
        //rotate model with velocity
        Vector3 newRot = _model.eulerAngles;
        if (Vector3.Scale(_velocity, new Vector3(1, 0, 1)) != Vector3.zero && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
            {
            newRot.y = Quaternion.Lerp(Quaternion.Euler(newRot), Quaternion.LookRotation(_velocity.normalized), 0.2f).eulerAngles.y;
            newRot.z = _amplitude * Mathf.Sin(_frequency * Time.frameCount);
            newRot.x = 0;
            }
        else
            {
            newRot.z = Quaternion.Lerp(Quaternion.Euler(newRot), Quaternion.identity, 0.2f).eulerAngles.z;
            newRot.x = 0;
            }
        _model.eulerAngles = newRot;
        }
    }
