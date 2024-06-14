using UnityEngine;
using RPG.Attribute;

namespace RPG.Battle.Player
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "RPG/Data/SkillData")]
    public class PlayerSkillDataScriptableObject : ScriptableObject
    {
        [SerializeField] private string _skillName = "Skill";
        public string SkillName => _skillName;

        [SerializeReference, SubclassSelector] private ISkill _skill;
        public ISkill Skill => _skill;
    }   
}