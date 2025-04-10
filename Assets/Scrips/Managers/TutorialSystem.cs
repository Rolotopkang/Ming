using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;


public class TutorialSystem : MonoBehaviour
{
    public GameObject[] layers;

    

    private void Start()
    {
        showLayer(0);
        EventCenter.Subscribe(EnumTools.GameEvent.PlayerHealth,Onhealth);
    }

    private void Onhealth(Dictionary<string, object> dictionary)
    {
        showLayer(3);
        EventCenter.Unsubscribe(EnumTools.GameEvent.PlayerHealth,Onhealth);
    }

    public void showLayer(int i)
    {
        foreach (GameObject gameObject in layers)
        {
            gameObject.SetActive(false);
        }
        layers[i].SetActive(true);
    }
}
