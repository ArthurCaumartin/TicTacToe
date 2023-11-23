using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridData _gridData;
    [SerializeField] private ViewManager _viewManager;
    [SerializeField] private int _playerIndex;
    [SerializeField] private int _turnNumber;
    [SerializeField] private int _numberBoxAvaiable;

    void Start()
    {
        // _gridData.onBoxChangeEvent += OnBoxChange;
        _numberBoxAvaiable = _gridData.GetGridColumnNumber() * _gridData.GetGridRowNumber();
    }


    //! Button Communication
    public void PlayerClicOnBox(int x, int y)
    {
        BoxState toSet = BoxState.Empty;
        _playerIndex = _turnNumber % 2;

        switch (_playerIndex)
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
        if (CheckVictory(args.x, args.y))
        {
            print("GG !");
            _viewManager.ShowVictoryPanel();
        }

        if (CheckDraw())
        {
            print("Draw !!!");
            _viewManager.ShowDrawPanel();
        }

        _numberBoxAvaiable--;
        _turnNumber++;
        _viewManager.UpdateCaseVisual(args.x, args.y, args.newState);
    }


    //! Game State
    public bool CheckVictory(int x, int y)
    {
        if (_gridData.GetBoxState(x, y) == BoxState.Empty)
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

    public bool CheckDraw()
    {
        return _numberBoxAvaiable == 0;
    }

    public void ResetGame()
    {
        _numberBoxAvaiable = _gridData.GetGridColumnNumber() * _gridData.GetGridRowNumber();
        _turnNumber = 0;
        _gridData.ResetGridData();
    }


    //! Data transfere
    public Vector2 GetGridSize()
    {
        return new Vector2(_gridData.GetGridColumnNumber(), _gridData.GetGridRowNumber());
    }

}
