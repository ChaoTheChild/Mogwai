using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public Sprite icon = null;
    public int stackNum = 10;
    
    [Range (0,1)]
    public float dropChance;
    public Biome bornBiome;
    public virtual void Use(){
        Debug.Log("Use" + name);
    }


}
