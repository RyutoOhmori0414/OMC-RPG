using RPG.Attribute;
using UnityEngine;

namespace RPG.Battle.Skill
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "RPG/Data/SkillData")]
    public class SkillData : ScriptableObject
    {
        [SerializeField] private string _skillName = "Skill";
        public string SkillName => _skillName;

        [SerializeReference, SubclassSelector] private ISkill _skill;
        public ISkill Skill => _skill;
    }   
}