using UnityEngine;

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

        if (PlayerController.PerspectiveLetFirstDummy && PlayerInventory.hasFirstDummy)
        {
            firstDummy.SetActive(true);
            PlayerInventory.hasFirstDummy = false;
            PlayerController.PerspectiveLetFirstDummy = false;
            PlayerController.PerspectivePhrase1 = true;
            PlayerController.PerspectiveTakeSecondDummy = true;
        }

        if (PlayerController.PerspectiveLetSecondDummy && PlayerInventory.hasSecondDummy)
        {
            secondDummy.SetActive(true);
            PlayerInventory.hasSecondDummy = false;
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
