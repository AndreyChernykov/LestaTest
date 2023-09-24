using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Play
{
    public class Cursor : MonoBehaviour
    {
        [SerializeField] List<GameObject> cursors = new List<GameObject>();

        public void SetCursore(int num)
        {
            foreach (GameObject go in cursors) go.SetActive(false);
            cursors[num].SetActive(true);
        }
    }
}

