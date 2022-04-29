using UnityEngine.UI;
using TMPro;

public class CellButton : TicTacToeElement
    // Кнопка для поля
{
    public int cellInt;
    public char cellChar;
    public bool taken;
    public TMP_Text buttonText;

    public delegate void ClickAction(CellButton cell);
    public static event ClickAction OnPlayerClick;

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
        taken = true;
        buttonText.text = game.human.marker;
        buttonText.gameObject.SetActive(true);
        OnPlayerClick(this);
    }

    public void CellTaken(CellButton chosenButton)
    // Если кнопку выбрал ПК
    {
        if (chosenButton.Equals(this))
        {
            taken = true;
            buttonText.text = game.pc.marker;
            buttonText.gameObject.SetActive(true);
            GetComponent<Button>().enabled = false;
        }
    }
}