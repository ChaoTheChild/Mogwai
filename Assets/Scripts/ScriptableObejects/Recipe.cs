using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
public class Recipe : ScriptableObject
{
    public Item yinItem;

    public int yinNum;

    public Item yangItem;

    [SerializeField]
    public int yangNum;

    [SerializeField]
    Item result;

    public Item GetResult(){
        return result;
    }
}
