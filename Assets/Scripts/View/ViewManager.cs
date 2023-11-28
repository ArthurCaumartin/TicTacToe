using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Threading;
using Unity.Collections;
using UnityEngine.UIElements.Experimental;
using Unity.VisualScripting;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GridLayoutGroup _buttonLayoutContainer;
    [SerializeField] private GameObject _buttonPrefab;

    [Header("Panel : ")]
    [SerializeField] private GameObject _inGamePanel;
    [SerializeField] private GameObject _panelVictory;
    [SerializeField] private GameObject _panelDraw;

    private Dictionary<(int x, int y), BoxControler> _boxControler = new Dictionary<(int x, int y), BoxControler>();
    private Vector2 _gridSize;
    private RectTransform _layoutRect;

    void Start()
    {
        _layoutRect = (RectTransform)_buttonLayoutContainer.transform;
        CreateBoxGrid();
    }


    //! User Input
    public void ClicOnBox(int x, int y)
    {
        _gameManager.PlayerClicOnBox(x, y);
    }

    public Vector2 _cellSize;

    //! Button Grid
    public float spacingRatio;
    private void CreateBoxGrid()
    {
        _gridSize = _gameManager.GetGridSize();
        float maxGridValue = Mathf.Max(_gridSize.x, _gridSize.y);

        _buttonLayoutContainer.constraintCount = (int)_gridSize.x;

        _cellSize = new Vector2(_layoutRect.rect.width / maxGridValue, _layoutRect.rect.height / maxGridValue);
        Vector2 cellSizeRatio = _cellSize * spacingRatio;
        _buttonLayoutContainer.cellSize = cellSizeRatio;

        //? (1 - "ratio") = prend l'inverse du ratio
        _buttonLayoutContainer.spacing = _cellSize * (1 - spacingRatio);

        for (int y = 0; y < _gridSize.y; y++)
        {
            for (int x = 0; x < _gridSize.x; x++)
            {
                GameObject newCase = Instantiate(_buttonPrefab, _buttonLayoutContainer.transform);
                BoxControler boxControler = newCase.GetComponentInChildren<BoxControler>();
                boxControler.X = x;
                boxControler.Y = y;

                _boxControler.Add((x, y), boxControler);
            }
        }
    }

    public void UpdateBox(int x, int y, BoxState state)
    {
        _boxControler[(x, y)].UpdateBox(state);
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

    //! Background
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