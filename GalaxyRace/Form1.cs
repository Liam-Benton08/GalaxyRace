using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


//Liam Benton
//May 21, 2024
//This is a two player racing game
namespace GalaxyRace
{
    public partial class SpaceRace : Form
    {

        Rectangle player1 = new Rectangle(200, 410, 20, 30);
        Rectangle player2 = new Rectangle(500, 410, 20, 30);

        int playerSpeed = 6;

        List<Rectangle> planets = new List<Rectangle>();
        List<int> planetSpeeds = new List<int>();
        List<int> planetSizes = new List<int>();
        List<string> planetColours = new List<string>();
        /*int[] planetSizes = {15, 30, 45, 60, 75, 90, 100 };*/

        int planetSpeed = 4;
        int planetSize = 30;

        bool upPressed = false;
        bool downPressed = false;
        bool wPressed = false;
        bool sPressed = false;

        SolidBrush orangeBrush = new SolidBrush(Color.Orange);
        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush YellowBrush = new SolidBrush(Color.Yellow);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush purpleBrush = new SolidBrush(Color.Purple);
        SolidBrush grayBrush = new SolidBrush(Color.Gray);

        Random randGen = new Random();
        int randValue = 0;

        int player1Score;
        int player2Score;

        int timer = 1000;

        public SpaceRace()
        {
            InitializeComponent();
            InitializeGame();
        }

        public void InitializeGame()
        {
            titleLabel.Text = "";
            subtitleLabel.Text = "";
            p1ScoreLabel.Text = "";
            p2ScoreLabel.Text = "";

            player1Score = 0;
            player2Score = 0;

            timer = 1000;
            player1.Y = 410;
            player2.Y = 410;

            for (int i = 0; i < planets.Count(); i++)
            {
                planets.RemoveAt(i);
            }


        }


        private void SpaceRace_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (gameTimer.Enabled == false)
                    {
                        Application.Exit();
                    }
                    break;
                case Keys.Space:
                    if (gameTimer.Enabled == false)
                    {
                        InitializeGame();
                        gameTimer.Enabled = true;
                    }
                    break;
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
            }
        }

        private void SpaceRace_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
            }
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //Move players
            if (wPressed && player1.Y >= 0)
            {
                player1.Y -= playerSpeed;
            }

            if (sPressed && player1.Y <= this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }

            if (upPressed && player2.Y >= 0)
            {
                player2.Y -= playerSpeed;
            }

            if (downPressed && player2.Y <= this.Height - player2.Height)
            {
                player2.Y += playerSpeed;
            }

            //Draw planets across screen
            for (int i = 0; i < planets.Count(); i++)
            {
                int x = planets[i].X + planetSpeeds[i];

                planets[i] = new Rectangle(x, planets[i].Y, planetSizes[i], planetSizes[i]);
            }

            //Add planets needed
            randValue = randGen.Next(0, 101);

            for (int i = 0; i < planets.Count(); i++)
            {
                if (randValue > 98)
                {

                    //randValue = randGen.Next(0, this.Height);
                    //int planetRandom = randGen.Next(0, 6);

                    Rectangle projectile = new Rectangle(0, randValue, 0, 0);


                    planets.Add(projectile);
                    planetSpeeds.Add(randGen.Next(5, 12));
                    planetSizes.Add(randGen.Next(60, 65));
                    planetColours.Add("Blue");


                }
                else if (randValue <= 98 && randValue > 95)
                {

                    //randValue = randGen.Next(0, this.Height);
                    //int planetRandom = randGen.Next(0, 6);

                    Rectangle projectile = new Rectangle(0, randValue, 0, 0);


                    planets.Add(projectile);
                    planetSpeeds.Add(randGen.Next(5, 12));
                    planetSizes.Add(randGen.Next(55, 60));
                    planetColours.Add("Red");

                }
                else if (randValue <= 95 && randValue > 92)
                {

                    //randValue = randGen.Next(0, this.Height);
                    //int planetRandom = randGen.Next(0, 6);

                    Rectangle projectile = new Rectangle(0, randValue, 0, 0);


                    planets.Add(projectile);
                    planetSpeeds.Add(randGen.Next(5, 12));
                    planetSizes.Add(randGen.Next(45, 50));
                    planetColours.Add("Yellow");

                }
                else if (randValue <= 92 && randValue > 89)
                {

                    //randValue = randGen.Next(0, this.Height);
                    //int planetRandom = randGen.Next(0, 6);

                    Rectangle projectile = new Rectangle(0, randValue, 0, 0);


                    planets.Add(projectile);
                    planetSpeeds.Add(randGen.Next(5, 12));
                    planetSizes.Add(randGen.Next(35, 40));
                    planetColours.Add("Purple");


                }
                else if (randValue <= 89 && randValue > 86)
                {

                    //randValue = randGen.Next(0, this.Height);
                    //int planetRandom = randGen.Next(0, 6);

                    Rectangle projectile = new Rectangle(0, randValue, 0, 0);


                    planets.Add(projectile);
                    planetSpeeds.Add(randGen.Next(5, 12));
                    planetSizes.Add(randGen.Next(25, 30));
                    planetColours.Add("Orange");


                }
                else if (randValue <= 86 && randValue > 83)
                {

                    //randValue = randGen.Next(0, this.Height);
                    //int planetRandom = randGen.Next(0, 6);

                    Rectangle projectile = new Rectangle(0, randValue, 0, 0);


                    planets.Add(projectile);
                    planetSpeeds.Add(randGen.Next(5, 12));
                    planetSizes.Add(randGen.Next(15, 20));
                    planetColours.Add("Gray");


                }
            }
            //Remove planets when off screen
            for (int i = 0; i < planets.Count(); i++)
            {
                if (planets[i].X > this.Width)
                {
                    planets.RemoveAt(i);
                    planetSpeeds.RemoveAt(i);

                }
            }

            //Check to see for collison
            for (int i = 0; i < planets.Count(); i++)
            {
                if (planets[i].IntersectsWith(player1))
                {

                }
                if (planets[i].IntersectsWith(player2))
                {

                }
            }


            //Check to see if player reaches the top
            if (player1.Y <= 0)
            {
                player1.Y = 410;
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";

            }
            if (player2.Y <= 0)
            {
                player2.Y = 410;
                player2Score++;
                p2ScoreLabel.Text = $"{player2Score}";
            }

            //Check to see if there is a winner
            if (player1Score == 3)
            {
                gameTimer.Enabled = false;
                titleLabel.Text = "P1 WINS";

            }
            else if (player2Score == 3)
            {
                gameTimer.Enabled = false;
                titleLabel.Text = "P2 WINS";
            }

            timer--;
            if (timer <= 0)
            {
                gameTimer.Enabled = false;
            }

            Refresh();
        }

        private void SpaceRace_Paint(object sender, PaintEventArgs e)
        {
            if (gameTimer.Enabled == false && timer == 1000)
            {
                titleLabel.Text = "SPACE RACE";
                subtitleLabel.Text = "PRESS SPACE TO START OR ESC TO EXIT";
            }
            else if (gameTimer.Enabled == true)
            {
                for (int i = 0; i < planets.Count(); i++)
                {
                    e.Graphics.FillEllipse(orangeBrush, planets[i]);
                }
                e.Graphics.FillRectangle(orangeBrush, player1);
                e.Graphics.FillRectangle(orangeBrush, player2);
            }
            else
            {
                subtitleLabel.Text = "PRESS SPACE TO START OR ESC TO EXIT";
            }

        }
    }
}
