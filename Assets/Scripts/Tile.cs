using UnityEngine;

namespace Game.BuildScene
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] GameObject boxBlock;
        [SerializeField] MeshCollider meshCollider;
        public TileState State { get; set; }

        public void SetCollider(bool isCollided)
        {
            meshCollider.enabled = isCollided;
            boxBlock.GetComponent<BoxCollider>().enabled = isCollided;
        }

        public void SetObstacle(bool activate)
        {           
            boxBlock.SetActive(activate);
        }
    }
    
}


