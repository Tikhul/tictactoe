using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalUI : TicTacToeElement
{
    public TMP_Text results;
    
    public void ActivateResults(string result)
    {
        results.text = result;
        results.gameObject.SetActive(true);
    }
}
