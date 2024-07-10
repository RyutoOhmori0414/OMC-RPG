using RPG.Battle.Player;
using RPG.Battle.Skill;
using RPG.Battle.Timeline;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Battle.System
{
    public sealed class AttackManager : MonoBehaviour
    {
        [SerializeField] private TimelineAttackController _timelineAttack;
        [SerializeField] private PlayableDirector _director;

        [Header("IDamageable")]
        [SerializeField] private PlayerController _player;
        

        public void StartPlayerAttack(SkillData skill, IDamageable[] targets)
        {
            Debug.Log("Test");
            
            _timelineAttack.SetAttackTransform(_player.transform, targets[0].FrontTransform);
            _director.Play();
        }
    }
}
