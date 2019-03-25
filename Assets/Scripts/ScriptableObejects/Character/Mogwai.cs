using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mogwai", menuName = "Character/Mogwai")]
public class Mogwai : ScriptableObject
{       
        public int baseDmg;

        public Item[] likedItems;

        public Item[] hatedItems;

        public Item[] producedItems;

        public int relationshipWithPlayer; 
        
        [Range(0,0.5f)]
        public float spawnChance;

        public int spawnNum;

        public int detectDis = 30;
        public int damageDis = 5;

        public int idleSpeed;

        public int chaseSpeed;


        public float attackCd;
        public float chaseCd;


}
