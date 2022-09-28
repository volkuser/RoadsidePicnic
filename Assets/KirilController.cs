using UnityEngine;

public class KirilController : MonoBehaviour
{
    private bool _playerDetect;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform groundPosition;

    private Animator _animator;
    private static readonly int Listen = Animator.StringToHash("Listen");
    private static readonly int Write = Animator.StringToHash("Write");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    } 

    private void Update()
    {
        _playerDetect = Physics2D.OverlapBox(groundPosition.position, 
            new Vector2(width, height), 0, whatIsPlayer);

        if (_playerDetect)
        {
            _animator.ResetTrigger(Write);
            _animator.SetTrigger(Listen);
        }
        else
        {
            _animator.ResetTrigger(Listen);
            _animator.SetTrigger(Write);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        var position = groundPosition.position;
        Gizmos.DrawWireCube(position, new Vector2(width, height));
    }
}
