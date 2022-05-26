using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellButton : TicTacToeElement, ICellButton
// Кнопка для поля
{
    private int _cellInt;
    private char _cellChar;
    private bool _taken;
    public int CellInt
    {
        get => _cellInt;
        set => _cellInt = value;
    }
    public char CellChar
    {
        get => _cellChar;
        set => _cellChar = value;
    }
    public bool Taken
    {
        get => _taken;
        set => _taken = value;
    }
    [SerializeField] private TMP_Text _buttonText;
    public TMP_Text ButtonText
    {
        get => _buttonText;
        set => _buttonText = value;
    }

    public delegate void ClickAction(CellButton cell);
    public static event ClickAction OnPlayerClick;

    private void OnEnable()
    {
        PCController.OnPCTurn += CellTaken;
    }

    private void OnDisable()
    {
        PCController.OnPCTurn -= CellTaken;
    }
    public void CellClicked()
    // Если нажал человек
    {
        Taken = true;
        ButtonText.text = Game.HumanModel.Marker;
        ButtonText.gameObject.SetActive(true);
        OnPlayerClick?.Invoke(this);
    }

    public void CellTaken(CellButton chosenButton)
    // Если кнопку выбрал ПК
    {
        if (chosenButton.Equals(this))
        {
            Taken = true;
            ButtonText.text = Game.PCModel.Marker;
            ButtonText.gameObject.SetActive(true);
            GetComponent<Button>().enabled = false;
        }
    }
}