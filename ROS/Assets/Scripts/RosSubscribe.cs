using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using PosRot = RosMessageTypes.ROSMsgSrv.MPosRot;
 
public class RosSubscribe : MonoBehaviour
{

    void Start()
    {
        ROSConnection.instance.Subscribe<PosRot>("unity", PosChange);
    }

    void PosChange(PosRot posMessage)
    {
        Debug.Log(posMessage);
    }
}
