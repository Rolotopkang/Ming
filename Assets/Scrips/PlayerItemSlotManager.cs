using System.Collections.Generic;
using Tools;
using UnityEngine;

public class PlayerItemSlotManager : Singleton<PlayerItemSlotManager>
{
    public List<ItemBase> ownedItems = new List<ItemBase>();
    private PlayerStatsManager _playerStatsManager;
    // private List<IItemEffect> specialEffects = new List<IItemEffect>(); // 存储特殊功能

    void Start()
    {
        _playerStatsManager = PlayerStatsManager.GetInstance();
    }

    // **拾取道具**
    public void AddItem(ItemBase newItem)
    {
        ownedItems.Add(newItem);
        foreach (PlayerStatModifier modifier in newItem.ItemData.statModifiers)
        {
            if (modifier.UseCurve)
            {
                _playerStatsManager.ApplyStatModifier(modifier.statType, modifier.IncrementCurve.Evaluate(newItem.ItemCount));
            }
            else
            {
                _playerStatsManager.ApplyStatModifier(modifier.statType, modifier.value * newItem.ItemCount);
            }
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
    public void RemoveItem(ItemBase itemToRemove)
    {
        if (ownedItems.Contains(itemToRemove))
        {
            ownedItems.Remove(itemToRemove);
            foreach (PlayerStatModifier modifier in itemToRemove.ItemData.statModifiers)
            {
                if (modifier.UseCurve)
                {
                    _playerStatsManager.RemoveStatModifier(modifier.statType, modifier.IncrementCurve.Evaluate(itemToRemove.ItemCount));
                }
                else
                {
                    _playerStatsManager.RemoveStatModifier(modifier.statType, modifier.value * itemToRemove.ItemCount);
                }
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
