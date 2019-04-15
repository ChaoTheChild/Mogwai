using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private float radius = 4.0f;
    Transform player;
    public Weapon interactObject;

    public Dictionary<string, List<Sprite>> sprites;




    void Start(){
        Debug.Log("Interactable Start");
    }
    public virtual void Interact(){
        
    }

    public virtual void Setup(){
        sprites =  new Dictionary<string, List<Sprite>>();
    }


 
    public void Onclicked (Transform playerTransform, Weapon equippedWeapon){
        this.interactObject = equippedWeapon;
         float distance = Vector3.Distance(playerTransform.position, transform.position);
            if(distance <= radius){
                //Debug.Log("Interact");
                Interact();
            }
        
    }
   

    public virtual void OnCollisionEnter(Collision col){
        //Debug.Log("collision enter");
         if(col.gameObject.name == "Player")
        {
            //Debug.Log("player is near me!!");
        }
    }
    public virtual void OnCollisionExit(Collision col){
        //Debug.Log("Collision exit");
         if(col.gameObject.name == "Player")
        {
           // Debug.Log("player is left me!!");
        }
    }
    


    void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
