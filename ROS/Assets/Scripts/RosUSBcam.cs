using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using usb_cam = RosMessageTypes.Sensor.MCompressedImage;
using Unity.Robotics.ROSTCPConnector;
public class RosUSBcam : MonoBehaviour
{
    Texture2D tex;
    public Material mat;

    private void Start()
    {
       tex = new Texture2D(640, 320, TextureFormat.RGB24, false);
       ROSConnection.instance.Subscribe<usb_cam>("/usb_cam/image_raw/compressed", UsbCameraCallBack);
    }
    void UsbCameraCallBack(usb_cam image)
    {
        tex.LoadImage(image.data);
        tex.Apply();
        mat.mainTexture = tex;
    }
}
