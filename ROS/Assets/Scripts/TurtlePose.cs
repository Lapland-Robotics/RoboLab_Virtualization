using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using PosPos = RosMessageTypes.ROSMsgSrv.MTurtlePose;
//using PosPos = RosMessageTypes.Geometry.MPose;

public class TurtlePose : MonoBehaviour
{
    Vector3 pos;
    Quaternion rot;
    public static Transform kilppari;
    void Start()
    {
        kilppari = this.transform;
        ROSConnection.instance.Subscribe<PosPos>("/turtle1/pose", Position);
    }
    private void FixedUpdate()
    {
        transform.position = pos;
        transform.rotation = rot;
    }
    void Position(PosPos posMessage)
    {

        Debug.Log(posMessage);
        pos = new Vector3(posMessage.x,0.0f,posMessage.y);
        rot = Quaternion.EulerAngles(0.0f,-posMessage.theta, 0.0f);
    }

}
