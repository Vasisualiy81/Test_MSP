using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication
{
    public class Rook
    {
        public static void AvlblMoves(int rowIndex, int colIndex, string color, Grid ChessBoard,ref List<string> listMouves)
        {
            int newRowIndex = 0;
            int newColIndex = 0;
            bool flag = false;
            string nameAvlbButton = "";

            for (int j = 0; j < 4; j++)
            {
                flag = false;
                for (int i = 1; i < 8; i++)
                {
                    if (!flag)
                    {
                        newRowIndex = 0;
                        newColIndex = 0;
                        switch (j)
                        {
                            case 0:
                                {
                                    newColIndex = colIndex + i;
                                    newRowIndex = 9 - rowIndex;
                                }
                                break;
                            case 1:
                                {
                                    newColIndex = colIndex - i;
                                    newRowIndex = 9 - rowIndex;
                                }
                                break;
                            case 2:
                                {
                                    newColIndex = colIndex;
                                    newRowIndex = 9 - rowIndex + i;
                                }
                                break;
                            case 3:
                                {
                                    newColIndex = colIndex;
                                    newRowIndex = 9 - rowIndex - i;
                                }
                                break;
                        }

                        nameAvlbButton = Letter.IntToLetter(newColIndex) + newRowIndex.ToString();

                        foreach (UIElement c in ChessBoard.Children)
                        {
                            if (!flag)
                            {
                                if (c is Button)
                                {
                                    if ((c as Button).Name == nameAvlbButton)
                                    {
                                        if ((c as Button).Tag.ToString() == "")
                                        {
                                            listMouves.Add((c as Button).Name.ToString());
                                        }
                                        else
                                        {
                                            if (!(c as Button).Tag.ToString().Contains(color))
                                            {
                                                listMouves.Add((c as Button).Name.ToString());
                                            }
                                            flag = true;
                                        }
                                    }
                                }
                            }
                            else break;
                        }
                    }
                    else break;
                }
            }
        }
    }
}
