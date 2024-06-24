using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Battle.UI
{
    public sealed class AttackButtonUIController : MonoBehaviour
    {
        [SerializeField] private UISkillSelectController _selectController;
        
        private Button _button = default;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(AddButtonOnClick);
        }

        private void AddButtonOnClick()
        {
            _selectController.OpenUI();
        }
    }
}
