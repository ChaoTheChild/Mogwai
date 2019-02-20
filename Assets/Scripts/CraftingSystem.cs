using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField]
    Recipe[] recipes;
    CraftingPanel craftingPanel;
    [SerializeField]
    GameObject craftingSlot_Yin;
    Item yinItem;
    [SerializeField]
    GameObject craftingSlot_Yang;
    Item yangItem;

    List<Item> craftableItems;

    GameObject craftablePrefab;
    float bottom;

    void Start(){
        recipes = Resources.LoadAll<Recipe>("ScriptableObjects/Recipes");
        craftablePrefab = Resources.Load<GameObject>("Prefabs/UI/Craftable");
        craftingPanel = GetComponent<CraftingPanel>();
        craftableItems = new List<Item>();
        yinItem = craftingSlot_Yin.GetComponent<InventorySlot>().item;
        yangItem = craftingSlot_Yang.GetComponent<InventorySlot>().item;

    }

    public void RefreshCraftable(){
        Invoke("Refreshing",0.5f);
    }

    void Refreshing(){
        yinItem = craftingSlot_Yin.GetComponent<InventorySlot>().item;
        yangItem = craftingSlot_Yang.GetComponent<InventorySlot>().item;
        if(yinItem!=null & yangItem!=null){
            if(yinItem.isYin && !yangItem.isYin){
                GoThroughRecipe();
                RenderCraftable();
            }
        }else{
            CleanUpCraftable();
        }
    }

    void GoThroughRecipe(){
        CleanUpCraftable();
        for(int i = 0; i<recipes.Length; i++){
            if(recipes[i].yinItem == yinItem){
                if(recipes[i].yangItem == yangItem){
                    craftableItems.Add(recipes[i].GetResult());
                }
            }
        }
    }

    void RenderCraftable(){
        RectTransform panelRect = craftingPanel.displayPanel.GetComponent<RectTransform>();
        bottom = panelRect.offsetMin.y;
        //hardcode here...
        float offSet = 70f * (craftableItems.Count - 1);
        panelRect.offsetMin = new Vector2(panelRect.offsetMin.x, bottom - offSet);
        for(int i=0; i<craftableItems.Count; i++){
            GameObject craftable_go = Instantiate(craftablePrefab);
            craftable_go.transform.SetParent(craftingPanel.displayPanel.transform);
            craftable_go.GetComponent<Craftable>().item = craftableItems[i];
        }

    }

    void CleanUpCraftable(){

        craftableItems.Clear();
        RectTransform panelRect = craftingPanel.displayPanel.GetComponent<RectTransform>();
        panelRect.offsetMin = new Vector2(panelRect.offsetMin.x, bottom);
        RectTransform[] children = craftingPanel.displayPanel.GetComponentsInChildren<RectTransform>();

        if(children.Length != 0){
             foreach(RectTransform child in children){
            if(child.CompareTag("Craftable")){
                 Destroy(child.gameObject);

            }
        }
        }
       

    }


}
