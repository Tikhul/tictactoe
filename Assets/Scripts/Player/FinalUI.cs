using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalUI : TicTacToeElement
{
    [SerializeField] private TMP_Text _results;

    public TMP_Text Results
    {
        get => _results;
        set => _results = value;
    }
    public void ActivateResults(string result)
    {
        Results.text = result;
        Results.gameObject.SetActive(true);
    }
}
