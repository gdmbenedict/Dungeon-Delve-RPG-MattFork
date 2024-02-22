﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    
        internal class GameManager
        {
            private Map map;
            private Player player;
            private Enemy goblin;
            private Enemy boss;
            private Enemy runner;
        public GameManager()
            {
                map = new Map("RPGMap.txt");
                player = new Player(10,10, 1, map.initialPlayerPositionX, map.initialPlayerPositionY);
                boss = new Enemy(5, 2, 8, 8, true);
                goblin = new Enemy(3, 1, map.initialEnemyPositionX, map.initialEnemyPositionY);
                runner = new Enemy(1, 2, map.initialEnemyPositionX, map.initialEnemyPositionY);
               
        }

            
         // Start up
        public void Start()
            {
                Console.WriteLine("Welcome to Dungeon Delve");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("\nYour goal is to collect seeds around a dungeon map while avoiding or defeating the enemies.");
                Console.WriteLine("\nThe world is known as The Underworld");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("You can attack by either running into the enemy but beware, they can hit you back");
                Console.WriteLine("It's dangerous to go alone... good luck!");
                Console.WriteLine("Press any key to start...");
                Console.ReadKey(true);
                Console.Clear();

            // game loop keeps on as long as the game isn't over or you haven't won   
            while (!player.gameOver)
            {
                map.DrawMap(player, goblin, boss, runner);
                DisplayHUD();
                DisplayLegend();
                PlayerInput();
                goblin.EnemyMovement(player.positionX, player.positionY, map.mapWidth, map.mapHeight, map.layout, player);
                boss.EnemyMovement(player.positionX, player.positionY, map.mapWidth, map.mapHeight, map.layout, player);
                runner.RunnerMovement(player.positionX, player.positionY, map.mapWidth, map.mapHeight, map.layout, player);

            }

            Console.Clear();
                
            // player wins
            if (player.youWin)
            {
                Console.WriteLine("You win!");
                Console.WriteLine($"\nYou collected: {player.currentSeeds} Seeds!");
                Console.WriteLine("Try to get more if you haven't got them all");    
                Console.ReadKey(true);
            }
            // players dead
            else
            {
                Console.WriteLine("You died...");
                Console.WriteLine("Try Again!");
                Console.ReadKey(true);
            }
        }
            // displays the HUD
        private void DisplayHUD()
        {
            Console.SetCursorPosition(0, map.mapHeight + 1);
            Console.WriteLine($"Player Health: {player.healthSystem.GetCurrentHealth()}/{player.healthSystem.GetMaximumHealth()} | Collected Seeds: {player.currentSeeds} | Goblin Health: {goblin.healthSystem.GetCurrentHealth()}/{goblin.healthSystem.GetMaximumHealth()}| Boss Health: {boss.healthSystem.GetCurrentHealth()}/{boss.healthSystem.GetMaximumHealth()}");
            List<string> liveLog = player.GetLiveLog();
            
            DisplayLiveLog(liveLog);
        }

        // displays the legend
        private void DisplayLegend()
        {
            Console.SetCursorPosition(0, map.mapHeight + 2);
            Console.WriteLine("Player = !" + " || Goblin = E" + " || Boss = B" + "\nWalls = #" + " || Floor = -" + "\nSeeds = &" + "\nSpikeTrap = ^ || Door: %" + "\nEnemySpawn = *" + " || BossSpawn = @" + "\nHealth Pack = + || Damage Boost = ?");
        }

        private void PlayerInput()
        {
            player.PlayerInput(map, goblin, boss, runner);
        }

        private void DisplayLiveLog(List<string> liveLog)
        {
            Console.SetCursorPosition(0, map.mapHeight + 9); 
            
            Console.WriteLine("Live Log:");

            int logLimit = Math.Min(3, liveLog.Count); // Limits log to 3 most recent messages
            for (int i = liveLog.Count - logLimit; i < liveLog.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(liveLog[i]);
            }
            Console.ResetColor();
        }
    }
    }


