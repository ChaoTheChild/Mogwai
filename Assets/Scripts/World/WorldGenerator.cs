using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{   

    World world;
   public static WorldGenerator instance;

    int unitX = 60;
    int unitY = 60;
    int numBiomeType = 6;
    int numBiome = 18;
    
    // Start is called before the first frame update
       void Awake(){
        if(instance == null){
            instance = this;
             DontDestroyOnLoad(this);
        }else{
            Destroy(this);
        }

    }
    void Start()
    {   
        
        world = new World(); 
        StartCoroutine("BuildWorldSeq");
        
    }

    IEnumerator BuildWorldSeq(){
  
        yield return StartCoroutine("LoadResources"); 
        yield return StartCoroutine("GenerateWorld");
        yield return StartCoroutine("SetBiomes");
        yield return StartCoroutine("RenderMap");
        yield return StartCoroutine("RenderTrees");
    }
    
    IEnumerator LoadResources(){
         world.LoadResources();
        Debug.Log("Loading Resources");
        yield return null;
    }
    IEnumerator GenerateWorld(){
        Debug.Log("Generating World");

        world.GenerateWorld(unitX,unitY,numBiomeType,numBiome);
        yield return null;
    }

    IEnumerator SetBiomes(){
         Debug.Log("Setting Biomes");

        world.SetBiomes();
        yield return null;
    }

    IEnumerator RenderMap(){
        Debug.Log("Rendering Map");

        world.RenderMap();
        yield return null;
    }

    IEnumerator RenderTrees(){
         Debug.Log("Rendering Trees");

        world.RenderTrees();
        yield return null;
    }
  
}
