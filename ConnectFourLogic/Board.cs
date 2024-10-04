using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connectFourLogic
{
	public class Board
	{
		private readonly int m_BoardCols;
		private readonly int m_BoardRows;
		private eCellOptions[,] m_GameBoard;

		public Board(int i_BoardRows, int i_BoardCols)
		{
			m_BoardRows = i_BoardRows;
			m_BoardCols = i_BoardCols;
			m_GameBoard = new eCellOptions[m_BoardRows, i_BoardCols];
			InitBoard();
		}

		public int BoardCols
		{
			get { return m_BoardCols; }
		}

		public int BoardRows
		{
			get { return m_BoardRows; }
		}
		
		public eCellOptions GetCellValue(int i_Row, int i_Col)
		{
			return m_GameBoard[i_Row, i_Col];
		}

		public void InitBoard()
		{
			for (int i = 0; i < m_BoardRows; i++)
			{
				for (int j = 0; j < m_BoardCols; j++)
				{
					m_GameBoard[i, j] = eCellOptions.Empty;
				}
			}
		}

		public Boolean IsFullCol(int i_ColToCheck)
		{
			return m_GameBoard[0, i_ColToCheck] != eCellOptions.Empty;
		}

		public Boolean IsBoardFull(Board i_GameBoard)
		{
			bool isFullFlag = true;

			for (int i = 0; i < i_GameBoard.m_BoardCols; i++)
			{
				isFullFlag = IsFullCol(i);
				if(!isFullFlag)
				{
				   break;
				}
			}

			return isFullFlag;
		}

		public Boolean InsertToBoard(ref int o_RowInserted, int i_ChoosenCol, eCellOptions i_PlayerToInsert)
		{
			bool invalidMoveFlag = false;
			o_RowInserted = 0;
  
			if(IsFullCol(i_ChoosenCol))
			{
				invalidMoveFlag = true;
			}
			else
			{
				for (int i = 0; i < m_BoardRows; i++)
				{
					if (m_GameBoard[i, i_ChoosenCol] == eCellOptions.Empty)
				   {
						o_RowInserted = i;
					} 
				}

				m_GameBoard[o_RowInserted, i_ChoosenCol] = i_PlayerToInsert;
			}

			return invalidMoveFlag;
		}
	}
}

