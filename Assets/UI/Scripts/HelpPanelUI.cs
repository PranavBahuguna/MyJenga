using UnityEngine;
using UnityEngine.UI;

namespace MyJenga.UI.Scripts
{
	public class HelpPanelUI : MonoBehaviour
	{
		[SerializeField] private Button _okButton;

		public Button OkButton => _okButton;
	}
}
