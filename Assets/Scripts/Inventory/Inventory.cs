using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{   

    #region Singleton
    public static Inventory instance;

    void Awake(){
        if(instance != null){
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }
        instance = this;
    }
    
    #endregion

    public delegate void OnItemChange();
    public OnItemChange onItemChangeCallBack;


    public int space = 8;
    public List<Item> items = new List<Item>();
    public InventoryUI inventoryUI;

    InputController input;

   
    public bool Add(Item item){

            bool isAdded =  inventoryUI.AddItemUI(item);
            if(isAdded){
                items.Add(item);
                return true;
            }
            return false;          
        }
        
    

    public void Remove(Item item){
        items.Remove(item);

    }




}
