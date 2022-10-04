using TMPro;
using UnityEngine;

public class FirstDoorScript : MonoBehaviour
{
    private bool _isFirst = true;
    
    private bool _playerDetect;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform zoneCenter;
    
    [SerializeField] private GameObject dialogWindow;
    [SerializeField] private TextMeshProUGUI guiText;

    private void Update()
    {
        _playerDetect = Physics2D.OverlapBox(zoneCenter.position, 
            new Vector2(width, height), 0, whatIsPlayer);
        
        if (!_playerDetect) return;
        if (!_isFirst) return;
        StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, "*Нужно найти Кирилла. " +
                                                                 "Думаю, он опять в своей лаборатории*", guiText));
        _isFirst = false;
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        var position = zoneCenter.position;
        Gizmos.DrawWireCube(position, new Vector2(width, height));  
    }
}
