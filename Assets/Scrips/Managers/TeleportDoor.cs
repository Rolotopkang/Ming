using System.Collections.Generic;
using Tools;
using UnityEngine;

public class TeleportDoor : MonoBehaviour
{
    public List<GameObject> doorList;
    private EnumTools.RoomKind currentRoomKind;

    
    public void Init(EnumTools.RoomKind roomKind, Transform transform)
    {
        this.transform.position = transform.position;
        currentRoomKind = roomKind;
        RefreshDoor();
    }

    public void Clear()
    {
        currentRoomKind = EnumTools.RoomKind.None;
        foreach (GameObject tmp in doorList)
        {
            tmp.SetActive(false);
        }
    }

    public void RefreshDoor()
    {
        SetCurrentDoor(currentRoomKind switch
        {
            EnumTools.RoomKind.Item => 0,
            EnumTools.RoomKind.Money => 1,
            EnumTools.RoomKind.Health =>2,
            EnumTools.RoomKind.Event => 3,
            EnumTools.RoomKind.Store => 4,
            EnumTools.RoomKind.Boss =>5,
            EnumTools.RoomKind.Test =>6,
            _ => 0
        } );
    }

    public void SetCurrentDoor(int index)
    {
        foreach (GameObject tmp in doorList)
        {
            tmp.SetActive(false);
        }
        doorList[index].SetActive(true);
    }
}
