using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    Dictionary <string,Mogwai> mogwaiData;

    Dictionary<string,List<Sprite>> mogwaiSprites;

    Dictionary<string,GameObject> mogwaiPrefabs;

    World world;



    public void LoadMonsterData(){
        mogwaiData = new Dictionary<string, Mogwai>();
        mogwaiPrefabs = new Dictionary<string, GameObject>();
        mogwaiSprites = new Dictionary<string,List<Sprite>>();

       Mogwai[] mogwais = Resources.LoadAll<Mogwai>("ScriptableObjects/Character/Mogwai");
       foreach(Mogwai m in mogwais){
            mogwaiData[m.name] = m;   

       }


       mogwaiSprites = ResourceManagement.SetUpSpriteDictionary("Sprites/Character/Mogwai",mogwaiSprites);
       mogwaiPrefabs = ResourceManagement.SetUpPrefabDictionary("Prefabs/Character/Mogwai"); 

       world = gameObject.GetComponent<World>();
    }

    public void SpawnMogwai(){

        for(int b = 0; b< world.biomes.Length; b++){
            foreach(Mogwai m in world.biomes[b].mogwais){
                for(int i=0; i<m.spawnNum; i++){
                     GameObject mogwai_go = Instantiate(mogwaiPrefabs[m.name]);
                    mogwai_go.transform.position = new Vector3(world.biomesCenter[b].x+Random.Range(-10,10),0,world.biomesCenter[b].y+Random.Range(-10,10));
                    mogwai_go.GetComponent<Monster>().enabled = false;
                }
                
            }
        }
    
    }


}
