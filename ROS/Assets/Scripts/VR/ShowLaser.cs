using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ShowLaser : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean teleportAction;

    private Vector3 hitPoint;
    public LineRenderer laser;


    public Transform cameraRigTransform;
    public Transform headTransform;
    public GameObject teleTarget;
    public Vector3 teleportReticleOffset;
    public LayerMask teleportLayer;
    void Start()
    {
        teleTarget.SetActive(false);
    }
    void Update()
    {
        if (teleportAction.GetState(handType))
        {
            RaycastHit hit;
            RaycastHit hitb;

            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100, teleportLayer))
            {
                if (Physics.Raycast((hit.point+new Vector3(0,1,0)), Vector3.down, out hitb, 8, teleportLayer))
                {
                    hitPoint = hitb.point;
                    teleTarget.SetActive(true);
                    teleTarget.transform.position = hitb.point;
                    //DrawLaser(hit.point, hitb.point);
                }

            }
        }
        else
        {
            laser.enabled = false;
            teleTarget.SetActive(false);
        }
        if (teleportAction.GetStateUp(handType))
        {
            Teleport();
        }
    }
    private void Teleport()
    {
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        difference.y = 0;
        cameraRigTransform.position = hitPoint + difference;
    }
}
