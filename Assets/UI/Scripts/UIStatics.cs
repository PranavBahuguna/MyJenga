using UnityEngine.EventSystems;

namespace MyJenga.UI.Scripts
{
	public static class UIStatics
	{
		public static bool IsMouseOverUI()
		{
			// There is likely a much better way to do this, but given that there are currently no
			// other event system objects other than UI in the game, this works well enough for now.
			return EventSystem.current.IsPointerOverGameObject();
		}
	}
}
