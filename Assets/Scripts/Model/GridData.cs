using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CaseState
{
    Empty,
    Cross,
    Circle
}

public class GridData : MonoBehaviour
{
    private CaseState[,] _grid = new CaseState[10, 10];
    [SerializeField] private GameManager _gameManager;
    public event EventHandler<CaseChangeArgs> onCaseChangeEvent;

    public void SetGrid(int x, int y, CaseState stateToSet)
    {
        if (_grid[x, y] == CaseState.Empty)
            return;

        _grid[x, y] = stateToSet;

        //! le "?" si onCase == null, call pas la fct
        onCaseChangeEvent?.Invoke(this, new CaseChangeArgs(x, y, stateToSet));
    }

    public CaseState GetCaseState(int x, int y)
    {
        return _grid[x, y];
    }

    public int GetGridColumnNumber()
    {
        return _grid.GetLength(0);
    }

    public int GetGridRowNumber()
    {
        return _grid.GetLength(1);
    }

    public void ResetGrid()
    {
        for (int x = 0; x < _grid.GetLength(0); x++)
            for (int y = 0; y < _grid.GetLength(1); y++)
                _grid[x, y] = CaseState.Empty;

        onCaseChangeEvent?.Invoke(this, new CaseChangeArgs(-1, -1, CaseState.Empty, true));
        //! 3 premier parametre osef
    }

}
public class CaseChangeArgs
{
    public CaseChangeArgs(int x, int y, CaseState newState, bool isAllCaseReset = false)
    {
        this.x = x;
        this.y = y;
        this.newState = newState;
        this.isAllCaseReset = isAllCaseReset;
    }

    public int x;
    public int y;
    public CaseState newState;
    public bool isAllCaseReset;
}
