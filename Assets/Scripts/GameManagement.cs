using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class GameManagement : MonoBehaviour
{
    WorldGenerator worldGenerator;

    Thread mapManagementThread;

    void Start(){
        mapManagementThread = new Thread(mapManagement);
        worldGenerator = GameObject.Find("WorldGenerator").GetComponent<WorldGenerator>();
        StartGame();
    }

    void StartGame(){
//        Debug.Log("Game Started");
        worldGenerator.GenerateWorld();
    }

    void StartMapLoadingThread(){
        mapManagementThread.Start();
    }

    void mapManagement(){
        //LoadMap
    }

         
}
