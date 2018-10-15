using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication
{
    /// <summary>
    /// Логика взаимодействия для Chess.xaml
    /// </summary>
    /// 
    public partial class
        Chess : Window
    {
        #region//поля
        bool flag = false;
        bool whiteMove = true;
        bool whKingMoved = false;
        bool blKingMoved = false;
        bool whRookA1Moved = false;
        bool whRookH1Moved = false;
        bool blRookA8Moved = false;
        bool blRookH8Moved = false;
        bool check = false;
        bool checkmate = false;       

        string nameButton1 = "";
        string nameButton2 = "";
        string tag1 = "";
        string lastNameButton2 = "";
        string lastTag1 = "";
        int rowIndex = 0;
        int colIndex = 0;
        int indOfBut = 0;
        int countCheck;
       
        int qwPosibleNextMoves = 0;//переменная для подсчета кол-ва всех доступных ходов опонента
        public static List<string> avlbMouves = new List<string>();
        public static List<string> posFiguresForRescue = new List<string>();
        public static List<string> avlbFiguresForRescue = new List<string>();
        public static List<string> avlbMouvesForRescue = new List<string>();
        #endregion
        public Chess()
        {
            InitializeComponent();

            foreach (UIElement c in ChessBoard.Children)
            {
                if (c is Button)
                {
                    if ((c as Button).Tag == null)
                        (c as Button).Tag = "";
                    ((Button)c).Click += Button_Click;//добавляем кнопкам событие
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {           
            #region//если клик - первый(выбор фигуры для хода)
            if (!flag)//если клик - первый(выбор фигуры для хода)
            {
                nameButton1 = (string)((Button)e.OriginalSource).Name.ToString();//определяем имя кликнутой кнопки(ячейки)
                tag1 = (string)((Button)e.OriginalSource).Tag.ToString();//определяем тэг кликнутой кнопки(ячейки) для определения наличия фигуры в данной ячейке
                if (tag1 != "")//если фигура в кликнутой ячейке есть...
                {
                    if (whiteMove && tag1.Contains("Wh") || !whiteMove && tag1.Contains("Bl"))//определяем доступность фигур по цвету согласно хода
                    {                        
                        WhichRowCol(nameButton1, ref rowIndex, ref colIndex);//определение координат(Row и Column)выбранной для хода фигуры
                        avlbMouves.Clear();//очищаем список от предыдущих доступных ходов
                        AvailableMoves(tag1, nameButton1, ref avlbMouves);//определяем текущие доступные ходы
                        Illumination(0.35);//подсветка доступных для хода ячеек на доске
                        if (avlbMouves.Count > 0)
                            flag = true;//отмечаем что фигура для хода выбрана - первый клик сделан корректно
                    }
                }
            }
            #endregion
            #region//если клик - второй(выбор ячейки для перемещения выбранной фигуры)
            else
            {
                nameButton2 = (string)((Button)e.OriginalSource).Name.ToString();//определяем имя кликнутой кнопки(ячейки)
                if (nameButton1 != nameButton2)
                {
                    WhichRowCol(nameButton2, ref rowIndex, ref colIndex);//определение координат(Row и Column)выбранной для хода фигуры
                   


                    for (int i = 0; i < avlbMouves.Count; i++)//проходим по списку доступных ходов для выбранной фигуры
                    {
                        if (nameButton2 == avlbMouves[i])//если имя кликнутой кнопки есть в списке доступных ходов
                        {
                            //if(check)
                            //{
                            //    foreach (UIElement c in ChessBoard.Children)
                            //    {
                            //        if (c is Button)
                            //        {
                            //            if ((c as Button).Name == posFiguresForRescue[i])
                            //            {//очищение кнопки с которой убрана фигура
                            //                oldTag = (c as Button).Tag.ToString();
                            //                (c as Button).Tag = "";
                            //                // (c as Button).Content = "";
                            //            }
                            //            else if ((c as Button).Name == avlbMouvesForRescue[j])
                            //            {//заполнение кнопки на которую поставлена фигура
                            //                oldTag2 = (c as Button).Tag.ToString();
                            //                oldIndex = avlbMouvesForRescue[j];
                            //                (c as Button).Tag = avlbFiguresForRescue[i].ToString();
                            //                // (c as Button).Content = img;
                            //            }
                            //        }
                            //    }
                            //    #endregion
                            //    if (IsKingRescued(_tag, _nameButton))
                            //        check = false;
                            //    foreach (UIElement c in ChessBoard.Children)
                            //    {
                            //        if (c is Button)
                            //        {
                            //            if ((c as Button).Name == posFiguresForRescue[i])
                            //                (c as Button).Tag = oldTag;
                            //            else if ((c as Button).Name == oldIndex)
                            //                (c as Button).Tag = oldTag2;
                            //        }
                            //    }
                            //}
                           // if (check && IsKingRescued(lastTag1, lastNameButton2) == false)




                                string path = "Images/" + tag1 + ".png";
                            Image img = new Image();
                            img.Source = new BitmapImage(new Uri(@path, UriKind.Relative));//сохдание картинки текущей фигуры
                            //перемещение картинки фигуры
                            foreach (UIElement c in ChessBoard.Children)
                            {
                                if (c is Button)
                                {
                                    if ((c as Button).Name == nameButton1)
                                    {//очищение кнопки с которой убрана фигура
                                        (c as Button).Tag = "";
                                        (c as Button).Content = "";
                                    }
                                    else if ((c as Button).Name == nameButton2)
                                    {//заполнение кнопки на которую поставлена фигура
                                        (c as Button).Tag = tag1;
                                        (c as Button).Content = img;
                                    }
                                }
                            }
                           


                                flag = false; //сброс (обнуление) первого клика
                            Illumination(1); //сброс подсветки доступных ходов
                            NextMove(tag1, nameButton2);
                            if (nameButton2 != "")
                            {
                                Check(tag1, nameButton2, true);

                                if (checkmate)
                                {
                                    MessageBox.Show("и Мат!");
                                    LockChessBoard();
                                }
                            }
                            ////////////////смена хода по цвету фигур
                            if (whiteMove)//если ходили белые, то теперь ход черных
                            {
                                LB_Moves.Items.Add(" белые: " + nameButton1 + " - " + nameButton2);
                                whiteMove = false;
                                if (checkmate)
                                    TB_WhoIsMoving.Text = "Черные проиграли";
                                else
                                    TB_WhoIsMoving.Text = "Ходят черные";
                            }
                            else          //если ходили черные, то теперь ход белых
                            {
                                LB_Moves.Items.Add("черные: " + nameButton1 + " - " + nameButton2);
                                whiteMove = true;
                                if (checkmate)
                                    TB_WhoIsMoving.Text = "Белые проиграли";
                                else
                                    TB_WhoIsMoving.Text = "Ходят белые";
                            }
                            break;
                        }
                    }
                }

                else//отмена выбранной фигуры
                {
                    flag = false; //сброс (обнуление) первого клика                     
                    Illumination(1); //сброс подсветки доступных ходов
                }
            }
            #endregion
        }
        private void AvailableMoves(string _tag, string _nameButton, ref List<string> listMouves)
        {
            #region//определяем список доступных ходов для выбранной фигуры(по тэгу кнопки)
            switch (_tag)
            {
                case "BlPawn":
                    Pawn.AvlblMoves(rowIndex, colIndex, "Bl", ChessBoard, _nameButton, whiteMove, ref listMouves);
                    break;
                case "WhPawn":
                    Pawn.AvlblMoves(rowIndex, colIndex, "Wh", ChessBoard, _nameButton, whiteMove, ref listMouves);
                    break;
                case "BlRook":
                    Rook.AvlblMoves(rowIndex, colIndex, "Bl", ChessBoard, ref listMouves);
                    break;
                case "WhRook":
                    Rook.AvlblMoves(rowIndex, colIndex, "Wh", ChessBoard, ref listMouves);
                    break;
                case "BlHorse":
                    Horse.AvlblMoves(rowIndex, colIndex, "Bl", ChessBoard, ref listMouves);
                    break;
                case "WhHorse":
                    Horse.AvlblMoves(rowIndex, colIndex, "Wh", ChessBoard, ref listMouves);
                    break;
                case "BlBishop":
                    Bishop.AvlblMoves(rowIndex, colIndex, "Bl", ChessBoard, ref listMouves);
                    break;
                case "WhBishop":
                    Bishop.AvlblMoves(rowIndex, colIndex, "Wh", ChessBoard, ref listMouves);
                    break;
                case "BlQueen":
                    Queen.AvlblMoves(rowIndex, colIndex, "Bl", ChessBoard, ref listMouves);
                    break;
                case "WhQueen":
                    Queen.AvlblMoves(rowIndex, colIndex, "Wh", ChessBoard, ref listMouves);
                    break;
                case "BlKing":
                    King.AvlblMoves(rowIndex, colIndex, "Bl", ChessBoard, ref listMouves);
                    break;
                case "WhKing":
                    King.AvlblMoves(rowIndex, colIndex, "Wh", ChessBoard, ref listMouves);
                    break;
                default:
                    break;
            }
            #endregion
            // Illumination(0.35);//подсветка доступных для хода ячеек на доске
        }
        private void WhichRowCol(string _nameButton, ref int _rowIndex, ref int _colIndex)
        {
            for (int i = 0; i < ChessBoard.Children.Count; i++)//проходим по дочерним объектам Grid_а
            {
                if (ChessBoard.Children[i] is Button)
                {
                    if ((ChessBoard.Children[i] as Button).Name == _nameButton)
                    { indOfBut = i; break; } //определяем индекс нажатой кнопки в Grid_е для дальнейшего определения Row и Column нажатой кнопки
                }
            }
            _rowIndex = Grid.GetRow(ChessBoard.Children[indOfBut]);
            _colIndex = Grid.GetColumn(ChessBoard.Children[indOfBut]);
        }
        private void Illumination(double opacityIndex)//подсветка ячеек доступных для хода
        {
            for (int i = 0; i < avlbMouves.Count; i++)
            {
                foreach (UIElement c in ChessBoard.Children)
                {
                    if (c is Button)
                    {
                        if ((c as Button).Name == avlbMouves[i])
                        {
                            (c as Button).Opacity = opacityIndex;

                        }

                    }
                }
            }
        }
        private void Check(string _tag, string _nameButton, bool needMessage)//шах или нет?
        {           
            avlbMouves.Clear();//очищаем список от предыдущих доступных ходов
            AvailableMoves(_tag, _nameButton, ref avlbMouves);//определяем доступные клетки на следующий ход
            // Illumination(1);//сброс подсветки доступных ходов
            for (int i = 0; i < avlbMouves.Count; i++)
            {
                foreach (UIElement c in ChessBoard.Children)
                {
                    if (c is Button)
                    {
                        if ((c as Button).Name == avlbMouves[i].ToString())
                        {   //если в списке доступных клеток на сл.ход есть Король опонента - ему уже шах
                            // о чем будет выдано сообщение
                            if (whiteMove && (c as Button).Tag.ToString().Contains("BlKing"))
                            {
                                check = true;
                                if (needMessage)
                                    MessageBox.Show("Шах черному королю!");
                            }
                            else if (!whiteMove && (c as Button).Tag.ToString().Contains("WhKing"))
                            {
                                check = true;
                                if (needMessage)
                                    MessageBox.Show("Шах белому королю!");
                            }
                            else
                                //rescueKing = true;
                                check = false;
                        }
                    }
                }
            }
            if (check)
                CheckMate(_tag, _nameButton);
        }
        private bool IsKingRescued(string _tag, string _nameButton)//спасен ли король от шаха?
        {
            bool kingRescue = false;
            avlbMouves.Clear();//очищаем список от предыдущих доступных ходов
            AvailableMoves(_tag, _nameButton, ref avlbMouves);//определяем доступные клетки на следующий ход
            // Illumination(1);//сброс подсветки доступных ходов
            for (int i = 0; i < avlbMouves.Count; i++)
            {
                foreach (UIElement c in ChessBoard.Children)
                {
                    if (c is Button)
                    {
                        if ((c as Button).Name == avlbMouves[i].ToString())
                        {   //если в списке доступных клеток на сл.ход есть Король опонента - ему уже шах
                            // о чем будет выдано сообщение
                            if (whiteMove && (c as Button).Tag.ToString().Contains("BlKing"))
                            {
                                kingRescue = false;
                                //if (needMessage)
                                //    MessageBox.Show("Шах черному королю!");
                            }
                            else if (!whiteMove && (c as Button).Tag.ToString().Contains("WhKing"))
                            {
                                kingRescue = false;
                                //if (needMessage)
                                //    MessageBox.Show("Шах белому королю!");
                            }
                            else
                                kingRescue = true;
                        }
                    }
                }
            }
            return kingRescue;
        }
        private void NextMove(string _tag, string _nameButton)//условный следующий ход опонента
        {            
            posFiguresForRescue.Clear();//адреса всех ячеек где есть фигуры опонента
            avlbFiguresForRescue.Clear();//имена всех доступных фигур опонента
            foreach (UIElement c in ChessBoard.Children)
            {
                if (c is Button)
                {
                    if (whiteMove)
                    {
                        if ((c as Button).Tag.ToString().Contains("Bl"))
                        {
                            posFiguresForRescue.Add((c as Button).Name.ToString());
                            avlbFiguresForRescue.Add((c as Button).Tag.ToString());
                        }
                    }
                    else
                    {
                        if ((c as Button).Tag.ToString().Contains("Wh"))
                        {
                            posFiguresForRescue.Add((c as Button).Name.ToString());
                            avlbFiguresForRescue.Add((c as Button).Tag.ToString());
                        }
                    }
                }
            }
            avlbMouvesForRescue.Clear();//очищаем список от предыдущих доступных ходов
            for (int i = 0; i < avlbFiguresForRescue.Count; i++)
            {                
                AvailableMoves(avlbFiguresForRescue[i], posFiguresForRescue[i], ref avlbMouvesForRescue);//определяем доступные клетки на следующий ход
            }
            qwPosibleNextMoves = avlbMouvesForRescue.Count;
        }
        private void CheckMate(string _tag, string _nameButton)//мат или нет?/////некорректная работа метода
        {

            string oldTag = "";
            string oldTag2 = "";
            string oldIndex = "";

            for (int i = 0; i < avlbFiguresForRescue.Count; i++)
            {
                avlbMouvesForRescue.Clear();//очищаем список от предыдущих доступных ходов
                AvailableMoves(avlbFiguresForRescue[i], posFiguresForRescue[i], ref avlbMouvesForRescue);//определяем доступные клетки на следующий ход
               // Illumination(1);//сброс подсветки доступных ходов
                if (avlbMouvesForRescue.Count > 0)
                {
                    for (int j = 0; j < avlbMouvesForRescue.Count; j++)
                    {
                        countCheck++;
                        #region// foreach (UIElement c in ChessBoard.Children)
                        foreach (UIElement c in ChessBoard.Children)
                        {
                            if (c is Button)
                            {
                                if ((c as Button).Name == posFiguresForRescue[i])
                                {//очищение кнопки с которой убрана фигура
                                    oldTag = (c as Button).Tag.ToString();
                                    (c as Button).Tag = "";
                                    // (c as Button).Content = "";
                                }
                                else if ((c as Button).Name == avlbMouvesForRescue[j])
                                {//заполнение кнопки на которую поставлена фигура
                                    oldTag2 = (c as Button).Tag.ToString();
                                    oldIndex = avlbMouvesForRescue[j];
                                    (c as Button).Tag = avlbFiguresForRescue[i].ToString();
                                    // (c as Button).Content = img;
                                }
                            }
                        }
                        #endregion
                        if (IsKingRescued(_tag, _nameButton))
                            check = false;
                        foreach (UIElement c in ChessBoard.Children)
                        {
                            if (c is Button)
                            {
                                if ((c as Button).Name == posFiguresForRescue[i])
                                    (c as Button).Tag = oldTag;
                                else if ((c as Button).Name == oldIndex)
                                    (c as Button).Tag = oldTag2;
                            }
                        }
                        if (!check)
                            break;
                    }
                }
                if (!check)
                    break;
            }
            if (countCheck == qwPosibleNextMoves&& check)
                checkmate = true;
           
        }


        private void BTN_Сastling_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Castling");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Завершить текущую игру?", "", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                this.Close();
                Chess newGame = new Chess();
                newGame.Show();
            }
        }
        private void LockChessBoard()
        {
            foreach (UIElement c in ChessBoard.Children)
            {
                if (c is Button)              
                        (c as Button).IsEnabled = false;
            }
        }
    }

}
