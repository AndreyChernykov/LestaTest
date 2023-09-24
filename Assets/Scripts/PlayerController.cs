using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Play
{
    public class PlayerController : MonoBehaviour
    {
        public bool ClickBox()
        {
            if (Input.GetMouseButtonDown(0)) return true;
            return false;
        }

        public bool UnClickBox()
        {
            if (Input.GetMouseButtonUp(0)) return true;
            return false;
        }
    }
}

