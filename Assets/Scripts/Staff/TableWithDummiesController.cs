using Persons.Player;
using UnityEngine;

namespace Staff
{
    public class TableWithDummiesController : MonoBehaviour
    {
        private bool _playerDetect;
        [SerializeField] private float width;
        [SerializeField] private float height;
        [SerializeField] private LayerMask whatIsPlayer;
        [SerializeField] private Transform center;

        [SerializeField] private GameObject firstDummy;
        [SerializeField] private GameObject secondDummy;

        private void Update()
        {
            _playerDetect = Physics2D.OverlapBox(center.position, 
                new Vector2(width, height), 0, whatIsPlayer);

            if (!_playerDetect) return;
            if (!Input.GetKeyDown(KeyCode.E)) return;

            if (PlayerController.PerspectiveLetFirstDummy && PlayerInventory.HasFirstDummy)
            {
                firstDummy.SetActive(true);
                PlayerInventory.HasFirstDummy = false;
                PlayerController.PerspectiveLetFirstDummy = false;
                PlayerController.PerspectivePhrase1 = true;
                PlayerController.PerspectiveTakeSecondDummy = true;
            }

            if (PlayerController.PerspectiveLetSecondDummy && PlayerInventory.HasSecondDummy)
            {
                secondDummy.SetActive(true);
                PlayerInventory.HasSecondDummy = false;
                PlayerController.PerspectiveLetSecondDummy = false;
                PlayerController.DialogWithKiril6 = true;
            }
        }
    
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.green;
            var position = center.position;
            Gizmos.DrawWireCube(position, new Vector2(width, height));
        }
    }
}
