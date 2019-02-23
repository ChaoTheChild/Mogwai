using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPooling : MonoBehaviour
{
    GameObject worldParent;
    Biome[] biomes;
    Unit[] worldUnits;

    void Start(){

        Player.OnPlayerMove += RefreshMap;
        World.OnWorldBuild += DisableAll;
        World.OnWorldBuild += RefreshMap;
         
    }

    void DisableAll(){
        worldParent = GameObject.Find("worldParent");
        foreach(Transform child in worldParent.transform){
            child.gameObject.SetActive(false);
        }
    }

    void RefreshMap(){

    }
}
