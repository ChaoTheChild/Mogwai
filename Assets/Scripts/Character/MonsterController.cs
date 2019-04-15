using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField]
      Dictionary <string,Mogwai> mogwaiData;


    Dictionary<string,GameObject> mogwaiPrefabs;

    World world;



    public void LoadMonsterData(){
        mogwaiData = new Dictionary<string, Mogwai>();
        mogwaiPrefabs = new Dictionary<string, GameObject>();

       Mogwai[] mogwais = Resources.LoadAll<Mogwai>("ScriptableObjects/Character/Mogwai");
       foreach(Mogwai m in mogwais){
            mogwaiData[m.name] = m;   

       }


       mogwaiPrefabs = ResourceManagement.SetUpPrefabDictionary("Prefabs/Character/Mogwai"); 

       world = gameObject.GetComponent<World>();
    }

    public void SpawnMogwai(){

        for(int b = 0; b< world.biomes.Length; b++){
            if(world.biomes[b].mogwais.Count > 0){
                //Debug.Log(world.biomesCenter[b]);
                foreach(Mogwai m in world.biomes[b].mogwais){
                    GameObject mogwais = new GameObject();
                    mogwais.name = "mogwais";
                for(int i=0; i<m.spawnNum; i++){
                    GameObject mogwai_go = Instantiate(mogwaiPrefabs[m.name]);
                    mogwai_go.transform.SetParent(mogwais.transform);
                    mogwai_go.transform.position = new Vector3(world.biomesCenter[b].x*4+Random.Range(-10,10),0,world.biomesCenter[b].y*4+Random.Range(-10,10));
                    mogwai_go.GetComponent<Monster>().enabled = false;
                }
                
            }
            }
      
        }
    
    }


}
