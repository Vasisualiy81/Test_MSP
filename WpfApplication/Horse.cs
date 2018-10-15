using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication
{
    public class Horse
    {
        public static void AvlblMoves(int rowIndex, int colIndex, string color, Grid ChessBoard,ref List<string> listMouves)
        {
            int newRowIndex = 0;
            int newColIndex = 0;
            string nameAvlbButton = "";
            for (int i = 0; i < 8; i++)
            {
                newRowIndex = 0;
                newColIndex = 0;
                switch (i)
                {
                    case 0:
                        {
                            newColIndex = colIndex - 2;
                            newRowIndex = 9 - rowIndex - 1;
                        }
                        break;
                    case 1:
                        {
                            newColIndex = colIndex - 1;
                            newRowIndex = 9 - rowIndex - 2;
                        }
                        break;
                    case 2:
                        {
                            newColIndex = colIndex + 1;
                            newRowIndex = 9 - rowIndex - 2;
                        }
                        break;
                    case 3:
                        {
                            newColIndex = colIndex + 2;
                            newRowIndex = 9 - rowIndex - 1;
                        }
                        break;
                    case 4:
                        {
                            newColIndex = colIndex + 2;
                            newRowIndex = 9 - rowIndex + 1;
                        }
                        break;
                    case 5:
                        {
                            newColIndex = colIndex + 1;
                            newRowIndex = 9 - rowIndex + 2;
                        }
                        break;
                    case 6:
                        {
                            newColIndex = colIndex - 1;
                            newRowIndex = 9 - rowIndex + 2;
                        }
                        break;
                    case 7:
                        {
                            newColIndex = colIndex - 2;
                            newRowIndex = 9 - rowIndex + 1;
                        }
                        break;
                }
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
    }
}
