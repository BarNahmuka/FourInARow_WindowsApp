using System;

namespace connectFourLogic
{
	public class PC
	{
		private readonly string m_PlayerSign;
		private int m_PlayerScore;

		public PC(string i_PlayerSign)
		{
			m_PlayerSign = i_PlayerSign;
		}

		public string PlayerSign
		{
			get { return m_PlayerSign; }
		}

		public int PlayerScore
		{
			get { return m_PlayerScore; }
		}

		public void AddScore()
		{
			m_PlayerScore++;
		}

		public void EvaluateBoard(Board i_GameBoard, ref int io_ChoosenRow, ref int io_ChoosenCol)
		{
			io_ChoosenRow = 0;
			int PcMoveScore;
			int MoveWithHighestScore = 0;
			int InsertedRow = 0;
			int UserMoveScore;

			for (int i = 0; i < i_GameBoard.BoardCols; i++)
			{
				if (i_GameBoard.IsFullCol(i))
				{
					continue;
				}

				for (int j = 0; j < i_GameBoard.BoardRows ; j++)
				{
					if (i_GameBoard.GetCellValue(j,i) == eCellOptions.Empty)
					{
						InsertedRow = j;
					}
				}

				UserMoveScore = Math.Max(checkMoveScore(i_GameBoard, InsertedRow, i, 1, 0, eCellOptions.Player1), Math.Max(checkMoveScore(i_GameBoard, InsertedRow, i, 0, 1, eCellOptions.Player1), Math.Max(checkMoveScore(i_GameBoard, InsertedRow, i, 1, 1, eCellOptions.Player1), checkMoveScore(i_GameBoard, InsertedRow, i, 1, -1, eCellOptions.Player1))));
				PcMoveScore = Math.Max(checkMoveScore(i_GameBoard, InsertedRow, i, 1, 0, eCellOptions.Player2), Math.Max(checkMoveScore(i_GameBoard, InsertedRow, i, 0, 1, eCellOptions.Player2), Math.Max(checkMoveScore(i_GameBoard, InsertedRow, i, 1, 1, eCellOptions.Player2), checkMoveScore(i_GameBoard, InsertedRow, i, 1, -1, eCellOptions.Player2))));
				if (PcMoveScore > MoveWithHighestScore)
				{
					io_ChoosenRow = InsertedRow;
					io_ChoosenCol = i;
					MoveWithHighestScore = PcMoveScore;
				}

				if(UserMoveScore > MoveWithHighestScore)
				{
					io_ChoosenRow = InsertedRow;
					io_ChoosenCol = i;
					MoveWithHighestScore = UserMoveScore;
				}
			}
		}

		private int checkMoveScore(Board i_board, int i_row, int i_col, int i_rowStep, int i_colStep, eCellOptions i_playerToCheck)
		{
			int counter = 0;
			
			for (int i = i_row + i_rowStep, j = i_col + i_colStep; (0 <= i && i < i_board.BoardRows) && (0 <= j && j < i_board.BoardCols); i += i_rowStep, j += i_colStep)
			{
				if (i_playerToCheck == i_board.GetCellValue(i, j))
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
				if (i_playerToCheck == i_board.GetCellValue(i, j))
				{
					counter++;
				}
				else
				{
					break;
				}
			}

			return counter;
		}
	}
}
