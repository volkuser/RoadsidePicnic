using UnityEngine;

public class BorovController : MonoBehaviour
{
    private bool _playerDetect;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform groundPosition;
    
    private Animator _animator;
    private static readonly int SawAPlayer = Animator.StringToHash("SawAPlayer");
    
    private void Awake() => _animator = GetComponent<Animator>();

    private void Update()
    {
        _playerDetect = Physics2D.OverlapBox(groundPosition.position, 
            new Vector2(width, height), 0, whatIsPlayer);

        _animator.SetBool(SawAPlayer, _playerDetect);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        var position = groundPosition.position;
        Gizmos.DrawWireCube(position, new Vector2(width, height));  
    }
}
