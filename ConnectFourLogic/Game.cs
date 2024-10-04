using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connectFourLogic
{
	public class Game
	{
		public readonly Player m_Player1;
		public readonly Player m_Player2;
		public readonly PC m_Computer;
		public readonly Board m_Board;
		private eCellOptions m_WhosTurn;
		private eCellOptions m_Winner;
		private readonly int m_GameMode;

		public Game(Player i_Player1, Player i_Player2, int i_BoardCols, int i_BoardRow)
		{
			m_Player1 = i_Player1;
			m_Player2 = i_Player2;
			m_GameMode = 1;
			m_Board = new Board(i_BoardCols, i_BoardRow);
			m_WhosTurn = eCellOptions.Player1;
			m_Winner = eCellOptions.Empty;
		}

		public Game(Player i_Player, PC i_Computer, int i_BoardCols, int i_BoardRows)
		{
			m_Player1 = i_Player;
			m_Computer = i_Computer;
			m_GameMode = 2;
			m_Board = new Board(i_BoardCols, i_BoardRows);
			m_WhosTurn = eCellOptions.Player1;
			m_Winner = eCellOptions.Empty;
		}

		public Boolean PlayerTwoSign()
		{
			return m_Player2 == null;
		}

		public eCellOptions WhosTurn
		{
			get { return m_WhosTurn; }
		}

		public int GameMode
		{
			get { return m_GameMode;}
		}

		public eCellOptions Winner
		{
			get { return m_Winner; }
		}

		public Boolean IsGameOver()
		{
			bool isGameOverFlag = false;

			if (m_Board.IsBoardFull(m_Board) && m_Winner == eCellOptions.Empty)
			{
				m_Winner = eCellOptions.Draw;
				isGameOverFlag = true;
			}
			else if (m_Winner != eCellOptions.Empty)
			{
				if (m_Winner == eCellOptions.Player1)
				{
					m_Player1.AddScore();
				}
				else
				{
					AddPlayerTwoScore();
				}

				isGameOverFlag = true;
			}

			return isGameOverFlag;  
		}

		public void GetPCMove()
		{
			int choosenRow = 0;
			int choosenCol = 0;

			m_Computer.EvaluateBoard(m_Board, ref choosenRow, ref choosenCol);
			m_Board.InsertToBoard(ref choosenRow, choosenCol, eCellOptions.Player2);
			updateWinner(m_Board, choosenRow, choosenCol);
			switchTurns();
		}

		public Boolean GetPlayerMove(int i_ChoosenCol)
		{
			bool failureToInsert = false;
			int row = 0;

			if (m_Winner == eCellOptions.Empty )
			{
				i_ChoosenCol -= 1;
				failureToInsert = m_Board.InsertToBoard(ref row, i_ChoosenCol, m_WhosTurn);

				if (!failureToInsert)
				{
					updateWinner(m_Board, row, i_ChoosenCol);
					switchTurns();
				}
			}

			return failureToInsert;
		}

		public void GiveUp()
		{
			if(m_WhosTurn == eCellOptions.Player1)
			{
				m_Winner = eCellOptions.Player2;
			}
			else
			{
				m_Winner = eCellOptions.Player1;
			}
		}

		private void switchTurns()
		{
			if (m_WhosTurn == eCellOptions.Player1)
			{
				m_WhosTurn = eCellOptions.Player2;
			}
			else
			{
				m_WhosTurn = eCellOptions.Player1;
			}
		}

		private void updateWinner(Board i_boardGame, int i_RowToCheck, int i_ColToCheck)
		{
			if(checkConnectFour(i_boardGame, i_RowToCheck, i_ColToCheck, 1, 0) || checkConnectFour(i_boardGame, i_RowToCheck, i_ColToCheck, 0, 1) || checkConnectFour(i_boardGame, i_RowToCheck, i_ColToCheck, 1, 1) || checkConnectFour(i_boardGame, i_RowToCheck, i_ColToCheck, 1, -1))
			{
				m_Winner = m_WhosTurn;
			}
		}

		public void RestartGame()
		{
			m_Board.InitBoard();
			m_WhosTurn = eCellOptions.Player1;
			m_Winner = eCellOptions.Empty;
		}

		private static Boolean checkConnectFour(Board i_board, int i_row, int i_col, int i_rowStep, int i_colStep)
		{
			bool isWinningFlag = false;
			int counter = 0;

			for (int i = i_row, j = i_col; (0 <= i && i < i_board.BoardRows) && (0 <= j && j < i_board.BoardCols); i += i_rowStep, j += i_colStep)
			{
				if (i_board.GetCellValue(i, j) == i_board.GetCellValue(i_row, i_col))
				{
					counter++;
				}
				else
				{
					break;
				}
			}

			for (int i = i_row - i_rowStep, j = i_col - i_colStep; (0 <= i && i < i_board.BoardRows) && (0 <= j && j < i_board.BoardCols); i -= i_rowStep, j -= i_colStep)
			{
				if (i_board.GetCellValue(i, j) == i_board.GetCellValue(i_row, i_col))
				{
					counter++;
				}
				else
				{
					break;
				}
			}

			if (counter == 4)
			{
				isWinningFlag = true;
			}

			return isWinningFlag;
		}

		public int GetPlayerTwoScore()
		{
			return m_Player2 == null ? m_Computer.PlayerScore : m_Player2.PlayerScore;
		}

		public void AddPlayerTwoScore()
		{
			if (m_Player2 == null)
			{
				m_Computer.AddScore();
			}
			else 
			{
				m_Player2.AddScore();
			}
		}
	}
}


