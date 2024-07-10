using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.Playables;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RPG.Battle.Timeline
{
    public class SkillUserReturnController : PlayableBehaviour
    {
        public Ease AnimEase { private get; set; }
        public AnimationCurve CustomAnimationCurve { private get; set; }

        private EaseFunction _customEase;

        public override void OnGraphStart(Playable playable)
        {
            _customEase = EaseFactory.StopMotion(60, CustomAnimationCurve);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (playerData is TimelineAttackController attackController)
            {
                UpdatePos(attackController, (float)playable.GetTime(), (float)playable.GetDuration());
            }
        }

        private void UpdatePos(TimelineAttackController attackController , float time, float duration)
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying) return;
#endif
            attackController.SkillUser.position = Vector3.Lerp(attackController.SkillMoveTarget,
                attackController.FirstUserPos,
                EaseManager.Evaluate(AnimEase, _customEase, time, duration, 1, 0));
        }
    }
}
