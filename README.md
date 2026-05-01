-----------
TIC TAC TOE
-----------


----------------------
(2026 Update) So I just wanted to add my story behind this. I was literally trying to get a job at this wonderful software engineering firm called 8th Light. They had the most beautiful understandings of code and the friendliest employees and back before the pandemic as this happened in 2014 they would host code events teaching coding as a true craftsmanship all the time.

So I got them to at least try me out for their apprentice program. They asked me to make an unbeatable tic tac toe AI. I noticed most solutions were text based so I was going to make my solution using the XNA Game Frame Work (Direct X version for Xbox 360). Most people were calling an API that already has some sort of game logic AI in it (the old type of AI not that new LLM greatness) so I decided to completely just figure out my own version of an unbeatable Tic Tac Toe AI. I thought solving the problem would be fun. So i did. So I wrote it out and made it and it was 3149 lines of code hand written. We didn't have AI back then so it was just me making countless diagrams about tic tac toe and then manifesting that logic into AI for the couple of days it took to make this AI.

I made an unbeatable tic tac toe AI by hand. It literally is unbeatable.

But it still has a glaring weakness.

You see I made it so that the AI goes first and always picks the middle spot. And then from there it perfectly makes the next move no matter what you choose. The problem is that they wanted the AI to go FIRST or SECOND and the AI be unbeatable. They told me this in their response e-mail and I understood it was going to take another week of writing another 3000+ line of code solution and dreaming about TiC Tac Toe grids. In the end, even though it was for a job, I didn't follow up and make it so that the tic tac toe was unbeatable no matter if it went first or second.

So I didn't get the job.

But I was apprenticed later 5 years later in 2019 at Accenture where, after a successful apprenticeship, worked there for 6 years. It was my Accenture Adventure. Wouldn't trade it for the world.

You can look at the Tic Tac Toe Ai its the file TicTacToeAI.cs
(End 2026 Update)
----------------------------------------
Hello Everyone,

Jimmy Ellis

Immediate Updates On The Tic Tac Toe Unbeatable A.I. Program / Application / Game


TitleScreen
*ButtonClick and Enter Key now Able to Get to Tic Tac Toe Game


Tic Tac Toe Game 
*Tic Tac Toe Game now with clean press for no unwanted movements.
This was achived by moving around the mouseCollision and Mouse.GetState() properties. 

*Game Replay Added With 'R' Key ONLY AT END OF TIC TAC TOE GAME.
Game Escape with 'Esc' Key AT ANY TIME


Inner Workings 
*Game A.I. seperated in a different class for easier comprehension of what is the A.I. and what is not.
*TicTacToeGameScren renamed to TicTacToeGameScreen 




=======================================================================








TicTacToe
=========

Candidate Jimmy Ellis 
C#, XNA framework.
visualstudio 2012

EXPLINATION OF CODE WRITTEN BY JIMMY ELLIS 
_____________________________________________


You see my design is not a design that calculates every possible outcome and then simply puts them all there based on some polymorhpic overload of class methods already written for me. 


My design is based on the logic the player thinks and then coding every possible manifestation of that logic using multi dimensional array boolean varriables and preset locations for puttting art assets in XNA framework C# programming language.

player: x piece
computer: o piece
1. You must always be in the middle to win easily.
a. As such method TicTacToeMoves() in GameScreen.cs  solves that problem.
b. The computer always goes first and calls TicTacToeMoves.

2. Proper A.I. based on the following thought decisions turned into probability mathematics algorithims were created.
a1. Make a move that stops the player from winning (x). Method StopX_ToWinPieceMoves()
a2. Make a move that causes the computer to win (o).  Method  O_ToWinPieceMoves()
a3. Decide on a case by case bases to Stop player from winning or cause computer to win when both the player (x) has two Xs and the computer (o) has two Os.  Method O_DecisionAlgorithim()
a4. Decide wheather to stop player from winning (x) or help self (computer that is O piece) win.
Method OX_DecisionWinner();
a5. Miscellenious moves that are possible in relationship to location of the X (player) pieces
filled and the O (computer) pieces filled. Method O_PieceMoves();


An enumeration is used to say who's turn it is and each method is called in order from most
important to victory to least important to victory so .

OnceOnly: TicTacToeMoves()  (opening move for O computer piece)

1.    OX_DecisionWinner();
2.  O_DecisionAlgorithim();
3.  StopX_ToWinPieceMoves();
4.   O_ToWinPieceMoves();
5.  O_PieceMoves();




PART 1 END.





PART 2 ON EXPLINATION OF ARTIFICIAL INTELLIGENCE PROBABILITY MATHEMATICS WRITTEN BY JIMMY ELLIS.
________________________________________________________________________________________________

