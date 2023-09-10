using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyJenga.Jenga.Scripts
{
    using BlockType = JengaBlock.BlockType;

    public partial class JengaTower
    {
        private readonly List<JengaBlock> _blocks = new();

        private readonly float _offset;
        private readonly float _blockHeight;

        public JengaTower(JengaBlock blockPrefab, IEnumerable<GradesData> dataAtGrade)
        {
            IEnumerable<GradesData> sortedGradesData = dataAtGrade
                .OrderBy(x => x.domain)
                .ThenBy(x => x.cluster)
                .ThenBy(x => x.standardId);

            // Instantiate and add all required blocks
            foreach (GradesData data in sortedGradesData)
            {
                JengaBlock newBlock = GameObject.Instantiate(blockPrefab);
                newBlock.Init(data);
                newBlock.gameObject.SetActive(false);
                _blocks.Add(newBlock);
            }

            // Calculate height and offset required for each block
            Vector3 sizeBounds = blockPrefab.SizeBounds;
            _blockHeight = sizeBounds.y;
            _offset = 0.5f * (sizeBounds.x - sizeBounds.z);
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
    }
}