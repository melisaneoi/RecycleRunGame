using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 defaultDistance = new(0f, 2f, -10f);
    [SerializeField] float distanceDamp = 10f;    

    Transform myT;

    public Vector3 velocity = Vector3.one;

    public Vector3 DefaultDistance { get => defaultDistance; set => defaultDistance = value; }

    void Awake()
    { myT = transform; }

    void LateUpdate()
    {
        SmoothFollow();
        
    }
    void SmoothFollow()
    {
        Vector3 toPos = target.position + (target.rotation * DefaultDistance);
        Vector3 curPos = Vector3.SmoothDamp(myT.position, toPos, ref velocity, distanceDamp);
        myT.position = curPos;

        myT.LookAt(target, target.up);
    }
}
