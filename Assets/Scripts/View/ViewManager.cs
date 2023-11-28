using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GridLayoutGroup _buttonLayoutContainer;
    [SerializeField] private GameObject _buttonPrefab;

    [Header("UI Elements : ")]
    [SerializeField] private GameObject _inGamePanel;
    [SerializeField] private GameObject _panelVictory;
    [SerializeField] private GameObject _panelDraw;
    [Space]
    [SerializeField] private float _turnItemOffset;
    [SerializeField] private RectTransform _crossRect;
    [SerializeField] private RectTransform _circleRect;

    private Dictionary<(int x, int y), BoxControler> _boxControler = new Dictionary<(int x, int y), BoxControler>();
    private Vector2 _gridSize;
    private Vector2 _cellSize;
    private RectTransform _layoutRect;
    const float SPACING_RATIO = 0.8f;


    void Start()
    {
        _layoutRect = (RectTransform)_buttonLayoutContainer.transform;
        CreateBoxGrid();
        HideTurnItem(_circleRect);
    }


    //! User Input
    public void ClicOnBox(int x, int y)
    {
        _gameManager.PlayerClicOnBox(x, y);
    }


    //! Button Grid
    private void CreateBoxGrid()
    {
        _gridSize = _gameManager.GetGridSize();
        InitialiseButtonLayout();

        for (int y = 0; y < _gridSize.y; y++)
        {
            for (int x = 0; x < _gridSize.x; x++)
            {
                GameObject newBox = Instantiate(_buttonPrefab, _buttonLayoutContainer.transform);
                BoxControler boxControler = newBox.GetComponentInChildren<BoxControler>();
                boxControler.X = x;
                boxControler.Y = y;

                _boxControler.Add((x, y), boxControler);
            }
        }
    }

    private void InitialiseButtonLayout()
    {
        float maxGridValue = Mathf.Max(_gridSize.x, _gridSize.y);
        _buttonLayoutContainer.constraintCount = (int)_gridSize.x;

        _cellSize = new Vector2(_layoutRect.rect.width / maxGridValue, _layoutRect.rect.height / maxGridValue);
        Vector2 cellSizeRatio = _cellSize * SPACING_RATIO;
        _buttonLayoutContainer.cellSize = cellSizeRatio;

        //? (1 - "ratio") = prend l'inverse du ratio
        _buttonLayoutContainer.spacing = _cellSize * (1 - SPACING_RATIO);
    }

    public void UpdateBox(int x, int y, BoxState state)
    {
        _boxControler[(x, y)].UpdateBox(state);
        int playerIndex = _gameManager.GetPlayerIndex();

        if(playerIndex == 1)
        {
            ShowTurnItem(_crossRect);
            HideTurnItem(_circleRect);
        }

        if(playerIndex == 0)
        {
            ShowTurnItem(_circleRect);
            HideTurnItem(_crossRect);
        }
    }

    public void ResetButtonClic()
    {
        ShowPanelInGame();
        _gameManager.ResetGame();
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                UpdateBox(x, y, BoxState.Empty);
            }
        }
    }

    void DisableAllButton()
    {
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                _boxControler[(x, y)].SetButtonValue(false);
            }
        }
    }

    //! Background / UI Element
    void ShowTurnItem(RectTransform itemRect)
    {
        Vector3 startPos = new Vector3(0, -_turnItemOffset, 0);
        Vector3 endPosition = Vector3.zero;
        DOTween.To((time) =>
        {
            itemRect.anchoredPosition = Vector3.Lerp(startPos, endPosition, time);
        }
        ,0, 1, .5f);
    }

    void HideTurnItem(RectTransform itemRect)
    {
        Vector3 startPos = Vector3.zero;
        Vector3 endPosition = new Vector3(0, -_turnItemOffset, 0);
        DOTween.To((time) =>
        {
            itemRect.anchoredPosition = Vector3.Lerp(startPos, endPosition, time);
        }
        ,0, 1, .5f);
    }

    public void ShowPanelInGame()
    {
        HideAllPanel();
        _inGamePanel.SetActive(true);
    }

    public void ShowVictoryPanel()
    {
        DisableAllButton();
        HideAllPanel();
        _panelVictory.SetActive(true);
    }

    public void ShowDrawPanel()
    {
        DisableAllButton();
        HideAllPanel();
        _panelDraw.SetActive(true);
    }

    void HideAllPanel()
    {
        _panelVictory.SetActive(false);
        _panelDraw.SetActive(false);
        _inGamePanel.SetActive(false);
    }
}