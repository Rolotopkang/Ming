using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_FirePersentageBar : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        if (WeaponController.IsInitialized)
        {
            _image.fillAmount = WeaponController.GetInstance().DragPersentage;
        }
    }
}
