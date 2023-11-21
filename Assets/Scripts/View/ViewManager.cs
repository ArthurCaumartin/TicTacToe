using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Threading;
using Unity.Collections;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GridLayoutGroup _buttonLayoutContainer;
    [SerializeField] private GameObject _buttonPrefab;

    [Header("Sprites :")]
    [SerializeField] private Sprite _circleImage;
    [SerializeField] private Sprite _crossImage;
    [SerializeField] private Sprite _emptyImage;

    //! box controler pour qu'il set lui meme son sprite
    private Dictionary<(int x, int y), CaseControler> _caseControlerRef = new Dictionary<(int x, int y), CaseControler>();
                        //? topel ?

    void Start()
    {
        CreateGrid();
    }

    public void ClicOnCase(int x, int y)
    {
        //! check tour de qui dois jouer
        _gameManager.PlayerClicOnCase(x, y);
    }

    private void CreateGrid()
    {
        Vector2 size = _gameManager.GetGridSize();

        _buttonLayoutContainer.constraintCount = (int)size.x;

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                GameObject newCase = Instantiate(_buttonPrefab, _buttonLayoutContainer.transform);
                CaseControler caseControler = newCase.GetComponent<CaseControler>();
                caseControler.X = i;
                caseControler.Y = j;

                _caseControlerRef.Add((i, j), caseControler);
            }
        }
    }

    public void UpdateCaseVisual(int x, int y, CaseState state)
    {
        Sprite toSet = null;

        switch(state)
        {
            case CaseState.Circle :
                toSet = _circleImage;
            break;

            case CaseState.Cross :
                toSet = _crossImage;
            break;

            case CaseState.Empty :
                toSet = _emptyImage;
            break;
        }

        _caseControlerRef[(x, y)].SetSprite(toSet);
    }
}
