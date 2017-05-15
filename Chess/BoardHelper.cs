using System;

namespace Chess
{

    [Serializable()]
    class BoardHelper
    {
        private Chessboard board;

        private Factory factory;
        
        public BoardHelper(Chessboard board)
        {
            this.board = board;
            this.factory = new Factory(this.board.game);
        }

        public void SetupBoard(string setup)
        {
            switch (setup)
            {
                case "new_game_setup":

                    // WEISSE SPIELFIGUREN
                    PawnWhite pawn_w_1 = factory.GetPawnWhite("A2");
                    PawnWhite pawn_w_2 = factory.GetPawnWhite("B2");
                    PawnWhite pawn_w_3 = factory.GetPawnWhite("C2");
                    PawnWhite pawn_w_4 = factory.GetPawnWhite("D2");
                    PawnWhite pawn_w_5 = factory.GetPawnWhite("E2");
                    PawnWhite pawn_w_6 = factory.GetPawnWhite("F2");
                    PawnWhite pawn_w_7 = factory.GetPawnWhite("G2");
                    PawnWhite pawn_w_8 = factory.GetPawnWhite("H2");
                    Rook rook_w_1 = factory.GetRook("white", "A1");
                    Knight knight_w_1 = factory.GetKnight("white", "B1");
                    Bishop bishop_w_1 = factory.GetBishop("white", "C1");
                    Queen queen_w = factory.GetQueen("white", "D1");
                    King king_w = factory.GetKing("white", "E1");
                    Bishop bishop_w_2 = factory.GetBishop("white", "F1");
                    Knight knight_w_2 = factory.GetKnight("white", "G1");
                    Rook rook_w_2 = factory.GetRook("white", "H1");

                    // SCHWARZE SPIELFIGUREN
                    PawnBlack pawn_b_1 = factory.GetPawnBlack("A7");
                    PawnBlack pawn_b_2 = factory.GetPawnBlack("B7");
                    PawnBlack pawn_b_3 = factory.GetPawnBlack("C7");
                    PawnBlack pawn_b_4 = factory.GetPawnBlack("D7");
                    PawnBlack pawn_b_5 = factory.GetPawnBlack("E7");
                    PawnBlack pawn_b_6 = factory.GetPawnBlack("F7");
                    PawnBlack pawn_b_7 = factory.GetPawnBlack("G7");
                    PawnBlack pawn_b_8 = factory.GetPawnBlack("H7");
                    Rook rook_b_1 = factory.GetRook("black", "A8");
                    Knight knight_b_1 = factory.GetKnight("black", "B8");
                    Bishop bishop_b_1 = factory.GetBishop("black", "C8");
                    Queen queen_b = factory.GetQueen("black", "D8");
                    King king_b = factory.GetKing("black", "E8");
                    Bishop bishop_b_2 = factory.GetBishop("black", "F8");
                    Knight knight_b_2 = factory.GetKnight("black", "G8");
                    Rook rook_b_2 = factory.GetRook("black", "H8");

                    // WEISSE SPIELFIGUREN
                    board.chessman.Add(pawn_w_1);
                    board.chessman.Add(pawn_w_2);
                    board.chessman.Add(pawn_w_3);
                    board.chessman.Add(pawn_w_4);
                    board.chessman.Add(pawn_w_5);
                    board.chessman.Add(pawn_w_6);
                    board.chessman.Add(pawn_w_7);
                    board.chessman.Add(pawn_w_8);
                    board.chessman.Add(rook_w_1);
                    board.chessman.Add(rook_w_2);
                    board.chessman.Add(bishop_w_1);
                    board.chessman.Add(bishop_w_2);
                    board.chessman.Add(knight_w_1);
                    board.chessman.Add(knight_w_2);
                    board.chessman.Add(queen_w);
                    board.chessman.Add(king_w);

                    // SCHWARZE SPIELFIGUREN
                    board.chessman.Add(pawn_b_1);
                    board.chessman.Add(pawn_b_2);
                    board.chessman.Add(pawn_b_3);
                    board.chessman.Add(pawn_b_4);
                    board.chessman.Add(pawn_b_5);
                    board.chessman.Add(pawn_b_6);
                    board.chessman.Add(pawn_b_7);
                    board.chessman.Add(pawn_b_8);
                    board.chessman.Add(rook_b_1);
                    board.chessman.Add(rook_b_2);
                    board.chessman.Add(bishop_b_1);
                    board.chessman.Add(bishop_b_2);
                    board.chessman.Add(knight_b_1);
                    board.chessman.Add(knight_b_2);
                    board.chessman.Add(queen_b);
                    board.chessman.Add(king_b);

                    break;

                case "promotion_setup":
                    // WEISSE SPIELFIGUREN
                    PawnWhite promotion_pawn_w_1 = factory.GetPawnWhite("A7");
                    PawnWhite promotion_pawn_w_2 = factory.GetPawnWhite("B7");
                    King promotion_king_w = factory.GetKing("white", "E1");
                    Bishop promotion_bishop_w_2 = factory.GetBishop("white", "F1");
                    Knight promotion_knight_w_2 = factory.GetKnight("white", "G1");
                    Rook promotion_rook_w_2 = factory.GetRook("white", "H1");

                    // SCHWARZE SPIELFIGUREN
                    PawnBlack promotion_pawn_b_1 = factory.GetPawnBlack("A2");
                    PawnBlack promotion_pawn_b_2 = factory.GetPawnBlack("B2");
                    King promotion_king_b = factory.GetKing("black", "E8");
                    Bishop promotion_bishop_b_2 = factory.GetBishop("black", "F8");
                    Knight promotion_knight_b_2 = factory.GetKnight("black", "G8");
                    Rook promotion_rook_b_2 = factory.GetRook("black", "H8");

                    // WEISSE SPIELFIGUREN
                    board.chessman.Add(promotion_pawn_w_1);
                    board.chessman.Add(promotion_pawn_w_2);
                    board.chessman.Add(promotion_king_w);
                    board.chessman.Add(promotion_knight_w_2);
                    board.chessman.Add(promotion_bishop_w_2);
                    board.chessman.Add(promotion_rook_w_2);

                    // SCHWARZE SPIELFIGUREN
                    board.chessman.Add(promotion_pawn_b_1);
                    board.chessman.Add(promotion_pawn_b_2);
                    board.chessman.Add(promotion_king_b);
                    board.chessman.Add(promotion_knight_b_2);
                    board.chessman.Add(promotion_bishop_b_2);
                    board.chessman.Add(promotion_rook_b_2);

                    break;

                case "en_passant_setup":

                    // WEISSE SPIELFIGUREN
                    PawnWhite en_passant_pawn_w_1 = factory.GetPawnWhite("A2");
                    PawnWhite en_passant_pawn_w_2 = factory.GetPawnWhite("B2");
                    PawnWhite en_passant_pawn_w_3 = factory.GetPawnWhite("C4");
                    PawnWhite en_passant_pawn_w_4 = factory.GetPawnWhite("D2");
                    PawnWhite en_passant_pawn_w_5 = factory.GetPawnWhite("E2");
                    PawnWhite en_passant_pawn_w_6 = factory.GetPawnWhite("F2");
                    PawnWhite en_passant_pawn_w_7 = factory.GetPawnWhite("G2");
                    PawnWhite en_passant_pawn_w_8 = factory.GetPawnWhite("H2");
                    Rook en_passant_rook_w_1 = factory.GetRook("white", "A1");
                    Knight en_passant_knight_w_1 = factory.GetKnight("white", "B1");
                    Bishop en_passant_bishop_w_1 = factory.GetBishop("white", "C1");
                    Queen en_passant_queen_w = factory.GetQueen("white", "D1");
                    King en_passant_king_w = factory.GetKing("white", "E1");
                    Bishop en_passant_bishop_w_2 = factory.GetBishop("white", "F1");
                    Knight en_passant_knight_w_2 = factory.GetKnight("white", "G1");
                    Rook en_passant_rook_w_2 = factory.GetRook("white", "H1");

                    // SCHWARZE SPIELFIGUREN
                    PawnBlack en_passant_pawn_b_1 = factory.GetPawnBlack("A7");
                    PawnBlack en_passant_pawn_b_2 = factory.GetPawnBlack("B7");
                    PawnBlack en_passant_pawn_b_3 = factory.GetPawnBlack("C7");
                    PawnBlack en_passant_pawn_b_4 = factory.GetPawnBlack("D7");
                    PawnBlack en_passant_pawn_b_5 = factory.GetPawnBlack("E7");
                    PawnBlack en_passant_pawn_b_6 = factory.GetPawnBlack("F5");
                    PawnBlack en_passant_pawn_b_7 = factory.GetPawnBlack("G7");
                    PawnBlack en_passant_pawn_b_8 = factory.GetPawnBlack("H7");
                    Rook en_passant_rook_b_1 = factory.GetRook("black", "A8");
                    Knight en_passant_knight_b_1 = factory.GetKnight("black", "B8");
                    Bishop en_passant_bishop_b_1 = factory.GetBishop("black", "C8");
                    Queen en_passant_queen_b = factory.GetQueen("black", "D8");
                    King en_passant_king_b = factory.GetKing("black", "E8");
                    Bishop en_passant_bishop_b_2 = factory.GetBishop("black", "F8");
                    Knight en_passant_knight_b_2 = factory.GetKnight("black", "G8");
                    Rook en_passant_rook_b_2 = factory.GetRook("black", "H8");

                    // WEISSE SPIELFIGUREN
                    board.chessman.Add(en_passant_pawn_w_1);
                    board.chessman.Add(en_passant_pawn_w_2);
                    board.chessman.Add(en_passant_pawn_w_3);
                    board.chessman.Add(en_passant_pawn_w_4);
                    board.chessman.Add(en_passant_pawn_w_5);
                    board.chessman.Add(en_passant_pawn_w_6);
                    board.chessman.Add(en_passant_pawn_w_7);
                    board.chessman.Add(en_passant_pawn_w_8);
                    board.chessman.Add(en_passant_rook_w_1);
                    board.chessman.Add(en_passant_rook_w_2);
                    board.chessman.Add(en_passant_bishop_w_1);
                    board.chessman.Add(en_passant_bishop_w_2);
                    board.chessman.Add(en_passant_knight_w_1);
                    board.chessman.Add(en_passant_knight_w_2);
                    board.chessman.Add(en_passant_queen_w);
                    board.chessman.Add(en_passant_king_w);

                    // SCHWARZE SPIELFIGUREN
                    board.chessman.Add(en_passant_pawn_b_1);
                    board.chessman.Add(en_passant_pawn_b_2);
                    board.chessman.Add(en_passant_pawn_b_3);
                    board.chessman.Add(en_passant_pawn_b_4);
                    board.chessman.Add(en_passant_pawn_b_5);
                    board.chessman.Add(en_passant_pawn_b_6);
                    board.chessman.Add(en_passant_pawn_b_7);
                    board.chessman.Add(en_passant_pawn_b_8);
                    board.chessman.Add(en_passant_rook_b_1);
                    board.chessman.Add(en_passant_rook_b_2);
                    board.chessman.Add(en_passant_bishop_b_1);
                    board.chessman.Add(en_passant_bishop_b_2);
                    board.chessman.Add(en_passant_knight_b_1);
                    board.chessman.Add(en_passant_knight_b_2);
                    board.chessman.Add(en_passant_queen_b);
                    board.chessman.Add(en_passant_king_b);

                    break;

            }
        }
    }
}