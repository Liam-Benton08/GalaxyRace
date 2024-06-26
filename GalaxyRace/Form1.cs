﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


//Liam Benton
//May 21, 2024
//This is a two player racing game
namespace GalaxyRace
{
    public partial class SpaceRace : Form
    {
        SoundPlayer collision = new SoundPlayer(Properties.Resources.collisionSound);
        SoundPlayer clapping = new SoundPlayer(Properties.Resources.clappingSound);
        SoundPlayer yay = new SoundPlayer(Properties.Resources.yaySound);

        Rectangle player1 = new Rectangle(190, 420, 20, 30);
        Rectangle player2 = new Rectangle(590, 420, 20, 30);

        int playerSpeed = 6;

        List<Rectangle> planets = new List<Rectangle>();
        List<int> planetSpeeds = new List<int>();
        List<int> planetSizes = new List<int>();
        List<string> planetColours = new List<string>();

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
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        Pen whitePen = new Pen(Color.White, 8);
        

        Random randGen = new Random();
        int randValue = 0;

        int player1Score;
        int player2Score;

        int timer = 0;

        bool overtime = false;
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

            timer = 0;
            player1.Y = 410;
            player2.Y = 410;

            for (int i = 0; i < planets.Count(); i++)
            {
                planets.RemoveAt(i);
                planetSpeeds.RemoveAt(i);
                planetColours.RemoveAt(i);
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
                        clockTimer.Enabled = true;
                        clapping.Stop();
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
            movePlayers();

            movePlanets();

            addPlanets();

            collisons();

            removePlanets();

            checkForWinner();

            Refresh();
        }
        private void SpaceRace_Paint(object sender, PaintEventArgs e)
        {
            if (gameTimer.Enabled == false && player1Score == 0 && player2Score == 0)
            {
                titleLabel.Text = "SPACE RACE";
                subtitleLabel.Text = "PRESS SPACE TO START OR ESC TO EXIT";
            }
            else if (gameTimer.Enabled == true)
            {
                if (overtime == true)
                {
                    subtitleLabel.Text = "OVERTIME";
                }
                e.Graphics.DrawLine(whitePen, 400, 450, 400, timer);
                for (int i = 0; i < planets.Count(); i++)
                {
                    if (planetColours[i] == "Blue")
                    {
                        e.Graphics.FillEllipse(blueBrush, planets[i]);
                    }
                    else if (planetColours[i] == "Purple")
                    {
                        e.Graphics.FillEllipse(purpleBrush, planets[i]);
                    }
                    else if (planetColours[i] == "Orange")
                    {
                        e.Graphics.FillEllipse(orangeBrush, planets[i]);
                    }
                    else if (planetColours[i] == "Red")
                    {
                        e.Graphics.FillEllipse(redBrush, planets[i]);
                    }
                    else if (planetColours[i] == "Gray")
                    {
                        e.Graphics.FillEllipse(grayBrush, planets[i]);
                    }
                    else if (planetColours[i] == "Yellow")
                    {
                        e.Graphics.FillEllipse(YellowBrush, planets[i]);
                    }
                }
                e.Graphics.FillRectangle(whiteBrush, player1);
                e.Graphics.FillRectangle(whiteBrush, player2);

            }
            else
            {
                titleLabel.Text = "Game Over";
                subtitleLabel.Text += "\n\n\nPRESS SPACE TO START OR ESC TO EXIT";
                p1ScoreLabel.Text = "";
                p2ScoreLabel.Text = "";
            }

        }

