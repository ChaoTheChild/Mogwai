using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField]
    Recipe[] recipes;
    CraftingPanel craftingPanel;
    
    List<Item> inventoryItems;
    List<Item> craftableItems;

    float bottom;

    void Start(){
        recipes = Resources.LoadAll<Recipe>("ScriptableObjects/Recipes");
        craftingPanel = GetComponent<CraftingPanel>();
        inventoryItems = new List<Item>();
        craftableItems = new List<Item>();
        
    }

    public void RefreshCraftable(){
        
    }



}
