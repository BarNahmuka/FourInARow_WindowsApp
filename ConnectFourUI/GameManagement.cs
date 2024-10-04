using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using connectFourLogic;

namespace connectFourUI
{
	public class GameManagement
	{
		public static void ManageGame(Game i_Game)
		{
			int minInputAvailable = 1;
			int maxInputAvailable = i_Game.m_Board.BoardCols;
			string inputFromUser;

			PrintBoard(i_Game);
			do
			{
				if ((i_Game.WhosTurn == eCellOptions.Player2 && i_Game.GameMode == 2))
				{
					i_Game.GetPCMove();
				}
				else
				{
					Console.WriteLine("Please enter number of columns:");
					inputFromUser = Console.ReadLine();
					int numericInputFromUser = Validate.ValidatePlayerMove(i_Game, inputFromUser, maxInputAvailable);
					while (i_Game.GetPlayerMove(numericInputFromUser))
					{
						Console.WriteLine("Column is full. Try to insert to a different column.");
						inputFromUser = Console.ReadLine();
						numericInputFromUser = Validate.ValidateMeasurement(inputFromUser, minInputAvailable, maxInputAvailable);
					}
				}

				Ex02.ConsoleUtils.Screen.Clear();
				PrintBoard(i_Game);
			}while (!i_Game.IsGameOver());

			if (i_Game.Winner == eCellOptions.Draw)
			{
				Console.WriteLine("The game finished in draw");
				Console.WriteLine("Player 1: " + i_Game.m_Player1.PlayerScore);
				Console.WriteLine(getPlayerTwoKind(i_Game) + ": " + i_Game.GetPlayerTwoScore());
			}
			else
			{
				Console.WriteLine((i_Game.Winner == eCellOptions.Player1 ? "player1" : getPlayerTwoKind(i_Game)) + " win the game");
				Console.WriteLine("Player 1 : " + i_Game.m_Player1.PlayerScore);
				Console.WriteLine(getPlayerTwoKind(i_Game) + ": " + i_Game.GetPlayerTwoScore());
			}

			Console.WriteLine();
			Console.WriteLine("Would you like to play another game? " + Environment.NewLine + "1 yes " + Environment.NewLine + "2 no");
			string answerFromTheUser = Console.ReadLine();
			int gameModeByNumeric = Validate.ValidateAnswer(answerFromTheUser);

			if (gameModeByNumeric == 1)
			{
				Ex02.ConsoleUtils.Screen.Clear();
				i_Game.RestartGame();
				ManageGame(i_Game);
			}
		}

		private static void PrintBoard(Game i_gameBoard)
		{
			int boardCols = i_gameBoard.m_Board.BoardCols;
			int boardRows = i_gameBoard.m_Board.BoardRows;
			StringBuilder messageToPrint = new StringBuilder();

			for (int i = 1; i < boardCols + 1; i++)
			{
				messageToPrint.Append("  ").Append(i).Append(" ");
			}

			messageToPrint.Append(Environment.NewLine);
			for (int i = 0; i < boardRows; i++)
			{
				for (int j = 0; j < boardCols; j++)
				{
					messageToPrint.Append("|").Append(" ");
					messageToPrint.Append(getSignForPrint(i_gameBoard, i_gameBoard.m_Board.GetCellValue(i, j)) + " ");
				}

				messageToPrint.Append("|");
				messageToPrint.Append(Environment.NewLine);
				for (int d = 0; d < (boardCols * 4) + 1; d++)
				{
					messageToPrint.Append("=");
				}

				messageToPrint.Append(Environment.NewLine);
			}

			Console.Write(messageToPrint.ToString());
		}

		private static string getSignForPrint(Game i_game, eCellOptions i_cellToCheck)
		{
			string cellValue;

			if (i_cellToCheck == eCellOptions.Empty)
			{
				cellValue = " ";
			}
			else if (i_cellToCheck == eCellOptions.Player1)
			{
				cellValue = i_game.m_Player1.PlayerSign;
			}
			else
			{
				if (i_game.PlayerTwoSign())
				{
					cellValue = i_game.m_Computer.PlayerSign;
				}
				else
				{
					cellValue = i_game.m_Player2.PlayerSign;
				}

			}

			return cellValue;
		}

		private static string getPlayerTwoKind(Game i_game)
		{
			return i_game.m_Player2 == null ? "Computer " : "Player 2 ";
		}
	}
}
