using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunLight : MonoBehaviour
{   
    int rotateX;
    void Start()
    {   
        rotateX = 0;
        transform.eulerAngles = new Vector3(rotateX,-30,0);
        InvokeRepeating("SunRotate",0,5);
    }

    // Update is called once per frame

    void SunRotate(){
        if(rotateX < 180) {
           rotateX += 1;
           transform.eulerAngles = new Vector3(rotateX,-30,0);
       }else{
           rotateX = -180;
       }
    }
}
