using System.Collections.Generic;
using Tools;
using UnityEngine;

public class PlayerItemSlotManager : Singleton<PlayerItemSlotManager>
{
    public List<ItemData> ownedItems = new List<ItemData>();
    private PlayerStatsManager _playerStatsManager;
    // private List<IItemEffect> specialEffects = new List<IItemEffect>(); // 存储特殊功能

    void Start()
    {
        _playerStatsManager = PlayerStatsManager.GetInstance();
    }

    // **拾取道具**
    public void AddItem(ItemData newItem)
    {
        ownedItems.Add(newItem);
        foreach (PlayerStatModifier modifier in newItem.statModifiers)
        {
            _playerStatsManager.ApplyStatModifier(modifier.statType, modifier.value);
        }


        // if (newItem.hasSpecialEffect)
        // {
        //     IItemEffect effect = ItemEffectFactory.CreateEffect(newItem.specialEffectName, gameObject);
        //     if (effect != null)
        //     {
        //         specialEffects.Add(effect);
        //         effect.ApplyEffect();
        //     }
        // }
    }

    // **丢弃道具**
    public void RemoveItem(ItemData itemToRemove)
    {
        if (ownedItems.Contains(itemToRemove))
        {
            ownedItems.Remove(itemToRemove);
            foreach (PlayerStatModifier modifier in itemToRemove.statModifiers)
            {
                _playerStatsManager.RemoveStatModifier(modifier.statType, modifier.value);
            }
            

            // if (itemToRemove.hasSpecialEffect)
            // {
            //     IItemEffect effectToRemove = specialEffects.Find(e => e.GetEffectName() == itemToRemove.specialEffectName);
            //     if (effectToRemove != null)
            //     {
            //         effectToRemove.RemoveEffect();
            //         specialEffects.Remove(effectToRemove);
            //     }
            // }
        }
    }
}
