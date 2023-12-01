using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public interface IVictoryChecker
{
    public bool Check(int x, int y, GridData gridData);
}

public class VictoryChecker3x3 : IVictoryChecker
{
    public bool Check(int x, int y, GridData gridData)
    {
        if(CollumnCheck(x, y, gridData))
            return true;

        if(LineCheck(x, y, gridData))
            return true;

        if(DiagonalCheck(x, y, gridData))
            return true;

        // if (gridData.GetState(x, y) == BoxState.Empty)
        //     return false;

        // if (gridData.GetState(x, 0) == gridData.GetState(x, 1) &&
        //     gridData.GetState(x, 1) == gridData.GetState(x, 2))
        // {
        //     return true;
        // }

        // if (gridData.GetState(0, y) == gridData.GetState(1, y) &&
        //     gridData.GetState(1, y) == gridData.GetState(2, y))
        // {
        //     return true;
        // }

        // if (gridData.GetState(0, 0) != BoxState.Empty && gridData.GetState(0, 0) == gridData.GetState(1, 1) &&
        //     gridData.GetState(1, 1) == gridData.GetState(2, 2))
        // {
        //     return true;
        // }

        // if (gridData.GetState(0, 2) != BoxState.Empty && gridData.GetState(0, 2) == gridData.GetState(1, 1) &&
        //     gridData.GetState(1, 1) == gridData.GetState(2, 0))
        // {
        //     return true;
        // }

        return false;
    }

    //! |
    bool CollumnCheck(int x, int y, GridData data)
    {
        Vector2 gridSize = data.GetGridSize() - Vector2.one;
        // Debug.Log(gridSize);
        // Debug.Log("x = " + x + " y = " + y);
        
        if(y > 1 
        && data.GetState(x, y) == data.GetState(x, y - 1) 
        && data.GetState(x, y - 1) == data.GetState(x, y - 2))
        {
            return true;
        }

        if(y > 0 && y < gridSize.y 
        && data.GetState(x, y) == data.GetState(x, y - 1) 
        && data.GetState(x, y) == data.GetState(x, y + 1))
        {   
            return true;
        }

        if(y + 2 <= gridSize.y 
        && data.GetState(x, y) == data.GetState(x, y + 1) 
        && data.GetState(x, y + 1) == data.GetState(x, y + 2))
        {
            return true;
        }
        
        return false;
    }

    //! _
    bool LineCheck(int x, int y, GridData data)
    {
        Vector2 gridSize = data.GetGridSize() - Vector2.one;
        // Debug.Log(gridSize);
        // Debug.Log("x = " + x + " y = " + y);
        
        if(x > 1 
        && data.GetState(x, y) == data.GetState(x - 1, y) 
        && data.GetState(x - 1, y) == data.GetState(x - 2, y))
        {
            return true;
        }

        if(x > 0 && x < gridSize.x 
        && data.GetState(x, y) == data.GetState(x - 1, y) 
        && data.GetState(x, y) == data.GetState(x + 1, y))
        {   
            return true;
        }

        if(x + 2 <= gridSize.x 
        && data.GetState(x, y) == data.GetState(x + 1, y) 
        && data.GetState(x + 1, y) == data.GetState(x + 2, y))
        {
            return true;
        }
        
        return false;
    }

    //! /
    bool DiagonalCheck(int x, int y, GridData data)
    {
        Vector2 gridSize = data.GetGridSize() - Vector2.one;
        // Debug.Log(gridSize);
        // Debug.Log("x = " + x + " y = " + y);
        
        // if(x + 2 < gridSize.x && y )
        // {
        //     return true;
        // }

        return false;
    }

    //! \
}
