using TMPro;
using UnityEngine;

public class LiftDoorController : MonoBehaviour
{
    private bool _playerDetect;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform zoneCenter;
    private BoxCollider2D _boxCollider2D;
    private bool _isClosed;

    [SerializeField] private GameObject dialogWindow;
    [SerializeField] private TextMeshProUGUI guiText;
    
    private Animator _animator;
    private static readonly int Open = Animator.StringToHash("Open");
    private static readonly int Close = Animator.StringToHash("Close");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        Invoke(nameof(OpenDoors), 0.2f);
        Invoke(nameof(CloseDoors), 7f);
    }

    private void Update()
    {
        _playerDetect = Physics2D.OverlapBox(zoneCenter.position, 
            new Vector2(width, height), 0, whatIsPlayer);

        if (!Input.GetKeyDown(KeyCode.E)) return;
        if (!_playerDetect) return;
        if (!_isClosed) return;
        StartCoroutine(ForDialogWindow.OneUseWithOne(dialogWindow, "*Ну емае. Застрял.*", guiText));
    }

    private void BoxColliderDisabled() => _boxCollider2D.enabled = false;

    private void OpenDoors() => _animator.SetTrigger(Open);

    private void AfterClosing()
    {
        _boxCollider2D.enabled = true;
        _isClosed = true;
    }

    private void CloseDoors()
    {
        _animator.ResetTrigger(Open);
        _animator.SetTrigger(Close);
        _boxCollider2D.enabled = true;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        var position = zoneCenter.position;
        Gizmos.DrawWireCube(position, new Vector2(width, height));  
    }
}
