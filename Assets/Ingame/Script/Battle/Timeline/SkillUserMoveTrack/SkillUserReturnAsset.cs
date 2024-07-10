using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Battle.Timeline
{
    public class SkillUserReturnAsset : PlayableAsset
    {
        [SerializeField] private Ease _animationEase = Ease.Linear;
        [SerializeField] private AnimationCurve _customEaseCurve;
        
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var player = ScriptPlayable<SkillUserReturnController>.Create(graph);

            var behaviour = player.GetBehaviour();

            behaviour.AnimEase = _animationEase;
            behaviour.CustomAnimationCurve = _customEaseCurve;
            
            return player;
        }
    }
}
