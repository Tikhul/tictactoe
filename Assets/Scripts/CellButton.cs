using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellButton : TicTacToeElement, ICell
    // Кнопка для поля
{
    [SerializeField] private TMP_Text _buttonText;

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
    public TMP_Text ButtonText
    {
        get => _buttonText;
        set => _buttonText = value;
    }

    //public delegate void ClickAction(CellButton cell);
    //public static event ClickAction OnPlayerClick;

    public event System.Action<CellButton> OnPlayerClick = delegate { };

    private void OnEnable()
    {
        PCController.OnGenerateFinished += CellTaken;
    }

    private void OnDisable()
    {
        PCController.OnGenerateFinished -= CellTaken;
    }
    public void CellClicked()
    // Если нажал человек
    {
        Taken = true;
        ButtonText.text = game.human.Marker;
        ButtonText.gameObject.SetActive(true);
        OnPlayerClick?.Invoke(this);
    }

    public void CellTaken(ICell chosenButton)
    // Если кнопку выбрал ПК
    {
        if (chosenButton.Equals(this))
        {
            Taken = true;
            ButtonText.text = game.pc.Marker;
            ButtonText.gameObject.SetActive(true);
            GetComponent<Button>().enabled = false;
        }
    }
}

public class BerdCell : ICell
{
    public void CellClicked()
    {
        throw new System.NotImplementedException();
    }

    public void CellTaken(ICell chosenButton)
    {
    }
}