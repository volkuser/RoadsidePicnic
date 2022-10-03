using TMPro;
using UnityEngine;

public class TakingCardFromTableController : MonoBehaviour
{
    private bool _playerDetect;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private Transform groundPosition;
    [SerializeField] private LayerMask whatIsPlayer;

    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite withoutCard;
    
    [SerializeField] private GameObject dialogWindow;
    [SerializeField] private TextMeshProUGUI guiText;

    private void Awake() => _spriteRenderer = GetComponent<SpriteRenderer>();    
    
    private void Update()
    {
        _playerDetect = Physics2D.OverlapBox(groundPosition.position, 
            new Vector2(width, height), 0, whatIsPlayer);

        if (!_playerDetect) return;
        if (!PlayerController.PerspectiveOnTakingCard) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;
        if (PlayerInventory.hasCardForLockSystem) return;
        StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow,
            "*Емае, Кирилл, я даже не особо удивлен.*", guiText));
        PlayerInventory.hasCardForLockSystem = true;
        _spriteRenderer.sprite = withoutCard;
        PlayerController.PerspectiveOnTakingCard = false;
    }
    
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.magenta;
        var position = groundPosition.position;
        Gizmos.DrawWireCube(position, new Vector2(width, height));  
    }
}
