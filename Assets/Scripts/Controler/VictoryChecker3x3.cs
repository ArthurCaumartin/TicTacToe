using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVictoryChecker
{
    public bool Check(int x, int y, GridData gridData);
}

public class VictoryChecker3x3 : IVictoryChecker
{
    public bool Check(int x, int y, GridData gridData)
    {

        if (gridData.GetBoxState(x, y) == BoxState.Empty)
            return false;

        if (gridData.GetBoxState(x, 0) == gridData.GetBoxState(x, 1) &&
            gridData.GetBoxState(x, 1) == gridData.GetBoxState(x, 2))
        {
            return true;
        }

        if (gridData.GetBoxState(0, y) == gridData.GetBoxState(1, y) &&
            gridData.GetBoxState(1, y) == gridData.GetBoxState(2, y))
        {
            return true;
        }

        if (gridData.GetBoxState(0, 0) != BoxState.Empty && gridData.GetBoxState(0, 0) == gridData.GetBoxState(1, 1) &&
            gridData.GetBoxState(1, 1) == gridData.GetBoxState(2, 2))
        {
            return true;
        }

        if (gridData.GetBoxState(0, 2) != BoxState.Empty && gridData.GetBoxState(0, 2) == gridData.GetBoxState(1, 1) &&
            gridData.GetBoxState(1, 1) == gridData.GetBoxState(2, 0))
        {
            return true;
        }

        return false;
    }
}
