using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GridLayoutGroup _buttonLayoutContainer;
    [SerializeField] private GameObject _buttonPrefab;

    //! fct pour onClic avec coordoné
    public void ClicOnCase(int x, int y, CaseState caseCurrentState)
    {

    }

    void Start()
    {
        Vector2 size = _gameManager.GetGridSize();
        _buttonLayoutContainer.constraintCount = (int)size.x;
        _buttonLayoutContainer.transform.localScale /= size.x * .5f;

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                GameObject newCase = Instantiate(_buttonPrefab, _buttonLayoutContainer.transform);
                CaseControler caseControler = newCase.GetComponent<CaseControler>();
                caseControler.X = i;
                caseControler.Y = j;
                //! get case et set coordoné
            }
        }
    }

    public void UpdateCaseVisual(int x, int y, CaseState state)
    {
        print("EJRGIGUHRIEJRGIGUHRIGUHRTIEJRGIGUHRIGUHRTIGUGHGUGHGUHRTIGUGHEJREJRGIGUHRIGUHRTIGUGHGIGUHRIGUHRTIGUGH");
        print("oui !");
        print("non !");
    }
}
