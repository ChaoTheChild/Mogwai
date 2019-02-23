
using UnityEngine;

public abstract class Object : ScriptableObject
{
    public int health;
    public Item[] dropItems;
    public GameObject dropped;
    [Range (0,1)]
    public float bornChance;
    public int bornNum;

  
    public virtual void DropItem(Vector3 position){
       // Debug.Log("drop item");

        for(int i=0; i<dropItems.Length; i++){

            GameObject dropItem = Instantiate(dropped);
            dropItem.transform.position = new Vector3 (position.x+Random.Range(-2,2),position.y,position.z + Random.Range(-2,2));
            dropItem.name = dropItems[i].name;
            ItemPickup itemPickUp = dropItem.GetComponent<ItemPickup>();
            itemPickUp.item = dropItems[i];
            SpriteRenderer sr = dropItem.GetComponentInChildren<SpriteRenderer>();
            sr.sprite = dropItems[i].icon;
            sr.sortingOrder = 1000 - Mathf.RoundToInt(position.z/2);
            Rigidbody rg = dropItem.GetComponent<Rigidbody>();
            rg.useGravity = false;
            rg.constraints = RigidbodyConstraints.FreezeAll;

            //dropped = Instantiate(dropObjects[i],position, Quaternion.Euler(45,0,0));
        }
    }

}
