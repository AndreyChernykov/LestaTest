using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Play
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] float speed;

        public void Move(Transform tr)
        {
            gameObject.transform.position = Vector3.Lerp(transform.position, new Vector3(tr.position.x, transform.position.y, tr.position.z), speed * Time.deltaTime);
        }
    }
}

