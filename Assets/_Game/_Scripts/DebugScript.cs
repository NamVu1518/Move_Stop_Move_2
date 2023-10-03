using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    public Enemy e;
    public float speed;
    public bool isStop;
    public bool isHit;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = e.speed;
        isStop = e.agent.isStopped;
        isHit = e.IsHited;
    }
}
