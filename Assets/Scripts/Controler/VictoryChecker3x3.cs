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


        return false;
    }

    bool CollumnCheck(int x, int y, GridData data)
    {
        Vector2 gridSize = data.GetGridSize() - Vector2.one;
        
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

    bool DiagonalCheck(int x, int y, GridData data)
    {
        Vector2 gridSize = data.GetGridSize();
        Debug.Log(gridSize);
        Debug.Log("x : " + x + "/ y : " + y);

        if(y > 1 && x < gridSize.x - 2
        && data.GetState(x, y) == data.GetState(x + 1, y - 1) 
        && data.GetState(x, y) == data.GetState(x + 2, y - 2))
        {
            return true;
        }

        if(y > 0 && x > 0 && y < gridSize.y - 1 && x < gridSize.x - 1
        && data.GetState(x, y) == data.GetState(x + 1, y - 1) 
        && data.GetState(x, y) == data.GetState(x - 1, y + 1))
        {
            return true;
        }

        if(y < gridSize.y - 2 && x > 1
        && data.GetState(x, y) == data.GetState(x - 1, y + 1) 
        && data.GetState(x, y) == data.GetState(x - 2, y + 2))
        {
            return true;
        }


        //!!!!!
        if(y < gridSize.y - 2 && x < gridSize.x -2
        && data.GetState(x, y) == data.GetState(x + 1, y + 1) 
        && data.GetState(x, y) == data.GetState(x + 2, y + 2))
        {
            return true;
        }

        if(y < gridSize.y - 1 && x < gridSize.x - 1 && y > 0 && x > 0
        && data.GetState(x, y) == data.GetState(x - 1, y - 1) 
        && data.GetState(x, y) == data.GetState(x + 1, y + 1))
        {
            return true;
        }

        if(x > 1 && y > 1
        && data.GetState(x, y) == data.GetState(x - 1, y - 1) 
        && data.GetState(x, y) == data.GetState(x - 2, y - 2))
        {
            return true;
        }

        return false;
    }
}
