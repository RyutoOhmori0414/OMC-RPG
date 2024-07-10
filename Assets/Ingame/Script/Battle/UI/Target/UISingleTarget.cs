using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG.Battle.UI
{
    public sealed class UISingleTarget : AbstractUITargetBase
    {
        public override void CreateTargetButton()
        {
            TargetImage.gameObject.SetActive(true);
            
            foreach (var enemy in Targets)
            {
                var button = Object.Instantiate(ButtonPrefab.gameObject, ScreenSpaceUITransform);
                var targetUI = button.GetComponent<TargetUIController>();
                
                targetUI.SetEnemyTransform(enemy);
                targetUI.OnSelectEvent.Subscribe(SelectTargetReceiver).AddTo(ScreenSpaceUITransform);
                targetUI.OnClickSubjectEvent.Subscribe(target => OnTargetSelectedObserver.OnNext(new[] { target }))
                    .AddTo(ScreenSpaceUITransform);
                _targetButtons.Add(targetUI);
            }

            for (int i = 0; i < _targetButtons.Count; i++)
            {
                int nextIndex = i + 1;
                int lastIndex = i - 1;

                if (nextIndex >= _targetButtons.Count) nextIndex = 0;
                if (lastIndex <= -1) lastIndex = _targetButtons.Count - 1;

                var navi = _targetButtons[i].Button.navigation;
                
                navi.mode = Navigation.Mode.Explicit;
                navi.selectOnLeft = _targetButtons[lastIndex].Button;
                navi.selectOnRight = _targetButtons[nextIndex].Button;

                _targetButtons[i].Button.navigation = navi;
            }

            EventSystem.current.SetSelectedGameObject(_targetButtons[0].Button.gameObject);
        }

        public override void DestroyTargetButton()
        {
            TargetImage.gameObject.SetActive(false);
            
            foreach (var button in _targetButtons)
            {
                Object.Destroy(button.gameObject);
            }
            
            _targetButtons.Clear();
        }

        protected override void SelectTargetReceiver(BaseEventData eventData)
        {
            TargetImage.transform.position = eventData.selectedObject.transform.position;
        }
    }
}
