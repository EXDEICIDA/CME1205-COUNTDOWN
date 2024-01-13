# CME1205-COUNTDOWN
 Written in C#,demonstrates fundamental C# skils etc.
Project: Countdown

The aim of the project is to develop a maze game, in which treasures/numbers are collected while walking in it.

General Information

The game is played in a 23×53 game field including outer walls. Game characters are P and numbers.

Human player is represented by P. Computer controls the numbers. The aim of the game is reaching the highest score.

Game Characters

P: Human player

- Cursor keys: To move the human player (4 directions)
- Human player can move in empty spaces, push number(s) or smash a number.
- At the beginning, human player has 5 lives.

Numbers: 9, 8, 7, 6, 5, 4, 3, 2, 1, 0

- These can be collected by the human player by smashing them.0 0 0

0

20 points.

1, 2, 3, 4

2 points.

5, 6, 7, 8, 9

: 1 point.

Only number 0 is alive and moves randomly in four directions. Other numbers are static.

Game Initialization

- Walls
- The inner walls in the game area are generated in random places.
- There are 3 types of inner walls. The number of walls in the game area;
- Long wall (length:11). It can be horizontal or vertical.
- Medium wall (length:7). It can be horizontal or vertical.

20 * Short wall (length:3). It can be horizontal or vertical.

- Wall placement Is random, but there must be at least 1 square (for 8 directions) among them.Walls cannot touch other walls.
- Human player P is located randomly.
- 70 numbers (0, 1, 2, 3, 4, 5, 6, 7, 8 or 9 with equal probability) are placed at random positions.

Game Playing Information

- Displayed time unit of the game is approx. 1 second.
- There can be only 1 game object (characters, items or walls) in a game square.
- Number 0: In 1 time unit (In 1 second); each "number 0" can move one square with random direction. If a "number 0" reaches the square of P, the player loses 1 life. If the player loses all lives,the game is over.
- Countdown action is done for every 15 seconds.
- Number 2,3,4,5,6,7,8,9: Their values are decreased by 1.
- Number 1: With the probabllity of 3%, number 1 ls converted into number 0. So, It becomes alive.
- Human Player: P is much faster than number 0 (approx. 20 times faster (50 ms for each move)).
- P can move around in empty spaces.
- Pushing: P can push non-increasing number sequences, if there is an empty square behind the last number.
- > P65543100
- > P544336(P can push this sequence to the right)(P cannot push this sequence to the right)
- > P84621(P cannot push this sequence to the right)
- Smashing: P can smash the last number when pushing non-Increasing number sequences, if there is a wall behind the last number. P cannot smash the number in the neighboring square.Non-pushable sequences are also non-smashable.
- > P6510#
- > P651#(P can smash 0 by pushing to the right)(P can smash 1 by pushing to the right)P65#(P can smash 5 by pushing to the right)P6# (p cannot smash 6, because it is neighbor to P)
- For each smashed number, a new random number (5, 6, 7, 8 or 9 with equal probability) is generated in a random place.
