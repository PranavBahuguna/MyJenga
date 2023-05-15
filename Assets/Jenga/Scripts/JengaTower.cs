using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyJenga.Jenga.Scripts
{
	using BlockType = JengaBlock.BlockType;

	public class JengaTower
	{
		private readonly List<JengaBlock> _blocks = new();

		private readonly JengaBlock _glassPrefab;
		private readonly JengaBlock _woodPrefab;
		private readonly JengaBlock _stonePrefab;

		private readonly float _offset;
		private readonly float _blockHeight;

		public JengaTower(IEnumerable<GradesData> dataAtGrade)
		{
			IEnumerable<GradesData> sortedGradesData = dataAtGrade
				.OrderBy(x => x.domain)
				.ThenBy(x => x.cluster)
				.ThenBy(x => x.standardId);

			_glassPrefab = Resources.Load<JengaBlock>("glass-block");
			_woodPrefab = Resources.Load<JengaBlock>("wood-block");
			_stonePrefab = Resources.Load<JengaBlock>("stone-block");

			// Instantiate and add all required blocks
			foreach (GradesData data in sortedGradesData)
			{
				JengaBlock blockPrefab = GetBlockPrefabFromMastery(data.mastery);
				JengaBlock newBlock = GameObject.Instantiate(blockPrefab);
				newBlock.Id = data.id;
				newBlock.gameObject.SetActive(false);
				_blocks.Add(newBlock);
			}

			// Will assume all blocks have the same dimensions
			MeshRenderer meshRenderer = _glassPrefab.GetComponent<MeshRenderer>();
			Vector3 blockSize = meshRenderer.bounds.size;

			_blockHeight = blockSize.y;
			_offset = 0.5f * (blockSize.x - blockSize.z);
		}

		/// <summary>
		/// Activates/deactivates all blocks of a given type
		/// </summary>
		/// <param name="blockType"></param>
		/// <param name="active"></param>
		public void SetBlocksActive(BlockType blockType, bool active)
		{
			foreach (JengaBlock block in _blocks.Where(x => x.Type == blockType))
			{
				block.gameObject.SetActive(active);
			}
		}

		/// <summary>
		/// Constructs a Jenga tower 
		/// </summary>
		/// <param name="spawnPosition"></param>
		public void Build(Vector3 spawnPosition)
		{
			for (int i = 0; i < _blocks.Count(); i++)
			{
				// Calculate block position and rotation
				int layerIndex = i / 3;
				bool evenLayer = layerIndex % 2 == 0;

				Quaternion rotation = evenLayer ? Quaternion.identity : Quaternion.Euler(0.0f, 90.0f, 0.0f);

				int rowIndex = (i % 3) - 1;
				float offset = rowIndex * _offset;

				Vector3 position = new()
				{
					x = spawnPosition.x + (evenLayer ? 0.0f : offset),
					y = spawnPosition.y + _blockHeight * (0.5f + layerIndex),
					z = spawnPosition.z + (evenLayer ? offset : 0.0f)
				};

				// Set the rotation / position to the stored block
				JengaBlock block = _blocks[i];
				block.transform.SetPositionAndRotation(position, rotation);

				// Remove any existing velocity in case we are resetting the tower
				Rigidbody blockRb = block.GetComponent<Rigidbody>();
				blockRb.velocity = Vector3.zero;
				blockRb.angularVelocity = Vector3.zero;

				// Reactivate any disabled blocks
				block.gameObject.SetActive(true);
			}
		}

		/// <summary>
		/// Obtains the appropriate block prefab from mastery level
		/// </summary>
		/// <param name="mastery"></param>
		/// <returns></returns>
		private JengaBlock GetBlockPrefabFromMastery(int mastery)
		{
			return mastery switch
			{
				0 => _glassPrefab,
				1 => _woodPrefab,
				2 => _stonePrefab,
				_ => _glassPrefab
			};
		}
	}
}