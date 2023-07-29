using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Observer : MonoBehaviour
{
    [NonSerialized] public Transform player;
    [NonSerialized] public LayerMask playerLayer;
    public event Action OnPlayerCaught;

    private bool _playerCaught;
    private bool _isPlayerInRange;
    private Collider _observerCollider;
    

    private void Awake()
    {
        _observerCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (_playerCaught) return;

        _isPlayerInRange = _observerCollider.bounds.Contains(player.position);

        if (!_isPlayerInRange) return;
        Vector3 direction = player.position - this.transform.position + Vector3.up;
        Vector3 rayOrigin = new Vector3(this.transform.position.x, _observerCollider.bounds.center.y,
            this.transform.position.z);
        if (!Physics.Raycast(rayOrigin, direction, out RaycastHit raycastHit, Mathf.Infinity,
                playerLayer)) return;
        _playerCaught = true;
        OnPlayerCaught?.Invoke();
    }
}
