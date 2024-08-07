using RPG.Battle.Skill;
using UnityEngine;

namespace RPG.Battle.Player
{
    public sealed class PlayerAttackController : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerDamageable;
        
        public void Attack(SkillData skill, IDamageable[] targets)
        {
            Debug.Log($"{skill.SkillName}で{targets[0]}を攻撃");
            //skill.Skill.UseSkill(_playerDamageable, targets);
        }
    }
}
