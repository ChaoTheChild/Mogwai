using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hulijing : Monster
{
    
        public override void UpdateSprite(){

        base.UpdateSprite();
        int baseOrder = 1000 - Mathf.RoundToInt(this.transform.position.z/2);

        foreach(SpriteRenderer s in spriteRds){
            s.sortingOrder = baseOrder;
            if(s.name.Contains("Eye")){
                s.sortingOrder +=3;
            }else if(s.name.Contains("Head")){
                s.sortingOrder +=2;
            }else if(s.name.Contains("Ear")){
                s.sortingOrder +=3;
            }else if(s.name.Contains("Scarf")){
                s.sortingOrder += 1;
            }
        }
    }
}
