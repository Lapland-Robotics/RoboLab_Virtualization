using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemaCameraControl : MonoBehaviour
{

    public float mouseSensitiny = 1;
    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;
    public InputAsset inputAsset;
    public Transform cameraTarget;
    public Transform cameraTransform;

    public GameObject aimCamera;
    public GameObject moveCamera;
    public GameObject topDownCamera;
    public CinemachineBrain cameraBrain;

    public bool aiming = false;
    public bool topDownView = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yAxis.m_MaxSpeed = mouseSensitiny;
        xAxis.m_MaxSpeed = mouseSensitiny;
        cameraTransform = moveCamera.transform;
        inputAsset.aimEvent += OnAim;
        inputAsset.topDownCameraEvent += OnTopDownView;
        inputAsset.publish2dNawGoalEvent += OnPublish2dNawGoal;
    }
    void LateUpdate()
    {
        xAxis.Update(Time.deltaTime * mouseSensitiny);
        yAxis.Update(Time.deltaTime * mouseSensitiny);
        cameraTarget.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);
    }

    void OnAim(bool state)
    {
        aiming = state;
        if (state && !aimCamera.activeInHierarchy)
        {
            aimCamera.SetActive(true);
            cameraTransform = aimCamera.transform;
        }
        else
        {
            aimCamera.SetActive(false);
            cameraTransform = moveCamera.transform;
        }
    }
    void OnTopDownView(bool state)
    {
        topDownView = state;
        if (state && !topDownCamera.activeInHierarchy)
        {
            topDownCamera.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            topDownCamera.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    void OnPublish2dNawGoal()
    {
        if (cameraBrain.IsBlending)
        {
            return;
        }
        Ray ray = cameraBrain.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, TebPose.tebTransform.position);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Nav2dGoalPublisher.Instance.Send2dNavGoal(point);
            Debug.Log(point);
        }
    }
}
