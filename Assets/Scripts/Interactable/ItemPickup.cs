using UnityEngine;

public class ItemPickup : Interactable
{   

    public Item item;
    private bool wasPickedUp;

    public override void Interact(){
        base.Interact();

        PickUp();
    }


    void PickUp(){
     Debug.Log("Picking up" + item.name);
        wasPickedUp = Inventory.instance.Add(item);
        if(wasPickedUp){
            Destroy(gameObject);
        }   
        
    }

}
