using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour
{
    public Transform TransformToLookAt;
    public Transform forwardTransform;
    private float speed = 1f;
    public GameObject dialogueBox;
    private Animator animator;
    public void Start()
    {
        forwardTransform = GameObject.FindGameObjectWithTag("ForwardObject").transform;
        animator = GetComponentInChildren<Animator>();
        TransformToLookAt = forwardTransform;
    }

    void Update()
    {
        
        Quaternion OriginalRot = transform.rotation;
        transform.LookAt(TransformToLookAt);
        Quaternion NewRot = transform.rotation;
        transform.rotation = OriginalRot;
        transform.rotation = Quaternion.Lerp(transform.rotation, NewRot, speed * Time.deltaTime);

        if(dialogueBox.activeInHierarchy)
        {
            if(!animator.GetBool("isTalking"))
            {
                animator.SetBool("isTalking", true);
            }

        }
        else
        {
            if (animator.GetBool("isTalking"))
            {
                animator.SetBool("isTalking", false);
            }

        }

    }

    public void SlowlyLookAt(Transform Target)
    {
        TransformToLookAt = Target;
    }
}