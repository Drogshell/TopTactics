using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePointer : MonoBehaviour
{
    private static MousePointer instance;
    [SerializeField] private LayerMask mask;
    
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        transform.position = GetMousePosition();
    }

    public static Vector3 GetMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Physics.Raycast(ray, out var hit, float.MaxValue, instance.mask);
        return hit.point;
    }
}
