using System.Threading;
using Cysharp.Threading.Tasks;

namespace RPG.Battle
{
    public interface ILoadable
    {
        /// <summary>AddressableObjectなどを読み込むための関数</summary>
        /// <returns></returns>
        public UniTask LoadAsync(CancellationToken ct);
    }   
}
