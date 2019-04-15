using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpriteController : MonoBehaviour
{
    private const string Name = "Player";

    public SpriteRenderer[] sprd;
    Dictionary<string, List<Sprite>> playerSprites; 

    Dictionary<string,int> spritIndex;
    public int WeaponIndex = 10;
    
    void Start(){
                LoadSprites();
                Player.OnPlayerMove += RefreshSpriteOrder;   
                InitializeSprite(); 
                RefreshSpriteOrder();
                
    }


    public void NextSprite(SpriteRenderer spriteRd){
        //Debug.Log("Next Sprite");
        string name = spriteRd.name.Remove(spriteRd.name.IndexOf("_"));
        //Debug.Log(name);
        //Debug.Log(playerSprites[name]);
        List<Sprite> sprites = playerSprites[name];
        if(spritIndex[name] < sprites.Count -1){
            spritIndex[name] += 1;
            spriteRd.sprite = sprites[spritIndex[name]];
        }else{
            spritIndex[name] = 0;
             spriteRd.sprite = sprites[spritIndex[name]];
        }
        
        }

    public void LastSprite(SpriteRenderer spriteRd){
        string name = spriteRd.name.Remove(spriteRd.name.IndexOf("_"));
        List<Sprite> sprites = playerSprites[name];
        if(spritIndex[name] > 0){
            spritIndex[name] -=1;
            spriteRd.sprite = sprites[spritIndex[name]];
        }else{
            spritIndex[name] = sprites.Count;
            spriteRd.sprite = sprites[spritIndex[name]];
        }
    }


        
    public void LoadSprite(SpriteRenderer spriteRd){
        string name = spriteRd.name.Remove(spriteRd.name.IndexOf("_"));
        List<Sprite> sprites = playerSprites[name];

        if(spritIndex[name]>0){
            spritIndex[name] -= 1;
            spriteRd.sprite = sprites[spritIndex[name]];
        }else{
            spritIndex[name] = sprites.Count;
            spriteRd.sprite = sprites[spritIndex[name]];
        }

    }

    
   
    void LoadSprites(){
        playerSprites = new Dictionary<string, List<Sprite>>();
        playerSprites = ResourceManagement.SetUpSpriteDictionary("Sprites/Character/Player",playerSprites);      
    }

    void InitializeSprite(){
        spritIndex = new Dictionary<string, int>();
        spritIndex["Hair"] = 0;
        spritIndex["Head"] = 0;
        spritIndex["Clothes"] = 0;
        spritIndex["Eye"] = 0;
        spritIndex["Mouth"] = 0;
        sprd = GetComponentsInChildren<SpriteRenderer>();
      
    }

    void RefreshSpriteOrder(){
        for(int i=0; i<sprd.Length; i++){
            int baseOrder = 1000 - Mathf.RoundToInt(this.transform.position.z/2);
            switch(sprd[i].name){
            case "Hair_Sprite":
            sprd[i].sortingOrder = baseOrder + 6;
            break;
            case "UpperArm_F_Sprite":
            sprd[i].sortingOrder = baseOrder + 5;
            break;
            case "LowerArm_F_Sprite":
            sprd[i].sortingOrder = baseOrder + 4;
            break;
            case "Hand_F_Sprite":
            sprd[i].sortingOrder = baseOrder + 4;
            break;
            case "Weapon_Sprite":
            sprd[i].sortingOrder = baseOrder + 4;
            WeaponIndex = i;
            break;
            case "Head_Sprite":
            sprd[i].sortingOrder = baseOrder + 3;
            break;
            case "Eye_Sprite":
            sprd[i].sortingOrder = baseOrder + 4;  
            break;
            case "Mouth_Sprite":
            sprd[i].sortingOrder = baseOrder + 4;
            break;
            case "UpperBody_Sprite":
            sprd[i].sortingOrder = baseOrder + 1;
            break;
            case "Clothes_Sprite":
            sprd[i].sortingOrder = baseOrder + 2;
            break;
            case "Butt_Sprite":
            sprd[i].sortingOrder = baseOrder + 1;
            break;
            case "UpperLeg_F_Sprite":
            sprd[i].sortingOrder = baseOrder + 1;
            break;
            case "Foot_F_Sprite":
            sprd[i].sortingOrder = baseOrder +1;
            break;
            case "LowerLeg_B_Sprite":
            sprd[i].sortingOrder = baseOrder -1;
            break;
            case "Foot_B_Sprite":
            sprd[i].sortingOrder = baseOrder -2;
            break;
            case "UpperArm_B_Sprite":
            sprd[i].sortingOrder = baseOrder -2;
            break;
            case "LowerArm_B_Sprite":
            sprd[i].sortingOrder = baseOrder -3;
            break;
            case "Hand_B_Sprite":
            sprd[i].sortingOrder = baseOrder -4;
            break;          

            default:
            sprd[i].sortingOrder = baseOrder;
            break;
            }
            

            
        
        }
        
    }
  

    
}
