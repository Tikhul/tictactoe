using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellButton : TicTacToeElement, ICellButton
// Кнопка для поля
{
    private int _cellInt;
    private char _cellChar;
    private bool _taken;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private Button _buttonElement;
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
    public TMP_Text ButtonText
    {
        get => _buttonText;
        set => _buttonText = value;
    }
    public Button ButtonElement
    {
        get => _buttonElement;
        set => _buttonElement = value;
    }

    public static event System.Action<CellButton> OnPlayerClick = delegate { };

    private void OnEnable()
    {
        Game.PCController.OnPCTurn += CellTaken;
    }

    private void OnDisable()
    {
        Game.PCController.OnPCTurn -= CellTaken;
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