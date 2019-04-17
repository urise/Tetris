using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGameLogic
{
    public enum TetrisCellState
    {
        Empty,
        Falling,
        Static
    }

    public enum TetrisCellType
    {
        Ordinal,
        RemoveForWin
    }

    public class TetrisCell
    {
        public TetrisCellState State { get; private set; } = TetrisCellState.Empty;
        public TetrisCellType CellType { get; set; } = TetrisCellType.Ordinal;

        public TetrisCell()
        {

        }

        public TetrisCell(TetrisCellState state, TetrisCellType cellType)
        {
            Set(state, cellType);
        }

        public override string ToString()
        {
            return State.ToString();
        }

        public void CopyFrom(TetrisCell tetrisCell)
        {
            State = tetrisCell.State;
            CellType = tetrisCell.CellType;
        }

        public void SetState(TetrisCellState state)
        {
            State = state;
        }

        public void Set(TetrisCellState state, TetrisCellType cellType)
        {
            State = state;
            CellType = cellType;
        }
    }
}
