using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public static event Action OnPlayerCaught; 

    private bool _isPlayerInRange;
    private Collider _observerCollider;
    private LayerMask _playerLayer;

    private void Awake()
    {
        _observerCollider = GetComponent<Collider>();
        _playerLayer = player.gameObject.layer;
    }

    private void Update()
    {
        _isPlayerInRange = _observerCollider.bounds.Contains(player.position);
        
        if (_isPlayerInRange)
        {
            Vector3 direction = player.position - this.transform.position + Vector3.up;
            if (Physics.Raycast(transform.position, direction, out RaycastHit raycastHit, Mathf.Infinity, _playerLayer))
            {
                OnPlayerCaught?.Invoke();
            }
        }
    }
}
