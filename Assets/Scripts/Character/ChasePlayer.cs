using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{   

    public Monster monsterControl;


      void OnTriggerEnter(){
        monsterControl.curStat = Monster.enemyStat.CHASE;

    }

    void OnTriggerExit(){
         monsterControl.curStat = Monster.enemyStat.IDLE;

    }
}
