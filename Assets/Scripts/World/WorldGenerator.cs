using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{   


    public World world{get;set;}

    int unitX = 180;
    int unitY = 180;
    int numBiomeType = 6;
    int numBiome = 18;
    public MonsterController monsterController;

    [SerializeField]
    GameObject playerPrefab;
    GameObject mainCamera;
    
    // Start is called before the first frame update
    void Start(){
        playerPrefab = Resources.Load<GameObject>("Prefabs/Character/Player/Player");
       // Debug.Log(playerPrefab);

    }
    public void GenerateWorld()
    {   
        
        world = gameObject.GetComponent<World>();
         monsterController = gameObject.GetComponent<MonsterController>();

        StartCoroutine("BuildWorldSeq");

        
    }

    public World GetWorld(){
        return world;
    }

    IEnumerator BuildWorldSeq(){
  
        yield return StartCoroutine("LoadResources"); 
        yield return StartCoroutine("GeneratingWorld");
        yield return StartCoroutine("SetBiomes");
        yield return StartCoroutine("RenderMap");
        yield return StartCoroutine("RenderZones");
        yield return StartCoroutine("RenderStuffs");
        yield return StartCoroutine("PlayerBorn");

        yield return StartCoroutine("RenderMogwai");
        
        //yield return StartCoroutine("FinishWorldBuild");
    }
    
    IEnumerator LoadResources(){
         world.LoadResources();
         monsterController.LoadMonsterData();
//        Debug.Log("Loading Resources");
        yield return null;
    }
    IEnumerator GeneratingWorld(){
        //Debug.Log("Generating World");
        world.GenerateWorld(unitX,unitY,numBiomeType,numBiome);
        yield return null;
    }

    IEnumerator SetBiomes(){

        world.SetBiomes();
        yield return null;
    }

    IEnumerator RenderMap(){
       // Debug.Log("Rendering Map");
        world.DivideWorld();
        world.RenderMap();
        world.DisableAll();

        yield return null;
    }

    IEnumerator RenderZones(){
        world.RenderStartingZone();
        yield return null;
    }

    IEnumerator RenderStuffs(){
         //Debug.Log("Rendering Trees");

        world.RenderStuffs();
        yield return null;
    }

    IEnumerator FinishWorldBuild(){

        world.FinishUp();
        yield return null;
    }
    IEnumerator PlayerBorn(){
        GameObject player = Instantiate(playerPrefab);
        player.name = "Player";
        yield return null;
    }
    

        void RenderMogwai(){
        //Debug.Log("Rendering Mogwai");
        monsterController.SpawnMogwai();
        
    }

  
}
