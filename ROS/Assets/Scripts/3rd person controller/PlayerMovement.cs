using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float speed;
    public float runSpeed;
    public float walkSpeed;
    public bool running = false;
    public GameObject aimCamera;
    public GameObject moveCamera;

    private CharacterController characterController;
    public CinemaCameraControl cinemaCameraControl; // täältä näin aktiivisen kameran transformi sun muut. Löytyy myös tieto tähdätäänkö taikka ollaanko topdown näkymässä.

    public Vector2 inputDir;
    private Vector3 moveDirForAim;

    [SerializeField] InputAsset inputAsset;
    [SerializeField] float targetRotDampTime = .1f;
    private float turnSmoothVelocity;

    #region start/awake/enable

    private void OnEnable()
    {
        inputAsset.moveEvent += OnMove;
        inputAsset.runEvent += OnRun;
    }
    void Start()
    {
        characterController = GetComponent<CharacterController>();  
    }
    #endregion
    void Update()
    {
        if (cinemaCameraControl.topDownView)
        {
            return;
        }

        if(inputDir != Vector2.zero || cinemaCameraControl.aiming)
        {
            float targetRot = ((cinemaCameraControl.aiming) ? cinemaCameraControl.cameraTransform.eulerAngles.y : Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cinemaCameraControl.cameraTransform.eulerAngles.y);
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRot, ref turnSmoothVelocity, !(cinemaCameraControl.aiming) ? targetRotDampTime: 0);
        }
        speed = (running ? runSpeed : walkSpeed) * inputDir.magnitude;

        moveDirForAim = (transform.right * inputDir.x + transform.forward * inputDir.y) * speed;
        characterController.Move(inputDir.magnitude * Time.deltaTime * (cinemaCameraControl.aiming ? moveDirForAim : transform.forward * speed));
    }

    #region input subs
    private void OnMove(Vector2 moveInput)
    {
        inputDir = moveInput.normalized;
    }
    private void OnRun()
    {
        if (!running)
        {
            running = true;
        }
        else
        {
            running = false;
        }
    }
    #endregion
}
