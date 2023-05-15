using UnityEngine;

namespace MyJenga.Jenga.Scripts
{
	public class JengaBlock : MonoBehaviour
	{
		public enum BlockType
		{
			glass,
			wood,
			stone
		}

		[SerializeField] private BlockType _blockType;
		[SerializeField] private Outline _outline;

		public BlockType Type => _blockType;

		public int Id { get; set; }

		private void Start()
		{
			_outline.enabled = false;
		}

		/// <summary>
		/// Sets the block's outline enabled
		/// </summary>
		/// <param name="enable"></param>
		public void SetOutlineEnabled(bool enable)
		{
			_outline.enabled = enable;
		}
	}
}