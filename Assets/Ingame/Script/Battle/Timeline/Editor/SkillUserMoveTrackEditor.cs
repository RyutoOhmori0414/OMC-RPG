using System.Collections;
using System.Collections.Generic;
using RPG.Battle.Timeline;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Timeline;

namespace RPG.BattleTimeline.Editor
{
    [CustomTimelineEditor(typeof(SkillUserMoveTrack))]
    public sealed class SkillUserMoveTrackEditor : TrackEditor
    {
        private Texture2D _iconTex;

        public override TrackDrawOptions GetTrackOptions(TrackAsset track, Object binding)
        {
            track.name = "SkillUserMove";

            if (!_iconTex)
            {
                var op = Addressables.LoadAssetAsync<Texture2D>("kkrn_icon_ken_2");
                _iconTex = op.WaitForCompletion();
                Addressables.Release(op);
            }
            
            var option = base.GetTrackOptions(track, binding);
            
            option.trackColor = Color.magenta;
            option.icon = _iconTex;

            return option;
        }
    }
}
