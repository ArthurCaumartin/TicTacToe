using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridData _gridData;
    [SerializeField] private ViewManager _viewManager;
    [SerializeField] private int _turnNumber;
    [SerializeField] private int _numberBoxAvaiable;

    void Start()
    {
        // _gridData.onBoxChangeEvent += OnBoxChange;
        _numberBoxAvaiable = _gridData.GetGridColumnNumber() * _gridData.GetGridRowNumber();
    }

    public void PlayerClicOnBox(int x, int y)
    {
        BoxState toSet = BoxState.Empty;
        int player = _turnNumber % 2;
        // print("Player : " + player);
        switch (player)
        {
            case 0:
                toSet = BoxState.Cross;
                break;
            case 1:
                toSet = BoxState.Circle;
                break;
        }
        _gridData.SetBoxState(x, y, toSet);
    }

    public void OnBoxChange(BoxChangeArgs args)
    {
        //! tej l'event et juste faire un apelle de fct
        if (CheckVictory(args.x, args.y))
        {
            print("GG !");
            _viewManager.ShowVictoryPanel();
        }

        if (CheckNull())
        {
            print("Draw !!!");
            _viewManager.ShowDrawPanel();
        }

        _turnNumber++;
        _numberBoxAvaiable--;
        _viewManager.UpdateCaseVisual(args.x, args.y, args.newState);
    }

    public bool CheckVictory(int x, int y)
    {
        if(_gridData.GetBoxState(x, y) == BoxState.Empty)
            return false;

        if (_gridData.GetBoxState(x, 0) == _gridData.GetBoxState(x, 1) && 
            _gridData.GetBoxState(x, 1) == _gridData.GetBoxState(x, 2))
        {
            return true;
        }

        if (_gridData.GetBoxState(0, y) == _gridData.GetBoxState(1, y) && 
            _gridData.GetBoxState(1, y) == _gridData.GetBoxState(2, y))
        {
            return true;
        }

        if (_gridData.GetBoxState(0, 0) != BoxState.Empty && _gridData.GetBoxState(0, 0) == _gridData.GetBoxState(1, 1) && 
            _gridData.GetBoxState(1, 1) == _gridData.GetBoxState(2, 2))
        {
            return true;
        }

        if (_gridData.GetBoxState(0, 2) != BoxState.Empty && _gridData.GetBoxState(0, 2) == _gridData.GetBoxState(1, 1) && 
            _gridData.GetBoxState(1, 1) == _gridData.GetBoxState(2, 0))
        {
            return true;
        }

        return false;
    }

    public bool CheckNull()
    {
        return _numberBoxAvaiable == 0;
    }

    public Vector2 GetGridSize()
    {
        return new Vector2(_gridData.GetGridColumnNumber(), _gridData.GetGridRowNumber());
    }

    public void ResetGrid()
    {
        _turnNumber = 0;
        _numberBoxAvaiable = _gridData.GetGridColumnNumber() * _gridData.GetGridRowNumber();
        _gridData.ResetGridData();
    }
}
