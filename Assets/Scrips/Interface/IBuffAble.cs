using System.Collections.Generic;
using Tools;

namespace Scrips.Buffs
{
    public interface IBuffAble
    {
        public void TriggerBuffs();

        public void AddNewBuff(EnumTools.BuffName buffName);

        public void RemoveBuff(EnumTools.BuffName buffName);

        public List<BuffBase> GetBuffList();
    }
}