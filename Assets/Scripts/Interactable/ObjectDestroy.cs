using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroy : Interactable
{
    public Object destroyableObject;
    public GameObject itemPickupPrefab;
    public int health;
    float attackingInterval;
    

    public virtual void Start(){
      //Debug.Log(destroyableObject.health);
       health = destroyableObject.health;
       itemPickupPrefab = Resources.Load<GameObject>("Prefabs/Item/ItemPickup");
       destroyableObject.dropped = itemPickupPrefab;
       attackingInterval = 0;
    }
     public override void Interact(){
        base.Interact();
        if (attackingInterval < 0.1){
          Attacking();
          attackingInterval = 1;
        }else{
          CountDown();
        }
        
        
    }

    public virtual void Attacking(){
      Debug.Log("Attacked");
        
    }

    void CountDown(){
      while(attackingInterval > 0 ){
        attackingInterval -= 0.5f;
      }
    }
    void Die(){
        Invoke("DropItem",1);
       
    }

    void DropItem(){

       if(destroyableObject){
          destroyableObject.DropItem(this.gameObject.transform.position);
        }
         Destroy(gameObject);

    }
}