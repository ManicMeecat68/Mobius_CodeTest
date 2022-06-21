using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobiusTest
{
    public partial class MainPage : ContentPage
    {
        int displayedCards = 0;
        int count;
        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7};
        int score = 0;
        BoxView lastBox;
        BoxView flippedBox;
        Random rnd = new Random();
        Color[] colors = new Color[8];

        public MainPage()
        {
           InitializeComponent();
            ColorGenerator();
            rButton.Clicked += RButton_Clicked;
            //Sets box color to black on startup
            foreach (BoxView box in myGrid.Children)
            {
                box.Color = Color.Black;
            }
            //Tap Inputs
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (sender, e) =>
            {

                foreach (BoxView box in myGrid.Children)
                {
                    //Checks which box is tapped and ensures it is not already activated or showing more than 2 boxes
                    if (sender.Equals(box) && box.Color == Color.Black && displayedCards < 2)
                    {
                        if (flippedBox!=null)
                        {
                            lastBox = flippedBox;
                        }
                        flippedBox = box;
                        displayedCards++;
                        DisplayCard();
                    }
                }
            };
            //Adds tap functionallity to each box view
            foreach (BoxView box in myGrid.Children)
            {
                box.GestureRecognizers.Add(tap);
            }
        }
        //Restarts game
        private void RButton_Clicked(object sender, EventArgs e)
        {
            foreach (BoxView box in myGrid.Children)
            {
                box.Color = Color.Black;
            }
            displayedCards = 0;
            ColorGenerator();
        }

        private void ColorGenerator()
        {
            //for loop thar creates 8 random RGB colours that will never be black or white
            for (int i = 0; i < 8; i++)
            {
                colors[i] = Color.FromRgb(rnd.Next(25, 225), rnd.Next(25, 225), rnd.Next(25, 225));
            }

            numbers = ShuffleArray(numbers);
            int[] ShuffleArray(int[] numbers)
            {   //randomizes the number array which saves the box colour
                Random r = new Random();
                for (int i = numbers.Length; i > 0; i--)
                {
                    int j = r.Next(i);
                    int k = numbers[j];
                    numbers[j] = numbers[i - 1];
                    numbers[i - 1] = k;
                }
                return numbers;
            }
            count = 0;

            foreach (BoxView box in myGrid.Children)
            {                
                box.BackgroundColor = colors[numbers[count]];
                count++;
            }

        }

        private void DisplayCard()
        {
            flippedBox.Color = flippedBox.BackgroundColor;
            //ensures 2 boxes are flipped before checking if they match
            if (lastBox != null) {
                //if they match add score
                if (flippedBox.Color == lastBox.Color)
                {
                    lastBox = null;
                    flippedBox = null;
                    displayedCards = 0;
                    score++;
                    //loads win page when score threshhold is reached
                    if (score >= 8)
                    {
                        App.Current.MainPage = new WinPage();                        
                    }
                }
                else if (flippedBox.Color != lastBox.Color)
                {
                    Incorrect();
                    
                }
            }
        }
        //Runs if incorrect combination is selected
        async void Incorrect()
        {
            //waits 1 seccond before executing
            await Task.Delay(1000);
            displayedCards = 0;
            lastBox.Color = Color.Black;
            flippedBox.Color = Color.Black;
            lastBox = null;
            flippedBox = null;
        }
    }
}
