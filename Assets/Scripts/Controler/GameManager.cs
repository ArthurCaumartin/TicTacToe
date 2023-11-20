using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;




public class GameManager : MonoBehaviour
{
    [SerializeField] private GridData _gridData;
    [SerializeField] private ViewManager _viewManager;

    //! oblige object sender pour le cotes generic
    public void OnBoxChange(object sender, CaseChangeArgs args)
    {
        if(args.isAllCaseReset)
        {
            print("Reset grid !");
            for (int x = 0; x < _gridData.GetGridColumnNumber(); x++)
            {
                for (int y = 0; y < _gridData.GetGridRowNumber(); y++)
                {
                    _viewManager.UpdateCaseVisual(x, y, CaseState.Empty);
                }
            }
            return;
        }
        
        //! as peut send un null, donc ? au cas ou
        print((sender as GridData)?.name  + " Change data " + args.x + "," + args.y + " to " + args.newState);
        _viewManager.UpdateCaseVisual(args.x, args.y, args.newState);
    }

    void Start()
    {
        //! abonement
        _gridData.onCaseChangeEvent += OnBoxChange;
    }
}
