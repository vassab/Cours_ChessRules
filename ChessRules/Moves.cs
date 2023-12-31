﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessRules
{
    class Moves
    {
        FigureMoving fm;
        Board board;

        public Moves(Board board)
        {
            this.board = board;
        }

        public bool CanMove(FigureMoving fm)
        {
            this.fm = fm;
            return CanMoveFrom() &&
                   CanMoveTo() &&
                   CanFigureMove();
        }

        private bool CanMoveFrom() {

            return fm.from.OnBoard() &&
                   board.GetFigureAt(fm.from) != Figure.none &&
                   board.GetFigureAt(fm.from).GetColor() == board.moveColor; ;
        }

        private bool CanMoveTo()
        {

            //Test on step yourself
            if (fm.from.x == fm.to.x) {
                if (fm.from.y == fm.to.y) {
                    return false;
                }
            }

            return fm.to.OnBoard() &&
                   CheckCellMove() && //We our write
                   board.GetFigureAt(fm.to).GetColor() != board.moveColor;

        }

        private bool CheckCellMove() {
            if (board.GetFigureAt(fm.to) == Figure.none) {
                return true;
            }

            //TODO: write if king under attack
            //         if () {
            //           return false;
            //       }

            return true;
        }

        private bool CanFigureMove()
        {
            switch (fm.figure)
            {
                case Figure.none:
                case Figure.whiteKing:
                case Figure.blackKing:
                    return CanKingMove();
                case Figure.whiteQueen:
                case Figure.blackQueen:
                    return CanStraightMove();
                case Figure.whiteRook:
                case Figure.blackRook:
                    return (fm.SignX == 0 || fm.SignY == 0) &&
                        CanStraightMove();
                case Figure.whiteBishop:
                case Figure.blackBishop:
                    return (fm.SignX != 0 && fm.SignY != 0) &&
                        CanStraightMove(); ;
                case Figure.whiteKnight:
                case Figure.blackKnight:
                    return CanKnightMove();
                case Figure.whitePawn:
                case Figure.blackPawn:
                    return CanPawnMove();
                default:
                    return false;
            }
        }

        private bool CanKingMove()
        {
            return fm.AbsDeltaX <= 1 && fm.AbsDeltaY <= 1;
        }

        private bool CanKnightMove()
        {
            return (fm.AbsDeltaX == 1 && fm.AbsDeltaY == 2) ||
                   (fm.AbsDeltaX == 2 && fm.AbsDeltaY == 1);
        }
        private bool CanStraightMove()
        {
            Square at = fm.from;
            do
            {
                at = new Square(at.x + fm.SignX, at.y + fm.SignY);
                if (at == fm.to)
                    return true;
            } while (at.OnBoard() &&
                     board.GetFigureAt(at) == Figure.none);
            return false;

        }
        bool CanPawnMove()
        {
            if (fm.from.y < 1 || fm.from.y > 6)
                return false;
            int stepY = fm.figure.GetColor() == Color.White ? 1 : -1;
            return
                CanPawnGo(stepY) ||
                CanPawnJump(stepY) ||
                CanPawnEat(stepY);

        }

        private bool CanPawnEat(int stepY)
        {
            if (board.GetFigureAt(fm.to) != Figure.none)
                if (fm.AbsDeltaX == 1 )
                    if (fm.DeltaY == stepY) 
                        return true;
            return false; 
        }

        private bool CanPawnJump(int stepY)
        {
            if (board.GetFigureAt(fm.to) == Figure.none)
                if (fm.DeltaX == 0)
                    if (fm.DeltaY == 2 * stepY)
                        if (fm.from.y == 1 || fm.from.y == 6)
                            if (board.GetFigureAt(new Square (fm.from.x, fm.from.y + stepY))== Figure.none)
                                return true;
            return false;
        }

        private bool CanPawnGo(int stepY)
        {
            if (board.GetFigureAt(fm.to)== Figure.none)
                if (fm.DeltaX == 0)
                    if (fm.DeltaY == stepY)
                        return true;
            return false;
        }
    }

}

