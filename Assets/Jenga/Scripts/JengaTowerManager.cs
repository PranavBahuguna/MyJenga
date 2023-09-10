using MyJenga.Jenga.Scripts.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyJenga.Jenga.Scripts
{
    using BlockType = JengaBlock.BlockType;

    public class JengaTowerManager : IJengaTowerManager
    {
        private readonly JengaTower.Factory _towerFactory;
        private readonly Dictionary<TowerGrade, JengaTower> _towersDict = new();

        private List<GradesData> _gradesData = new();

        public JengaTowerManager(JengaTower.Factory towerFactory)
        {
            _towerFactory = towerFactory;
        }

        /// <summary>
        /// Tries loading all grades data from a given text file
        /// </summary>
        /// <param name="inputData"></param>
        public void LoadData(TextAsset inputData)
        {
            _gradesData = JsonConvert.DeserializeObject<List<GradesData>>(inputData.text);
        }

        /// <summary>
        /// Activates/deactivates all blocks of a given type for a JengaTower
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="blockType"></param>
        /// <param name="active"></param>
        public void SetTowerBlocksActive(TowerGrade grade, BlockType blockType, bool active)
        {
            if (_towersDict.TryGetValue(grade, out JengaTower tower))
            {
                tower.SetBlocksActive(blockType, active);
            }
            else
            {
                Debug.LogError("No tower for " + grade + " exists!");
            }
        }

        /// <summary>
        /// Constructs a Jenga tower 
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="spawnPosition"></param>
        public void BuildTower(TowerGrade grade, Vector3 spawnPosition)
        {
            // Rebuild tower if it already exists
            if (_towersDict.TryGetValue(grade, out JengaTower tower))
            {
                tower.Build(spawnPosition);
                return;
            }

            // Get all data for this grade, ordered by domain, cluster then standard id ascending
            IEnumerable<GradesData> dataAtGrade = _gradesData.Where(x => x.grade == TowerGradeToString(grade));

            if (dataAtGrade.Count() == 0)
            {
                Debug.LogWarning("Cannot create tower, no data for " + grade + " has been loaded.");
                return;
            }

            // Construct a new tower and add to storage
            JengaTower newTower = _towerFactory.Create(dataAtGrade);
            newTower.Build(spawnPosition);
            _towersDict[grade] = newTower;
        }

        /// <summary>
        /// Tries retrieving grades data for a block, given its id
        /// </summary>
        /// <param name="blockId"></param>
        /// <param name="outData"></param>
        /// <returns>True if data for that id was found, false otherwise</returns>
        public bool TryGetDataForBlock(int blockId, out GradesData outData)
        {
            outData = default;
            foreach (GradesData data in _gradesData)
            {
                if (data.id == blockId)
                {
                    outData = data;
                    return true;
                }
            }
            return false;
        }

        private string TowerGradeToString(TowerGrade towerGrade)
        {
            return towerGrade switch
            {
                TowerGrade.Grade6 => "6th Grade",
                TowerGrade.Grade7 => "7th Grade",
                TowerGrade.Grade8 => "8th Grade",
                _ => "Unknown"
            };
        }
    }
}