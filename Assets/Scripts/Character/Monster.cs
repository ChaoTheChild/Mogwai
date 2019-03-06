using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{   
      public enum enemyStat{
        IDLE,
        CHASE,
        ATTACK,
        DIE
    }
    enum IdleDir{
        LEFT,
        RIGHT,
        UP,
        DOWN
        
    }
    
    [SerializeField]    Mogwai mogwai;
    public enemyStat curStat = enemyStat.IDLE;
    Vector3 bornLocation;
    SpriteRenderer  spriteRenderer;
    IdleDir idleDir;

    public int chaseDistance = 30;
    public int damageDistance = 5;
    // Start is called before the first frame update
    void Awake(){
        //Debug.Log("Monster start");
        StartCoroutine ("SetUp");
       
     
    }

    IEnumerator SetUp(){
        //Debug.Log("Monster Set up");
         bornLocation = transform.position;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
         Debug.Log(spriteRenderer);

        idleDir = IdleDir.LEFT;
        yield return StartCoroutine("FollowSetUp");
    }

    IEnumerator FollowSetUp(){
           UpdateSprite();
           yield return null;


    }
    void OnEnable(){
        Debug.Log("enabledd");
        UpdateSprite();

        
    }
    void Disabled(){
        Debug.Log("disabled");
    }
    void Update(){
        Debug.Log(curStat);
        React();
        FindPlayer();
        UpdateSprite();
    }
    

    void React(){
        switch(curStat){
            case enemyStat.IDLE:
            Idle();
            break;

            case enemyStat.CHASE:
            ChasePalayer();
            break;

            case enemyStat.ATTACK:
            Attack();
            break;
        }
    }



    void FindPlayer(){
        Vector3 playerPosition =  GameObject.Find("Player").transform.position;
        float distance = Vector3.Distance(transform.position, playerPosition);
        if(distance < damageDistance){
            curStat = enemyStat.ATTACK;
        }else if(distance < chaseDistance){
            curStat = enemyStat.CHASE;
        }else{
            curStat = enemyStat.IDLE;
        }
    }

    void Idle(){
        //HARD CODE NOW
         Move();
        speedMultiplier = Random.Range(0.5f,1.2f);
        if(idleDir == IdleDir.RIGHT){
             if(transform.position.x < bornLocation.x+Random.Range(4,10)){
                 dir = new Vector3(1,0,0);
             }else{
                 idleDir = IdleDir.UP;
             }
        }else if(idleDir ==  IdleDir.UP){
             if(transform.position.y < bornLocation.x +Random.Range(4,10)){
               dir = new Vector3(0,0,1);
            }else {
                idleDir = IdleDir.LEFT;
            }
        }else if (idleDir== IdleDir.LEFT){
            if(transform.position.x > bornLocation.x-Random.Range(4,10)){
                 dir = new Vector3(1,0,0);

            }else{
                 idleDir = IdleDir.DOWN;
            }
        }else if(idleDir== IdleDir.DOWN){
             if(transform.position.y > bornLocation.y-Random.Range(4,10)){
                 dir = new Vector3(0,0,1);

            }else{
                 idleDir = IdleDir.RIGHT;
            }
        }
    }


    void ChasePalayer(){
       // Debug.Log("Chase");
         Vector3 target = GameObject.Find("Player").transform.position;
         speedMultiplier = Random.Range(1.5f,2.0f);
         dir = target - transform.position;
         Move();         
    }


    void Attack(){
            GameObject player = GameObject.Find("Player");
            Debug.Log("Attack"+ player);
    }



    void UpdateSprite(){
//        Debug.Log(spriteRenderer);
        if(idleDir == IdleDir.LEFT){
            spriteRenderer.flipX = false;
        }else if(idleDir == IdleDir.RIGHT){
            spriteRenderer.flipX = true;
        }
        spriteRenderer.sortingOrder = 1000 - Mathf.RoundToInt(this.transform.position.z/2);
    }
}
