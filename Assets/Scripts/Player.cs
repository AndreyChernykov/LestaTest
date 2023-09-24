using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.BuildScene;
using System;

namespace Game.Play
{

    public class Player : MonoBehaviour
    {
        [SerializeField] PlayerController playerController;
        [SerializeField] GameObject cursor;
        [SerializeField] LevelBuilder levelBuilder;
        [SerializeField] ConditionBuilder conditionBuilder;
        [SerializeField] GameManager gameManager;
        [SerializeField] CameraMover cameraMover;
        [SerializeField] Audio audioObj;

        int maxItemOnTile = 2;

        private GameObject selectedObject;
        private Vector3 lastPositionBox;
        private Tile lastTile = null;
        private List<(int, int)> numsTile = new List<(int, int)>();

        private RaycastHit[] rcColliders;

        public void Raycast(List<GameObject> boxes)
        {
            Vector3 point = Camera.main.transform.position;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(-point, Input.mousePosition, Color.red);

            rcColliders = Physics.RaycastAll(ray);

            if (Physics.Raycast(ray, out hit) && gameManager.isPlay)
            {
                
                Drag(boxes, hit);

                if (rcColliders.Length > maxItemOnTile)
                {
                    NotDrag();
                }
            }

        }

        private void NotDrag()
        {
            if (selectedObject != null)
            {
                
                selectedObject.transform.position = lastPositionBox;
                selectedObject.transform.parent = gameObject.transform;

                lastTile.State = (TileState)Enum.Parse(typeof(TileState), selectedObject.tag);

                
            }
        }

        private void Drag(List<GameObject> boxes, RaycastHit rh)
        {
            cameraMover.Move(cursor.transform);
            cursor.transform.position = new Vector3(rh.transform.position.x, cursor.transform.position.y, rh.transform.position.z);
            Cursor cursorScr = cursor.GetComponent<Cursor>();           

            if (playerController.ClickBox())
            {
       
                foreach (GameObject box in boxes)
                {
                    if (rh.collider.tag == box.tag)
                    {
                        cursorScr.SetCursore(1);
                        audioObj.PlayEffect(0);

                        lastPositionBox = rh.transform.position;
                        rh.collider.transform.parent = cursor.transform;
                        selectedObject = rh.collider.gameObject;

                        lastTile = SearchInArray(levelBuilder.TileArray, selectedObject.transform).GetComponent<Tile>();
                        lastTile.State = TileState.empty;
                        
                        break;
                    }

                }

            }

            if (playerController.UnClickBox())
            {
                cursorScr.SetCursore(0);
                

                if (selectedObject != null)
                {
                    Tile newTile = SearchInArray(levelBuilder.TileArray, selectedObject.transform).GetComponent<Tile>();
                    if (newTile.State == TileState.empty)
                    {
                        if (Mathf.Abs(numsTile[0].Item1 - numsTile[1].Item1) >= 1 && Mathf.Abs(numsTile[0].Item2 - numsTile[1].Item2) >= 1)
                        {
                            NotDrag();
                        }
                        else
                        {
                            audioObj.PlayEffect(0);
                            newTile.State = (TileState)Enum.Parse(typeof(TileState), selectedObject.tag);
                        }
                        
                    }
                    else
                    {
                        audioObj.PlayEffect(1);

                        selectedObject.transform.position = lastPositionBox;
                        lastTile.State = (TileState)Enum.Parse(typeof(TileState), selectedObject.tag);
                    }

                    if (newTile != lastTile) gameManager.StepCounter();
                    numsTile.Clear();
                    selectedObject.transform.parent = gameObject.transform;
                    selectedObject = null;
                }

                gameManager.CheckArray(conditionBuilder.conditions, conditionBuilder.TileList, levelBuilder.TileArray);
            }

        }
        
        private GameObject SearchInArray(GameObject[,] array, Transform trans)
        {
            Vector2 tVector = new Vector2(trans.position.x, trans.position.z);
            
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Vector2 aVector = new Vector2(array[i, j].transform.position.x, array[i, j].transform.position.z);
                    if (tVector == aVector)
                    {
                        numsTile.Add((i, j));
                        return array[i, j];
                    }
                }
            }
            return null;
        }

    }

}

