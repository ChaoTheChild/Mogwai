using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPooling : MonoBehaviour
{   
    [SerializeField]
    World world;
    Transform playerTransform;
    WorldGenerator worldGenerator;
 
     GameObject[,] unit_gos;


    void Start(){

        worldGenerator = GameObject.Find("WorldGenerator").GetComponent<WorldGenerator>();
        world = GameObject.Find("WorldGenerator").GetComponent<World>();
//        Debug.Log("GET WORLD!!!");
         Debug.Log(world);

        playerTransform = GameObject.Find("Player").transform;
       // Player.OnPlayerMove += RefreshMap;
      //  World.OnWorldBuild += DisableAll;
       // World.OnWorldBuild += RefreshMap;
        
         
    }

    public void DisableAll(){
         unit_gos = world.unit_gos;
        Debug.Log("disable all units");
        Debug.Log(unit_gos[0,0]);
                Debug.Log(unit_gos[0,1]);


        for(int x = 0; x<world.unitX; x++){
            for(int y = 0; y<world.unitY; y++){
                            Debug.Log(y);

                unit_gos[x,y].SetActive(false);

            }
        }
    }

    void RefreshMap(){
        int playerX = Mathf.RoundToInt(playerTransform.position.x/4);
        int playerZ = Mathf.RoundToInt(playerTransform.position.z/4);
        for(int x = playerX - 10; x < playerX + 10; x++){
            for(int z = playerZ - 10; z < playerZ + 10; z++){
                Debug.Log(unit_gos[x,z]);
                unit_gos[x,z].SetActive(true);
            }
        }
        
    }
}
