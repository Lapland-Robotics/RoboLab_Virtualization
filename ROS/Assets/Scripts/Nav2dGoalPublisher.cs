using RosMessageTypes.Geometry;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.ROSMsgSrv;

public class Nav2dGoalPublisher : Singleton<Nav2dGoalPublisher>
{
    ROSConnection ros;
    void Start()
    {
        ros = ROSConnection.instance;
    }
    public void Send2dNavGoal(Vector3 goal)
    {
        MPoseStamped temp = new MPoseStamped();
        temp.header.frame_id = "map";
        temp.pose.position.x = goal.x;
        temp.pose.position.z = goal.y;
        temp.pose.position.y = goal.z;
        temp.pose.orientation.w = 1f;
        ros.Send("/move_base_simple/goal", temp);

    }
}
