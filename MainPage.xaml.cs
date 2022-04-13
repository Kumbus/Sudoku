using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SudokuSolver
{
    public partial class MainPage : ContentPage
    {
        int?[,] t = new int?[9, 9];
        bool [,] Unchangeable = new bool[9, 9];
        public MainPage()
        {
            InitializeComponent();


        }

        void Solve(object sender, EventArgs e)
        {

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    //Entry entry = (Entry)MyGrid.GetChildElements(new Point(i, j));
                    // t[i, j] = int.Parse(entry.Text);
                    
                    
                    var boxView = MyGrid.Children.Where(c => Grid.GetRow(c) == i && Grid.GetColumn(c) == j);

                    //System.Console.WriteLine((boxView.Last() as CustomEntry).Text);
                    //System.Console.WriteLine("Hej1");
                    if ((boxView.Last() as CustomEntry).Text != null)
                    {
                        t[i, j] = int.Parse((boxView.Last() as CustomEntry).Text);
                        Unchangeable[i, j] = true;
                        //System.Console.WriteLine("Hej");
                        //System.Console.WriteLine(t[i,j]);

                    }
                    else
                    {
                        Unchangeable[i, j] = false;
                        t[i, j] = 0;
                    }
                }
            }
            int x = 0;
            int y = 0;

            while (true)
            {
                System.Console.WriteLine("xD");
                if(Unchangeable[y, x] == false)
                {
                    t[y, x]++;

                    if (t[0, 0] == 10)
                        break;
                    //System.Console.WriteLine(t[y,x]);   
                    if (t[y, x] < 10 && CheckRow(y, x) && CheckColumn(y, x) && CheckSquare(y, x))
                    {
                        if (y == 8 && x == 8)
                            break;

                        x++;
                        if (x == 9)
                        {
                            y++;
                            x = 0;
                        }
                    }
                    else if (t[y, x] == 10) 
                    {
                        t[y, x] = 0;
                        x--;
                        if (x == -1)
                        {
                            y--;
                            x = 8;
                        }
                    }
                }
                else
                {
                    x++;
                    if (x == 9)
                    {
                        if (y == 8)
                            break;
                        y++;
                        x = 0;
                    }
                }
            }


            if(t[0, 0] == 10)
            {
                //return;
            }//zło

            for (int i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    System.Console.WriteLine(t[i, j]);

                    var boxView = MyGrid.Children.Where(c => Grid.GetRow(c) == i && Grid.GetColumn(c) == j);

                    ((boxView.Last() as CustomEntry).Text) = t[i, j].ToString();
                    //((Entry)MyGrid.GetChildElements(new Point(i, j))).Text = t[i, j].ToString();

                    //if (Unchangeable[i, j] == false)
                     //  ((Entry)MyGrid.GetChildElements(new Point(i, j))).FontAttributes = FontAttributes.None;
                }
            }



        }

        bool CheckRow(int y, int x)
        {
            for (int i = 0; i < 9; i++) 
            {
                if (t[y, i] == t[y, x] && i != x) 
                    return false;
            }
            return true;
        }

        bool CheckColumn(int y, int x)
        {
            for (int i = 0; i < 9; i++)
            {
                if (t[i, x] == t[y, x] && i != y)
                    return false;
            }
            return true;
        }

        bool CheckSquare(int y, int x)
        {
            for (int i = 3 * (y / 3); i < 3 * (y / 3 + 1); i++)
                for (int j = 3 * (x / 3); j < 3 * (x / 3 + 1); j++) 
                    if (t[i, j] == t[y, x] && i != y && j != x)
                        return false;



            /*if (y < 3 && x < 3) 
            {
                for (int i = 0; i < 3; i++) 
                    for (int j = 0; j < 3; j++) 
                        if (t[i, j] == t[y, x] && i != x && j != y)
                            return false;
            }
            else if(y < 3 && x < 6)
            {
                for (int i = 0; i < 3; i++)
                    for (int j = 3; j < 6; j++)
                        if (t[i, j] == t[y, x] && i != x && j != y)
                            return false;
            }
            else if(y < 3 && x < 9)
            {
                for (int i = 0; i < 3; i++)
                    for (int j = 6; j < 9; j++)
                        if (t[i, j] == t[y, x] && i != x && j != y)
                            return false;
            }
            else if(y < 6 && x < 3)
            {
                for (int i = 3; i < 6; i++)
                    for (int j = 0; j < 3; j++)
                        if (t[i, j] == t[y, x] && i != x && j != y)
                            return false;
            }
            else if (y < 6 && x < 6)
            {
                for (int i = 3; i < 6; i++)
                    for (int j = 3; j < 6; j++)
                        if (t[i, j] == t[y, x] && i != x && j != y)
                            return false;
            }
            else if (y < 6 && x < 9)
            {
                for (int i = 3; i < 6; i++)
                    for (int j = 6; j < 9; j++)
                        if (t[i, j] == t[y, x] && i != x && j != y)
                            return false;
            }
            else if (y < 9 && x < 3)
            {
                for (int i = 6; i < 9; i++)
                    for (int j = 0; j < 3; j++)
                        if (t[i, j] == t[y, x] && i != x && j != y)
                            return false;
            }
            else if (y < 9 && x < 6)
            {
                for (int i = 6; i < 9; i++)
                    for (int j = 3; j < 6; j++)
                        if (t[i, j] == t[y, x] && i != x && j != y)
                            return false;
            }
            else if (y < 9 && x < 9)
            {
                for (int i = 6; i < 9; i++)
                    for (int j = 6; j < 9; j++)
                        if (t[i, j] == t[y, x] && i != x && j != y)
                            return false;
            }*/

            return true;
        }

    }
}
