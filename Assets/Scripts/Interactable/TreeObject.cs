using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject : ObjectDestroy
{
    Animator treeAnimator;

    int attackDamage;


    
    public override void Setup(){
         base.Setup();

          treeAnimator = GetComponent<Animator>();

          sprites = ResourceManagement.SetUpSpriteDictionary("Sprites/World/Nature/Trees",sprites);
    }
    
      
    public override void Attacking(){
        base.Attacking();

        if(interactObject){
             attackDamage =  interactObject.attackDamageOnTrees;
        }else{
            attackDamage = 1;
        }
       
        if(health > 0){
            //Debug.Log("Taking Damage "+attackDamage+"and current health is :" + health);
            health -= attackDamage;
            StartCoroutine("Attacked");
        }else{
            //Debug.Log("health below zero");
            //destroyableObject.DropItem(transform.position);
            treeAnimator.SetInteger("TreeStat",2);
            Invoke("Die",1);
            }
    }

    
    IEnumerator Attacked(){
    treeAnimator.SetInteger("TreeStat",1);
    yield return new WaitForSeconds(1);
    treeAnimator.SetInteger("TreeStat",0);

    }
}
