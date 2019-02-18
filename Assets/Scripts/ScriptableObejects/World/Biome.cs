using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Biome", menuName = "World/Biome")]
public class Biome : ScriptableObject
{   
    public string biomeName = "";
    public TileType Type = (TileType)0;
    Vector2 centerPosition;
    public List<Unit> units = new List<Unit>();
    public List<Tree> trees = new List<Tree>();
   
  
}
