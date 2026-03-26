using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;
    public event EventHandler onSelectedUnitChanged;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is another Unit Action System Active! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            if (TryHandleUnitSelection()) return;
            selectedUnit.Move(MousePointer.GetMousePosition());
        }
    }

    private bool TryHandleUnitSelection()
    {
        var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out var hit, float.MaxValue, unitLayerMask))
        {
            if (hit.transform.TryGetComponent<Unit>(out var unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        // if (onSelectedUnitChanged != null)
        // {
        //     onSelectedUnitChanged(this, EventArgs.Empty);
        // }
        
        // A better way of doing the above code
        onSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}