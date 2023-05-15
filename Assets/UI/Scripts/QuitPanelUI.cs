using UnityEngine;
using UnityEngine.UI;

namespace MyJenga.UI.Scripts
{
	public class QuitPanelUI : MonoBehaviour
	{
		[SerializeField] private Button _yesButton;
		[SerializeField] private Button _noButton;

		public Button YesButton => _yesButton;
		public Button NoButton => _noButton;
	}
}