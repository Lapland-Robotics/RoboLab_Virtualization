using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSgeometry = Unity.Robotics.ROSTCPConnector.ROSGeometry;
using PoseWithCovariance = RosMessageTypes.Geometry.MPoseWithCovarianceStamped;

public static class RosToUnity
{
    public static Vector3 RosToUnityConversion(Vector3 vec)
    {
        ROSgeometry.Vector3<ROSgeometry.FLU> temp = new ROSgeometry.Vector3<ROSgeometry.FLU>(vec.x, vec.y, vec.z); //huutista
        return temp.toUnity;
    }
    public static Vector3 RosToUnityConversion(PoseWithCovariance p)
    {
        ROSgeometry.Vector3<ROSgeometry.FLU> temp = new ROSgeometry.Vector3<ROSgeometry.FLU>((float)p.pose.pose.position.x, (float)p.pose.pose.position.y, (float)p.pose.pose.position.z);
        Debug.Log(temp.toUnity);
        return temp.toUnity;
    }
    public static Vector3 UnityToRosConversion(Vector3 vec)
    {
        ROSgeometry.Vector3<ROSgeometry.RUF> temp = new ROSgeometry.Vector3<ROSgeometry.RUF>(vec.x, vec.y, vec.z); //huutista
        return new Vector3(temp.x, temp.y,temp.z);
    }
    public static Quaternion RosToUnityQuaternionConversion(Quaternion q)
    {
        ROSgeometry.Quaternion<ROSgeometry.FLU> temp = new ROSgeometry.Quaternion<ROSgeometry.FLU>(q.x, q.y, q.z, q.w);
        return temp.toUnity;
    }
    public static Quaternion RosToUnityQuaternionConversion(PoseWithCovariance p)
    {
        ROSgeometry.Quaternion<ROSgeometry.FLU> temp = new ROSgeometry.Quaternion<ROSgeometry.FLU>(
            (float)p.pose.pose.orientation.x,
            (float)p.pose.pose.orientation.y,
            (float)p.pose.pose.orientation.z,
            (float)p.pose.pose.orientation.w);

        return temp.toUnity;
    }
    public static Quaternion UnityToRosQuaternionConversion(Quaternion q)
    {
        ROSgeometry.Quaternion<ROSgeometry.RUF> temp = new ROSgeometry.Quaternion<ROSgeometry.RUF>(q.x, q.y, q.z, q.w);
        return new Quaternion(0, 0, 0, 0);
    }
}
