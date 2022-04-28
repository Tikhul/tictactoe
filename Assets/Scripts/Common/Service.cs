using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Service : TicTacToeElement
{
    public static void BlockButtons()
    {
        foreach (var cell in FindObjectsOfType<CellButton>())
        {
            if (cell.GetComponent<Button>()) cell.GetComponent<Button>().enabled = false;
        }
    }

    public static void ActivateButtons()
    {
        foreach (var cell in FindObjectsOfType<CellButton>())
        {
            if (cell.GetComponent<Button>() && !cell.taken) cell.GetComponent<Button>().enabled = true;
        }
    }
}
