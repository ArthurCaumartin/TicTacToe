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

    [Header("Panel : ")]
    [SerializeField] private GameObject _panelVictory;
    [SerializeField] private GameObject _panelDraw;

    [Header("Sprites : ")]
    [SerializeField] private Sprite _circleImage;
    [SerializeField] private Sprite _crossImage;
    [SerializeField] private Sprite _emptyImage;

    //! box controler pour qu'il set lui meme son sprite
    private Dictionary<(int x, int y), BoxControler> _caseControlerRef = new Dictionary<(int x, int y), BoxControler>();
    //? topel ?

    void Start()
    {
        CreateGrid();
    }

    public void ClicOnCase(int x, int y)
    {
        //! check tour de qui dois jouer
        _gameManager.PlayerClicOnBox(x, y);
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
                BoxControler caseControler = newCase.GetComponentInChildren<BoxControler>();
                caseControler.X = i;
                caseControler.Y = j;

                _caseControlerRef.Add((i, j), caseControler);
            }
        }
        // _buttonLayoutContainer.enabled = false;
    }

    public void UpdateCaseVisual(int x, int y, BoxState state)
    {
        Sprite toSet = null;

        switch (state)
        {
            case BoxState.Circle:
                toSet = _circleImage;
                break;

            case BoxState.Cross:
                toSet = _crossImage;
                break;

            case BoxState.Empty:
                toSet = _emptyImage;
                break;
        }

        _caseControlerRef[(x, y)].SetSprite(toSet);
    }

    public void ResetButton()
    {
        _gameManager.ResetGrid();
    }

    public void ShowVictoryPanel()
    {
        HideAllPanel();
        _panelVictory.SetActive(true);
    }

    public void ShowDrawPanel()
    {
        HideAllPanel();
        _panelDraw.SetActive(true);
    }

    void HideAllPanel()
    {
        _panelVictory.SetActive(false);
        _panelDraw.SetActive(false);
    }
}
