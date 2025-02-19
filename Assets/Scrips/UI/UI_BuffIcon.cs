using Scrips.Buffs;
using Scrips.Effects;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Scrips.UI
{
    public class UI_BuffIcon : MonoBehaviour
    {
        public Texture FireIcon;
        public Texture PoisonIcon;
        public Texture IceIcon;
        public Texture FrozenIcon;
        public Texture EliteIcon;
        public Texture ShieldArmedIcon;
        public RawImage BuffIcon;
        public TextMeshProUGUI Num;

        public void Register(BuffBase buffBase)
        {
            BuffIcon.texture = buffBase.BuffName switch
            {
                EnumTools.BuffName.Fire => FireIcon,
                EnumTools.BuffName.Poison => PoisonIcon,
                EnumTools.BuffName.Ice => IceIcon,
                EnumTools.BuffName.Frozen => FrozenIcon,
                EnumTools.BuffName.Elite => EliteIcon,
                EnumTools.BuffName.ShieldArmed => ShieldArmedIcon,
                _ => null
            };
            Num.text = buffBase.currentLayer.ToString("D");
        }
    }
}