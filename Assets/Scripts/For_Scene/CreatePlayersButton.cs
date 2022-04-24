using UnityEngine;
using TMPro;

public class CreatePlayersButton : MonoBehaviour
{
    public TMP_Text playerName;
    public string marker;

    public delegate void ClickAction(string text);
    public static event ClickAction OnPlayerChosen;

    public void PlayerChosen()
    {
        OnPlayerChosen(marker);
    }
}
