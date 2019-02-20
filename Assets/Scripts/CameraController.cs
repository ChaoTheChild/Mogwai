using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    private Transform lookAt;
    private Camera thisCamera;
    private int angle;
    private float minField = 20f;
    private float maxField = 60f;
    float sensitivity = 10f;


    void Start(){
        lookAt = transform.parent;
        thisCamera = GetComponent<Camera>();
        //transform.position = new Vector3(0f,0f,0f);
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Q)){
            angle = 90;
            OrbitAround(angle);
        }
        if(Input.GetKeyDown(KeyCode.E)){
            angle = -90;
            OrbitAround(angle);
        }

        if(Input.GetAxis("Mouse ScrollWheel") != 0){
                  if(!EventSystem.current.IsPointerOverGameObject()){

            float y = Input.GetAxis("Mouse ScrollWheel");

            if(y<0 && thisCamera.fieldOfView > minField){
                thisCamera.fieldOfView +=y*  sensitivity;
            }
            if(y>0 &&  thisCamera.fieldOfView < maxField)
                 thisCamera.fieldOfView += y*sensitivity;
                  }
        }

       
    }

    void OrbitAround(int angle){
         transform.RotateAround(lookAt.position,Vector3.up,angle);
    }


}