        private void clockTimer_Tick(object sender, EventArgs e)
        {
            timer++;
            if (timer >= 450)
            {
                if (player1Score > player2Score)
                {
                    gameTimer.Enabled = false;
                    subtitleLabel.Text = "PLAYER 1 WINS";

                }
                else if (player2Score > player1Score)
                {
                    gameTimer.Enabled = false;
                    subtitleLabel.Text = "PLAYER 2 WINS";
                }
                else
                {
                    overtime = true;

                }
            }
            Refresh();
        }
        public void movePlayers()
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
        }
        public void movePlanets()
        {
            //Draw planets across screen
            for (int i = 0; i < planets.Count(); i++)
            {
                randValue = randGen.Next(1, 3);

                int x = planets[i].X + planetSpeeds[i];

                planets[i] = new Rectangle(x, planets[i].Y, planets[i].Width, planets[i].Height);
            }
        }
        public void addPlanets()
        {
            //Add planets needed
            randValue = randGen.Next(0, 101);


            if (randValue > 98)
            {

                int y = randGen.Next(0, 350);
                int size = randGen.Next(60, 65);

                Rectangle projectile = new Rectangle(0, y, size, size);

                planets.Add(projectile);
                planetSpeeds.Add(randGen.Next(5, 12));
                planetColours.Add("Blue");


            }
            else if (randValue <= 98 && randValue > 95)
            {

                int y = randGen.Next(0, 350);
                int size = randGen.Next(50, 55);

                Rectangle projectile = new Rectangle(800, y, size, size);


                planets.Add(projectile);
                planetSpeeds.Add(randGen.Next(-12, -5));
                planetColours.Add("Red");

            }
            else if (randValue <= 95 && randValue > 92)
            {

                int y = randGen.Next(0, 350);
                int size = randGen.Next(45, 50);

                Rectangle projectile = new Rectangle(0, y, size, size);


                planets.Add(projectile);
                planetSpeeds.Add(randGen.Next(5, 12));
                planetColours.Add("Yellow");

            }
            else if (randValue <= 92 && randValue > 89)
            {

                int y = randGen.Next(0, 350);
                int size = randGen.Next(35, 40);

                Rectangle projectile = new Rectangle(800, y, size, size);


                planets.Add(projectile);
                planetSpeeds.Add(randGen.Next(-12, -5));
                planetColours.Add("Purple");


            }
            else if (randValue <= 89 && randValue > 86)
            {

                int y = randGen.Next(0, 350);
                int size = randGen.Next(25, 30);

                Rectangle projectile = new Rectangle(0, y, size, size);


                planets.Add(projectile);
                planetSpeeds.Add(randGen.Next(5, 12));
                planetColours.Add("Orange");


            }
            else if (randValue <= 86 && randValue > 81)
            {

                int y = randGen.Next(0, 350);
                int size = randGen.Next(15, 20);

                Rectangle projectile = new Rectangle(800, y, size, size);


                planets.Add(projectile);
                planetSpeeds.Add(randGen.Next(-12, -5));
                planetColours.Add("Gray");
            }
        }
        public void removePlanets()
        {
            for (int i = 0; i < planets.Count(); i++)
            {
                if (planets[i].X > 900 || planets[i].X < -100)
                {
                    planets.RemoveAt(i);
                    planetColours.RemoveAt(i);
                    planetSpeeds.RemoveAt(i);
                }
            }
        }
        public void collisons()
        {
            //Check to see for collison
            for (int i = 0; i < planets.Count(); i++)
            {
                if (planets[i].IntersectsWith(player1))
                {
                    player1.Y = 420;
                    planets.RemoveAt(i);
                    planetSpeeds.RemoveAt(i);
                    planetColours.RemoveAt(i);

                    collision.Play();
                }
                if (planets[i].IntersectsWith(player2))
                {
                    player2.Y = 420;
                    planets.RemoveAt(i);
                    planetSpeeds.RemoveAt(i);
                    planetColours.RemoveAt(i);

                    collision.Play();
                }
            }
        }
        public void checkForWinner()
        {
            //Check to see if player reaches the top
            if (player1.Y <= 0)
            {
                player1.Y = 410;
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";
                yay.Play();

            }
            if (player2.Y <= 0)
            {
                player2.Y = 410;
                player2Score++;
                p2ScoreLabel.Text = $"{player2Score}";
                yay.Play();
            }

            //Check to see if there is a winner
            if (player1Score == 3)
            {
                yay.Stop();
                gameTimer.Enabled = false;
                subtitleLabel.Text = "PLAYER 1 WINS";
                clapping.Play();
            }
            else if (player2Score == 3)
            {
                yay.Stop();
                gameTimer.Enabled = false;
                subtitleLabel.Text = "PLAYER 2 WINS";
                clapping.Play();
            }
        }
    }
}
