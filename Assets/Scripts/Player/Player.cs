using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    string curScene;
    private Interactable currentInteractable;

     public delegate void OnPlayerChange();
    public static event OnPlayerChange OnPlayerMove;

    void Awake(){
        if(instance == null){
            instance = this;
             DontDestroyOnLoad(this);
        }else{
            Destroy(this);
        }

    }

    void Start(){
        maincamera = FindObjectOfType<Camera>();
        playerAnimator = GetComponent<Animator>();
        curScene = SceneManagement.GetCurrentSceneName();
        weaponDictionary = ResourceManagement.SetUpWeaponDictionay("ScriptableObjects/Items/Equipable/Weapon");
        CheckIfEquippedWeapon();
            
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
        this.health = 32;
        this.damage = 2;
        this.speed = 10;
        maincamera = GameObject.Find("Camera").GetComponent<Camera>();
        transform.position = new Vector3(100f,0f,100f);
        transform.Find("Root").transform.Rotate(new Vector3(45f,0,0));
        input = GameObject.Find("GameManager").GetComponent<InputController>();
        input.OnLeftClick += Interact; 

        //Debug.Log("root rotation:"+transform.Find("Root").transform.rotation);
        rd = GetComponent<Rigidbody>();
        
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
            playerAnimator.SetInteger("PlayerStat",0);

       }
     

   }

  void Interact(){
      if(!EventSystem.current.IsPointerOverGameObject()){
            Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
          RaycastHit hit;
          if(Physics.Raycast(ray, out hit)){
              //if hit interactable
              //Debug.Log("Hit Collider");
              currentInteractable = hit.collider.GetComponent<Interactable>();
              if(currentInteractable != null){

                  currentInteractable.Onclicked(this.transform, equippedWeapon);
                    playerAnimator.SetInteger("PlayerStat",2);

              }
          }
      
      }
        
  }

  void  CheckIfEquippedWeapon(){
      PlayerSpriteController sc= gameObject.GetComponent<PlayerSpriteController>();
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
