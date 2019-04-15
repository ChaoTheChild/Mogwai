using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    

    World world;

    [SerializeField]
    [Range(0,3)]
    int direction = 0;

    public int indexX;
    public int indexY;

    void Start(){
        
        world = GameObject.Find("WorldGenerator").GetComponent<World>();
    }

    void OnTriggerExit(Collider col){
        if(col.name == "Player"){
           // Debug.Log("player collide");
        switch(direction){
            case 0:             
            world.RenderGoUp();
            break;
            case 1:
            world.RenderGoLeft();
            break;
            case 2:
            world.RenderGoDown();
            break;
            case 3:
            world.RenderGoRight();
            break;
    
        }
       // enabled = false;
          

    }
    }


}
