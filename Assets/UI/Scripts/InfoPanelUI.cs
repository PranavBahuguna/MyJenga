using UnityEngine;
using UnityEngine.UI;

namespace MyJenga.UI.Scripts
{
	public class InfoPanelUI : MonoBehaviour
	{
		[SerializeField] private Text _text;
		[SerializeField] private ScrollRect _scrollRect;

		/// <summary>
		/// Set the text content of the panel
		/// </summary>
		/// <param name="text"></param>
		public void SetText(string text)
		{
			_text.text = text;
			_scrollRect.verticalNormalizedPosition = 1.0f; // reset to top
		}

		/// <summary>
		/// Set the info panel enabled or not
		/// </summary>
		/// <param name="enabled"></param>
		public void SetEnabled(bool enabled)
		{
			gameObject.SetActive(enabled);
		}
	}
}
