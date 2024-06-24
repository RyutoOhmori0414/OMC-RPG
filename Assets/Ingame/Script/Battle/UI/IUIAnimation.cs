using UnityEngine;

namespace RPG.Battle.UI
{
    public abstract class AbstractUIAnimation : MonoBehaviour
    {
        public abstract void OpenUI();

        public abstract void CloseUI();
    }   
}