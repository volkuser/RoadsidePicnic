using UnityEngine;

public class LiftDoorController : MonoBehaviour
{
    private bool _playerDetect;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private LayerMask whatIsPlayer;
    private bool _isOpened;
    [SerializeField] private Transform groundPosition;
    private BoxCollider2D _boxCollider2D;
    
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
        //TODO LIFT SONG
        Invoke(nameof(CloseDoors), 5f);
    }

    private void Update()
    {
        _playerDetect = Physics2D.OverlapBox(groundPosition.position, 
            new Vector2(width, height), 0, whatIsPlayer);

        if (!_playerDetect || !Input.GetKeyDown(KeyCode.E)) return;
        if(!_isOpened) {
            //TODO LIFT IS BROKEN
        }
    }

    private void OpenDoors()
    {
        _animator.SetTrigger(Open);
        Invoke(nameof(BoxColliderDisabled), 0.6f);
    }
    
    private void BoxColliderDisabled() => _boxCollider2D.enabled = false;
    
    private void CloseDoors()
    {
        _animator.ResetTrigger(Open);
        _animator.SetTrigger(Close);
        _boxCollider2D.enabled = true;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        var position = groundPosition.position;
        Gizmos.DrawWireCube(position, new Vector2(width, height));  
    }
}
