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
        get => Game.TicTacToeModel.BoardModel.BoardSettings.RowNumber;
        set => Game.TicTacToeModel.BoardModel.BoardSettings.RowNumber = value;
    }
    public float ParentPanelSide
    {
        get => _parentPanelSide = Game.TicTacToeModel.BoardModel.BoardSettings.ParentPanel.GetComponent<RectTransform>().sizeDelta.x;
        set => _parentPanelSide = value;
    }
    public float ButtonWidth
    {
        get => _buttonWidth = _parentPanelSide / Game.TicTacToeModel.BoardModel.BoardSettings.RowNumber;
        set => _buttonWidth = value;
    }
    public GameObject ParentPanel
    {
        get => _parentPanel = Game.TicTacToeModel.BoardModel.BoardSettings.ParentPanel;
        set => _parentPanel = value;
    }
    public GameObject BoardParent
    {
        get => _boardParent = Game.TicTacToeModel.BoardModel.BoardParent;
        set => _boardParent = value;
    }
    public GameObject Rows
    {
        get => _rows = Game.TicTacToeModel.BoardModel.BoardSettings.Rows;
        set => _rows = value;
    }
    public GameObject Columns
    {
        get => _columns = Game.TicTacToeModel.BoardModel.BoardSettings.Columns;
        set => _columns = value;
    }
    public GameObject ButtonExample
    {
        get => _buttonExample = Game.TicTacToeModel.BoardModel.BoardSettings.ButtonExample;
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