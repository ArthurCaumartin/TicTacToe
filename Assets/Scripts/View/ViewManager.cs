using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GridLayoutGroup _buttonLayoutContainer;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private Sprite _crossSprite;
    [SerializeField] private Sprite _circleSprite;

    [Header("UI Elements : ")]
    [SerializeField] private GameObject _inGamePanel;
    [SerializeField] private GameObject _panelVictory;
    [SerializeField] private GameObject _panelDraw;
    [Space]
    [SerializeField] private float _turnItemPositionOffset;
    [SerializeField] private RectTransform _crossRect;
    [SerializeField] private RectTransform _circleRect;
    [Space]
    [SerializeField] private RectTransform _previewForm;
    [SerializeField] private float _previewFormFadeSpeed;
    [Space]
    [SerializeField] private DoTweenAtHome _removeBoxAnimation;


    private Dictionary<(int x, int y), BoxControler> _boxControler = new Dictionary<(int x, int y), BoxControler>();
    private Vector2 _gridSize;
    private Vector2 _cellSize;
    private RectTransform _layoutRect;
    private RectTransform _previewFormTarget;
    private Image _previewFormImage;
    private Vector2 _layoutCellSizeRatio;
    private Vector3 _previewFormVelocity = Vector3.zero;
    private float _removeAnimationTimeSpend;
    const float SPACING_RATIO = 0.8f;


    void Start()
    {
        _layoutRect = (RectTransform)_buttonLayoutContainer.transform;
        _previewFormImage = _previewForm.GetComponent<Image>();
        CreateBoxGrid();
        HideTurnItem(_circleRect);

        _removeBoxAnimation.StartAction = RemoveBoxStart;
        _removeBoxAnimation.UpdateAction = RemoveBoxUpdate;
    }

    void Update()
    {
        UpdatePreviewForm();
        _removeBoxAnimation.Update(Time.deltaTime);
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

        _previewForm.sizeDelta = _layoutCellSizeRatio * .8f;
    }

    private void InitialiseButtonLayout()
    {
        float maxGridValue = Mathf.Max(_gridSize.x, _gridSize.y);
        _buttonLayoutContainer.constraintCount = (int)_gridSize.x;

        _cellSize = new Vector2(_layoutRect.rect.width / maxGridValue, _layoutRect.rect.height / maxGridValue);
        _layoutCellSizeRatio = _cellSize * SPACING_RATIO;
        _buttonLayoutContainer.cellSize = _layoutCellSizeRatio;

        //? (1 - "ratio") = prend l'inverse du ratio
        _buttonLayoutContainer.spacing = _cellSize * (1 - SPACING_RATIO);
    }

    public void UpdateBox(int x, int y, BoxState state)
    {
        _boxControler[(x, y)].UpdateBox(state);
        int playerIndex = _gameManager.GetPlayerIndex();

        if (playerIndex == 1)
        {
            ShowTurnItem(_crossRect);
            HideTurnItem(_circleRect);
        }

        if (playerIndex == 0)
        {
            ShowTurnItem(_circleRect);
            HideTurnItem(_crossRect);
        }

        ChangePreviewSprite(state);
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

        HideTurnItem(_circleRect);
        ShowTurnItem(_crossRect);
        ChangePreviewSprite(BoxState.Circle);
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



    void RemoveAllEmptyBox()
    {
        _removeBoxAnimation.Start();
    }

    void RemoveBoxStart()
    {
        _removeAnimationTimeSpend = 0;
    }


    int _removeXIndex;

    void RemoveBoxUpdate(float time)
    {
        float callThresold = _removeBoxAnimation.Duration / (_gridSize.x + 1);
        print(callThresold);
        _removeAnimationTimeSpend += Time.deltaTime;

        if (_removeAnimationTimeSpend >= callThresold)
        {
            _removeAnimationTimeSpend = 0;
            for (int i = 0; i < _gridSize.y; i++)
            {
                RemoveBox(_removeXIndex, i);
            }
            // RemoveBox(removeXIndex, removeYIndex);
            _removeXIndex++;
        }
    }

    void RemoveBox(int x, int y)
    {
        if (_gameManager.GetBoxState(x, y) == BoxState.Empty)
            _boxControler[(x, y)].PlayRemoveAnimation();
    }


    //! Background / UI Element
    void UpdatePreviewForm()
    {
        Color colorTarget = Color.white;
        Vector2 target = Vector2.zero;

        if (_previewFormTarget)
        {
            target = _previewFormTarget.position;
            colorTarget.a = .8f;
        }
        else if (!_previewFormTarget)
        {
            target = Input.mousePosition;
            colorTarget.a = 0f;
        }

        _previewFormImage.color = Color.Lerp(_previewFormImage.color, colorTarget, Time.deltaTime * _previewFormFadeSpeed);
        _previewForm.position = Vector3.SmoothDamp(_previewForm.position, target, ref _previewFormVelocity, .2f, Mathf.Infinity, Time.deltaTime);
    }

    public void SetPreviewFormTarget(RectTransform value)
    {
        _previewFormTarget = value;
    }

    void ChangePreviewSprite(BoxState justPlayState)
    {
        switch (justPlayState)
        {
            case BoxState.Cross:
                _previewFormImage.sprite = _circleSprite;
                break;
            case BoxState.Circle:
                _previewFormImage.sprite = _crossSprite;
                break;
        }
    }

    void ShowTurnItem(RectTransform itemRect)
    {
        Vector3 startPos = itemRect.anchoredPosition;
        Vector3 endPosition = Vector3.zero;
        DOTween.To((time) =>
        {
            itemRect.anchoredPosition = Vector3.Lerp(startPos, endPosition, time);
        }
        , 0, 1, .5f);
    }

    void HideTurnItem(RectTransform itemRect)
    {
        Vector3 startPos = itemRect.anchoredPosition;
        Vector3 endPosition = new Vector3(0, -_turnItemPositionOffset, 0);
        DOTween.To((time) =>
        {
            itemRect.anchoredPosition = Vector3.Lerp(startPos, endPosition, time);
        }
        , 0, 1, .5f);
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
        RemoveAllEmptyBox();

        _panelVictory.SetActive(true);
        HideTurnItem(_circleRect);
        HideTurnItem(_crossRect);
    }

    public void ShowDrawPanel()
    {
        DisableAllButton();
        HideAllPanel();
        _panelDraw.SetActive(true);
        HideTurnItem(_circleRect);
        HideTurnItem(_crossRect);
    }

    void HideAllPanel()
    {
        _panelVictory.SetActive(false);
        _panelDraw.SetActive(false);
        _inGamePanel.SetActive(false);
    }
}