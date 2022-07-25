using gameplay;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] Inputable target;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        var deltaX = eventData.delta.x;
        var deltaY = eventData.delta.y;
        
        target?.HandleMove(deltaX / Screen.height, deltaY);
    }
}
