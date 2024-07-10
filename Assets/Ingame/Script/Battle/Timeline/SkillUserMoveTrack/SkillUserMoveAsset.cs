using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace RPG.Battle.Timeline
{
    public class SkillUserMoveAsset : PlayableAsset
    {
        [SerializeField] private Ease _animationEase = Ease.Linear;
        [SerializeField] private AnimationCurve _customEaseCurve;
        
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var player = ScriptPlayable<SkillUserMoveController>.Create(graph);

            var behaviour = player.GetBehaviour();

            behaviour.AnimEase = _animationEase;
            behaviour.CustomAnimationCurve = _customEaseCurve;
            
            return player;
        }
    }
}
