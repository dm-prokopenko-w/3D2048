using UnityEngine;
using UnityEngine.EventSystems;

public static class Utils
{
    public static Vector3? GetRaycastPointOnPlane(PointerEventData eventData, Vector3 spawnPosition)
    {
        var screenPos = eventData.position; 
        var ray = Camera.main.ScreenPointToRay(screenPos);
        var plane = new Plane(Vector3.up, spawnPosition);
        return plane.Raycast(ray, out float distance) ? ray.GetPoint(distance) : null;
    }
}