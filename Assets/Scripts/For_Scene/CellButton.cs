using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellButton : MonoBehaviour
{
    public int cellInt;
    public char cellChar;
    public bool taken;
    public TMP_Text buttonText;

    public void CellClicked()
    {
        taken = true;
        buttonText.text = Process.human.marker;
        buttonText.gameObject.SetActive(true);
        GetComponent<Button>().enabled = false;
    }
}