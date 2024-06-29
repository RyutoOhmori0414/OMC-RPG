using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RPG.Battle.System
{
    public sealed class LoadManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _loadGameObject;

        private Action _onLoadEnd;
        
        public event Action OnLoadEnd
        {
            add => _onLoadEnd += value;
            remove => _onLoadEnd -= value;
        }

        private void Awake()
        {
            LoadDataAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTask LoadDataAsync(CancellationToken ct)
        {
            var loadAwait = new List<UniTask>();

            foreach (var load in _loadGameObject)
            {
                foreach (var loadableComponent in load.GetComponents<ILoadable>())
                {
                    loadAwait.Add(loadableComponent.LoadAsync(ct));
                }
            }

            await UniTask.WhenAll(loadAwait);
            
            _onLoadEnd?.Invoke();
        }
    }   
}