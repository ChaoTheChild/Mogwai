using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPanel : MonoBehaviour
{   

    public RectTransform startPosition;
    public Vector3 moveDirection;
    public RectTransform displayPanel;
    

    bool isActive = false;
    void Start(){
        startPosition = GetComponent<RectTransform>();
        
    
    }
    void Update()
    {   
        if(Input.GetKeyUp(KeyCode.Tab)){
            if(this.isActive == false){
                 startPosition.localPosition += moveDirection;
                isActive = true;
            }else{
                startPosition.localPosition -= moveDirection;
                isActive = false;
            }
        }
    }

    
}
