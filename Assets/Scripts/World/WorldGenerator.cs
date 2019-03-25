using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{   


    public World world{get;set;}

    int unitX = 60;
    int unitY = 60;
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
        yield return StartCoroutine("CalculateWorld");
        yield return StartCoroutine("SetBiomes");
        yield return StartCoroutine("RenderMap");
        yield return StartCoroutine("RenderTrees");
        yield return StartCoroutine("RenderMogwai");
        yield return StartCoroutine("PlayerBorn");
      //  yield return StartCoroutine("FinishWorldBuild");
    }
    
    IEnumerator LoadResources(){
         world.LoadResources();
         monsterController.LoadMonsterData();
//        Debug.Log("Loading Resources");
        yield return null;
    }
    IEnumerator CalculateWorld(){
        Debug.Log("Generating World");

        world.GenerateWorld(unitX,unitY,numBiomeType,numBiome);
        yield return null;
    }

    IEnumerator SetBiomes(){

        world.SetBiomes();
        yield return null;
    }

    IEnumerator RenderMap(){
       // Debug.Log("Rendering Map");

        world.RenderMap();
        yield return null;
    }

    IEnumerator RenderTrees(){
         //Debug.Log("Rendering Trees");

        world.RenderTrees();
        yield return null;
    }

    IEnumerator RenderMogwai(){
        //Debug.Log("Rendering Mogwai");
        monsterController.SpawnMogwai();
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
  
}
