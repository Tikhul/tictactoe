using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellButton : MonoBehaviour
{
    public int cellInt;
    public char cellChar;
    public bool taken;
    public TMP_Text buttonText;

    public delegate void ClickAction(string marker, int cellInt, char cellChar);
    public static event ClickAction OnPlayerClick;

    public void CellClicked()
    {
        taken = true;
        buttonText.text = Process.human.marker;
        buttonText.gameObject.SetActive(true);
        OnPlayerClick(Process.human.marker, cellInt, cellChar);
    }
}