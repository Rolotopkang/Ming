using System;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemBase : MonoBehaviour
{
    private GameObject _discriptionUI;
    private Outline _outline;
    private int _showUICounter = 0;

    private void Start()
    {
        _discriptionUI = transform.GetChild(0).gameObject;
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }
    
    private void CheckShowDiscriptionUI()
    {
        if (_showUICounter > 0)
        {
            _discriptionUI.SetActive(true);
            _outline.enabled = true;
        }
        else
        {
            _discriptionUI.SetActive(false);
            _outline.enabled = false;
        }
    }

    public void AddShowDiscriptionUI()
    {
        _showUICounter++;
        CheckShowDiscriptionUI();
    }
    
    public void DecreaseShowDiscriptionUI()
    {
        _showUICounter--;
        CheckShowDiscriptionUI();
    }
}
