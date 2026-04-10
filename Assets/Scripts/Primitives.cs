

enum NeighbourType
{
    Forward,
    ForwardRight,
    Right,   
    BackwardRight,
    Backward, 
    BackwardLeft,
    Left,
    ForwardLeft 
}
public enum Team
{
    White,
    Black
}
public enum GameEvent
{
    Empty = 0,
    Select,
    Cancel,
    Confirm
}

public enum GameStatus
{   
    Error,
    Lock,
    Unlock,
    Select,
    Move,
    Confirm
}

public enum ChessPieces
{
    Pawn,
    Rook,
    Knight,
    Bishop,
    Queen,
    King
}

