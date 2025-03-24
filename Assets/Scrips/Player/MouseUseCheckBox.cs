using System;
using Autohand;
using UnityEngine;

public class MouseUseCheckBox : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<PlacePoint>().OnPlace.AddListener(OnPlace);
    }

    public void OnPlace(PlacePoint placePoint, Grabbable grabbable)
    {
        Player.GetInstance().UseHealthBottle();
        grabbable.lastHeldBy.PlayHapticVibration(0.2f);
        grabbable.DoDestroy();
    }
}
