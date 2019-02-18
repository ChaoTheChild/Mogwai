using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Q)){
            transform.Rotate(new Vector3(0,90,0));
        }
        if(Input.GetKeyDown(KeyCode.E)){
           transform.Rotate(new Vector3(0,-90,0));
        } 
    }
}
