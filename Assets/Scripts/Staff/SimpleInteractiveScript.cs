using TMPro;
using UnityEngine;

public class SimpleInteractiveScript : MonoBehaviour
{
    [SerializeField] private GameObject dialogWindow;
    [SerializeField] private string textIfClockedKeyE;
    [SerializeField] private TextMeshProUGUI guiText;
    
    private bool _playerDetect;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private Transform center;
    [SerializeField] private LayerMask whatIsPlayer;

    private void Update()
    {
        _playerDetect = Physics2D.OverlapBox(center.position, 
            new Vector2(width, height), 0, whatIsPlayer);
        
        if (!_playerDetect) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;

        StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, textIfClockedKeyE, guiText));
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        var position = center.position;
        Gizmos.DrawWireCube(position, new Vector2(width, height));  
    }
}
