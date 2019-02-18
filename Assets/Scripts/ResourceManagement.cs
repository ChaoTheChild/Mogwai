using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManagement : MonoBehaviour
{   
  
    public static ResourceManagement instance;

    public static Dictionary<string,List<Sprite>> SetUpSpriteDictionary(string path, Dictionary<string,List<Sprite>> dictionaryName){
        Sprite[] sprites = Resources.LoadAll<Sprite>(path);
        string lastName = "";
       List<Sprite> spritList = new List<Sprite>();
    for(int i=0; i<sprites.Length; i++){
        string name = sprites[i].name;
        name = name.Remove(name.IndexOf("_"));
        if(name != lastName){
            lastName = name;
            spritList = new List<Sprite>();
            spritList.Add(sprites[i]);
            dictionaryName.Add(name,spritList);
        }else{
            spritList.Add(sprites[i]);
            dictionaryName[name].Add(sprites[i]);
        }
        
    }   
   
        return dictionaryName;
    }

    public static Dictionary<string,ScriptableObject> SetUpScriptableObjectDictionary(string path){
        Dictionary<string,ScriptableObject> result = new Dictionary<string,ScriptableObject>();
        ScriptableObject[] objects = Resources.LoadAll<ScriptableObject>(path);
        foreach(ScriptableObject s in objects){
            result[s.name] = s;
        }

        return result;
    }

    public static Dictionary<string,GameObject> SetUpPrefabDictionary(string path){
        Dictionary<string,GameObject> result = new Dictionary<string,GameObject>();
        GameObject[] objects = Resources.LoadAll<GameObject>(path);
        foreach(GameObject s in objects){
            result[s.name] = s;
        }

        return result;
    }

    public static Dictionary<string,Tree> SetUpTreeDictionay(string path)
    {

        Dictionary<string,Tree> result=  new Dictionary<string,Tree>();
        Tree[] trees = Resources.LoadAll<Tree>(path);
         foreach(Tree t in trees){
            result[t.name] = t;
        }
         return result;
    }

     public static Dictionary<string,Weapon> SetUpWeaponDictionay(string path)
    {

        Dictionary<string,Weapon> result=  new Dictionary<string,Weapon>();
        Weapon[] weapons = Resources.LoadAll<Weapon>(path);
         foreach(Weapon w in weapons){
            result[w.name] = w;
        }
         return result;
    }

}
