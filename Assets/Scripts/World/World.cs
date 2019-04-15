using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World: MonoBehaviour
{   

    public Unit[,] units;
    Biome[] stereopTypeBiome;
    public Biome[] biomes;
    public Vector2[] biomesCenter;
    int numBiome;
    int numBiomeType;
    public int unitX{get;set;}
    public int unitY{get;set;}
    
    Dictionary<string, Sprite> tileSprite;

    Dictionary<string,Tree> treeData;

    GameObject worldParent;
    //GameObject[] biomeGos;
    GameObject[,] mapZones;

    int zoneDiv=9;

    GameObject mapTrigger;

    Dictionary<string, GameObject> worldPrefabDictionary;

    Dictionary<string,GameObject> worldItemDictionary;
    public GameObject[,] unit_gos;
    GameObject[,] nature_gos;

    Vector2Int activeCenter = new Vector2Int(4,4);



    public delegate void OnWorldChange();
    public static event OnWorldChange OnWorldBuild;



    public void GenerateWorld(int unitX, int unitY, int numBiomeType,int numBiome){

        this.unitX = unitX;
        this.unitY = unitY;
        this.numBiome = numBiome;
        this.numBiomeType = numBiomeType;
        biomes = new Biome[numBiome];
        biomesCenter = new Vector2[numBiome];
        units = new Unit[unitX,unitY];
        unit_gos = new GameObject[unitX,unitY];
        //biomeGos = new GameObject[numBiome];
        worldParent = new GameObject("worldParent");

       
    }

    public void LoadResources(){
        //Debug.Log("Loading Resources");
        tileSprite = new Dictionary<string,Sprite>();
       // treeSprites = new Dictionary<string,List<Sprite>>();
        //mogwaiSprites = new Dictionary<string,List<Sprite>>();
        
        treeData = new Dictionary<string,Tree>();

        worldPrefabDictionary = new Dictionary<string, GameObject>();

        Sprite[] tilesprites = Resources.LoadAll<Sprite>("Sprites/World/Tile");
        foreach(Sprite s in tilesprites){
            tileSprite[s.name] = s;
        }
        //treeSprites = ResourceManagement.SetUpSpriteDictionary("Sprites/World/Nature/Trees",treeSprites);
        //mogwaiSprites = ResourceManagement.SetUpSpriteDictionary("Sprites/Character/Mogwai", mogwaiSprites);
  
        worldPrefabDictionary = ResourceManagement.SetUpPrefabDictionary("Prefabs/World");
        worldItemDictionary = ResourceManagement.SetUpPrefabDictionary("Prefabs/Item");

        treeData = ResourceManagement.SetUpTreeDictionay("ScriptableObjects/World/Nature/Tree");
    
        stereopTypeBiome = Resources.LoadAll<Biome>("ScriptableObjects/World");

        //Debug.Log("biomes data loaded" + stereopTypeBiome[2]);

    }

#region Render floorTiles

    public void SetBiomes(){
          for(int i=0; i<numBiome; i++){
            int x = Random.Range(0,unitX);
            int y = Random.Range(0,unitY);
            biomesCenter[i] = new Vector2(x,y);
            biomes[i] = new Biome();
            //biomeGos[i] =  new GameObject();
            //biomeGos[i].name = "Biome_" + i;
            for(int j = 0; j<numBiomeType; j++){
                if(j*numBiome/numBiomeType>=i & i<(j+1)*numBiome/numBiomeType){

                    biomes[i] = stereopTypeBiome[j];
                 break;
                }else{
                    biomes [i] = stereopTypeBiome[0];
                }
            }
            
        }
        DefineBiomeForEachUnit();
  

    }
  

    public void DefineBiomeForEachUnit(){
         for ( int x = 0; x < unitX; x++){
            for(int y = 0; y < unitY; y++){
                units[x,y] = new Unit(this,x,y);
                float lowestdistance = unitX*unitY;
                units[x,y].Type = (TileType)0;

                for(int i=0; i<numBiome; i++){
                    float distance = Mathf.Abs(biomesCenter[i].x - x) + Mathf.Abs(biomesCenter[i].y - y);
                    //store the distance if it's closer than the last one
                    if(distance < lowestdistance){
                        lowestdistance = distance; 
                units[x,y].Type = biomes[i].Type;
                units[x,y].ID = i;

                 
                    }
                }

                biomes[units[x,y].ID].units.Add(units[x,y]);
            }

        }
    }


    public void DivideWorld(){
    mapTrigger = worldPrefabDictionary["MapTrigger"];
    mapZones = new GameObject[zoneDiv,zoneDiv];
   
    for(int i=0; i<zoneDiv;i++){{
        for(int j = 0; j<zoneDiv; j++){
        mapZones[i,j] = new GameObject();
        mapZones[i,j].name = "mapZone_" + i + "_" + j;
         GameObject unitsGos = new GameObject();
         unitsGos.name = "Units";
        unitsGos.transform.SetParent(mapZones[i,j].transform);
        GameObject mapTrig = Instantiate(mapTrigger);
        MapTrigger[] maptriggers = mapTrig.GetComponentsInChildren<MapTrigger>();

        foreach(MapTrigger trigger in maptriggers){
            trigger.indexX = i;
            trigger.indexY = j;
        }
    
        mapTrig.transform.SetParent(mapZones[i,j].transform);
        mapTrig.transform.position += new Vector3(i*unitX*4/zoneDiv,0,j*unitY*4/zoneDiv);
    
        }
    }
     
    }
    

}

    public void RenderMap(){
        //Instantiate base TitleUnits
          for ( int x = 0; x < unitX; x++){
            for(int y = 0; y < unitY; y++){
                unit_gos[x,y] = Instantiate(worldPrefabDictionary["Unit"], new Vector3(x*4, 0, y*4), Quaternion.Euler(90,0,0));

                unit_gos[x,y].transform.SetParent(mapZones[x/20,y/20].transform.Find("Units"));
                unit_gos[x,y].name = "Unit_" + x+"_"+y;
                
                //Set 4 basic tileTypes for the units
                for(int i = 0; i<4; i++){
                GameObject tile_go = unit_gos[x,y].transform.GetChild(i).gameObject;
                SpriteRenderer tile_sr = tile_go.GetComponent<SpriteRenderer>();
                string spriteName = units[x,y].Type.ToString() +"_";
                switch(i){
                    case 0:
                    spriteName += "CBL";
                    break;
                    case 1:
                    tile_go.transform.position += new Vector3(2,0,0);
                    spriteName += "CBR";
                    break;
                    case 2:
                    tile_go.transform.position += new Vector3(0,0,2);
                    spriteName += "CTL";
                    break;
                    case 3:
                    tile_go.transform.position += new Vector3(2,0,2);
                    spriteName += "CTR";
                    break;
                    default:
                    Debug.LogError("There's no default case for tile!");
                    break;
                }
                //Debug.Log("Sprite name is:"+spriteName);
                tile_go.name ="Tile_" + spriteName;
                tile_sr.sprite = tileSprite[spriteName];
                }

                }

            }

        /* for(int i =0; i<numBiome; i++){
            biomeGos[i].transform.SetParent(worldParent.transform);
        }*/

         for ( int x = 0; x < unitX; x++){
            for(int y = 0; y < unitY; y++){
                SmoothEdge(x,y);
                SortingLayer(x,y);
            }
        }
        

    }

    public void SmoothEdge(int x, int y){
      //Debug.Log("smooth edge" + x +","+y);
      TileType thisType = units[x,y].Type;
      bool TOP,LEFT,RIGHT,BOTTOM;
      TOP=LEFT=RIGHT=BOTTOM = false;
      //check bottom
     if (y != 0 ){
         if(units[x,y-1].Type != thisType){
             //Create edge object and attach it to its neibour and I am exremly cute
             for(int i=1 ; i<3; i++){
              GameObject go = Instantiate(worldPrefabDictionary["Tile"], new Vector3 (0,0,0), Quaternion.identity) as GameObject;
              string spriteName = thisType.ToString() +"_" + "B" + i;
              SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
              sr.sprite = tileSprite[spriteName];
              go.name = "Tile_" + spriteName;
              go.transform.parent = GameObject.Find(unit_gos[x,y].name).transform;

              if(i==1){
                  go.transform.localPosition= new Vector3(0,-2,0);
                  go.transform.localRotation = Quaternion.identity;
                  //Debug.Log("move to tp left");
              }
              if(i==2){
                  go.transform.localPosition = new Vector3(2,-2,0);
                  go.transform.localRotation = Quaternion.identity;
                  //Debug.Log("move to tp right");
              }

             }
              BOTTOM = true;
         }
     }
     //check left
    if (x != 0 ){
         if(units[x-1,y].Type != thisType){
             //Create edge object and attach it to its neibour and I am exremly cute
             for(int i=1 ; i<3; i++){
              GameObject go = Instantiate(worldPrefabDictionary["Tile"], new Vector3 (0,0,0), Quaternion.identity) as GameObject;
              string spriteName = thisType.ToString() +"_" + "L" + i;
              SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
              sr.sprite = tileSprite[spriteName];
              go.name = "Tile_" + spriteName;
              go.transform.parent = GameObject.Find(unit_gos[x,y].name).transform;

              if(i==1){
                  go.transform.localPosition= new Vector3(-2,2,0.001f);
                  go.transform.localRotation = Quaternion.identity;
                  //Debug.Log("move to right top");
              }
              if(i==2){
                  go.transform.localPosition = new Vector3(-2,0,0.001f);
                  go.transform.localRotation = Quaternion.identity;
                  //Debug.Log("move to right bot");
              }

             }
              LEFT = true;
         }
     }
    //check right
     if (x != unitX -1 ){
         if(units[x+1,y].Type != thisType){
             //Create edge object and attach it to its neibour and I am exremly cute
             for(int i=1 ; i<3; i++){
              GameObject go = Instantiate(worldPrefabDictionary["Tile"], new Vector3 (0,0,0), Quaternion.identity) as GameObject;
              string spriteName = thisType.ToString() +"_" + "R" + i;
              SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
              sr.sprite = tileSprite[spriteName];
              go.name = "Tile_" + spriteName;
              go.transform.parent = GameObject.Find(unit_gos[x,y].name).transform;

              if(i==1){
                  go.transform.localPosition= new Vector3(4,2,0.002f);
                  go.transform.localRotation = Quaternion.identity;
                  //Debug.Log("move to left top");
              }
              if(i==2){
                  go.transform.localPosition = new Vector3(4,0,0.002f);
                  go.transform.localRotation = Quaternion.identity;
                  //Debug.Log("move to left bot");
              }

             }
              RIGHT = true;
         }
     }

    //check top
     if (y != unitY -1 ){
         //Debug.Log("x=" + x +"y="+y);
         if(units[x,y+1].Type != thisType){
             //Create edge object and attach it to its neibour and I am exremly cute
             for(int i=1 ; i<3; i++){
              GameObject go = Instantiate(worldPrefabDictionary["Tile"], new Vector3 (0,0,0), Quaternion.identity) as GameObject;
              string spriteName = thisType.ToString() +"_" + "T" + i;
              SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
              sr.sprite = tileSprite[spriteName];
              go.name = "Tile_" + spriteName;
              go.transform.parent = GameObject.Find(unit_gos[x,y].name).transform;

              if(i==1){
                  go.transform.localPosition= new Vector3(0,4,0.003f);
                  go.transform.localRotation = Quaternion.identity;
                  //Debug.Log("move to bot left");
              }
              if(i==2){
                  go.transform.localPosition = new Vector3(2,4,0.003f);
                  go.transform.localRotation = Quaternion.identity;
                  //Debug.Log("move to bot right");
              }

             }
              TOP = true;
         }
     }
        // add corner baby
        if(TOP&LEFT){
            //Debug.Log("adding topleft corner");
             GameObject go = Instantiate(worldPrefabDictionary["Tile"], new Vector3 (0,0,0), Quaternion.identity) as GameObject;
             string spriteName = thisType.ToString()+"_"+"TL";
             go.GetComponent<SpriteRenderer>().sprite = tileSprite[spriteName];
             go.name = "Tile_"+spriteName;
             go.transform.parent = GameObject.Find(unit_gos[x,y].name).transform;
             go.transform.localPosition= new Vector3(-2,4,0);
            go.transform.localRotation = Quaternion.identity;
        }

        if(TOP&RIGHT){
            //Debug.Log("adding topright corner");
             GameObject go = Instantiate(worldPrefabDictionary["Tile"], new Vector3 (0,0,0), Quaternion.identity) as GameObject;
             string spriteName = thisType.ToString()+"_"+"TR";
             go.GetComponent<SpriteRenderer>().sprite = tileSprite[spriteName];
             go.name = "Tile_"+spriteName;
             go.transform.parent = GameObject.Find(unit_gos[x,y].name).transform;
             go.transform.localPosition= new Vector3(4,4,0);
             go.transform.localRotation = Quaternion.identity;
        }

        if(BOTTOM&LEFT){
           // Debug.Log("adding bottomleft corner");
             GameObject go = Instantiate(worldPrefabDictionary["Tile"], new Vector3 (0,0,0), Quaternion.identity) as GameObject;
             string spriteName = thisType.ToString()+"_"+"BL";
             go.GetComponent<SpriteRenderer>().sprite = tileSprite[spriteName];
             go.name = "Tile_"+spriteName;
             go.transform.parent = GameObject.Find(unit_gos[x,y].name).transform;
             go.transform.localPosition= new Vector3(-2,-2,0);
            go.transform.localRotation = Quaternion.identity;
        }

        if(BOTTOM&RIGHT){
            //Debug.Log("adding bottomright corner");
             GameObject go = Instantiate(worldPrefabDictionary["Tile"], new Vector3 (0,0,0), Quaternion.identity) as GameObject;
             string spriteName = thisType.ToString()+"_"+"BR";
             go.GetComponent<SpriteRenderer>().sprite = tileSprite[spriteName];
             go.name = "Tile_"+spriteName;
             go.transform.parent = GameObject.Find(unit_gos[x,y].name).transform;
             go.transform.localPosition= new Vector3(4,-2,0);
            go.transform.localRotation = Quaternion.identity;
        }




    }



    public void SortingLayer(int x, int y){
        foreach (Transform child in unit_gos[x,y].transform){
        //Debug.Log("get children in unitgameobject["+x+y+"]");
        SpriteRenderer sr = child.gameObject.GetComponent<SpriteRenderer>();
        sr.sortingOrder = x+y;
    }
    unit_gos[x,y].transform.position += new Vector3(0,units[x,y].ID *0.01f,0);
    }

    public void DisableAll(){
        foreach(GameObject zone in mapZones){
            zone.SetActive(false);
        }
    }

    public void RenderStartingZone(){

        for(int x = activeCenter.x - 1; x< activeCenter.x + 2;x++){
            for(int y = activeCenter.y - 1; y< activeCenter.y + 2; y++){
                mapZones[x,y].SetActive(true);
                if(x != activeCenter.x || y!= activeCenter.y){
                    Collider[] colliders = mapZones[x,y].GetComponentsInChildren<Collider>();
                    foreach ( Collider col in colliders){
                        col.enabled = false;
                    }
                }


            }
        }
    }

    public void RenderGoRight(){
       Debug.Log("render goes right");
        StartCoroutine(RefreshTrigger(1,0));


    }
    public void RenderGoLeft(){
        Debug.Log("render goes left");
        StartCoroutine(RefreshTrigger(-1,0));

    }

    public void RenderGoUp(){
        Debug.Log("render goes up");
        StartCoroutine(RefreshTrigger(0,1));

    }

    public void RenderGoDown(){
        Debug.Log("render goes down");
        StartCoroutine(RefreshTrigger(0,-1));

       
    }


    IEnumerator RefreshTrigger(int difX, int difY){
        yield return StartCoroutine(DisableCenterTrigger());
        yield return StartCoroutine(RefreshCenter(difX,difY));
        yield return StartCoroutine(RefreshZones());
        yield return StartCoroutine(EnableCenterTrigger());
    }
        

    IEnumerator DisableCenterTrigger(){
        Debug.Log("disable center trigger");
         Collider[] colliders = mapZones[activeCenter.x,activeCenter.y].GetComponentsInChildren<Collider>();
        foreach(Collider col in colliders){
            col.enabled = false;
        }
        yield return null;
    }

    IEnumerator RefreshCenter(int difX, int difY){
        Debug.Log("refresh Center");
        activeCenter += new Vector2Int(difX,difY);
        yield return null;

    }

    IEnumerator RefreshZones(){
        Debug.Log("refresh zones");
         for(int x = activeCenter.x - 1; x< activeCenter.x + 2;x++){
              int y = activeCenter.y;
                mapZones[x,y-2].SetActive(false);
                mapZones[x,y+2].SetActive(false);
                }


        for(int y = activeCenter.y -1; y< activeCenter.y + 2; y++){
            int x = activeCenter.x;
                   mapZones[x-2,y].SetActive(false);
                   mapZones[x+2,y].SetActive(false);
             }
        for(int x = activeCenter.x - 1; x< activeCenter.x + 2;x++){
            for(int y = activeCenter.y -1; y< activeCenter.y + 2; y++){
        mapZones[x,y].SetActive(true);
        Collider[] colliders = mapZones[x,y].GetComponentsInChildren<Collider>();
        foreach(Collider col in colliders){
            col.enabled = false;
        }
    }
}        

yield return null;

    }
    IEnumerator EnableCenterTrigger(){
        Debug.Log("enable Center trigger");

        Collider[] colliders = mapZones[activeCenter.x,activeCenter.y].GetComponentsInChildren<Collider>();
        foreach(Collider col in colliders){
            col.enabled = true;
        }
        yield return null;

    }

#endregion


//render Tree
public void RenderStuffs(){
   
    for(int i=0; i<zoneDiv; i++){
        for (int j = 0; j<zoneDiv;j++){
    GameObject tree_parent = new GameObject();
    tree_parent.name = "Trees"; 
    GameObject item_parent = new GameObject();
    item_parent.name = "Items";
    GameObject mogwaiSpawners = new GameObject();
    mogwaiSpawners.name = "MogwaiSpawners";

    tree_parent.transform.SetParent(mapZones[i,j].transform);
    item_parent.transform.SetParent(mapZones[i,j].transform);
    mogwaiSpawners.transform.SetParent(mapZones[i,j].transform);




    for(int x = 0; x< unitX/zoneDiv; x++){
            for(int y=0; y<unitY/zoneDiv; y++){
                //Debug.Log("check the units");
                float rand = Random.value;
                int unitx = unitX*i/zoneDiv + x;
                int unity = unitY*j/zoneDiv + y;
                int id = units[unitx,unity].ID;


                //Render Tree
                for(int w = 0; w< biomes[id].trees.Count; w++){

                    float val = Random.value;
                    if(val < biomes[id].trees[w].bornChance && units[unitx,unity].isOccupied == false){

                        units[unitx,unity].Tree = biomes[id].trees[w];
                        units[unitx,unity].isOccupied = true;

            GameObject tree_go = Instantiate(worldPrefabDictionary["Tree"],Vector3.zero,Quaternion.identity);
            tree_go.name = units[unitx,unity].Tree.name;
           tree_go.transform.SetParent(tree_parent.transform);
           tree_go.transform.position = new Vector3(unitx*4,0.9f,unity*4);

           TreeObject treeObject = tree_go.GetComponent<TreeObject>();
         
            treeObject.destroyableObject = units[unitx,unity].Tree;
            treeObject.Setup();

            tree_go.GetComponentInChildren<SpriteRenderer>().sprite = treeObject.sprites[tree_go.name][Random.Range(0,treeObject.sprites[tree_go.name].Count)];
            tree_go.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1000 - units[unitx,unity].Y*2;

                    }
                }
                //Render Items
                if(biomes[id].items.Count > 0){
                    for(int w = 0; w<biomes[id].items.Count; w++){
                    float val = Random.value;
                    if(val < biomes[id].items[w].dropChance && units[unitx,unity].isOccupied == false){
                        units[unitx,unity].Item = biomes[id].items[w];
                    
                        units[unitx,unity].isOccupied = true;

                        GameObject item_go = Instantiate(worldItemDictionary["ItemPickup"],Vector3.zero,Quaternion.identity);
                        item_go.name = units[unitx,unity].Item.name;
                        item_go.transform.SetParent(item_parent.transform);
                        item_go.transform.position = new Vector3(unitx*4,0f,unity*4);

                        ItemPickup itemPickup = item_go.GetComponent<ItemPickup>();
                        itemPickup.item = units[x*i,y*j].Item;
                        itemPickup.Setup();
                        
                        itemPickup.item = units[unitx,unity].Item;

                        item_go.GetComponentInChildren<SpriteRenderer>().sprite = itemPickup.sprites[item_go.name][Random.Range(0,itemPickup.sprites[item_go.name].Count)];
                        item_go.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1000 - units[unitx,unity].Y*2;


                    }
                }
                }

                //Render mogwaiSpanwers
               /*if(biomes[id].mogwaispawner.Count > 0){
                    
                } */ 
               


            }
        }

        }
    }

}







public void FinishUp(){
    OnWorldBuild();
}

  
}