using MyJenga.UI.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace MyJenga.UI.Scripts
{
    public class QuitPanelUI : MonoBehaviour, IQuitPanelUI
    {
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;

        /// <inheritdoc/>
        public Button YesButton => _yesButton;

        /// <inheritdoc/>
        public Button NoButton => _noButton;

        /// <inheritdoc/>
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}