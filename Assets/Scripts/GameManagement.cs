using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    WorldGenerator worldGenerator;
    MapPooling mapPool;


    void Start(){
        worldGenerator = GameObject.Find("WorldGenerator").GetComponent<WorldGenerator>();
        mapPool = gameObject.GetComponent<MapPooling>();
        StartCoroutine("StartGame");
    }

    IEnumerator StartGame(){
//        Debug.Log("Game Started");
        worldGenerator.GenerateWorld();
        yield return null;
    }
         
    IEnumerator MapPool(){
        Debug.Log("map pool time");
        mapPool.DisableAll();
        yield return null;
    }
}
