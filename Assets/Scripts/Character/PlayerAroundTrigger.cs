using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAroundTrigger : MonoBehaviour
{   

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player"){
            Debug.Log("enable the monster");
            transform.parent.GetComponent<Monster>().enabled = true;

        }
    }

    void OnTriggerExit(Collider col){
         if(col.tag == "Player"){
             Debug.Log("Disable the monster");
            transform.parent.GetComponent<Monster>().enabled = false;
        }
    }
}
