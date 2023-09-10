using UnityEngine.UI;

namespace MyJenga.UI.Scripts.Interfaces
{
    public interface IRightToolbarUI
    {
        /// <summary>
        /// Accessor to test stack button to test current stack
        /// </summary>
        Button TestStackButton { get; }
        
        /// <summary>
        /// Accessor to reset button to reset the current stack
        /// </summary>
        Button ResetButton { get; }

        /// <summary>
        /// Accessor to help button to show the help UI
        /// </summary>
        Button HelpButton { get; }
        
        /// <summary>
        /// Accessor to quit button to show the exit menu UI
        /// </summary>
        Button QuitButton { get; }

        /// <summary>
        /// Set all buttons enabled or not
        /// </summary>
        /// <param name="value"></param>
        void SetButtonsEnabled(bool value);
    }
}