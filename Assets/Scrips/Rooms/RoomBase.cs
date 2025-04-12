using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

public class RoomBase : MonoBehaviour
{
    public EnumTools.RoomKind RoomKind;
    public Transform currentBirthPoint;
    public Transform[] currentDoorPos;
    public Transform currentItemTablePos;
    
    
    private void OnEnable()
    {
        EventCenter.Subscribe(EnumTools.GameEvent.LevelStart,OnLevelStart);
        EventCenter.Subscribe(EnumTools.GameEvent.LevelEnd,OnLevelEnd);
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(EnumTools.GameEvent.LevelStart,OnLevelStart);
        EventCenter.Unsubscribe(EnumTools.GameEvent.LevelEnd,OnLevelEnd);
    }
    
    protected virtual async void OnLevelStart(Dictionary<String, object> args)
    {
        Debug.Log("关卡开始");
        await UniTask.WaitForSeconds(0.5f);
        EventCenter.Publish(EnumTools.GameEvent.LevelEnd,null);
    }

    protected virtual void OnLevelEnd(Dictionary<String, object> args)
    {
        Debug.Log("关卡结束");
    }
}
