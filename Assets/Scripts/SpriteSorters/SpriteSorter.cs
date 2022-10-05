using UnityEngine;

namespace SpriteSorters
{
    public class SpriteSorter : MonoBehaviour
    {
        [SerializeField] private bool isStatic = true;
        [SerializeField] private float offset;
        private const int SortingOrderBase = 0;
        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void LateUpdate()
        {
            _renderer.sortingOrder = (int)(SortingOrderBase - transform.position.y + offset);
        
            if (isStatic) Destroy(this);
        }
    }
}
