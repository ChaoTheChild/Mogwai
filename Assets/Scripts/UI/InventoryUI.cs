
using UnityEngine;

public class InventoryUI : MonoBehaviour
{   
    public Transform itemParent;
    public InventorySlot[] slots;

     public GameObject startSlot;
    [SerializeField] 
  
    public GameObject endSlot;
     public bool itemTransferred = false;
     public Item draggedItem;


    // Start is called before the first frame update
    void Start()
    {
           slots = itemParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    public bool AddItemUI(Item item)
    {   
        for(int i=0; i<slots.Length; i++){
              if(slots[i].isTheSame(item)){
                 if(!slots[i].IsFull()){
                     slots[i].AddItem();
                     //Debug.Log("add mroe the same item");
                     return true;
                 }
             } 
        }
            
        for(int i=0; i<slots.Length; i++)
             if(slots[i].stackNum == 0){
                slots[i].AddNewItem(item);
               // Debug.Log("new item added"); 
                return true;               
             }       
        return false;
    }
    
   
}
