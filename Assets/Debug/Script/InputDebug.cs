using MessagePipe;
using RPG.Input;
using UnityEngine;
using VContainer;

namespace RPG.Debug
{
    public class InputDebug : MonoBehaviour
    {
        [Inject] private ISubscriber<IngameInputParams> _subscriber;

        private void Awake()
        {
            _subscriber.Subscribe(InputEventReceived);
        }

        private void InputEventReceived(IngameInputParams inputParams)
        {
            if (inputParams.UI.IsSubmitted)
            {
                UnityEngine.Debug.Log("Submit");
            }

            if (inputParams.UI.NavigateDir != Vector2.zero)
            {
                UnityEngine.Debug.Log($"{inputParams.UI.NavigateDir.ToString()}");
            }
        }
    }
}
