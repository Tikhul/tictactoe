using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardController : TicTacToeElement
{
    public event System.Action<string> OnBoardCreated = delegate { };

    private void OnEnable()
    {
        Game.TicTacToeController.PCController.OnPCTurn += CellsAfterTurn;
        //CellButton.OnPlayerClick += CellsAfterTurn;
    }
    private void OnDisable()
    {
        Game.TicTacToeController.PCController.OnPCTurn -= CellsAfterTurn;
        //CellButton.OnPlayerClick -= CellsAfterTurn;
    }

    public void CreateBoard(string actualMarker)
     // Создание борда для игры
    {
        BoardPartsPrototype prototype = new StandardPartPrototype();

        GameObject boardPanel = prototype.Clone(prototype.ParentPanel, prototype.BoardParent, 
            prototype.ParentPanelSide, prototype.ParentPanelSide);

        GameObject row = prototype.Clone(prototype.Rows, boardPanel, prototype.ParentPanelSide, prototype.ParentPanelSide);

        for (int r = 0; r < prototype.RowNumber; r++)
        {
            GameObject column = prototype.Clone(prototype.Columns, row, prototype.ButtonWidth, prototype.ParentPanelSide);

            for (int b = 0; b < prototype.RowNumber; b++)
            {
                GameObject button = prototype.Clone(prototype.ButtonExample, column, prototype.ButtonWidth, prototype.ButtonWidth);
                FillCellist(button, b, r);
            }
        }
        CreateWinCombinations();
        OnBoardCreated?.Invoke(actualMarker);
    }

    private void CreateWinCombinations()
    // Создание общих выигрышных комбинаций в игре
    {
        Builder builder = new WinCombinationsBuilder();
        Director director = new Director(builder);
        director.Construct();
        Game.TicTacToeModel.BoardModel.WinCombinations.AddRange(builder.GetResult());
    }

    public void FillCellist(GameObject button, int b, int r)
    // Заполняю список ячеек в борде
    {
        CellButton buttonSettings = button.GetComponent<CellButton>();
        buttonSettings.CellChar = BoardModel.Alphabet[b];
        buttonSettings.CellInt = r;
        Game.TicTacToeModel.BoardModel.CellList.Add(buttonSettings);
        buttonSettings.OnPlayerClick += CellsAfterTurn;
    }

    public void CellsAfterTurn(CellButton receivedCell)
    // Проверка оставшихся ячеек после каждого хода
    {
        if (Game.TicTacToeModel.BoardModel.CellList.Any())
        {
            List<CellButton> tempList = new List<CellButton>();

            foreach (var cell in Game.TicTacToeModel.BoardModel.CellList)
            {
                if (cell.CellChar.Equals(receivedCell.CellChar) && cell.CellInt.Equals(receivedCell.CellInt)) 
                {
                    tempList.Add(cell);
                } 
            }
            Game.TicTacToeModel.BoardModel.CellList.RemoveAll(item => tempList.Contains(item));
            receivedCell.OnPlayerClick -= CellsAfterTurn;
        }
        else
        {
            Game.TicTacToeController.GameController.CheckGameState(true);
            Game.TicTacToeController.GameController.GetResults("Ничья");
        }    
    }
    public void ManageButtons(bool state)
    {
        foreach (var cell in Game.TicTacToeModel.BoardModel.CellList)
        {
            cell.ButtonElement.enabled = state;
        }
    }
}
