using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{   
   
    public Item item;
    public Image icon;
    public TextMeshProUGUI stackText;
    public int stackNum = 0;

    void Start(){
        stackText = GetComponentInChildren<TextMeshProUGUI>();
        stackText.enabled = false;
    }
    public void AddItem()
    {   
       stackNum += 1;
       stackText.text = stackNum.ToString();
    }

    public void AddNewItem(Item newItem){

        item = newItem;
        icon.sprite = item.icon;
        icon.transform.localPosition = new Vector3(0,8,0);
        Color currColor = Color.white;
        currColor.a= 1.0f;
        icon.color = currColor;
        icon.enabled = true;
        stackNum += 1;
        stackText.enabled = true;
        stackText.text = stackNum.ToString();

    }

    public void AddStackedItem(Item newItem, int stakNum){
        AddNewItem(newItem);
        for(int i =0; i< stackNum -1; i++){
            AddItem();
        }
    }

    public void RemoveItem(){
        if(stackNum <= 1){
            ClearSlot();
        }else{
            stackNum -=1;
            stackText.text = stackNum.ToString();
        }
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        Color currColor = Color.white;
        currColor.a= 0f;
        icon.enabled = false;
        stackText.enabled = false;
        stackNum = 0;
    }


    public bool isTheSame(Item newitem){
        if(item){
           if(newitem == item){
               return true;
           } else {
               return false;
           }
        }
        return false;
    }

    public bool IsFull(){
        if(stackNum >= item.stackNum){
            return true;
        }else{
            return false;
        }
    }

    public void UseItem(){
        if(item != null){
            item.Use();
            //RemoveItem();
        }
    }

 
    

}
