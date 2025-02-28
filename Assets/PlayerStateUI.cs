using System;
using TMPro;
using UnityEngine;

public class PlayerStateUI : MonoBehaviour
{
    public TextMeshProUGUI hp;
    
    private void Update()
    {
            hp.text = Player.GetInstance().CurrentHP.ToString();    
    }
}
