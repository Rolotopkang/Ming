using System.Collections.Generic;
using Tools;
using UnityEngine;
 
    [System.Serializable]
    public class PlayerStatModifier
    {
        public EnumTools.PlayerStatType statType;
        public float value;
        public bool UseCurve = false;
        public AnimationCurve IncrementCurve = AnimationCurve.Linear(0,0,10,1);
    }

    [CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public bool isOnly = false;
        public bool isEvent = false;
        public int MaxLevel = 4;
        public Sprite icon;
        [TextArea (3,20) ]
        public string description;

        public List<PlayerStatModifier> statModifiers = new List<PlayerStatModifier>();

        public bool hasSpecialEffect;  
        public List<EnumTools.EffectName> specialEffectNames = new List<EnumTools.EffectName>();
    }