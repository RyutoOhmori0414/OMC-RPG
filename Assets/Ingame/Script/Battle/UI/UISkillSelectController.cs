using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using RPG.Battle.Skill;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG.Battle.UI
{
    public sealed class UISkillSelectController : AbstractUIAnimation
    {
        [Header("DoTween周り")]
        [SerializeField] private RectTransform _animRoot;
        [SerializeField] private RectTransform _openAncher;
        [SerializeField] private RectTransform _closeAncher;
        [SerializeField] private float _openAndCloseSec = 1F;

        [Header("FirstSelect")]
        [SerializeField] private GameObject _select;
        [Space]
        [SerializeField] private SkillButtonController[] _skillButtons;
        
        private Image _panel = default;
        private SkillData[] _skills;

        private void Awake()
        {
            gameObject.SetActive(false);
            _animRoot.position = _closeAncher.position;
        }

        public void SetSkillData(SkillData[] skillData)
        {
            _skills = skillData;
  　　　　　　　　　　　　　　　　　　　
            for (int i = 0; i < _skillButtons.Length; i++)
            {
                if (i < _skills.Length)
                {
                    _skillButtons[i].SetSkill(_skills[i]);
                }
                else
                {
                    _skillButtons[i].gameObject.SetActive(false);
                }
            }
        }

        public override void OpenUI()
        {
            EventSystem.current.SetSelectedGameObject(_select);
            _animRoot.DOMove(_openAncher.position, _openAndCloseSec).OnStart(() => gameObject.SetActive(true));
        }

        public override void CloseUI()
        {
            _animRoot.DOMove(_openAncher.position, _openAndCloseSec).OnComplete(() => gameObject.SetActive(false));
        }
    }
}
