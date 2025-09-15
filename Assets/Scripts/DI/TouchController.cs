using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TouchController
{
    public UnityEvent<PointerEventData> TouchStart = new();
    public UnityEvent<PointerEventData> TouchEnd = new();
    public UnityEvent<PointerEventData> TouchMoved = new();

    public void OnPointerDown(PointerEventData eventData)
    {
        TouchStart?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TouchEnd?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        TouchMoved?.Invoke(eventData);
    }
}