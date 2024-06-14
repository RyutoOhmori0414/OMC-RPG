using System;

namespace RPG.Battle
{
    [Serializable, Flags]
    public enum SkillAttributeEnum
    {
        None = 0,
        Fire = 1,
        Ice = 1 << 1,
        Wind = 1 << 2,
        Shine = 1 << 3,
        Dark = 1 << 4
    }   
}