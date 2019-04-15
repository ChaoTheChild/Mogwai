using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField]
     List<Item> resources;

    [SerializeField]
    Item result;

    public Item GetResult(){
        return result;
    }
}
