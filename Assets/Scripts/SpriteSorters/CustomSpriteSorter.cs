using System;
using UnityEngine;

public class CustomSpriteSorter : MonoBehaviour
{
    private Renderer _renderer;
    
    [SerializeField] private LayerMask whatIsPlayer;
    private bool _playerDetect; // in zone
    // zone 
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private Transform center;

    [SerializeField] private int layerPlayerInZone;
    [SerializeField] private int layerOther;

    private void Awake() => _renderer = GetComponent<Renderer>();

    private void LateUpdate()
    {
        _playerDetect = Physics2D.OverlapBox(center.position, 
            new Vector2(width, height), 0, whatIsPlayer);
        _renderer.sortingOrder = _playerDetect ? layerPlayerInZone : layerOther;
    }

    private void OnDrawGizmosSelected()
    {
        var position = center.position;
        Gizmos.color = Color.grey;
        Gizmos.DrawWireCube(position, new Vector2(width, height));
    }
}
