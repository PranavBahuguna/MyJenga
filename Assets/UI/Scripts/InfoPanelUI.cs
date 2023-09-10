using MyJenga.UI.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace MyJenga.UI.Scripts
{
	public class InfoPanelUI : MonoBehaviour, IInfoPanelUI
    {
		[SerializeField] private Text _text;
		[SerializeField] private ScrollRect _scrollRect;

		/// <inheritdoc/>
		public void SetText(string text)
		{
			_text.text = text;
			_scrollRect.verticalNormalizedPosition = 1.0f; // reset to top
		}

        /// <inheritdoc/>
        public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}
	}
}
