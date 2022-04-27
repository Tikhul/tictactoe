using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeElement : MonoBehaviour
{
    public TicTacToeGame game { get { return FindObjectOfType<TicTacToeGame>(); } }
}
public class TicTacToeGame : MonoBehaviour
{
    public BoardModel boardModel;
    public BoardController boardController;
    public StepExecutionController stepExecutionController;
    public GameController gameController;
    public PlayerModel human;
    public PlayerModel pc;
    public PlayerView playerView;
    public PlayerController playerController;
    public Service service;

    public void Notify(string p_event_path, Object p_target, params object[] p_data)
    {
        TicTacToeElement[] elements_list = FindObjectsOfType<TicTacToeElement>();
        foreach (TicTacToeElement e in elements_list)
        {
          //  e.OnNotification(p_event_path, p_target, p_data);
        }
    }

}
