using UnityEngine.UI;

namespace MyJenga.UI.Scripts.Interfaces
{
    public interface IHelpPanelUI
    {
        /// <summary>
        /// Accessor to the help panel's Ok button to close it
        /// </summary>
        Button OkButton { get; }

        /// <summary>
        /// Sets the help panel active or not
        /// </summary>
        /// <param name="active"></param>
        void SetActive(bool active);
    }
}
