using UnityEngine;

namespace MyJenga.Jenga.Scripts.Interfaces
{
    using BlockType = JengaBlock.BlockType;

    public enum TowerGrade { Grade6, Grade7, Grade8 };

    public interface IJengaTowerManager
    {
        /// <summary>
        /// Tries loading all grades data from a given text file
        /// </summary>
        /// <param name="inputData"></param>
        void LoadData(TextAsset inputData);

        /// <summary>
        /// Activates/deactivates all blocks of a given type for a JengaTower
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="blockType"></param>
        /// <param name="active"></param>
        void SetTowerBlocksActive(TowerGrade grade, BlockType blockType, bool active);

        /// <summary>
        /// Constructs a Jenga tower 
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="spawnPosition"></param>
        void BuildTower(TowerGrade grade, Vector3 spawnPosition);

        /// <summary>
        /// Tries retrieving grades data for a block, given its id
        /// </summary>
        /// <param name="blockId"></param>
        /// <param name="outData"></param>
        /// <returns>True if data for that id was found, false otherwise</returns>
        bool TryGetDataForBlock(int blockId, out GradesData outData);
    }
}