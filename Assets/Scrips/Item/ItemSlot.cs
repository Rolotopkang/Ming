using System;
using Autohand;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public PlacePoint _placePoint;

    private void Awake()
    {
        _placePoint = GetComponent<PlacePoint>();
    }
    
    public void OnPlaceEvent(PlacePoint point, Grabbable grabbable)
    {
        PlayerItemSlotManager.GetInstance()?.AddItem(grabbable.GetComponent<ItemBase>()); 
    }
    
    public void OnRemoveEvent(PlacePoint point, Grabbable grabbable)
    {
        PlayerItemSlotManager.GetInstance()?.RemoveItem(grabbable.GetComponent<ItemBase>()); 
    }
}
