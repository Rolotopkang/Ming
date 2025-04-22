using System;
using System.Collections.Generic;
using SerializableCallback;
using Tools;
using UnityEngine;


public class TutorialSystem : MonoBehaviour
{
    public GameObject[] layers;
    public AudioClip bgm;

    private GameObject bgmInstance;
    

    public void GameSrart()
    {
        Destroy(bgmInstance);
    }
    

    private void Start()
    {
        showLayer(0);
        EventCenter.Subscribe(EnumTools.GameEvent.PlayerHealth,Onhealth);
        bgmInstance = AudioManager.GetInstance().PlayBGSoundReturn(bgm);
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
