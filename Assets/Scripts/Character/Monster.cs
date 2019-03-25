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
    public List<SpriteRenderer>  spriteRds;
    IdleDir idleDir;

    public int chaseDistance = 30;
    public int damageDistance = 5;

    public int baseDamage = 2;

     float attackCd;
     float chaseCd;

    bool canAttack = true;
    bool canChase = true;

    Animator monsterAnimator;
    // Start is called before the first frame update
    void Awake(){
        //Debug.Log("Monster start");
        StartCoroutine ("SetUp");    

    }

    IEnumerator SetUp(){
        //Debug.Log("Monster Set up");
        bornLocation = transform.position;
        spriteRds = new List<SpriteRenderer>();
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer s in renderers){
            spriteRds.Add(s);
        }

        idleDir = IdleDir.LEFT;

        chaseDistance = mogwai.detectDis;
        damageDistance = mogwai.damageDis;
        baseDamage = mogwai.baseDmg;
        speed = mogwai.idleSpeed;
        attackCd = mogwai.attackCd;
        chaseCd = mogwai.chaseCd;
        
        monsterAnimator = GetComponentInChildren<Animator>();

        yield return StartCoroutine("FollowSetUp");
    }

    IEnumerator FollowSetUp(){
            RotateSprite();
           UpdateSprite();
           yield return null;

    }
    void OnEnable(){
       // Debug.Log("enabledd");
        UpdateSprite();

        
    }
    void Disabled(){
       // Debug.Log("disabled");
    }
    void FixedUpdate(){
//        Debug.Log(curStat);
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
            Chase();

            break;

            case enemyStat.ATTACK:
            Attack();
            break;
        }
    }



    void FindPlayer(){

        if(GameObject.Find("Player")){
        Vector3 playerPosition =  GameObject.Find("Player").transform.position;
        float distance = Vector3.Distance(transform.position, playerPosition);
        if(distance < damageDistance){
                     
            curStat = enemyStat.ATTACK;
        }else if(distance < chaseDistance){
    
                curStat = enemyStat.CHASE;
            
            curStat = enemyStat.CHASE;
        }else{
            curStat = enemyStat.IDLE;
        }
        }
      
    }

    void Idle(){
        //HARD CODE NOW
         Move();
        monsterAnimator.SetInteger("Stat",0);

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



    void Chase(){
        if(canChase == true){
            canChase = false;
            StartCoroutine("ReChase");
            if(GameObject.Find("Player")){
           Vector3 target = GameObject.Find("Player").transform.position;
        monsterAnimator.SetInteger("Stat",1);
        speedMultiplier = mogwai.chaseSpeed/speed;
        dir = target - transform.position;
        Move();
       }
        }
    }


    void Attack(){
        if(canAttack == true){
            canAttack = false;
            StartCoroutine("ReAttack");
            if(GameObject.Find("Player")){
            Player player = GameObject.Find("Player").GetComponent<Player>();
            monsterAnimator.SetInteger("Stat",2);
            player.TakeDamage(baseDamage);

            }
        }
    

    }

    IEnumerator ReChase(){
        yield return new WaitForSeconds(chaseCd);
       // monsterAnimator.SetInteger("Stat",0);
        canChase = true;
    }
    IEnumerator ReAttack(){
        yield return new WaitForSeconds(attackCd);
         monsterAnimator.SetInteger("Stat",0);

        canAttack = true;
    }

    
    public virtual void RotateSprite(){
                Transform spritesParent = transform.Find("MogwaiSprite");
                spritesParent.Rotate(new Vector3(45,0,0));
    }

    public virtual void UpdateSprite(){
//        Debug.Log(spriteRenderer);

        if(idleDir == IdleDir.LEFT){
            this.transform.localScale = new Vector3(-1,1,1);
        }else if(idleDir == IdleDir.RIGHT){
            this.transform.localScale = new Vector3(1,1,1);
            }         
    
    }

}
