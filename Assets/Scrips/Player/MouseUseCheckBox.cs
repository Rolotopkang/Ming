using System;
using Autohand;
using UnityEngine;

public class MouseUseCheckBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<AddHealthBottle>(out AddHealthBottle addHealthBottle))
        {
            Player.GetInstance().UseHealthBottle();
            addHealthBottle.GetComponent<Grabbable>().heldBy[0].PlayHapticVibration(0.2f);
        }
    }
}
