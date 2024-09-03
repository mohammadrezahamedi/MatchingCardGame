using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private int _gridColumnSize;
    [SerializeField] private GridLayoutGroup _grid;
    public static Action<int> OnSpawnCard;

    void Start()
    {
        InitUI();
    }

    private void InitUI()
    {
        _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _grid.constraintCount = _gridColumnSize;
        OnSpawnCard?.Invoke(_gridColumnSize);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