These 3 boolean 2 dimensional arrays and the 2 dimensional array boxLocations is the heart of the understanding of Tic tac toe
  bool[,] isLocationUsed; 
        bool[,] locationUsedByX;
        bool[,] locationUsedByO;
        Vector2[,] boxLocations;
        
        boxLocations is the X, Y axis of each of the places pieces can be put in 
        isLocationUsed tells if a square is used in tic tac toe no matter what.
        locationUsedByX tells if player X has used that location. everytime this is
        fired isLocationUsed is fired as well.
        
        locationUsedByO tells if the computer O has used a square or not.
        
        boxLocations multidimensional array holds the X and Y location in the following way.
        
        00 | 01 | 02
        ------------
        10 | 11 | 12
        ------------
        20 | 21 | 22
        
        as such boxLocations[0,0], isLocationUsed[0,0], locationUsedByX[0,0], locationUsedByO[0,0] all describe the same location in four different ways.
        
        here are how they are all used together
        
        
        the following algorithim cchecks to see if it is the computer turn. if it is it's goal is to find an empty spot next to the x piece that was just layed down.

will now use standard ' // ' to denote description


// if cheks to see if its 0s turn
 if (TurnPiece.oturn == turnpiece)
            {
              // if it is os turn it checks location 00 in the tic tac to grid to see if the player set a piece there
                if (locationUsedByX[0, 0] == true)
                {
                // if the player set a piece down then in square 01 place an x piece
                    if (isLocationUsed[0, 1] == false)
                    {
                        // rectangle is x, y width, height of a image. the image is an O.
                        // these two algorithims set the location of the picture to boxLocations
                        oRect[1].X = Convert.ToInt32(boxLocations[0, 1].X);
                        oRect[1].Y = Convert.ToInt32(boxLocations[0, 1].Y);
                        
                        // since the location has changed the picture is now in that area only to the human eye
                        // in code the piece is seen as used by setting the locationUsedByO to true as well is 
                        // isLocationUsed to true now the the program and the player looking at the game register
                        // the piece as used.
                        locationUsedByO[0, 1] = true;
                        isLocationUsed[0, 1] = true;
                        // the loop terminates and it is xturn which means the player now controls the x piece again
                        turnpiece = TurnPiece.xturn;

                    }

// WHAT IS HAPPENING IN ALL BELOW EQUATIONS IS THE SAME LOGIC FOR DIFFERENT PIECES AND SPACES
                    else if (isLocationUsed[1, 0] == false)
                    {
                        oRect[2].X = Convert.ToInt32(boxLocations[1, 0].X);
                        oRect[2].Y = Convert.ToInt32(boxLocations[1, 0].Y);
                        locationUsedByO[1, 0] = true;
                        isLocationUsed[1, 0] = true;
                        turnpiece = TurnPiece.xturn;

                    }
                    else if (isLocationUsed[1, 1] == false)
                    {
                        oRect[67].X = Convert.ToInt32(boxLocations[1, 1].X);
                        oRect[67].Y = Convert.ToInt32(boxLocations[1, 1].Y);
                        locationUsedByO[1, 1] = true;
                        isLocationUsed[1, 1] = true;
                        turnpiece = TurnPiece.xturn;

                    }



                }
            }

                  if (TurnPiece.oturn == turnpiece)
                 {
                if (locationUsedByX[0, 1] == true)
                {
                    if (isLocationUsed[0, 0] == false)
                    {
                        oRect[4].X = Convert.ToInt32(boxLocations[0, 0].X);
                        oRect[4].Y = Convert.ToInt32(boxLocations[0, 0].Y);
                        locationUsedByO[0, 0] = true;
                        isLocationUsed[0, 0] = true;
                        turnpiece = TurnPiece.xturn;
                    }

                    else if (isLocationUsed[0, 2] == false)
                    {
                        oRect[5].X = Convert.ToInt32(boxLocations[0, 2].X);
                        oRect[5].Y = Convert.ToInt32(boxLocations[0, 2].Y);
                        locationUsedByO[0, 2] = true;
                        isLocationUsed[0, 2] = true;
                        turnpiece = TurnPiece.xturn;
                    }

                    else if (isLocationUsed[1, 0] == false)
                    {
                        oRect[68].X = Convert.ToInt32(boxLocations[1,0].X);
                        oRect[68].Y = Convert.ToInt32(boxLocations[1,0].Y);
                        locationUsedByO[1, 0] = true;
                        isLocationUsed[1, 0] = true;
                        turnpiece = TurnPiece.xturn;
                    }

                    else if (isLocationUsed[1, 2] == false)
                    {
                        oRect[69].X = Convert.ToInt32(boxLocations[1, 2].X);
                        oRect[69].Y = Convert.ToInt32(boxLocations[1, 2].Y);
                        locationUsedByO[1, 2] = true;
                        isLocationUsed[1, 2] = true;
                        turnpiece = TurnPiece.xturn;
                    }
                    
                }

                  }
                  
                  

 PART 3 OF A.I. EXPLINATION
 _______________________________________
 
 
 
 
The way to discover what A.I. is is to discover what mathe is.


What is a number?

a number is how many.

What is a number?

a number is the object being counted.

What is a number?

A number is the future event calculated.


How is the number a future.

This tic tac toe application a number is a variable in computer science and the future location 
of the number decides the fate of the tic tac toe player.




Written By Jimmy Ellis.
