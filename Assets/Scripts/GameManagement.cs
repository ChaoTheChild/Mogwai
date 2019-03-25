using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    WorldGenerator worldGenerator;


    void Start(){
        worldGenerator = GameObject.Find("WorldGenerator").GetComponent<WorldGenerator>();
        StartCoroutine("StartGame");
    }

    IEnumerator StartGame(){
//        Debug.Log("Game Started");
        worldGenerator.GenerateWorld();
        yield return null;
    }
         
}
