using UnityEngine;
using UnityEngine.UI;

namespace MyJenga.UI.Scripts
{
	public class LeftToolbarUI : MonoBehaviour
	{
		[SerializeField] private Toggle _sixthGradeToggle;
		[SerializeField] private Toggle _seventhGradeToggle;
		[SerializeField] private Toggle _eighthGradeToggle;

		public Toggle SixthGradeToggle => _sixthGradeToggle;
		public Toggle SeventhGradeToggle => _seventhGradeToggle;
		public Toggle EighthGradeToggle => _eighthGradeToggle;

		/// <summary>
		/// Set all toggles enabled or not
		/// </summary>
		/// <param name="value"></param>
		public void SetTogglesEnabled(bool value)
		{
			_sixthGradeToggle.enabled = value;
			_seventhGradeToggle.enabled = value;
			_eighthGradeToggle.enabled = value;
		}
	}
}
