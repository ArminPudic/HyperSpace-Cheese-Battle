# HyperSpace Cheese Battle

HyperSpace Cheese Battle is a board game for up to 4 players written in C# Console.

Each player has a "ship" represented by a counter on the board and start in the bottom left corner. The goal is to reach the top right corner of the board to win.

To move each player must roll a d6 and go in the direction of the arrow their coutner is currently on. If a player rolls a 6 then they get to roll again. If the player rolls 3 6's in a row then their "ship" blows up and returns to the bottom row of the board.

There are "cheese" powerups scattered accross the board that the players can pick up by landing on them. If a player picks up a "cheese" powerup then they have the choice to either roll again or shoot another player of their choice. If they choose to shoot another player then the player they chose will have to choose a particular spot on the lowest row to land on.

If at any point a player lands on a spot that already contains a player they will bounce of this play onto the next spot along based on the arrow direction of the tile they bounced off of. If a player goes off the board at any point then they are instantly returned to their original spot and ends their turn.

An Ascii art board representation is constantly updated in the console display as well as useful information such as player positions and roll counts as well as choices for the player.

This program was my first coursework of my first year and hence both the quality of code and techniques are quite poor but it was the start of, hopefully, something good.
