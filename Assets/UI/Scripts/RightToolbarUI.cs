using MyJenga.UI.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace MyJenga.UI.Scripts
{
	public class RightToolbarUI : MonoBehaviour, IRightToolbarUI
    {
		[SerializeField] private Button _testStackButton;
		[SerializeField] private Button _resetButton;
		[SerializeField] private Button _helpButton;
		[SerializeField] private Button _quitButton;

		/// <inheritdoc/>
		public Button TestStackButton => _testStackButton;

        /// <inheritdoc/>
        public Button ResetButton => _resetButton;

        /// <inheritdoc/>
        public Button HelpButton => _helpButton;

        /// <inheritdoc/>
        public Button QuitButton => _quitButton;

        /// <inheritdoc/>
        public void SetButtonsEnabled(bool value)
		{
			_testStackButton.enabled = value;
			_resetButton.enabled = value;
			_helpButton.enabled = value;
			_quitButton.enabled = value;
		}
	}
}