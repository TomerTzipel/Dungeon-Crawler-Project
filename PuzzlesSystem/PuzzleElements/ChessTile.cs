
namespace PuzzleSystem
{

    public enum ChessPieceType
    {
        King, Queen,Rook1, Rook2, Bishop1, Bishop2, Knight1, Knight2, Pawn1, Pawn2, Pawn3, Pawn4, Pawn5, Pawn6, Pawn7, Pawn8, Empty
    }
 
    public struct ChessPiece
    {
        public ChessPieceType PieceType { get; private set; }

        public ConsoleColor PieceColor { get; private set; }
        public string Identifier { get; private set; }
        public ChessPiece()
        {
            PieceType = ChessPieceType.Empty;
            PieceColor = ConsoleColor.Red;
            Identifier = EMPTY_EI;
        }
        public ChessPiece(ChessPieceType pieceType, string identifier, ConsoleColor pieceColor )
        {
            PieceType = pieceType;
            PieceColor = pieceColor;
            Identifier = identifier;     
        }
    }

    public class ChessTile : WalkableElement
    {
        private ChessPiece _piece;

        public ChessTile(ConsoleColor backgroundColor) : base(backgroundColor, EMPTY_EI) { }

        public ChessPieceType PieceType
        {
            get { return _piece.PieceType;}
        }

        public void PlacePiece(ChessPiece piece)
        {
            _piece = piece;
            Foreground = _piece.PieceColor;
            Identifier = piece.Identifier;
        }

        public ChessPiece LiftPiece()
        {
            ChessPiece piece = _piece;
            _piece = new ChessPiece();
            Identifier = _piece.Identifier;
            return piece;
        }



    }
}
