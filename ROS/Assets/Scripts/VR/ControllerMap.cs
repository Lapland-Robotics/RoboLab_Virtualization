using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using Valve.VR;

public class ControllerMap : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean mapAction;
    public Animator map;

    // Update is called once per frame
    void Update()
    {
        map.SetBool("MapActive", mapAction.GetState(handType));
    }
}
