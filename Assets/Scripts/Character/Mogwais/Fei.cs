using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fei: Monster{
    public override void UpdateSprite(){

        base.UpdateSprite();
        int baseOrder = 1000 - Mathf.RoundToInt(this.transform.position.z/2);

        foreach(SpriteRenderer s in spriteRds){
            s.sortingOrder = baseOrder;
            if(s.name == "Eye"){
                s.sortingOrder +=1;
            }else if(s.name == "Lid"){
                s.sortingOrder +=2;
            }else if(s.name == "Horn_2"){
                s.sortingOrder -=1;
            }else if(s.name == "Tatoo"){
                s.sortingOrder += 1;
            }
        }
    }
}