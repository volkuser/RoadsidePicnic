using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    private void LateUpdate()
    {
        Vector3 direction = transform.position;
        var position = _player.position;
        direction.x = position.x;
        direction.y = position.y;

        transform.position = direction;
    }
}
