using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaseControler : MonoBehaviour
{
    //! faire des get set
    [SerializeField] private int _x;
    [SerializeField] private int _y;

    public int X{get{return _x;} set{_x = value;}}
    public int Y{get{return _y;} set{_y = value;}}

    private ViewManager _viewManager;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClic);
        _viewManager = GetComponentInParent<ViewManager>();
    }

    public void OnClic()
    {
        print("Clic on " + _x + " : " + _y);
    }
}
