using Android.Graphics.Drawables;
using SudokuSolver;
using SudokuSolver.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace SudokuSolver.Droid
{
    [System.Obsolete]
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();

                gd.SetColor(Android.Graphics.Color.Transparent);
                this.Control.SetBackground(gd);
                this.Control.SetPadding(0, 0, 0, 0);

            }
        }


    }
}