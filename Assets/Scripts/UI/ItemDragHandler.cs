
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour,IDragHandler, 
IEndDragHandler,IBeginDragHandler,IDropHandler




{
    Image icon;
    InventoryUI inventoryUI;

    [SerializeField]
    InventorySlot slot;
    

    void Start(){
        inventoryUI = FindObjectOfType<InventoryUI>();
        Image[] images = GetComponentsInChildren<Image>();
        foreach(Image child in images){
            if(child.CompareTag("Icon")){
                icon = child;
            }
        }
        slot = this.gameObject.GetComponent<InventorySlot>();
    }
    public void OnBeginDrag(PointerEventData eventData){
        Debug.Log("If the start dragging slot contains item"+ slot.item);

        if(slot.item){
             inventoryUI.itemTransferred = false;
            inventoryUI.draggedItem = slot.item;
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

    icon.transform.localPosition = new Vector3(0,8,0);
     icon.transform.localScale = new Vector3(1f,1f,1f);
     if(inventoryUI.itemTransferred){
         Debug.Log("transffered" + inventoryUI.itemTransferred);
        this.gameObject.GetComponent<InventorySlot>().ClearSlot();
        Debug.Log("the "+ this.gameObject.name + "slot contains" + slot.item);
     }

    }

       public void OnDrop(PointerEventData eventData)
    {   
         Debug.Log("the "+ this.gameObject.name + "slot contains" + slot.item);

        inventoryUI.endSlot = this.gameObject;

        Debug.Log(slot.item);
       StartCoroutine(DropToSlot());
        
    }
    
    IEnumerator DropToSlot(){
        if(slot.item == null & !inventoryUI.itemTransferred){
            slot.AddNewItem(inventoryUI.draggedItem);
         yield return StartCoroutine(Transffered());
        }else{
            Debug.Log("Inventory Slot has been taken");
            yield return 0;
        }
   

    }
    IEnumerator Transffered(){
         inventoryUI.itemTransferred = true;
         yield return null;
    }
  
 
}
