using UnityEngine;

namespace RPG.Input
{
    public struct IngameInputParams
    {
        public UIInput UI { get; }

        internal IngameInputParams(IngameInput input)
        {
            UI = new(input.UI);
        }

        public struct UIInput
        {
            public Vector2 NavigateDir { get; }
            public bool IsSubmitted { get; }

            internal UIInput(IngameInput.UIActions uiActions)
            {
                NavigateDir = uiActions.Navigate.ReadValue<Vector2>();
                IsSubmitted = uiActions.Submit.IsPressed();
            }
        }
    }
}
