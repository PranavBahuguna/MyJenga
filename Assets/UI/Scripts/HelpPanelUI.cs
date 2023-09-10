using UnityEngine;
using UnityEngine.UI;
using MyJenga.UI.Scripts.Interfaces;

namespace MyJenga.UI.Scripts
{
	public class HelpPanelUI : MonoBehaviour, IHelpPanelUI
    {
		[SerializeField] private Button _okButton;

		/// <inheritdoc/>
		public Button OkButton => _okButton;

        /// <inheritdoc/>
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
