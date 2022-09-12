using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool _playerDetect;
    //private Transform _transform;
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
    
    void Update()
    {
        _playerDetect = Physics2D.OverlapBox(groundPosition.position, 
            new Vector2(width, height), 0, whatIsPlayer);

        if (!_playerDetect) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;
        if(!_isOpened) {
            _animator.ResetTrigger(Close);
            _animator.SetTrigger(Open);
            _boxCollider2D.enabled = false;
            _isOpened = true;
        }
        else
        {
            _animator.ResetTrigger(Open);
            _animator.SetTrigger(Close);
            _boxCollider2D.enabled = true;
            _isOpened = false;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        var position = groundPosition.position;
        Gizmos.DrawWireCube(position, new Vector2(width, height));  
    }
}
