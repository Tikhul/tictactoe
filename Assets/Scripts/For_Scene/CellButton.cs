using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellButton : MonoBehaviour
{
    public int cellInt;
    public char cellChar;
    public bool taken;

    public delegate void CellClickAction();
    public static event CellClickAction OnCellHumanClicked;

    public void CellClicked()
    {
        taken = true;
        GetComponent<Button>().enabled = false;
        Process current = new Process();
        GetComponent<TMP_Text>().text = current.human.marker;
        OnCellHumanClicked();
    }
}
