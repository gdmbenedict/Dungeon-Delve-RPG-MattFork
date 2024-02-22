﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class Enemy
    {
        // variables | encapsulation

        public HealthSystem healthSystem;
        public int enemyDamage { get; set; }
        public int positionX { get; set; }
        public int positionY { get; set; }
        public bool enemyAlive { get; set; }

        public Enemy(int maxHealth, int damage, int startX, int startY)
        {
            healthSystem = new HealthSystem(maxHealth);
            enemyDamage = damage;
            positionX = startX;
            positionY = startY;
            enemyAlive = true;
        }

        public Enemy(int maxHealth, int damage, int startX, int startY, bool isAlive)
        {
            healthSystem = new HealthSystem(maxHealth);
            enemyDamage = damage;
            positionX = startX;
            positionY = startY;
            enemyAlive = isAlive;
        }

        
        

        public void EnemyMovement(int playerX, int playerY, int mapWidth, int mapHeight, char[,] mapLayout, Player player)
        {
            int enemyMovementX = positionX;
            int enemyMovementY = positionY;
            int newEnemyPositionX = positionX;
            int newEnemyPositionY = positionY;

            // random roll to move
            Random randomRoll = new Random();

            // checks if enemy is alive so it doesn't bug out when it is actually killed
            if (enemyAlive == true)
            {
                int rollResult = randomRoll.Next(1, 5);
                while ((enemyMovementX == playerX && enemyMovementY == playerY) ||
                       (enemyMovementX == newEnemyPositionX && enemyMovementY == newEnemyPositionY) ||
                       mapLayout[enemyMovementY, enemyMovementX] == '#')
                {

                    // retries the role
                    rollResult = randomRoll.Next(1, 5);

                    if (rollResult == 1)
                    {
                        enemyMovementY = positionY + 1;
                        if (enemyMovementY >= mapHeight)
                        {
                            enemyMovementY = mapHeight - 1;
                        }
                    }
                    else if (rollResult == 2)
                    {
                        enemyMovementY = positionY - 1;
                        if (enemyMovementY <= 0)
                        {
                            enemyMovementY = 0;
                        }
                    }
                    else if (rollResult == 3)
                    {
                        enemyMovementX = positionX - 1;
                        if (enemyMovementX <= 0)
                        {
                            enemyMovementX = 0;
                        }
                    }
                    else // rollResult == 4
                    {
                        enemyMovementX = positionX + 1;
                        if (enemyMovementX >= mapWidth)
                        {
                            enemyMovementX = mapWidth - 1;
                        }
                    }
                }

                
                if (enemyMovementX == playerX && enemyMovementY == playerY)
                {
                    player.healthSystem.Damage(enemyDamage);
                    player.UpdateLiveLog($"Enemy dealt {enemyDamage} damage to you!");
                    if (player.healthSystem.IsDead())
                    {
                        player.gameOver = true;
                    }
                }
            }


                // Updates the enemies position
                positionY = enemyMovementY;
            positionX = enemyMovementX;
        }


        // Movement AI for runner
        public void RunnerMovement(int playerX, int playerY, int mapWidth, int mapHeight, char[,] mapLayout, Player player)
        {
            int enemyMovementX = positionX;
            int enemyMovementY = positionY;

            
            int distanceX = Math.Abs(playerX - positionX);
            int distanceY = Math.Abs(playerY - positionY);

            if (distanceX <= 5 && distanceY <= 5)
            {
                
                if ((Math.Abs(positionX - playerX) == 1 && positionY == playerY) ||
                    (Math.Abs(positionY - playerY) == 1 && positionX == playerX))
                {
                    
                    player.healthSystem.Damage(enemyDamage);
                    player.UpdateLiveLog($"Runner dealt {enemyDamage} damage to you!");
                    if (player.healthSystem.IsDead())
                    {
                        player.gameOver = true;
                    }
                    return; 
                }

                
                if (playerX < positionX && mapLayout[positionY, positionX - 1] != '#' && positionX - 1 != playerX)
                {
                    enemyMovementX--;
                }
                else if (playerX > positionX && mapLayout[positionY, positionX + 1] != '#' && positionX + 1 != playerX)
                {
                    enemyMovementX++;
                }

                if (playerY < positionY && mapLayout[positionY - 1, positionX] != '#' && positionY - 1 != playerY)
                {
                    enemyMovementY--;
                }
                else if (playerY > positionY && mapLayout[positionY + 1, positionX] != '#' && positionY + 1 != playerY)
                {
                    enemyMovementY++;
                }
            }

            
            if (enemyAlive)
            {
                
                positionY = enemyMovementY;
                positionX = enemyMovementX;
            }
        }


        // draws the enemy
        public void DrawEnemy()
        {
        if (enemyAlive == true) 
        {
            Console.SetCursorPosition(positionX, positionY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("E");
            Console.ResetColor();
        }
        }

        public void DrawBoss()
        {
            if (enemyAlive == true)
            {
                Console.SetCursorPosition(positionX, positionY);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("\u00B1");
                Console.ResetColor();
            }
        }

        public void DrawRunner()
        {
            if (enemyAlive == true)
            {
                Console.SetCursorPosition(positionX, positionY);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("R");
                Console.ResetColor();
            }
        }
    }
}