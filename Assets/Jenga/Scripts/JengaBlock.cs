using UnityEngine;

namespace MyJenga.Jenga.Scripts
{
	public class JengaBlock : MonoBehaviour
	{
		[SerializeField] private Outline _outline;
		[SerializeField] private MeshRenderer _renderer;

		[Header("Mastery level materials")]
		[SerializeField] private Material _glass;
        [SerializeField] private Material _wood;
        [SerializeField] private Material _stone;

        public enum BlockType
        {
            Glass,
            Wood,
            Stone
        }

        public int Id { get; private set; }

        public BlockType Type { get; private set; }

        public Vector3 SizeBounds => _renderer.bounds.size;

		private void Start()
		{
			_outline.enabled = false;
		}

		/// <summary>
		/// Initialises the block's properties from grades data
		/// </summary>
		/// <param name="type"></param>
		public void Init(GradesData data)
		{
			Id = data.id;

            Type = data.mastery switch
            {
                0 => BlockType.Glass,
                1 => BlockType.Wood,
                2 => BlockType.Stone,
                _ => BlockType.Glass
            };

            _renderer.material = Type switch
			{
				BlockType.Glass => _glass,
				BlockType.Wood => _wood,
				BlockType.Stone => _stone,
				_ => _glass
			};

        }

		/// <summary>
		/// Sets the block's outline enabled
		/// </summary>
		/// <param name="enable"></param>
		public void SetOutlineEnabled(bool enable)
		{
			_outline.enabled = enable;
		}

        /// <summary>
        /// Obtains the appropriate material from mastery level
        /// </summary>
        /// <param name="mastery"></param>
        /// <returns></returns>
        private BlockType GetBlockTypeFromMastery(int mastery)
        {
            return mastery switch
            {
                0 => BlockType.Glass,
                1 => BlockType.Wood,
                2 => BlockType.Stone,
                _ => BlockType.Glass
            };
        }
    }
}