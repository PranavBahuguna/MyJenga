using UnityEngine.UI;

namespace MyJenga.UI.Scripts.Interfaces
{
    public interface ILeftToolbarUI
    {
        /// <summary>
        /// 6th grade toggle accessor
        /// </summary>
        Toggle SixthGradeToggle { get; }

        /// <summary>
        /// 7th grade toggle accessor
        /// </summary>
        Toggle SeventhGradeToggle { get; }

        /// <summary>
        /// 8th grade toggle accessor
        /// </summary>
        Toggle EighthGradeToggle { get; }

        /// <summary>
        /// Set all grade toggles enabled or not
        /// </summary>
        /// <param name="value"></param>
        void SetTogglesEnabled(bool value);
    }
}
