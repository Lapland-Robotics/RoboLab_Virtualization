using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideRend : MonoBehaviour
{
    public float height = 0.1f;
    private Transform camT;
    public GameObject[] directions;
    private Renderer[] rend;
    private bool _isVisible = false;
    // Start is called before the first frame update
    void Start()
    {
        camT = Camera.main.transform;
        for (int i = 0; i < directions.Length; i++)
        {
            rend[i] = directions[i].GetComponent<Renderer>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(camT.position.x, height, camT.position.z);
    }
}
