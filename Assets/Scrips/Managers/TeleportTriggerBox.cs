using System;
using Tools;
using UnityEngine;

public class TeleportTriggerBox : MonoBehaviour
{
    public EnumTools.RoomKind RoomKind;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoguelikeManager.GetInstance().NextLevel(RoomKind);
            TeleportDoorsController.GetInstance().ClearDoor();
        }
    }
}
