using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

public class PlayerItemSlotManager : Singleton<PlayerItemSlotManager>
{
    public List<ItemBase> ownedItems = new List<ItemBase>();
    private PlayerStatsManager _playerStatsManager;
    private List<IItemEffect> specialEffects = new List<IItemEffect>();
    void Start()
    {
        _playerStatsManager = PlayerStatsManager.GetInstance();
    }

    // **拾取道具**
    public void AddItem(ItemBase newItem)
    {
        ownedItems.Add(newItem);
        if (newItem.ItemData.isOnly)
        {
            foreach (IItemEffect itemEffect in specialEffects)
            {
                if (itemEffect.GetEffectName() == newItem.ItemData.specialEffectNames[0])
                {
                    return;
                }
            }
        }
        
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
        
        if (newItem.ItemData. hasSpecialEffect)
        {
            foreach (EnumTools.EffectName specialEffectName in newItem.ItemData.specialEffectNames)
            {
                bool flag = true;
                foreach (IItemEffect itemEffect in specialEffects)
                {
                    if (itemEffect.GetEffectName() == specialEffectName)
                    {
                        flag = false;
                    }
                }

                if (flag)
                {
                    IItemEffect effect = ItemEffectFactory.CreateEffect(specialEffectName);
                    if (effect != null)
                    {
                        effect.ApplyEffect();
                        specialEffects.Add(effect);
                    }
                }
            }


        }
    }

    // **丢弃道具**
    public void RemoveItem(ItemBase itemToRemove)
    {
        if (ownedItems.Contains(itemToRemove))
        {
            ownedItems.Remove(itemToRemove);
            
            if (itemToRemove.ItemData.isOnly)
            {
                foreach (ItemBase itemBase in ownedItems)
                {
                    if (itemBase.ItemData.itemName.Equals(itemToRemove.ItemData.itemName))
                    {
                        return;
                    }
                }
            }
            
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
            
            

            if (itemToRemove.ItemData. hasSpecialEffect)
            {
                foreach (EnumTools.EffectName specialEffectName in itemToRemove.ItemData.specialEffectNames)
                {
                    bool flag = true;
                    foreach (ItemBase itemBase in ownedItems)
                    {
                        foreach (EnumTools.EffectName effectName in itemBase.ItemData.specialEffectNames)
                        {
                            if (effectName.Equals(specialEffectName))
                            {
                                flag = false;
                            }
                        }
                    }
                    if (flag)
                    {
                        IItemEffect effectToRemove = specialEffects.Find(e => e.GetEffectName() == specialEffectName);
                        if (effectToRemove != null)
                        {
                            effectToRemove.RemoveEffect();
                            specialEffects.Remove(effectToRemove);
                        }
                    }
                }
            }
        }
    }
}
