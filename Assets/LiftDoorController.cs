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
    [SerializeField] private GameObject roomDarkening;
    private GameObject _player;
    
    private Animator _animator;
    private static readonly int Open = Animator.StringToHash("Open");
    private static readonly int Close = Animator.StringToHash("Close");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        _playerDetect = Physics2D.OverlapBox(groundPosition.position, 
            new Vector2(width, height), 0, whatIsPlayer);

        if (!_playerDetect) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;
        if(!_isOpened) {
            roomDarkening.SetActive(false);
            _animator.ResetTrigger(Close);
            _animator.SetTrigger(Open);
            _boxCollider2D.enabled = false;
            _isOpened = true;
        }
        else
        {
            _animator.ResetTrigger(Open);
            _animator.SetTrigger(Close);
            if (_player.transform.position.y < groundPosition.position.y) Invoke(nameof(DarkeningOverTime), 0.6f);
            _boxCollider2D.enabled = true;
            _isOpened = false;
        }
    }

    private void DarkeningOverTime() => roomDarkening.SetActive(true);

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        var position = groundPosition.position;
        Gizmos.DrawWireCube(position, new Vector2(width, height));  
    }
}
