using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfApplication
{
    public class Pawn
    {
        public static void AvlblMoves(int rowIndex, int colIndex, string color, Grid ChessBoard, string _nameButton, bool _whiteMove, ref List<string> listMouves)
        {

            int newRowIndex = 0;
            int newColIndex = 0;
            string nameAvlbButton = "";

            newRowIndex = 0;
            newColIndex = 0;
            bool firstMove = false;//переменная для определения первого/последующего хода любой пешки(т.к есть разница в доступных ходах)

            //определения первый или нет ход пешки по её месторасполажению на доске
            if (_nameButton.Contains("2") && _whiteMove)
                firstMove = true;
            else if (_nameButton.Contains("7") && !_whiteMove)
                firstMove = true;
            else firstMove = false;

            if (firstMove)
            {
                for (int i = 1; i < 3; i++)
                {
                    newColIndex = colIndex;
                    if (color == "Wh")
                        newRowIndex = 9 - rowIndex + i;
                    else
                        newRowIndex = 9 - rowIndex - i;

                    nameAvlbButton = Letter.IntToLetter(newColIndex) + newRowIndex.ToString();
                    foreach (UIElement c in ChessBoard.Children)
                    {
                        if (c is Button)
                        {
                            if ((c as Button).Name == nameAvlbButton)
                            {
                                if ((c as Button).Tag.ToString() == "" ||
                                    !(c as Button).Tag.ToString().Contains(color))
                                {
                                    listMouves.Add((c as Button).Name.ToString());
                                }
                            }
                        }
                    }
                }
            }
          
            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            newColIndex = colIndex;
                            if (color == "Wh")
                                newRowIndex = 9 - rowIndex + 1;
                            else
                                newRowIndex = 9 - rowIndex - 1;
                        }
                        break;
                    case 1:
                        {
                            newColIndex = colIndex - 1;
                            if (color == "Wh")
                                newRowIndex = 9 - rowIndex + 1;
                            else
                                newRowIndex = 9 - rowIndex - 1;
                        }
                        break;
                    case 2:
                        {
                            newColIndex = colIndex + 1;
                            if (color == "Wh")
                                newRowIndex = 9 - rowIndex + 1;
                            else
                                newRowIndex = 9 - rowIndex - 1;
                        }
                        break;
                }
                nameAvlbButton = Letter.IntToLetter(newColIndex) + newRowIndex.ToString();
                foreach (UIElement c in ChessBoard.Children)
                {
                    if (c is Button)
                    {
                        if (i == 0 && (c as Button).Name == nameAvlbButton)
                        {
                            if ((c as Button).Tag.ToString() == "")
                            {
                                listMouves.Add((c as Button).Name.ToString());
                                break;
                            }
                        }
                        else if (i == 1 && (c as Button).Name == nameAvlbButton)
                        {
                            if ((c as Button).Tag.ToString() != "" &&
                                !(c as Button).Tag.ToString().Contains(color))
                            {
                                listMouves.Add((c as Button).Name.ToString());
                                break;
                            }
                        }
                        else if (i == 2 && (c as Button).Name == nameAvlbButton)
                        {
                            if ((c as Button).Tag.ToString() != "" &&
                                !(c as Button).Tag.ToString().Contains(color))
                            {
                                listMouves.Add((c as Button).Name.ToString());
                                break;
                            }
                        }

                    }
                }
            }
        }
    }
}
