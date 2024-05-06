

namespace PuzzleSystem
{
    public class Chess : Puzzle
    {
        private const string CHESS_DIRECTORY_PATH = "..\\..\\..\\PuzzlesSystem\\Puzzles\\Chess";

        private int _loadedPuzzle = -1;

        private Point _solutionPosition;

        private ChessPiece _solutionPiece;

        private ChessPiece _heldPiece;

        public bool IsHoldingPiece { get; private set; } = false;

        public Chess()
        {
            Size = 12;
            Elements = new Element[Size, Size];

            LoadPuzzle(_loadedPuzzle);
        }

        private void LoadPuzzle(int puzzleID)
        {
            GenerateBoard();

            _solver.WalkOn((WalkableElement)ElementAt(1, 1));
            AddElement(_solver, 1, 1);

            string[] chessPuzzles = Directory.GetFiles(CHESS_DIRECTORY_PATH);

            if (puzzleID == -1)
            { 
                _loadedPuzzle = RandomIndex(chessPuzzles.Length);
                GeneratePieces(chessPuzzles[_loadedPuzzle]);
            }
            else
            {
                GeneratePieces(chessPuzzles[_loadedPuzzle]);
            }
            
        }

        private void GeneratePieces(string fileName)
        {
            string rawData = File.ReadAllText(fileName);
            string[] pieces = rawData.Replace("\r\n", "").Split(' ');

            for (int i = 2; i <= 9; i++)
            {
                for (int j = 2; j <= 9; j++)
                {
                    string pieceData = pieces[((i-2) * 8) + (j-2)];
                    ChessTile tile = (ChessTile)ElementAt(i, j);
                    PlacePieceOnTile(tile, pieceData,new Point(j,i));
                }
            }

        }

        private void PlacePieceOnTile(ChessTile tile, string pieceData,Point position)
        {
            char pieceTypeData = pieceData[0];
            char pieceColorData, pieceSolutionData, positionSolutionData, pieceNumberData;

            if (pieceTypeData == 'P' || pieceTypeData == 'B' || pieceTypeData == 'N' || pieceTypeData == 'R')
            {
                pieceNumberData = pieceData[1];
                pieceColorData = pieceData[2];
                pieceSolutionData = pieceData[3];
                positionSolutionData = pieceData[4];
            }
            else
            {
                pieceNumberData = ' ';
                pieceColorData = pieceData[1];
                pieceSolutionData = pieceData[2];
                positionSolutionData = pieceData[3];
            }

            ChessPiece piece = CreatePieceFromData(pieceTypeData, pieceColorData, pieceNumberData);

            tile.PlacePiece(piece);

            if(pieceSolutionData == 'T')
            {
                _solutionPiece = piece;
            }

            if (positionSolutionData == 'T')
            {
                _solutionPosition = position;
            }
        }

        private ChessPiece CreatePieceFromData(char pieceTypeData, char pieceColorData, char pawnNumberData)
        {
            ChessPieceType type = ChessPieceType.Empty;
            ConsoleColor color = ConsoleColor.Red;
            string identifier = EMPTY_EI;

            switch (pieceTypeData)
            {
                case ('K'):
                    type = ChessPieceType.King;
                    identifier = " K";
                    break;

                case ('Q'):
                    type = ChessPieceType.Queen;
                    identifier = " Q";
                    break;

                case ('B'):
                    if(pawnNumberData == '1')
                    {
                        type = ChessPieceType.Bishop1;
                    }
                    else
                    {
                        type = ChessPieceType.Bishop2;
                    }

                    identifier = " B";
                    break;

                case ('N'):
                    if (pawnNumberData == '1')
                    {
                        type = ChessPieceType.Knight1;
                    }
                    else
                    {
                        type = ChessPieceType.Knight2;
                    }
                    identifier = " N";
                    break;

                case ('R'):
                    if (pawnNumberData == '1')
                    {
                        type = ChessPieceType.Rook1;
                    }
                    else
                    {
                        type = ChessPieceType.Rook2;
                    };
                    identifier = " R";
                    break;

                case ('P'):
                    identifier = " P";
                    switch (pawnNumberData)
                    {
                        case ('1'):
                            type = ChessPieceType.Pawn1;
                            break;
                        case ('2'):
                            type = ChessPieceType.Pawn2;
                            break;
                        case ('3'):
                            type = ChessPieceType.Pawn3;
                            break;
                        case ('4'):
                            type = ChessPieceType.Pawn4;
                            break;
                        case ('5'):
                            type = ChessPieceType.Pawn5;
                            break;
                        case ('6'):
                            type = ChessPieceType.Pawn6;
                            break;
                        case ('7'):
                            type = ChessPieceType.Pawn7;
                            break;
                        case ('8'):
                            type = ChessPieceType.Pawn8;
                            break;
                    }
                    break;
            }

            if(pieceColorData == 'B') color = ConsoleColor.Blue;

            return new ChessPiece(type, identifier,color);


        }
        private void GenerateBoard()
        {

            ConsoleColor tileColor = ConsoleColor.White;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (j == 0 || j == Size - 1)
                    {
                        AddElement(ObstacleElement.VerticalWallInstance, i, j);
                        continue;
                    }
                    if (i == 0 || i == Size - 1)
                    {
                        AddElement(ObstacleElement.HorizontalWallInstance, i, j);
                        continue;
                    }
                    if (i == 1 || i == Size - 2|| j == 1 || j == Size - 2)
                    {
                        AddElement(new WalkableElement(ConsoleColor.DarkYellow,EMPTY_EI), i, j);
                        continue;
                    }

                    AddElement(new ChessTile(tileColor), i, j);

                    tileColor = SwapTileColor(tileColor);

                }

                tileColor = SwapTileColor(tileColor);
            }
        }

        private ConsoleColor SwapTileColor(ConsoleColor color)
        {
            if(color == ConsoleColor.White) return ConsoleColor.Black;

            return ConsoleColor.White;
        }

        public void LiftPiece()
        {
            if(_solver.WalkableElementOnTopOf is ChessTile tile)
            {
                if (tile.PieceType == ChessPieceType.Empty) return;
                _solver.ResetIdentifier();
                _heldPiece = tile.LiftPiece();
                IsHoldingPiece = true;
            }
        }

        public bool PlacePiece()
        {
            if (_heldPiece.PieceType == _solutionPiece.PieceType && _heldPiece.PieceColor == _solutionPiece.PieceColor && Point.Equals(_solver.Position,_solutionPosition))
            {
                return true;
            }

            return false;
        }

        public void ResetPuzzle()
        {
            _solver = new PuzzleSolver();
            IsHoldingPiece = false;
            LoadPuzzle(_loadedPuzzle);
            SceneManager.PrintCurrentScene();
        }

    }
}
