
namespace SrcChess2 {
    /// <summary>Basic board evaluation function</summary>
    public class BoardEvaluationBasic : IBoardEvaluation {
        /// <summary>Value of each piece/color.</summary>
        static protected int[]      s_piPiecePoint;

        /// <summary>
        /// Static constructor
        /// </summary>
        static BoardEvaluationBasic() {
            s_piPiecePoint                                                                = new int[16];
            s_piPiecePoint[(int)ChessBoard.PieceE.Pawn]                                   = 100;
            s_piPiecePoint[(int)ChessBoard.PieceE.Rook]                                   = 500;
            s_piPiecePoint[(int)ChessBoard.PieceE.Knight]                                 = 300;
            s_piPiecePoint[(int)ChessBoard.PieceE.Bishop]                                 = 325;
            s_piPiecePoint[(int)ChessBoard.PieceE.Queen]                                  = 900;
            s_piPiecePoint[(int)ChessBoard.PieceE.King]                                   = 1000000;
            s_piPiecePoint[(int)(ChessBoard.PieceE.Pawn | ChessBoard.PieceE.Black)]       = -100;
            s_piPiecePoint[(int)(ChessBoard.PieceE.Rook | ChessBoard.PieceE.Black)]       = -500;
            s_piPiecePoint[(int)(ChessBoard.PieceE.Knight | ChessBoard.PieceE.Black)]     = -300;
            s_piPiecePoint[(int)(ChessBoard.PieceE.Bishop | ChessBoard.PieceE.Black)]     = -325;
            s_piPiecePoint[(int)(ChessBoard.PieceE.Queen | ChessBoard.PieceE.Black)]      = -900;
            s_piPiecePoint[(int)(ChessBoard.PieceE.King | ChessBoard.PieceE.Black)]       = -1000000;
        }

        /// <summary>
        /// Name of the evaluation method
        /// </summary>
        public virtual string Name {
            get {
                return("Basic");
            }
        }

        /// <summary>
        /// Evaluates a board. The number of point is greater than 0 if white is in advantage, less than 0 if black is.
        /// </summary>
        /// <param name="pBoard">           Board.</param>
        /// <param name="piPiecesCount">    Number of each pieces</param>
        /// <param name="posInfo">          Information about pieces position</param>
        /// <param name="iWhiteKingPos">    Position of the white king</param>
        /// <param name="iBlackKingPos">    Position of the black king</param>
        /// <param name="bWhiteCastle">     White has castled</param>
        /// <param name="bBlackCastle">     Black has castled</param>
        /// <param name="iMoveCountDelta">  Number of possible white move - Number of possible black move</param>
        /// <returns>
        /// Points
        /// </returns>
        public virtual int Points(ChessBoard.PieceE[]   pBoard,
                                  int[]                 piPiecesCount,
                                  ChessBoard.PosInfoS   posInfo,
                                  int                   iWhiteKingPos,
                                  int                   iBlackKingPos,
                                  bool                  bWhiteCastle,
                                  bool                  bBlackCastle,
                                  int                   iMoveCountDelta) {
            int iRetVal = 0;
            
            for (int iIndex = 0; iIndex < piPiecesCount.Length; iIndex++) {
                iRetVal += s_piPiecePoint[iIndex] * piPiecesCount[iIndex];
            }
            if (pBoard[12] == ChessBoard.PieceE.Pawn) {
                iRetVal -= 4;
            }
            if (pBoard[52] == (ChessBoard.PieceE.Pawn | ChessBoard.PieceE.Black)) {
                iRetVal += 4;
            }
            if (bWhiteCastle) {
                iRetVal += 10;
            }
            if (bBlackCastle) {
                iRetVal -= 10;
            }
            iRetVal += iMoveCountDelta;
            iRetVal += posInfo.m_iAttackedPieces << 1;
            //iRetVal += posInfo.m_iAttackedPos + posInfo.m_iAttackedPieces * 2 + posInfo.m_iPiecesDefending * 2;
            return(iRetVal);
        }
    } // Class BoardEvaluationBasic
} // Namespace
