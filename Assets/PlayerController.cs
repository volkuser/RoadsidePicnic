using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    
    private Vector2 _direction;
    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _direction.x = Input.GetAxisRaw("Horizontal");
        _direction.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        _rigidBody.MovePosition(_rigidBody.position + _direction * (speed * Time.fixedDeltaTime));
    }
}
