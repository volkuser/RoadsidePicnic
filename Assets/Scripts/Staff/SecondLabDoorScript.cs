using Persons.Player;
using UnityEngine;

namespace Staff
{
    public class SecondLabDoorScript : MonoBehaviour
    {
        private bool _playerDetect;
        [SerializeField] private float width;
        [SerializeField] private float height;
        [SerializeField] private LayerMask whatIsPlayer;
        [SerializeField] private Transform zoneCenter;

        private void Update()
        {
            _playerDetect = Physics2D.OverlapBox(zoneCenter.position, 
                new Vector2(width, height), 0, whatIsPlayer);
        
            if (!_playerDetect) return;
            if (!PlayerController.PerspectiveOnDialogWithKiril4) return;
            PlayerController.DialogWithKiril4 = true;
        }
    
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.green;
            var position = zoneCenter.position;
            Gizmos.DrawWireCube(position, new Vector2(width, height));  
        }
    }
}
