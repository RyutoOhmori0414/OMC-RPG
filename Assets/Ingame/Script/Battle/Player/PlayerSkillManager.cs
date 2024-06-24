using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using RPG.Battle.Skill;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RPG.Battle.Player
{
    public sealed class PlayerSkillManager : MonoBehaviour, ILoadable
    {
        [SerializeField] private bool _loadSkill = true;
        [SerializeField] private AssetReference[] _loadSkillRef = default;

        private SkillData[] _playerSkill;

        public SkillData[] PlayerSkill
        {
            get
            {
                if (_isSkillLoaded) return _playerSkill;
                
                Debug.LogWarning("まだロードされていません");
                return null;
            }
        }
        
        private bool _isSkillLoaded = false;
        
        public async UniTask LoadAsync(CancellationToken ct)
        {
            _playerSkill = new SkillData[_loadSkillRef.Length];
            var awaits = new UniTask<SkillData>[_loadSkillRef.Length];
            
            for (int i = 0; i < _loadSkillRef.Length; i++)
            {
                awaits[i] = Addressables.LoadAssetAsync<SkillData>(_loadSkillRef[i])
                    .ToUniTask(cancellationToken: ct);
            }

            _playerSkill = await UniTask.WhenAll(awaits);
            _isSkillLoaded = true;
        }
    }   
}