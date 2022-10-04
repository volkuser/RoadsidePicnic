using TMPro;
using UnityEngine;

public class TakingDummyFromBucketController : MonoBehaviour
{
    private bool _playerDetect;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private Transform groundPosition;
    [SerializeField] private LayerMask whatIsPlayer;

    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite withoutDummy;
    
    [SerializeField] private GameObject dialogWindow;
    [SerializeField] private TextMeshProUGUI guiText;

    private void Awake() => _spriteRenderer = GetComponent<SpriteRenderer>();    
    
    private void Update()
    {
        _playerDetect = Physics2D.OverlapBox(groundPosition.position, 
            new Vector2(width, height), 0, whatIsPlayer);

        if (!_playerDetect) return;
        if (PlayerController.PerspectivePhrase1)
        {
            StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow,
                "*Уборщицы нигде не видно. Зато, кажется, в ведре что-то лежит…*", guiText));
            PlayerController.PerspectivePhrase1 = false;
        }
        if (!PlayerController.PerspectiveTakeSecondDummy) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;
        if (PlayerInventory.hasSecondDummy) return;
        PlayerInventory.hasSecondDummy = true;
        _spriteRenderer.sprite = withoutDummy;
        StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, 
            "*Попалась. От меня не спрячешься.*", guiText));
        PlayerController.PerspectiveTakeSecondDummy = false;
        PlayerController.PerspectiveLetSecondDummy = true;
    }
    
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.magenta;
        var position = groundPosition.position;
        Gizmos.DrawWireCube(position, new Vector2(width, height));  
    }
}
