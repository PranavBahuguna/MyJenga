namespace MyJenga.UI.Scripts.Interfaces
{
    public interface IInfoPanelUI
    {
        /// <summary>
        /// Set the text content of the panel
        /// </summary>
        /// <param name="text"></param>
        void SetText(string text);

        /// <summary>
        /// Set the info panel active or not
        /// </summary>
        /// <param name="active"></param>
        void SetActive(bool active);
    }
}
