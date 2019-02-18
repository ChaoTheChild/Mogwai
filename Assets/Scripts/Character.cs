﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{   

    public float speed = 2.0f;
    public Rigidbody rd;
    public int health;
    public int damage;
    //public GameObject bloodEffect;

    protected Vector3 dir;
    private Animator animator; 
    private Transform spritesTransform;



    void Start(){
        animator = GetComponent<Animator>();
 
    }
 
    protected void Move(){
        //Debug.Log(dir);
        if(dir != Vector3.zero){
            transform.Translate(dir * speed *Time.deltaTime);
        }else{
            rd.velocity = Vector3.Lerp(rd.velocity,Vector3.zero,2f);
        }
        
        
       // SetAnimation();
    }
    protected void Die(){   
            Destroy(gameObject);
    }
    public void SetAnimation(){
       animator.SetFloat("x",dir.x);
       animator.SetFloat("z",dir.z);
    }

  

     public void TakeDamage(int damage){
         //Instantiate(bloodEffect,transform.position,Quaternion.identity);
         health -= damage;
         Debug.Log("Damage Taken");
     }
    
}