using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace RPG.Battle.Enemy
{
    public sealed class EnemyController : MonoBehaviour, IDamageable, ILoadable
    {
        [Inject] private ISubscriber<PhaseCallback> _subscriber;
        [SerializeField] private AssetReference _assetReference;
        [SerializeField] private Transform _frontTransform;

        Transform IDamageable.Transform => transform.root;
        Transform IDamageable.FrontTransform => _frontTransform;

        private EnemyDataScriptableObject _enemyData;
        private bool _isEnemyDataLoaded = false;
        
        private List<IDamageable.BuffData> _attackBuffData = new();
        private List<IDamageable.BuffData> _defenseBuffData = new();
        private int _rawAttack = -1;
        private int _rawDefense = -1;
        
        public int CurrentHp { get; private set; } = -1;
        
        public int MaxHp
        {
            get
            {
                if (_isEnemyDataLoaded) return _enemyData.EnemyHp;
                
                Debug.LogWarning("EnemyDataがLoadされていません");
                return -1;
            }
        }

        public SkillAttributeEnum WeakAttribute
        {
            get
            {
                if (_isEnemyDataLoaded) return _enemyData.WeakAttribute;
                
                Debug.LogWarning("EnemyDataがLoadされていません");
                return SkillAttributeEnum.None;
            }
        }

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

        private void Awake()
        {
            _subscriber.Subscribe(OnPhaseChangeReceived);
        }
        
        public async UniTask LoadAsync(CancellationToken ct)
        {
            _enemyData = await Addressables.LoadAssetAsync<EnemyDataScriptableObject>(_assetReference)
                .ToUniTask(cancellationToken: ct);

            _isEnemyDataLoaded = true;
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