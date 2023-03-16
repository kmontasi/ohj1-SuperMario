# Super Mario Clone

This is a clone of the classic Super Mario game created using C# and Jypeli game engine.

## Installation

To install and run the game, follow these steps:

Clone or download the repository from Github
Open the project solution file SuperMario.sln in Visual Studio
Build the project using the Build menu or by pressing F6 key
Run the game by pressing F5 key
Usage
The game follows the same gameplay mechanics as the original Super Mario game. Use the arrow keys to move Mario and the space key to jump. The objective of the game is to reach the end of each level by avoiding obstacles and defeating enemies. Collect coins to increase your score and power-ups to gain special abilities.

## Code Structure
The game consists of multiple C# source code files, which are structured as follows:

### Game.cs
This file contains the main entry point of the game and sets up the initial game objects and resources. The Game class inherits from the Jypeli.Game class and defines the game loop, input handling, and other game-related functions.

### Level.cs
This file contains the definition of the Level class, which represents a single level in the game. Each level is composed of a 2D array of tiles, which are either solid or non-solid. The Level class also defines functions for loading and rendering the level, as well as detecting collisions with the level geometry.

### Mario.cs
This file contains the definition of the Mario class, which represents the player-controlled character. The Mario class inherits from the Jypeli.PhysicsObject class and defines the movement and behavior of Mario, including collision detection with enemies, power-ups, and other game objects.

### Enemy.cs
This file contains the definition of the Enemy class, which represents the various enemies that Mario must defeat to progress through the game. The Enemy class inherits from the Jypeli.PhysicsObject class and defines the movement and behavior of each enemy type.

### Coin.cs
This file contains the definition of the Coin class, which represents the collectible coins that increase the player's score. The Coin class inherits from the Jypeli.PhysicsObject class and defines the behavior of the coins, including collision detection with Mario and removal from the game world.

### PowerUp.cs
This file contains the definition of the PowerUp class, which represents the various power-ups that give Mario special abilities, such as invincibility or increased jumping ability. The PowerUp class inherits from the Jypeli.PhysicsObject class and defines the behavior of each power-up type.

## Credits
This game was created as a student project for the "Programming 1" course at the University of Helsinki by the following contributors:

Khondker Montasirzzaman (@kmontasi)

## License
This game is released under the MIT License. See the LICENSE file for details.
