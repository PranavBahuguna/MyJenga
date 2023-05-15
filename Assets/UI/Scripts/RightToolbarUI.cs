using UnityEngine;
using UnityEngine.UI;

namespace MyJenga.UI.Scripts
{
	public class RightToolbarUI : MonoBehaviour
	{
		[SerializeField] private Button _testStackButton;
		[SerializeField] private Button _resetButton;
		[SerializeField] private Button _helpButton;
		[SerializeField] private Button _quitButton;

		public Button TestStackButton => _testStackButton;
		public Button ResetButton => _resetButton;
		public Button HelpButton => _helpButton;
		public Button QuitButton => _quitButton;

		/// <summary>
		/// Set all buttons enabled or not
		/// </summary>
		/// <param name="value"></param>
		public void SetButtonsEnabled(bool value)
		{
			_testStackButton.enabled = value;
			_resetButton.enabled = value;
			_helpButton.enabled = value;
			_quitButton.enabled = value;
		}
	}
}