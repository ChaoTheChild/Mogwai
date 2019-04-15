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


    [SerializeField]
    int space = 8;
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

    public bool ContainsItem(Item item){
        foreach( Item i in items){
            if (i==item){
            return true;
        }
    }
            return false;

    }

    public int CountItem(Item item){
        int count = 0;
        for(int i=0; i<inventoryUI.slots.Length; i++){
            if(inventoryUI.slots[i].item == item){
                count += inventoryUI.slots[i].stackNum;
            }
        }
        return count;
    }



}
