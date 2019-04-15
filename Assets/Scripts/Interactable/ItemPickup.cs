using UnityEngine;

public class ItemPickup : Interactable
{   

    public Item item;
    private bool wasPickedUp;




    /* public override void Start(){
      base.Start();
      sprites = ResourceManagement.SetUpSpriteDictionary("Sprites/World/Items/RawResources",sprites);
    }*/

    public override void Setup(){

    base.Setup();
     sprites = ResourceManagement.SetUpSpriteDictionary("Sprites/Items/ItemPickups",sprites);

    }
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
