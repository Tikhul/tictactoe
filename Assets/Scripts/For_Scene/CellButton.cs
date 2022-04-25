using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellButton : MonoBehaviour
{
    public int cellInt;
    public char cellChar;
    public bool taken;
    public TMP_Text buttonText;
    private string humanMarker;

    public void GetHumanMarker(string marker)
    {
        humanMarker = marker;
    }
    public void CellClicked()
    {
        taken = true;
        GetComponent<Button>().enabled = false;
        buttonText.text = humanMarker;
    }
}