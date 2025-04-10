

using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HPHint : MonoBehaviour
{
    public Volume volume; // 拖拽你的 Volume 对象进来
    public float decaySpeed = 1.0f; // 衰减速度，可在 Inspector 调整

    private Vignette vignette;
    private bool isActive = false;

    private void Start()
    {
        volume = GetComponent<Volume>();
        if (volume == null)
        {
            Debug.LogError("VignettePulse: Volume 未设置！");
            return;
        }

        // 尝试获取 Vignette
        if (volume.profile.TryGet<Vignette>(out vignette))
        {
            vignette.intensity.value = 0f;
        }
        else
        {
            Debug.LogError("VignettePulse: Volume 没有包含 Vignette 组件！");
        }
    }

    private void Update()
    {
        if (vignette == null || vignette.intensity == null)
            return;

        if (vignette.intensity.value > 0f)
        {
            vignette.intensity.value -= decaySpeed * Time.deltaTime;
            if (vignette.intensity.value < 0f)
                vignette.intensity.value = 0f;
        }
    }

    private void OnEnable()
    {
        EventCenter.Subscribe(EnumTools.GameEvent.PlayerHit,TriggerHitVignette);
        EventCenter.Subscribe(EnumTools.GameEvent.PlayerHealth,TriggerHealthVignette);
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(EnumTools.GameEvent.PlayerHit,TriggerHitVignette);
        EventCenter.Unsubscribe(EnumTools.GameEvent.PlayerHealth,TriggerHealthVignette);
    }

    public void TriggerHealthVignette(Dictionary<string, object> dictionary)
    {
        if (vignette != null)
        {
            vignette.color.Override(Color.green);
            vignette.intensity.value = 0.5f;
        }
    }
    
    public void TriggerHitVignette(Dictionary<string, object> dictionary)
    {
        if (vignette != null)
        {
            vignette.color.Override(Color.red);
            vignette.intensity.value = 0.5f;
        }
    }
}
