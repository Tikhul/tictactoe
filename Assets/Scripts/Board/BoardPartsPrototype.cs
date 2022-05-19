using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class BoardPartsPrototype : TicTacToeElement
{
    private float _rowNumber;
    private float _parentPanelSide;
    private float _buttonWidth;
    private GameObject _parentPanel;
    private GameObject _boardParent;
    private GameObject _rows;
    private GameObject _columns;
    private GameObject _buttonExample;
    public int RowNumber
    {
        get => game.boardModel.BoardSettings.rowNumber;
        set => game.boardModel.BoardSettings.rowNumber = value;
    }
    public float ParentPanelSide
    {
        get => _parentPanelSide = game.boardModel.BoardSettings.parentPanel.GetComponent<RectTransform>().sizeDelta.x;
        set => _parentPanelSide = value;
    }
    public float ButtonWidth
    {
        get => _buttonWidth = _parentPanelSide / game.boardModel.BoardSettings.rowNumber;
        set => _buttonWidth = value;
    }
    public GameObject ParentPanel
    {
        get => _parentPanel = game.boardModel.BoardSettings.parentPanel;
        set => _parentPanel = value;
    }
    public GameObject BoardParent
    {
        get => _boardParent = game.boardModel.BoardParent;
        set => _boardParent = value;
    }
    public GameObject Rows
    {
        get => _rows = game.boardModel.BoardSettings.rows;
        set => _rows = value;
    }
    public GameObject Columns
    {
        get => _columns = game.boardModel.BoardSettings.columns;
        set => _columns = value;
    }
    public GameObject ButtonExample
    {
        get => _buttonExample = game.boardModel.BoardSettings.buttonExample;
        set => _buttonExample = value;
    }
    public abstract GameObject Clone(GameObject objToCreate, GameObject parent, float width, float height);
}
class StandardPartPrototype : BoardPartsPrototype
{
    public override GameObject Clone(GameObject objToCreate, GameObject parent, float width, float height)
    {
        GameObject newObject = Instantiate(objToCreate);
        newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        newObject.transform.SetParent(parent.transform);
        newObject.transform.localScale = new Vector3(1, 1, 1);
        newObject.transform.localPosition = newObject.transform.position;
        return newObject;
    }
}