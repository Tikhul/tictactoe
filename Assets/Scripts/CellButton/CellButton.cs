using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellButton : TicTacToeElement
    // Кнопка для поля
{
    private bool _taken;
    private CellButtonModel cell = new CellButtonModel();

    [SerializeField] private TMP_Text _buttonText;
    public TMP_Text ButtonText
    {
        get => _buttonText;
        set => _buttonText = value;
    }

    public bool Taken
    {
        get => _taken;
        set => _taken = value;
    }

    public delegate void ClickAction(CellButtonModel cell);
    public static event ClickAction OnPlayerClick;

    private void OnEnable()
    {
        BoardController.OnButtonCreated += GetCellData;
        PCController.OnGenerateFinished += CellTaken;
    }

    private void OnDisable()
    {
        PCController.OnGenerateFinished -= CellTaken;
    }
    public void CellClicked()
    // Если нажал человек
    {
        ButtonText.text = game.human.Marker;
        ButtonText.gameObject.SetActive(true);
        OnPlayerClick?.Invoke(cell);
    }

    public void CellTaken(CellButtonModel chosenButton)
    // Если кнопку выбрал ПК
    {
        if (chosenButton.Equals(this))
        {
            //Taken = true;
            ButtonText.text = game.pc.Marker;
            ButtonText.gameObject.SetActive(true);
            GetComponent<Button>().enabled = false;
        }
    }

    public delegate void GetAction(CellButtonModel cell, int buttonIndex, int rowIndex);
    public static event GetAction OnDataReceived;

    public void GetCellData(int buttonIndex, int rowIndex)
    {
        BoardController.OnButtonCreated -= GetCellData;
        OnDataReceived?.Invoke(cell, buttonIndex, rowIndex);
    }
}