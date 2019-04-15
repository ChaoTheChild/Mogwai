using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroy : Interactable
{
    public Object destroyableObject;
    public GameObject itemPickupPrefab;
    public int health;
    float attackingInterval;
    


   /* public override void Start(){
        base.Start();
   }*/
    public override void Setup(){
      //Debug.Log(destroyableObject.health);
      if(destroyableObject){
            health = destroyableObject.health;
                   destroyableObject.dropped = itemPickupPrefab;


      }
      
      sprites = new Dictionary<string, List<Sprite>>();

       itemPickupPrefab = Resources.Load<GameObject>("Prefabs/Item/ItemPickup");
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
      //Debug.Log("Attacked");
        
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