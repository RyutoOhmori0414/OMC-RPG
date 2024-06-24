using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace RPG.Battle.Player
{
    public sealed class PlayerController : MonoBehaviour, IDamageable, ILoadable
    {
        private PlayerData _playerData;
        private bool _isPlayerDataLoaded = false;
        [Inject] private ISubscriber<PhaseCallback> _phaseSubscriber;

        private List<IDamageable.BuffData> _attackBuffData = new();
        private List<IDamageable.BuffData> _defenseBuffData = new();
        private int _rawAttack = 0;
        private int _rawDefense = 0;
        
        public int CurrentHp { get;　private set; } = -1;

        public int Attack
        {
            get
            {
                float attack = _rawAttack;

                foreach (var buff in _attackBuffData)
                {
                    attack *= buff.Multiplier;
                }

                return (int)attack;
            }
            set => _rawAttack = value;

        }

        public int Defense
        {
            get
            {
                float defense = _rawDefense;

                foreach (var buff in _defenseBuffData)
                {
                    defense *= buff.Multiplier;
                }

                return (int)defense;
            }
            set => _rawDefense = value;
        }

        public int MaxHp
        {
            get
            {
                if (_isPlayerDataLoaded) return _playerData.PlayerHp;

                Debug.LogWarning("PlayerDataがLoadされていません");
                return -1;
            }
        }

        public SkillAttributeEnum WeakAttribute
        {
            get
            {
                if (_isPlayerDataLoaded) return _playerData.WeakAttribute;
                
                Debug.LogWarning("PlayerDataがLoadされていません");
                return SkillAttributeEnum.None;
            }
        }

        private void Awake()
        {
            // サブスクライバーに関数を登録
            _phaseSubscriber.Subscribe(OnPhaseChangeReceived).AddTo(this.GetCancellationTokenOnDestroy());
        }
        
        public async UniTask LoadAsync(CancellationToken ct)
        {
            // PlayerDataのロード
            _playerData = await Addressables.LoadAssetAsync<PlayerData>("PlayerData")
                .ToUniTask(cancellationToken: ct);
            
            LoadedInit();

            _isPlayerDataLoaded = true;
        }

        /// <summary>ロード後の初期化処理</summary>
        void LoadedInit()
        {
            CurrentHp = _playerData.PlayerHp;
            Attack = _playerData.Attack;
            Defense = _playerData.Defense;
        }

        private void OnPhaseChangeReceived(PhaseCallback callback)
        {
            if (callback.IsTurnChanged)
            {
                PassBuffTurn(_attackBuffData);
                PassBuffTurn(_defenseBuffData);
            }

            void PassBuffTurn(List<IDamageable.BuffData> buffData)
            {
                var removes = new List<IDamageable.BuffData>();
                
                foreach (var buff in buffData)
                {
                    buff.PassTurn();

                    if (!buff.IsEnableBuff)
                    {
                        removes.Add(buff);
                    }
                }

                foreach (var removeBuff in removes)
                {
                    buffData.Remove(removeBuff);
                }
            }
        }
        
        public void SendDamage(int damage)
        {
            CurrentHp -= damage;
        }

        public void Heal(int healHp)
        {
            CurrentHp += healHp;
        }

        public void SetBuff(IDamageable.BuffData buffData)
        {
            if (buffData.Type == IDamageable.BuffData.BuffType.Attack)
            {
                _attackBuffData.Add(buffData);
            }
            else
            {
                _defenseBuffData.Add(buffData);
            }
        }
    }
}