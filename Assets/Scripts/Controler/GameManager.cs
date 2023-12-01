using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridData _gridData;
    [SerializeField] private ViewManager _viewManager;
    [SerializeField] private int _playerIndex;
    [SerializeField] private int _turnNumber;
    [SerializeField] private int _numberBoxAvaiable;
    private VictoryChecker3x3 victoryChecker = new VictoryChecker3x3();

    void Start()
    {
        _numberBoxAvaiable = _gridData.GetGridColumnNumber() * _gridData.GetGridRowNumber();
    }


    //! Button Communication
    public void PlayerClicOnBox(int x, int y)
    {
        BoxState toSet = BoxState.Empty;
        _playerIndex = _turnNumber % 2;
        print("Player index : " + _playerIndex);

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
        _numberBoxAvaiable--;
        _turnNumber++;
        _viewManager.UpdateBox(args.x, args.y, args.newState);
        WinCheck(args.x, args.y);
    }

    //! Game State  
    void WinCheck(int x, int y)
    {
        if (CheckDraw())
        {
            _viewManager.ShowDrawPanel();
        }

        if (victoryChecker.Check(x, y, _gridData))
        {
            _viewManager.ShowVictoryPanel();
        }
    }

    bool CheckDraw()
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

    public int GetPlayerIndex()
    {
        return _playerIndex;
    }
}
