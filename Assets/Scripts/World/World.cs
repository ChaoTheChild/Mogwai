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
    Dictionary<string, List<Sprite>> treeSprites;

    Dictionary<string,List<Sprite>> mogwaiSprites;
    Dictionary<string,Tree> treeData;

    GameObject worldParent;
    GameObject[] biomeGos;

    Dictionary<string, GameObject> worldPrefabDictionary;
    public GameObject[,] unit_gos;
    GameObject[,] nature_gos;


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
        biomeGos = new GameObject[numBiome];
        worldParent = new GameObject("worldParent");

       
    }

    public void LoadResources(){
        //Debug.Log("Loading Resources");
        tileSprite = new Dictionary<string,Sprite>();
        treeSprites = new Dictionary<string,List<Sprite>>();
        mogwaiSprites = new Dictionary<string,List<Sprite>>();
        
        treeData = new Dictionary<string,Tree>();

        worldPrefabDictionary = new Dictionary<string, GameObject>();

        Sprite[] tilesprites = Resources.LoadAll<Sprite>("Sprites/World/Tile");
        foreach(Sprite s in tilesprites){
            tileSprite[s.name] = s;
        }
        treeSprites = ResourceManagement.SetUpSpriteDictionary("Sprites/World/Nature/Trees",treeSprites);
        //mogwaiSprites = ResourceManagement.SetUpSpriteDictionary("Sprites/Character/Mogwai", mogwaiSprites);
  
        worldPrefabDictionary = ResourceManagement.SetUpPrefabDictionary("Prefabs/World");

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
            biomeGos[i] =  new GameObject();
            biomeGos[i].name = "Biome_" + i;
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

    public void RenderMap(){
        //Instantiate base TitleUnits
          for ( int x = 0; x < unitX; x++){
            for(int y = 0; y < unitY; y++){
                unit_gos[x,y] = Instantiate(worldPrefabDictionary["Unit"], new Vector3(x*4, 0, y*4), Quaternion.Euler(90,0,0));
                unit_gos[x,y].transform.SetParent(biomeGos[units[x,y].ID].transform);
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

        for(int i =0; i<numBiome; i++){
            biomeGos[i].transform.SetParent(worldParent.transform);
        }

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

    public void RenderEdge(){
        //Left
        for(int x = -4; x< 0 ; x++){
            for(int y = -4; y < unitY + 4; y++){
                
            }
        }
    }

  

#endregion


//render Tree
public void RenderTrees(){
    GameObject tree_parent = new GameObject();
    tree_parent.name = "Trees"; 
    
    for(int i=0; i<numBiome; i++){
     
        for(int j = 0; j< biomes[i].trees.Count; j++){
        float bornChance = biomes[i].trees[j].bornChance;
        int bornNum = 0;
        if(bornChance != 0){
             bornNum = Mathf.RoundToInt(biomeGos[i].transform.childCount*bornChance);
        }else {
             bornNum =  biomes[i].trees[j].bornNum;
        }
       // Debug.Log("gonna generate " + bornNum + biomes[i].destroyables[j]);
       
       GameObject parent_go = new GameObject();
       parent_go.name =  biomes[i].trees[j].name + "_parent";
    
       int totalUnit = biomes[i].units.Count; 
        List<int> randUnit = new List<int>(); 
        for(int w = 0; w<bornNum; w ++){   
           int newNum  = Mathf.RoundToInt(Random.Range(0,totalUnit)) ;
            randUnit.Add(newNum);
             
           GameObject tree_go = Instantiate(worldPrefabDictionary["Tree"],Vector3.zero,Quaternion.identity);
           tree_go.name = biomes[i].trees[j].name;
           tree_go.transform.SetParent(parent_go.transform);
           parent_go.transform.SetParent(tree_parent.transform);
           tree_go.transform.position = new Vector3(biomes[i].units[newNum].X*4,0.9f,biomes[i].units[newNum].Y*4);
           tree_go.GetComponent<TreeObject>().destroyableObject= treeData[tree_go.name];
           tree_go.GetComponentInChildren<SpriteRenderer>().sprite = treeSprites[tree_go.name][Random.Range(0,treeSprites[tree_go.name].Count)];
           tree_go.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1000 - biomes[i].units[newNum].Y*2;

       }
     
        
    }
    }
    

}


public void FinishUp(){
    OnWorldBuild();
}

  
}