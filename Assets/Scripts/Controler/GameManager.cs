using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridData _gridData;
    [SerializeField] private ViewManager _viewManager;
    [SerializeField] private int _turnNumber;

    void Start()
    {
        _gridData.onCaseChangeEvent += OnBoxChange;
    }

    public void PlayerClicOnCase(int x, int y)
    {
        CaseState toSet = CaseState.Empty;
        int player = _turnNumber % 2;
        print("Player : " + player);
        switch (player)
        {
            case 0 :
                toSet = CaseState.Cross;
            break;
            case 1 :
                toSet = CaseState.Circle;
            break;
        }
        _gridData.SetCaseState(x, y, toSet);
        _turnNumber++;
    }

    public void OnBoxChange(object sender, CaseChangeArgs args)
    {
        if(args.isAllCaseReset)
        {
            ResetAllVisual();
            return;
        }

        //? as peut send un null, donc ? au cas ou
        print((sender as GridData)?.name  + " Change data " + args.x + "," + args.y + " to " + args.newState);
        _viewManager.UpdateCaseVisual(args.x, args.y, args.newState);
    }

    private void ResetAllVisual()
    {
        print("Reset grid !");
        for (int x = 0; x < _gridData.GetGridColumnNumber(); x++)
        {
            for (int y = 0; y < _gridData.GetGridRowNumber(); y++)
            {
                _viewManager.UpdateCaseVisual(x, y, CaseState.Empty);
            }
        }
    }

    public Vector2 GetGridSize()
    {
        return new Vector2(_gridData.GetGridColumnNumber(), _gridData.GetGridRowNumber());
    }
}
