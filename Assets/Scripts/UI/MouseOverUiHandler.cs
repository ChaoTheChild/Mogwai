using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverUiHandler : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData){
        Debug.Log("Mosue over UI");
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData){
        Debug.Log("Mouse leave UI");
    }
}
