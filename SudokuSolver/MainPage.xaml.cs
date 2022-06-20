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
                    var boxView = MyGrid.Children.Where(c => Grid.GetRow(c) == i && Grid.GetColumn(c) == j);

                    if ((boxView.Last() as CustomEntry).Text != null)
                    {
                        t[i, j] = int.Parse((boxView.Last() as CustomEntry).Text);
                        Unchangeable[i, j] = true;
                    }
                    else
                    {
                        Unchangeable[i, j] = false;
                        t[i, j] = 0;
                    }
                }
            }
            bool changed = true;
            bool ok = true;
            
            while(changed == true)
            {
                changed = false;
                for(int i = 0; i < 9; i++)
                {
                    for(int j = 0; j < 9; j++)
                    {
                        if (Unchangeable[i, j] == true)
                            continue;



                        for (int k = 1; k <= 9; k++)
                        {
                            t[i, j] = k;
                            if(CheckColumn(i, j) && CheckRow(i, j) && CheckSquare(i, j))
                            {
                                t[i, j] = 0;
                                ok = true;
                                for (int l = 0; l < 9; l++)
                                {
                                    if (Unchangeable[i, l] == true)
                                        continue;

                                    t[i, l] = k;

                                    if (l != j && CheckColumn(i, l) && CheckRow(i, l) && CheckSquare(i, l))
                                    {
                                        t[i, l] = 0;
                                        ok = false;
                                        break;
                                    }
                                    else
                                        t[i, l] = 0;
                                }

                                if(ok == true)
                                {
                                    t[i, j] = k;
                                    Unchangeable[i, j] = true;
                                    changed = true;
                                    break;
                                }

                                ok = true;


                                for (int l = 0; l < 9; l++)
                                {
                                    if (Unchangeable[l, j] == true)
                                        continue;

                                    t[l, j] = k;

                                    if (l != i && CheckColumn(l, j) && CheckRow(l, j) && CheckSquare(l, j))
                                    {
                                        t[l, j] = 0;
                                        ok = false;
                                        break;
                                    }
                                    else
                                        t[l, j] = 0;
                                }

                                if (ok == true)
                                {
                                    t[i, j] = k;
                                    Unchangeable[i, j] = true;
                                    changed = true;
                                    break;
                                }

                                ok = true;

                                for (int l = 3 * (i / 3); l < 3 * (i / 3 + 1); l++)
                                {
                                    for (int h = 3 * (j / 3); h < 3 * (j / 3 + 1); h++)
                                    {
                                        if (Unchangeable[l, h] == true)
                                            continue;

                                        t[l, h] = k;

                                        if ((l != i || h!=j) && CheckColumn(l, h) && CheckRow(l, h) && CheckSquare(l, h))
                                        {
                                            t[l, h] = 0;
                                            ok = false;
                                            break;
                                        }
                                        else
                                            t[l, h] = 0;
                                    }

                                    if (ok == false)
                                        break;
                                }

                                if (ok == true)
                                {
                                    t[i, j] = k;
                                    Unchangeable[i, j] = true;
                                    changed = true;
                                    break;
                                }
                            }
                            else
                                t[i, j] = 0;


                        }
                    }
                }
            }





            int x = 0;
            int y = 0;

            while (true)
            {
                if(Unchangeable[y, x] == false)
                {
                    t[y, x]++;

                    if (t[0, 0] == 10)
                        break;
  
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


            for (int i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    System.Console.WriteLine(t[i, j]);

                    var boxView = MyGrid.Children.Where(c => Grid.GetRow(c) == i && Grid.GetColumn(c) == j);

                    ((boxView.Last() as CustomEntry).Text) = t[i, j].ToString();
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

            return true;
        }

        void Reset(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {

                    var boxView = MyGrid.Children.Where(c => Grid.GetRow(c) == i && Grid.GetColumn(c) == j);

                    ((boxView.Last() as CustomEntry).Text) = null;
                    t[i, j] = 0;
                    Unchangeable[i, j] = false;
                }
            }
        }

    }
}
