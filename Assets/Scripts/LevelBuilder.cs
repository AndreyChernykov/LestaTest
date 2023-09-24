using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.BuildScene
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] GameObject tilePrefab;
        [SerializeField] Transform positionTile;
        [SerializeField] ConditionBuilder conditionBuilder;

        public List<GameObject> boxList { get; private set; }
        public GameObject[,] TileArray { get; private set; }

        private float sizeTileX;
        private float sizeTileZ;

        private int linesInLevel;

        public void Build(int columns, int lines, List<int> condList, List<GameObject> prefabsBoxList)
        {
            TileArray = new GameObject[lines, columns];
            linesInLevel = lines;
            boxList = new List<GameObject>();

            for (int i = 0; i < lines; i++)
            {
                sizeTileX = 0;
                sizeTileZ = i;
                for (int j = 0; j < columns; j++)
                {
                    GameObject tileObject = Instantiate(tilePrefab, new Vector3(positionTile.position.x + sizeTileX, positionTile.position.y, sizeTileZ), Quaternion.identity);
                    sizeTileX += 1;
                    TileArray[i, j] = tileObject;
                    tileObject.transform.parent = positionTile;
                    Tile tile = tileObject.GetComponent<Tile>();

                    foreach(int cond in condList)
                    {
                        tile.State = TileState.block;
                        tile.SetObstacle(true);
                        if (j == cond || i % 2 != 0)
                        {
                            tile.State = TileState.empty;
                            tile.SetObstacle(false);
                            break;
                        }
                    }
                    if (j == 0 || j == columns - 1)
                    {
                        tile.State = TileState.block;
                        tile.SetObstacle(true);
                    }
                    if (i == 0 || i == lines - 1)
                    {
                        tile.State = TileState.block;
                        tile.SetObstacle(true);
                    }
                }

            }
        }

        public void CreateBoxes(List<int> condList, List<GameObject> prefabsBoxList)
        {           

            for(int i = 0; i < prefabsBoxList.Count; i++)
            {
                for(int j = 0; j < linesInLevel-2; j++)
                {
                    GameObject box = Instantiate(prefabsBoxList[i], Vector3.zero, Quaternion.identity);
                    boxList.Add(box);
                }
            }

            conditionBuilder.RandomizeList(boxList);

            int boxObj = 0;

            for (int i = 1; i < TileArray.GetLength(0)-1; i++)
            {
                for (int j = 0; j < TileArray.GetLength(1); j++)
                {

                    foreach (int cond in condList)
                    {
                        if (j == cond)
                        {
                            boxList[boxObj].transform.position = TileArray[i, j].transform.position;
                            TileArray[i, j].GetComponent<Tile>().State = (TileState)Enum.Parse(typeof(TileState), boxList[boxObj].tag);
                            boxObj++;
                            break;
                        }
                    }


                }

            }
        }

    }
}


