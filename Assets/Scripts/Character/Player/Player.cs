using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Player : Character
{   
    public static Player instance;
    Animator playerAnimator;
    //Transform playerTransform;
    public Camera maincamera;
    Weapon equippedWeapon;
    Dictionary<string, Weapon> weaponDictionary;

    InputController input;
    bool isWeaponEquipped = false;
    bool isAttacing  = false;
    string curScene;
    private Interactable currentInteractable;
    private Monster currentMonster;

     public delegate void OnPlayerChange();
    public static event OnPlayerChange OnPlayerMove;
    PlayerSpriteController sc;

    [SerializeField]
    TextMeshProUGUI healthText;

 
    void Awake(){
        if(instance == null){
            instance = this;
             DontDestroyOnLoad(this);
        }else{
            Destroy(this);
        }

    }

    void Start(){
        sc= gameObject.GetComponent<PlayerSpriteController>();

        maincamera = FindObjectOfType<Camera>();
        playerAnimator = GetComponent<Animator>();
        curScene = SceneManagement.GetCurrentSceneName();
        weaponDictionary = ResourceManagement.SetUpWeaponDictionay("ScriptableObjects/Items/Equipable/Weapon");
       // CheckIfEquippedWeapon();

       
            
        switch(curScene){
            case "CharacterCustomization":
            transform.position = new Vector3(-5f,-1f,0);
            SceneManagement.GameSceneLoad += PlayerBorn;
            break;
            case "GameScene":
            PlayerBorn();
            break;
            default:
            Debug.LogError("No default scene");
            break;
        } 

       

    
    }

    private void PlayerBorn()
    {     
        this.health = 100;
        this.damage = 2;
        this.speed = 10;
        maincamera =  GameObject.Find("CameraController").GetComponent<Camera>();
        FollowObject followObject = GameObject.Find("Camera").GetComponent<FollowObject>();
        followObject.FindCameraTarget();
        transform.position = new Vector3(100f,0f,100f);
        transform.Find("Root").transform.Rotate(new Vector3(45f,0,0));
        input = GameObject.Find("GameManager").GetComponent<InputController>();
        input.OnLeftClick += Interact; 

        //Debug.Log("root rotation:"+transform.Find("Root").transform.rotation);
        rd = GetComponent<Rigidbody>();
        healthText =  GameObject.Find("PlayerHealthText").GetComponent<TextMeshProUGUI>();
//        Debug.Log(healthText.name);
         healthText.text = health.ToString();
        
    }


    void Update(){
        GetInput();
        Move();
    }

   void GetInput(){
       float hor = Input.GetAxisRaw("Horizontal");
       float ver = Input.GetAxisRaw("Vertical");
        dir = new Vector3(hor,0,ver);   

       if(hor !=0 || ver!=0){
           playerAnimator.SetInteger("PlayerStat",1);
           if(hor < 0){
               transform.localScale = new Vector3(-1,1,1);
           }else{
               transform.localScale = new Vector3(1,1,1);
           }
           OnPlayerMove();
       }else{
           if(isAttacing == false){
                           playerAnimator.SetInteger("PlayerStat",0);

           }

       }
     

   }

  void Interact(){
      CheckIfEquippedWeapon();
      if(!EventSystem.current.IsPointerOverGameObject()){
            Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
          RaycastHit hit;
          if(Physics.Raycast(ray, out hit)){
              //if hit interactable
              //Debug.Log("Hit Collider");

              if(hit.collider.GetComponent<Interactable>() != null){
                  Debug.Log("hit interactable");
                                      playerAnimator.SetInteger("PlayerStat",2);
                                      isAttacing =  true;
                    StartCoroutine("attackCd");

                  currentInteractable = hit.collider.GetComponent<Interactable>(); 
                  currentInteractable.Onclicked(this.transform, equippedWeapon);

              }else if(hit.collider.GetComponent<Monster>() != null){
                                      playerAnimator.SetInteger("PlayerStat",2); 
                                      isAttacing = true;
                         StartCoroutine("attackCd");


                     currentMonster = hit.collider.GetComponent<Monster>();
                     currentMonster.TakeDamage(equippedWeapon.attackDamageOnCreature);

              }
          }
      
      }
        
  }

  IEnumerator attackCd(){

      yield return new WaitForSeconds(0.6f);
      isAttacing = false;
  }

  void  CheckIfEquippedWeapon(){
      if(sc){
          if(sc.sprd[sc.WeaponIndex]!=null){
          if(sc.sprd[sc.WeaponIndex].sprite != null){
                 isWeaponEquipped = true;
                 //Debug.Log("weapon equipped");
                 string weaponName = sc.sprd[sc.WeaponIndex].sprite.name;
                 //Debug.Log("weapon name is " + weaponName);
                //check which weapn
                if(weaponDictionary[weaponName]!=null){
                    equippedWeapon = weaponDictionary[weaponName];
                    //Debug.Log("weapon" + equippedWeapon + "equipped");
                }else{
                    Debug.Log("cant find weapon in dictionay");
                    return;
                }

          }else{
                isWeaponEquipped = false;

          }
      }else{
          Debug.LogError("Weapon object doesn't exist in player");
          return;
      }
      }
      
  }

  public override void TakeDamage(int damage){
//      Debug.Log("Player Take Damage");
      base.TakeDamage(damage);
        healthText.text = health.ToString();
  }

 

 
}
