using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour
{
    public Transform TransformToLookAt;
    public Transform forwardTransform;
    private float speed = 1f;
    public void Start()
    {
        forwardTransform = GameObject.FindGameObjectWithTag("ForwardObject").transform;
        TransformToLookAt = forwardTransform;
    }

    void Update()
    {
        
        Quaternion OriginalRot = transform.rotation;
        transform.LookAt(TransformToLookAt);
        Quaternion NewRot = transform.rotation;
        transform.rotation = OriginalRot;
        transform.rotation = Quaternion.Lerp(transform.rotation, NewRot, speed * Time.deltaTime);

    }

    public void SlowlyLookAt(Transform Target)
    {
        TransformToLookAt = Target;
    }
}