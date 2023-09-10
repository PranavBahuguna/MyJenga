using MyJenga.UI.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace MyJenga.UI.Scripts
{
    public class LeftToolbarUI : MonoBehaviour, ILeftToolbarUI
    {
        [SerializeField] private Toggle _sixthGradeToggle;
        [SerializeField] private Toggle _seventhGradeToggle;
        [SerializeField] private Toggle _eighthGradeToggle;

        /// <inheritdoc/>
        public Toggle SixthGradeToggle => _sixthGradeToggle;

        /// <inheritdoc/>
        public Toggle SeventhGradeToggle => _seventhGradeToggle;

        /// <inheritdoc/>
        public Toggle EighthGradeToggle => _eighthGradeToggle;

        /// <inheritdoc/>
        public void SetTogglesEnabled(bool value)
        {
            _sixthGradeToggle.enabled = value;
            _seventhGradeToggle.enabled = value;
            _eighthGradeToggle.enabled = value;
        }
    }
}
