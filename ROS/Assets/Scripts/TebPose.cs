using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using PoseWithCovariance = RosMessageTypes.Geometry.MPoseWithCovarianceStamped;
using Cmd_vel = RosMessageTypes.Geometry.MTwist;
using ROS_to_unity_converter = Unity.Robotics.ROSTCPConnector.ROSGeometry;
public class TebPose : MonoBehaviour
{
    Vector3 pose;
    Quaternion orientation;
    public float speed = 1.0f;
    float journey = 0;
    public static Transform tebTransform;
    public static Vector2 roboStartPos;
   
    void Start()
    {
        StartCoroutine(HttpClient.Instance.HttpGetHedgehog(InitialPositionInDaRealWorld, InitialPositionInDaRealWorldError)); // wobotin lähtö spotti irl :D

        tebTransform = transform;
        orientation = transform.rotation;
        roboStartPos = new Vector2(transform.position.x, transform.position.y);
        ROSConnection.instance.Subscribe<PoseWithCovariance>("/amcl_pose", Position); // Robot's estimated pose in the map, with covariance.
        //ROSConnection.instance.Subscribe<Cmd_vel>("/cmd_vel", Robo_vel); // Twist viesti, vector3 linear ja angular 
    }
    void Position(PoseWithCovariance posMessage)
    {
        
        pose = new Vector3((float)posMessage.pose.pose.position.x, (float)posMessage.pose.pose.position.z, (float)posMessage.pose.pose.position.y);
        //pose = RosToUnity.RosToUnityConversion(posMessage); // kääntä koordinaatit oikein päin esim rossissa X akeli on eteen ja unityssa taasen Z akseli on eteen.
        //orientation = RosToUnity.RosToUnityQuaternionConversion(posMessage); // samaa mitä yllä.
        orientation = new Quaternion
        (
            (float)posMessage.pose.pose.orientation.w,
            (float)posMessage.pose.pose.orientation.x,
            (float)posMessage.pose.pose.orientation.z,
            (float)posMessage.pose.pose.orientation.y
         );

        journey = Vector3.Distance(transform.position, pose);
        Debug.Log((float)posMessage.pose.pose.position.x + " ," + (float)posMessage.pose.pose.position.y + " ," + (float)posMessage.pose.pose.position.z + " raw msg");
        Debug.Log(pose + " muunnoksen jälkeen");
        Debug.Log(transform.position + " unity robotin pose");
        

    }
    void Robo_vel(Cmd_vel vel)
    {
        //ei mitään vielä
    }
    private void Update()
    {
        float step = (speed * journey) * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pose, step);
        transform.rotation = orientation;
        
    }
    void InitialPositionInDaRealWorld(HedgehogData data)
    {
        Debug.Log(data.X + " " + data.Y + " " + data.Z);
    }
    void InitialPositionInDaRealWorldError()
    {
        Debug.Log("Ei saatu XYZ infoa marvelmind super beacon hedgehogilta");
    }
}
