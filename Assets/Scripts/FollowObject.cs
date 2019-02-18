using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform objectToFollow;

    private Vector3 desiredPosition;
    public float speed = 0.2f;

    void Start(){
        objectToFollow = GameObject.Find("CameraTarget").GetComponent<Transform>();
    }
     void LateUpdate(){      
       desiredPosition = objectToFollow.position;
       transform.position = Vector3.Lerp(transform.position,desiredPosition,speed);     
    }
}
