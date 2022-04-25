using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellButton : MonoBehaviour
{
    public int cellInt;
    public char cellChar;
    public bool taken;
    public TMP_Text buttonText;

    void OnEnable()
    {
        Player.OnPCTurn += PCTurn;
    }

    void OnDisable()
    {
        Player.OnPCTurn -= PCTurn;
    }

    public delegate void CellClickAction(string marker, int cellInt, char cellChar);
    public static event CellClickAction OnCellHumanClicked;

    public void CellClicked()
    {
        taken = true;
        buttonText.text = Process.human.marker;
        OnCellHumanClicked(Process.human.marker, cellInt, cellChar);
    }

    public delegate void PCTurnAction(string marker, int cellInt, char cellChar);
    public static event PCTurnAction OnCellPCTaken;

    void PCTurn(CellButton chosenButton)
    {
        if (this == chosenButton)
        {
            taken = true;
            buttonText.text = Process.pc.marker;
            OnCellPCTaken(Process.pc.marker, cellInt, cellChar);
        }
    }
}
