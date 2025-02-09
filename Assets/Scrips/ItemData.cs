using System.Collections.Generic;
using Tools;
using UnityEngine;
 
    [System.Serializable]
    public class PlayerStatModifier
    {
        public EnumTools.PlayerStatType statType;
        public float value;
    }

    [CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
        [TextArea (3,20) ]
        public string description;

        public List<PlayerStatModifier> statModifiers = new List<PlayerStatModifier>();

        public bool hasSpecialEffect;  
        public List<string> specialEffectNames = new List<string>();
    }