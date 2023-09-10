using UnityEngine.UI;

namespace MyJenga.UI.Scripts.Interfaces
{
    public interface IQuitPanelUI
    {
        /// <summary>
        /// Accessor to yes button to close application
        /// </summary>
        Button YesButton { get; }

        /// <summary>
        /// Accessor to no button to cancel closing application
        /// </summary>
        Button NoButton { get; }

        /// <summary>
        /// Sets the quit panel active or not
        /// </summary>
        /// <param name="active"></param>
        void SetActive(bool active);
    }
}