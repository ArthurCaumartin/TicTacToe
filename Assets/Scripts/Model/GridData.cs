using System;
using UnityEngine;

[Serializable]
public enum BoxState
{
    Empty,
    Cross,
    Circle
}

public class GridData : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Vector2Int _gridSize;
    private BoxState[,] _grid;
    public event EventHandler<BoxChangeArgs> onBoxChangeEvent;

    void Awake()
    {
        _grid = new BoxState[_gridSize.x, _gridSize.y];
    }

    //! Grid Interaction
    public void SetBoxState(int x, int y, BoxState stateToSet)
    {
        //? return si deja cross ou circle
        if (_grid[x, y] != BoxState.Empty)
            return;

        _grid[x, y] = stateToSet;
        _gameManager.OnBoxChange(new BoxChangeArgs(x, y, stateToSet));
        
        //? le "?" si onCase == null, call pas la fct
        // onBoxChangeEvent?.Invoke(this, new BoxChangeArgs(x, y, stateToSet));
    }

    public void ResetGridData()
    {
        for (int x = 0; x < _grid.GetLength(0); x++)
            for (int y = 0; y < _grid.GetLength(1); y++)
                _grid[x, y] = BoxState.Empty;

        // onBoxChangeEvent?.Invoke(this, new BoxChangeArgs(-1, -1, BoxState.Empty));
        //? 3 premier parametre osef
    }


    //! Get Data
    public BoxState GetState(int x, int y)
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

    public Vector2 GetGridSize()
    {
        return _gridSize;
    }
}

public class BoxChangeArgs
{
    public BoxChangeArgs(int x, int y, BoxState newState)
    {
        this.x = x;
        this.y = y;
        this.newState = newState;
    }

    public int x;
    public int y;
    public BoxState newState;
}
