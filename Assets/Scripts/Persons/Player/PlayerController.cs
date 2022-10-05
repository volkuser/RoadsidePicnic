using UnityEngine;

namespace Persons.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Vector2 _direction;
        
        private Rigidbody2D _rigidBody;
        [SerializeField] private Animator animator;
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Speed = Animator.StringToHash("Speed");
    
        // checkers for story
        public static bool DialogWithKiril1 = true; 
        public static bool PerspectiveOnDialogWithKiril2 = false;
        public static bool DialogWithKiril2 = false;
        public static bool DialogWithKiril3 = false;
        public static bool PerspectiveOnDialogWithKiril4 = false;
        public static bool DialogWithKiril4 = false;
        public static bool PerspectiveOnTakingCard = false;
        public static bool DialogWithKiril5 = false;
        public static bool PerspectiveLetFirstDummy = false;
        public static bool PerspectivePhrase1 = false;
        public static bool PerspectiveTakeSecondDummy = false;
        public static bool PerspectiveLetSecondDummy = false;
        public static bool DialogWithKiril6 = false; 
        public static bool DialogWithBorov = false; 
        public static bool PerspectiveExitFromBorovOffice = false;
        public static bool DialogWithKiril7 = false;
        public static bool PerspectiveWindow = false;
        public static bool DialogWithKiril8 = false;
    
        public static bool GamePassed = false;
    
        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }
    
        private void Update()
        {
            _direction.x = Input.GetAxisRaw("Horizontal");
            _direction.y = Input.GetAxisRaw("Vertical");
            
            animator.SetFloat(Horizontal, _direction.x);
            animator.SetFloat(Vertical, _direction.y);
            animator.SetFloat(Speed, _direction.sqrMagnitude);
        }
    
        private void FixedUpdate()
        {
            _rigidBody.MovePosition(_rigidBody.position + _direction * (speed * Time.fixedDeltaTime));
        }
    }
}
