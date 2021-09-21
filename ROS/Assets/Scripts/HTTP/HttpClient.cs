using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpClient : Singleton<HttpClient>
{
    public string IP;
    public IEnumerator HttpGetHedgehog(Action<HedgehogData> onComplete, Action OnError)
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://" + IP + "/hedgehog"))
        {
            yield return www.SendWebRequest();
            switch (www.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    OnError();
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    OnError();
                    break;
                case UnityWebRequest.Result.Success:
                    HedgehogData temp = JsonUtility.FromJson<HedgehogData>(www.downloadHandler.text);
                    onComplete(temp);
                    break;
            }

        }
    }
}