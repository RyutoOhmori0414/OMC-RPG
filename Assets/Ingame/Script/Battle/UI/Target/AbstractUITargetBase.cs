using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG.Battle.UI
{
    public abstract class AbstractUITargetBase
    {
        protected readonly List<TargetUIController> _targetButtons = new();
        
        public ScreenSpaceEnemyUI.TargetData[] Targets { protected get; set; }
        public TargetUIController ButtonPrefab { protected get; set; }
        public Transform ScreenSpaceUITransform { protected get; set; }
        public Image TargetImage { protected get; set; }
        
        public IObserver<IDamageable[]> OnTargetSelectedObserver { protected get; set; }

        public void Init(TargetUIController buttonPrefab, Transform ssUITransform, Image targetImage, IObserver<IDamageable[]> onTargetSelectedObserver)
        {
            ButtonPrefab = buttonPrefab;
            ScreenSpaceUITransform = ssUITransform;
            TargetImage = targetImage;
            OnTargetSelectedObserver = onTargetSelectedObserver;
        }

        public abstract void CreateTargetButton();

        public abstract void DestroyTargetButton();

        protected abstract void SelectTargetReceiver(BaseEventData eventData);
    }
}
