using System;

namespace Chess
{

    [Serializable()]
    class BoardHelper
    {
        private Chessboard board;
        
        public BoardHelper(Chessboard board)
        {
            this.board = board;
        }

        public void SetupBoard(string setup)
        {
            switch (setup)
            {
                case "new_game_setup":

                    // WEISSE SPIELFIGUREN
                    PawnWhite pawn_w_1 = new PawnWhite(true, "A2", board.game);
                    PawnWhite pawn_w_2 = new PawnWhite(true, "B2", board.game);
                    PawnWhite pawn_w_3 = new PawnWhite(true, "C2", board.game);
                    PawnWhite pawn_w_4 = new PawnWhite(true, "D2", board.game);
                    PawnWhite pawn_w_5 = new PawnWhite(true, "E2", board.game);
                    PawnWhite pawn_w_6 = new PawnWhite(true, "F2", board.game);
                    PawnWhite pawn_w_7 = new PawnWhite(true, "G2", board.game);
                    PawnWhite pawn_w_8 = new PawnWhite(true, "H2", board.game);
                    Rook rook_w_1 = new Rook(true, "A1", board.game);
                    Knight knight_w_1 = new Knight(true, "B1", board.game);
                    Bishop bishop_w_1 = new Bishop(true, "C1", board.game);
                    Queen queen_w = new Queen(true, "D1", board.game);
                    King king_w = new King(true, "E1", board.game);
                    Bishop bishop_w_2 = new Bishop(true, "F1", board.game);
                    Knight knight_w_2 = new Knight(true, "G1", board.game);
                    Rook rook_w_2 = new Rook(true, "H1", board.game);

                    // SCHWARZE SPIELFIGUREN
                    PawnBlack pawn_b_1 = new PawnBlack(false, "A7", board.game);
                    PawnBlack pawn_b_2 = new PawnBlack(false, "B7", board.game);
                    PawnBlack pawn_b_3 = new PawnBlack(false, "C7", board.game);
                    PawnBlack pawn_b_4 = new PawnBlack(false, "D7", board.game);
                    PawnBlack pawn_b_5 = new PawnBlack(false, "E7", board.game);
                    PawnBlack pawn_b_6 = new PawnBlack(false, "F7", board.game);
                    PawnBlack pawn_b_7 = new PawnBlack(false, "G7", board.game);
                    PawnBlack pawn_b_8 = new PawnBlack(false, "H7", board.game);
                    Rook rook_b_1 = new Rook(false, "A8", board.game);
                    Knight knight_b_1 = new Knight(false, "B8", board.game);
                    Bishop bishop_b_1 = new Bishop(false, "C8", board.game);
                    Queen queen_b = new Queen(false, "D8", board.game);
                    King king_b = new King(false, "E8", board.game);
                    Bishop bishop_b_2 = new Bishop(false, "F8", board.game);
                    Knight knight_b_2 = new Knight(false, "G8", board.game);
                    Rook rook_b_2 = new Rook(false, "H8", board.game);

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
                    PawnWhite promotion_pawn_w_1 = new PawnWhite(true, "A7", board.game);
                    PawnWhite promotion_pawn_w_2 = new PawnWhite(true, "B7", board.game);
                    King promotion_king_w = new King(true, "E1", board.game);
                    Bishop promotion_bishop_w_2 = new Bishop(true, "F1", board.game);
                    Knight promotion_knight_w_2 = new Knight(true, "G1", board.game);
                    Rook promotion_rook_w_2 = new Rook(true, "H1", board.game);

                    // SCHWARZE SPIELFIGUREN
                    PawnBlack promotion_pawn_b_1 = new PawnBlack(false, "A2", board.game);
                    PawnBlack promotion_pawn_b_2 = new PawnBlack(false, "B2", board.game);
                    King promotion_king_b = new King(false, "E8", board.game);
                    Bishop promotion_bishop_b_2 = new Bishop(false, "F8", board.game);
                    Knight promotion_knight_b_2 = new Knight(false, "G8", board.game);
                    Rook promotion_rook_b_2 = new Rook(false, "H8", board.game);

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
                    PawnWhite en_passant_pawn_w_1 = new PawnWhite(true, "A2", board.game);
                    PawnWhite en_passant_pawn_w_2 = new PawnWhite(true, "B2", board.game);
                    PawnWhite en_passant_pawn_w_3 = new PawnWhite(true, "C4", board.game);
                    PawnWhite en_passant_pawn_w_4 = new PawnWhite(true, "D2", board.game);
                    PawnWhite en_passant_pawn_w_5 = new PawnWhite(true, "E2", board.game);
                    PawnWhite en_passant_pawn_w_6 = new PawnWhite(true, "F2", board.game);
                    PawnWhite en_passant_pawn_w_7 = new PawnWhite(true, "G2", board.game);
                    PawnWhite en_passant_pawn_w_8 = new PawnWhite(true, "H2", board.game);
                    Rook en_passant_rook_w_1 = new Rook(true, "A1", board.game);
                    Knight en_passant_knight_w_1 = new Knight(true, "B1", board.game);
                    Bishop en_passant_bishop_w_1 = new Bishop(true, "C1", board.game);
                    Queen en_passant_queen_w = new Queen(true, "D1", board.game);
                    King en_passant_king_w = new King(true, "E1", board.game);
                    Bishop en_passant_bishop_w_2 = new Bishop(true, "F1", board.game);
                    Knight en_passant_knight_w_2 = new Knight(true, "G1", board.game);
                    Rook en_passant_rook_w_2 = new Rook(true, "H1", board.game);

                    // SCHWARZE SPIELFIGUREN
                    PawnBlack en_passant_pawn_b_1 = new PawnBlack(false, "A7", board.game);
                    PawnBlack en_passant_pawn_b_2 = new PawnBlack(false, "B7", board.game);
                    PawnBlack en_passant_pawn_b_3 = new PawnBlack(false, "C7", board.game);
                    PawnBlack en_passant_pawn_b_4 = new PawnBlack(false, "D7", board.game);
                    PawnBlack en_passant_pawn_b_5 = new PawnBlack(false, "E7", board.game);
                    PawnBlack en_passant_pawn_b_6 = new PawnBlack(false, "F5", board.game);
                    PawnBlack en_passant_pawn_b_7 = new PawnBlack(false, "G7", board.game);
                    PawnBlack en_passant_pawn_b_8 = new PawnBlack(false, "H7", board.game);
                    Rook en_passant_rook_b_1 = new Rook(false, "A8", board.game);
                    Knight en_passant_knight_b_1 = new Knight(false, "B8", board.game);
                    Bishop en_passant_bishop_b_1 = new Bishop(false, "C8", board.game);
                    Queen en_passant_queen_b = new Queen(false, "D8", board.game);
                    King en_passant_king_b = new King(false, "E8", board.game);
                    Bishop en_passant_bishop_b_2 = new Bishop(false, "F8", board.game);
                    Knight en_passant_knight_b_2 = new Knight(false, "G8", board.game);
                    Rook en_passant_rook_b_2 = new Rook(false, "H8", board.game);

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