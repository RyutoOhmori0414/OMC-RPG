using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.Timeline
{
    public sealed class TimelineAttackController : MonoBehaviour
    {
        [SerializeField] private Transform _user;
        [SerializeField] private Transform _target;

        public Transform SkillUser => _user;
        public Vector3 SkillMoveTarget => _target.position;
        public Vector3 FirstUserPos { get; private set; }


        public bool IsInit { get; private set; } = false;
        
        public void SetAttackTransform(Transform user, Transform moveTarget)
        {
            _user = user;
            FirstUserPos = user.position;
            _target = moveTarget;

            IsInit = true;
        }

        public void Reset()
        {
            SkillUser.position = FirstUserPos;
        }
    }
}
