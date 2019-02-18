using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{   
    public static InputController instance;

       void Awake(){
        if(instance == null){
            instance = this;
             DontDestroyOnLoad(this);
        }else{
            Destroy(this);
        }

    }
    public delegate void OnInput();
    public OnInput OnLeftClick;

    void Start(){
        OnLeftClick += null;
    }
    void Update(){
         if(Input.GetMouseButtonDown(0)){
            //Debug.Log("left clicked");
             OnLeftClick();
         }
    }


}
