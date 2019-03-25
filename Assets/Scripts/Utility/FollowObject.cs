using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform objectToFollow;

    private Vector3 desiredPosition;
    public float speed = 0.2f;

    void Start(){
        FindCameraTarget();

    }

   public void FindCameraTarget(){
        if(GameObject.Find("CameraTarget")){
             objectToFollow = GameObject.Find("CameraTarget").GetComponent<Transform>();

        }
    }


     void LateUpdate(){ 
         if(objectToFollow){
            desiredPosition = objectToFollow.position;
       transform.position = Vector3.Lerp(transform.position,desiredPosition,speed);     
         }     

    }
}
