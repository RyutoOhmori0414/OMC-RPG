using UnityEngine.Timeline;

namespace RPG.Battle.Timeline
{
    [TrackClipType(typeof(SkillUserMoveAsset)),
     TrackClipType(typeof(SkillUserReturnAsset)),
     TrackBindingType(typeof(TimelineAttackController))]
    public sealed class SkillUserMoveTrack : TrackAsset
    {
        
    }
}
