using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Craftable : MonoBehaviour,IPointerClickHandler
{
    public Item item;
    Image image;


    void Start(){
        image = GetComponent<Image>();
        image.sprite = item.icon;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {   
        if(item){
            Debug.Log(item.name + " should be crafted");

        }
    }

}
