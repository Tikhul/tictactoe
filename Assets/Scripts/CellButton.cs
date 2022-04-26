using UnityEngine;
using TMPro;

public class CellButton : TicTacToeElement
{
    public int cellInt;
    public char cellChar;
    public bool taken;
    public TMP_Text buttonText;

    public delegate void ClickAction(string marker, int cellInt, char cellChar);
    public static event ClickAction OnPlayerClick;

    private void OnEnable()
    {
        PlayerController.OnTurnGenerated += CellTaken;
    }

    private void OnDisable()
    {
        PlayerController.OnTurnGenerated -= CellTaken;
    }

    public void CellClicked()
    {
        taken = true;
        buttonText.text = game.human.marker;
        buttonText.gameObject.SetActive(true);
        OnPlayerClick(game.human.marker, cellInt, cellChar);
    }

    public delegate void TakeAction(string marker, int cellInt, char cellChar);
    public static event TakeAction OnPCTaken;

    public void CellTaken(CellButton chosenButton)
    {
        if(this == chosenButton)
        {
            taken = true;
            buttonText.text = game.pc.marker;
            buttonText.gameObject.SetActive(true);
            OnPCTaken(game.pc.marker, cellInt, cellChar);
        }
    }
}