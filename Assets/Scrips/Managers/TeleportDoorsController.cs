using System.Collections.Generic;
using Tools;
using UnityEngine;

public class TeleportDoorsController : Singleton<TeleportDoorsController>
{
    public List<TeleportDoor> _teleportDoors;

    public void SetNextLevelDoor(List<EnumTools.RoomKind> roomKinds, List<Transform> pos)
    {
        int i = 0;
        foreach (TeleportDoor door in _teleportDoors)
        {
            door.Init(roomKinds[i],pos[i]);
            i++;
        }
    }

    public void HideDoors(Transform _transform)
    {
        foreach (TeleportDoor teleportDoor in _teleportDoors)
        {
            teleportDoor.transform.position = _transform.position;
        }
    }
}
