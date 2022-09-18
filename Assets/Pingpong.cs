using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pingpong : MonoBehaviour
{
    public Vector3 to;

    public float time;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(gameObject, to, time).setEase(LeanTweenType.easeInCubic).setLoopPingPong();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
