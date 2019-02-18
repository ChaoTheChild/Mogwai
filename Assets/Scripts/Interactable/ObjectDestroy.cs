using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroy : Interactable
{
    public Object destroyableObject;
    public GameObject itemPickupPrefab;
    public int health;
    

    public virtual void Start(){
      //Debug.Log(destroyableObject.health);
       health = destroyableObject.health;
       itemPickupPrefab = Resources.Load<GameObject>("Prefabs/Item/ItemPickup");
       destroyableObject.dropped = itemPickupPrefab;
    }
     public override void Interact(){
        base.Interact();
        Attacking();
        
    }

    public virtual void Attacking(){
      Debug.Log("Attacked");
        
    }
    void Die(){
        Destroy(gameObject);
        Invoke("DropItem",2);
       
    }

    void DropItem(){
       if(destroyableObject){
          destroyableObject.DropItem(this.gameObject.transform.position);
        }
    }
}