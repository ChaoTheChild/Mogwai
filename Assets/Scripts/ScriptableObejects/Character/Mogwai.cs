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
        
        public Biome spawnBiome;

        [Range(0,0.5f)]
        public float spawnChance;

        public int spawnNum;

        public int detectDis = 10;

        public int idleSpeed;

        public int chaseSpeed;




}
