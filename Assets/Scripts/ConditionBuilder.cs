
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.BuildScene
{
    public enum TileState
    {
        empty,
        block,
        red,
        green,
        yellow,
    }

    public class ConditionBuilder : MonoBehaviour
    {
        [SerializeField] GameObject tilePrefab;
        [SerializeField] Transform positionTile;

        public List<GameObject> TileList { get; private set; }
        public List<int> conditions { get; private set; }

        private float sizeTileX = 0;
        private int boxCount = 0;

        public void Build(int levelColumns, List<int> condList, List<GameObject> prefabsBoxList)
        {
            TileList = new List<GameObject>();
            conditions = condList;

            RandomizeList(prefabsBoxList);

            for(int i = 0; i < levelColumns; i++)
            {
                GameObject tileObject = Instantiate(tilePrefab, new Vector3(positionTile.position.x + sizeTileX, positionTile.position.y, positionTile.position.z), Quaternion.identity);
                sizeTileX += 1;
                tileObject.transform.parent = positionTile;

                Tile tile = tileObject.GetComponent<Tile>();
                tile.SetCollider(false);

                foreach(int cond in condList)
                {
                    if(cond == i)
                    {
                        GameObject box = Instantiate(prefabsBoxList[boxCount], tileObject.transform.position, Quaternion.identity);
                        
                        box.transform.parent = tileObject.transform;
                        box.GetComponent<BoxCollider>().enabled = false;
                        tile.State = (TileState)Enum.Parse(typeof(TileState), box.tag);                     
                        tile.SetObstacle(false);
                        boxCount++;
                        break;
                    }
                    else
                    {
                        tile.State = TileState.block;
                        tile.SetObstacle(true);
                    }
                }
                
                TileList.Add(tileObject);
            }

        }

        public void RandomizeList(List<GameObject> lst)
        {
            for (int i = lst.Count - 1; i >= 1; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                var temp = lst[j];
                lst[j] = lst[i];
                lst[i] = temp;
            }
        }

    }
}

