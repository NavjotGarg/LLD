using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
/*
 * Problem Statement

Design and implement a Snake & Ladder game in Java. The game runs on the console. Focus on clean class design and SOLID principles — a working game with bad design scores less than a well-designed incomplete one.

Functional Requirements

	•	2 to 4 players with names
	•	Board is 10×10 (cells 1 to 100)
	•	Snakes and Ladders are configured at the start (positions hardcoded in main)
	•	Player rolls one 6-sided die per turn
	•	Landing on snake’s head → slide to tail; landing on ladder’s base → climb to top
	•	Overshoot rule: if roll takes player beyond 100, position stays unchanged
	•	First player to reach exactly 100 wins; game stops immediately
	•	Print each turn: player name, roll value, old position → new position, and if a snake/ladder was hit
*/
namespace SnakesAndLadders
{
	class Dice
	{
		public int Sides { get; set; }
        private Random rand = new Random();
		
		public Dice(int sides)
		{
			Sides = sides;
		}

		public int Roll()
		{
            return rand.Next(1, Sides + 1);
        }
    }

	class Player
	{
		public string Name { get; set; }
		public int Position { get; set; }

		public Player(string name)
		{
			Name = name;
			Position = 0;
		}
    }

	enum MovingEntityType
	{
		SNAKE = 0,
        LADDER = 1
    }

    class Board
	{
		public int Dimension { get; set; }
		public int BoardSize { get; set; }


		private Dictionary<int, KeyValuePair<int, MovingEntityType>> MovingEntities = new Dictionary<int, KeyValuePair<int, MovingEntityType>>();

		public Board(int dimension, Dictionary<int, int> snakes, Dictionary<int, int> ladders)
		{
			Dimension = dimension;
			BoardSize = dimension * dimension;
			foreach(var snake in snakes)
				MovingEntities.Add(snake.Key, new KeyValuePair<int, MovingEntityType>(snake.Value, MovingEntityType.SNAKE));
            foreach (var ladder in ladders)
                MovingEntities.Add(ladder.Key, new KeyValuePair<int, MovingEntityType>(ladder.Value, MovingEntityType.LADDER));
        }

		public bool IsHit(int position, out int newPosition, out string hitBy)
		{
			newPosition = position;
			hitBy = null;
			if (MovingEntities.ContainsKey(position)) {
                newPosition = MovingEntities[position].Key;
				hitBy = MovingEntities[position].Value == MovingEntityType.SNAKE ? "snake" : "ladder";
                return true;
            }
			return false;
		}
    }

	class GameEngine
	{
		public Board Board { get; set; }
		public List<Player> Players = new List<Player>();
		public Dice Dice { get; set; }
        public GameEngine(Board board, List<string> names, Dice dice)
		{
			Board = board;
            foreach (var name in names)
            {
                Players.Add(new Player(name));
            }
			Dice = dice;
        }

        public void TakeTurn(Player player, Board board, int rollValue)
        {
            Console.WriteLine("Player {0} rolled a {1}", player.Name, rollValue);

            // Check if the new position exceeds the board size
            if ((player.Position + rollValue) > board.BoardSize)
            {
                Console.WriteLine("Player {0} position unchanged from {1}", player.Name, player.Position);
                return;
            }

            player.Position += rollValue;

            // assuming the snake and ladders are not overlaping
            if (board.IsHit(player.Position, out int newPosition, out string hitBy))
            {
                player.Position = newPosition;
                Console.WriteLine("Player {0} hit a {1}! New position: {2}", player.Name, hitBy, player.Position);
            }
            else
            {
                Console.WriteLine("Player {0} moved to position {1}", player.Name, player.Position);
            }
        }

        public void gameLoop()
        {
            bool GameEnded = false;
            while (!GameEnded)
            {
                foreach (var player in Players)
                {
                    TakeTurn(player, Board, Dice.Roll());
                    if (player.Position == Board.BoardSize)
                    {
                        GameEnded = true;
                        Console.WriteLine("Player {0} wins!", player.Name);
                        break;
                    }
                }
            }
            Console.WriteLine("Game Over");
        }
    }

    internal class SnakesAndLaddersGame
    {
        static void Main(string[] args)
        {
			Board board = new Board(10, new Dictionary<int, int> { { 16, 6 }, { 47, 26 }, { 49, 11 }, { 56, 53 }, { 62, 19 }, { 64, 60 }, { 87, 24 }, { 93, 73 }, { 95, 75 }, { 98, 78 } },
				new Dictionary<int, int> { { 1, 38 }, { 4, 14 }, { 9, 31 }, { 21, 42 }, { 28, 84 }, { 36, 44 }, { 51, 67 }, { 71, 91 }, { 80, 100 } });
			GameEngine engine = new GameEngine(board, new List<string> { "Alice", "Bob", "Charlie", "David" }, new Dice(6));

			engine.gameLoop();
        }
    }
}
