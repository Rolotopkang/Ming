using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionUI : MonoBehaviour
{
    public TextMeshProUGUI ItemnName;
    public TextMeshProUGUI ItemDiscription;
    public TextMeshProUGUI Level;
    public Image ItemImage;
    
    
    public Transform target;
    public Transform player;
    public Vector3 trueOffset = new Vector3(0, 0.1f, 0);
    public Vector3 offset = new Vector3(0, 0.5f, 0);
    public float minScale = 0.5f;
    public float maxScale = 2f;
    public float minDistance = 1f;
    public float maxDistance = 5f;
    public float followSmoothTime = 0.2f;
    public float rotateSmoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;
    private ItemBase _itemBase;

    private void Start()
    {
        target = transform.parent;
        if (Camera.main != null) player = Camera.main.transform;
    }

    public void ItemDescriptionUIRegister(ItemBase itemBase)
    {
        _itemBase = itemBase;
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        ItemnName.text = _itemBase.ItemData.itemName;
        ItemnName.color = GetItemColor();
        ItemImage.sprite = _itemBase.ItemData.icon;
        ItemDiscription.text = _itemBase.DiscriptionToString();
        Level.text = _itemBase.ItemCount.ToString();
        Level.color = GetItemColor();
    }

    private Color GetItemColor()
    {
        if (_itemBase.ItemData.isEventItem)
        {
            return Color.green;
        }
        else
        {
            return _itemBase.ItemCount switch
            {
                1 => Color.white ,
                2 => Color.blue ,
                3 => Color.magenta ,
                >=4 => Color.yellow,
                _ => Color.white // default case
            };
        }
    }

    void Update()
    {
        if (target == null || player == null) return;
        float distance = Vector3.Distance(player.position, transform.position);
        float scaleFactor = Mathf.Clamp01((distance - minDistance) / (maxDistance - minDistance));
        
        Vector3 targetPos = target.position + offset*scaleFactor + trueOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, followSmoothTime);
        Vector3 forward = transform.position - player.position;
        forward.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(forward, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime / rotateSmoothTime);
        float scale = Mathf.Lerp(minScale, maxScale, scaleFactor);
        transform.localScale = Vector3.one * scale;
    }
}