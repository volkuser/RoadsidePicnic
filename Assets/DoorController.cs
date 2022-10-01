using JetBrains.Annotations;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // actions
    private bool _playerDetect;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private LayerMask whatIsPlayer;
    private bool _isOpened;
    [SerializeField] private Transform groundPosition;
    private BoxCollider2D _boxCollider2D;
    [SerializeField] private GameObject roomDarkening;
    private GameObject _player;
    
    // lock system
    [SerializeField] private bool hasLockSystem;
    [SerializeField] [CanBeNull] private GameObject lockSystem; 
    [SerializeField] [CanBeNull] private GameObject uiLockSystem;
    [SerializeField] [CanBeNull] private Sprite openedLockSystem;
    [SerializeField] [CanBeNull] private Sprite closedLockSystem;
    
    // animations
    private Animator _animator;
    private static readonly int Open = Animator.StringToHash("Open");
    private static readonly int Close = Animator.StringToHash("Close");
    private static readonly int Use = Animator.StringToHash("Use");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        _playerDetect = Physics2D.OverlapBox(groundPosition.position, 
            new Vector2(width, height), 0, whatIsPlayer);

        if (!_playerDetect) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;
        if(!_isOpened)
        {
            if (hasLockSystem)
            {
                if (!PlayerInventory.hasCardForLockSystem) return;
                if (lockSystem != null && uiLockSystem != null)
                {
                    uiLockSystem.SetActive(true); 
                    var uiLockSystemAnimator = uiLockSystem.GetComponent<Animator>();
                    // uiLockSystemAnimator.ResetTrigger(Use); 
                    uiLockSystemAnimator.SetTrigger(Use);
                }
            }
            roomDarkening.SetActive(false);
            _animator.SetTrigger(Open);
            lockSystem!.GetComponent<SpriteRenderer>().sprite = openedLockSystem;
        }
        else
        {
            _animator.ResetTrigger(Open);
            _animator.SetTrigger(Close);
            if (_player.transform.position.y < groundPosition.position.y) Invoke(nameof(DarkeningOverTime), 0.3f);
            _boxCollider2D.enabled = true;
            _isOpened = false;
            lockSystem!.GetComponent<SpriteRenderer>().sprite = closedLockSystem;
        }
    }
    
    // event of open
    private void AfterDoorOpened()
    {
        _boxCollider2D.enabled = false;
        _isOpened = true;
    }

    private void DarkeningOverTime() => roomDarkening.SetActive(true);

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        var position = groundPosition.position;
        Gizmos.DrawWireCube(position, new Vector2(width, height));  
    }
}
