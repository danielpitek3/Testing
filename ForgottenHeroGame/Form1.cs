using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForgottenHeroGame
{
    public partial class Form1 : Form
    {
        // variables
        bool jumping = false;
        int jumpSpeed;
        int force = 12;
        int score = 0;
        int obstacleSpeed = 10;
        Random rand = new Random();
        int position;
        bool isGameOver = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            fhero.Top += jumpSpeed;

            scoreText.Text = "Score: " + score;

            if (jumping == true && force < 0) // jump
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }


            if (fhero.Top > 366 && jumping == false)
            {
                force = 12;
                fhero.Top = 367;
                jumpSpeed = 0;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed;

                    if (x.Left < -100)
                    {
                        x.Left = this.ClientSize.Width + rand.Next(200, 500) + (x.Width * 15);
                        score++;
                    }

                    if (fhero.Bounds.IntersectsWith(x.Bounds))
                    {
                        gameTimer.Stop();
                        fhero.Image = Properties.Resources.dead;

                        // GAME END SIGNS //
                        if (score < 5)
                        {
                            gameEnd.Text = "- GAME OVER -";
                            scoreText.Text = "Score : " + score;
                            gameOver.Text = "Level: Noob - Press R";
                            isGameOver = true;
                        }
                        else if (score < 10)
                        {
                            gameEnd.Text = "- GAME OVER -";
                            scoreText.Text = "Score : " + score;
                            gameOver.Text = "Level: Normal - Press R";
                            isGameOver = true;
                        }
                        else if (score < 20)
                        {
                            gameEnd.Text = "- GAME OVER -";
                            scoreText.Text = "Score : " + score;
                            gameOver.Text = "Level: Good - Press R";
                            isGameOver = true;
                        }
                        else if (score < 30)
                        {
                            gameEnd.Text = "- GAME OVER -";
                            scoreText.Text = "Score : " + score;
                            gameOver.Text = "Level: Pro - Press R";
                            isGameOver = true;
                        }
                        else if (score > 30)
                        {
                            gameEnd.Text = "- GAME OVER -";
                            scoreText.Text = "Score : " + score;
                            gameOver.Text = "Level: Insane - Press R";
                            isGameOver = true;
                        }
                    }
                }
            }

            if (score > 5) // SPEED
            {
                obstacleSpeed = 15;
            }
            else if (score > 10)
            {
                obstacleSpeed = 20;
            }
            else if (score > 20)
            {
                obstacleSpeed = 25;
            }
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && jumping == false) // method for jumping down
            {
                jumping = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (jumping == true) // method for jumping up
            {
                jumping = false;
            }

            if (e.KeyCode == Keys.R && isGameOver == true)
            {
                GameReset();
            }
        }
        private void GameReset()
        {
            // Game reset //
            force = 12;
            jumpSpeed = 0;
            jumping = false;
            score = 0;
            obstacleSpeed = 10;
            scoreText.Text = "Score: " + score;
            gameEnd.Text = " ";
            gameOver.Text = " ";
            fhero.Image = Properties.Resources.running; // texture change from dead to running //
            isGameOver = false;
            fhero.Top = 367;

            foreach (Control x in this.Controls)
            {

                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    position = this.ClientSize.Width + rand.Next(500, 800) + (x.Width * 10);

                    x.Left = position;
                }
            }

            gameTimer.Start(); // start of the game //

        }

    }
}

