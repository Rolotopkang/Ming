using System;
using System.Collections.Generic;
using Autohand;
using Tools;
using UnityEngine;

public class OutComeBall : MonoBehaviour
{
    private Grabbable currentGrabbable;
    public PlacePoint PlacePoint;


    private void OnEnable()
    {
        EventCenter.Subscribe(EnumTools.GameEvent.LevelStart,OnLevelStart);
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(EnumTools.GameEvent.LevelStart,OnLevelStart);
    }

    private void OnLevelStart(Dictionary<String, object> args)
    {
        Destroy(gameObject);
    }
        

    public void OnJointBreak(float breakForce)
    {
        if (currentGrabbable != null)
        {
            PlacePoint.gameObject.SetActive(true);
        }
    }

    public void Init(Grabbable grabbable)
    {
        PlacePoint.enabled = true;
        currentGrabbable = grabbable;
        PlacePoint.Place(currentGrabbable);
        PlacePoint.gameObject.SetActive(false);
    }

}
