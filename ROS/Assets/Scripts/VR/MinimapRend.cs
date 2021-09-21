using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapRend : MonoBehaviour
{
    public Transform hand;
    public float offset = 0.15f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(hand.position.x, hand.position.y + offset, hand.position.z);
    }
}
