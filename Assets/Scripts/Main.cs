using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Configs;
using Game.BuildScene;
using Game.Play;

namespace Game
{
    public class Main : MonoBehaviour
    {
        [SerializeField] ConfigsLevel configsLevel;
        [SerializeField] ConditionBuilder conditionBuilder;
        [SerializeField] LevelBuilder levelBuilder;
        [SerializeField] GameManager gameManager;
        [SerializeField] UIManager uiManager;
        [SerializeField] Player player;
        private void Start()
        {
            conditionBuilder.Build(configsLevel.levelColumns, configsLevel.conditionBox, configsLevel.boxes);
            levelBuilder.Build(configsLevel.levelColumns, configsLevel.levelLines, configsLevel.conditionBox, configsLevel.boxes);
            levelBuilder.CreateBoxes(configsLevel.conditionBox, configsLevel.boxes);

            
        }

        private void Update()
        {
            player.Raycast(configsLevel.boxes);
            //gameManager.CheckArray(configsLevel.conditionBox, conditionBuilder.TileList, levelBuilder.TileArray);
            GameManager.SendEvent();
        }
    }
}

