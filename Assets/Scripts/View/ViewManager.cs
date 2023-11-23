using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Threading;
using Unity.Collections;
using UnityEngine.UIElements.Experimental;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GridLayoutGroup _buttonLayoutContainer;
    [SerializeField] private GameObject _buttonPrefab;

    [Header("Panel : ")]
    [SerializeField] private GameObject _inGamePanel;
    [SerializeField] private GameObject _panelVictory;
    [SerializeField] private GameObject _panelDraw;

    [Header("Sprites : ")]
    [SerializeField] private Sprite _circleImage;
    [SerializeField] private Sprite _crossImage;
    [SerializeField] private Sprite _emptyImage;

    private Dictionary<(int x, int y), BoxControler> _boxControler = new Dictionary<(int x, int y), BoxControler>();
    private Vector2 _gridSize;

    void Start()
    {
        CreateBoxGrid();
    }


    //! User Input
    public void ClicOnBox(int x, int y)
    {
        //! check tour de qui dois jouer
        _boxControler[(x, y)].SetButtonAvaiable(false);
        _gameManager.PlayerClicOnBox(x, y);
    }


    //! Button Grid
    private void CreateBoxGrid()
    {
        _gridSize = _gameManager.GetGridSize();

        _buttonLayoutContainer.constraintCount = (int)_gridSize.y;

        for (int i = 0; i < _gridSize.x; i++)
        {
            for (int j = 0; j < _gridSize.y; j++)
            {
                GameObject newCase = Instantiate(_buttonPrefab, _buttonLayoutContainer.transform);
                BoxControler caseControler = newCase.GetComponentInChildren<BoxControler>();
                caseControler.X = i;
                caseControler.Y = j;

                _boxControler.Add((i, j), caseControler);
            }
        }
    }

    public void UpdateCaseVisual(int x, int y, BoxState state)
    {
        // print("Set Visual");
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

        _boxControler[(x, y)].SetSprite(toSet);
    }

    public void ResetButtonClic()
    {
        ShowPanelInGame();
        _gameManager.ResetGame();
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                UpdateCaseVisual(x, y, BoxState.Empty);
                _boxControler[(x, y)].SetButtonAvaiable(true);
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
        _inGamePanel.SetActive(false);
    }
}