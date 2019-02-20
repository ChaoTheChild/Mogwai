
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour,IDragHandler, 
IEndDragHandler,IBeginDragHandler, IDropHandler


{
    Image icon;
    InventoryUI inventoryUI;
    CraftingSystem craftingSystem;

    [SerializeField]
    InventorySlot slot;
    Transform parentTransform;
    

    void Start(){
        parentTransform = this.gameObject.GetComponentInChildren<Button>().transform;
        inventoryUI = FindObjectOfType<InventoryUI>();
        craftingSystem = GameObject.FindObjectOfType<CraftingSystem>();
        Image[] images = GetComponentsInChildren<Image>();
        foreach(Image child in images){
            if(child.CompareTag("Icon")){
                icon = child;
            }
        }
        slot = this.gameObject.GetComponent<InventorySlot>();
    }
    public void OnBeginDrag(PointerEventData eventData){

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        if(slot.item){
             inventoryUI.itemTransferred = false;
            inventoryUI.draggedItem = slot.item;
            inventoryUI.draggedNum = slot.stackNum;
        }
        inventoryUI.startSlot = this.gameObject;
        icon.transform.localScale = new Vector3(0.9f,0.9f,0.9f);

   }


   public void OnDrag(PointerEventData eventData)
    {  
        icon.transform.position = Input.mousePosition;

    }
 
    public void OnEndDrag(PointerEventData eventData)
    {  
    //Debug.Log(Input.mousePosition);
    icon.transform.SetParent(parentTransform);
    icon.transform.localPosition = new Vector3(0,8,0);
     icon.transform.localScale = new Vector3(1f,1f,1f);
    GetComponent<CanvasGroup>().blocksRaycasts = true;
    craftingSystem.RefreshCraftable();

     if(inventoryUI.itemTransferred){
         slot.ClearSlot();
     }
    
    }

    public void OnDrop(PointerEventData eventData){
        Debug.Log("On Drop");
        if(slot.item == null){
            inventoryUI.endSlot = this.gameObject;
            inventoryUI.itemTransferred = true; 
            if(inventoryUI.draggedItem){
            slot.AddNewItem(inventoryUI.draggedItem);
            inventoryUI.draggedItem = null;
            }    
            
            craftingSystem.RefreshCraftable();
        }
    }


  
}
