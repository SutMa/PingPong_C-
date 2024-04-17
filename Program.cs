Console.CursorVisible = false;
int playerpos = 0;
int player2pos = 0;
int prevpos = 1;

GameLoop(playerpos, player2pos, prevpos);

static void GameLoop(int playerpos, int player2pos, int prevpos)
{
    Console.Write("When youre ready press any key!");
    Console.ReadKey(); 
    ConsoleKeyInfo cki = new ConsoleKeyInfo();
    string gamestart = "";
    while (gamestart != "N")
    {
        int score = 0;
        int lenght = 50 * 2;
        int height = 20 * 2;
        playerpos = height / 2;
        player2pos = height / 2;
        int[] ballpos = new int[] { lenght, height / 2, 1, 1 };
        Board(height, lenght);
        int who = 0;
        bool game = true;
        while (game != false)
        {
            do
            {
                while (Console.KeyAvailable == false)
                {
                    Thread.Sleep(20);
                    score = Score(height, lenght, score, playerpos, ballpos);
                    Player(playerpos, height, lenght, player2pos, who);
                    ballpos = Ball(lenght, height, ballpos, playerpos, player2pos);
                    if (ballpos[3] == 5 || ballpos[3] == 6)
                    {
                        game = false;
                        break;
                    }
                }
                if (ballpos[3] == 5 || ballpos[3] == 6)
                    break;
                cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.W || cki.Key == ConsoleKey.S)
                {
                    who = 1;
                    playerpos = Move(cki, playerpos, height, player2pos);
                }
                if (cki.Key == ConsoleKey.UpArrow || cki.Key == ConsoleKey.DownArrow)
                {
                    who = 2;
                    player2pos = Move(cki, playerpos, height, player2pos);
                }
            } while (cki.Key != ConsoleKey.W || cki.Key != ConsoleKey.S || ballpos[3] != 5 || ballpos[3] != 6);
        }
        Console.Clear();
        if (ballpos[3] == 5)
            Console.WriteLine("Player 2 Won the game! With a score of = " + score);
        if (ballpos[3] == 6)
            Console.WriteLine("Player 1 Won the game!");
        Console.WriteLine("\nDo you want to end the game? N = exit, anything else continue");
        gamestart = Console.ReadLine().ToUpper();
    }
}

static void Board(int height, int lenght)
{
    Console.SetCursorPosition(0, 0);
    for (int i = 0; i < height; i++){
        Console.CursorVisible = false;
        for (int j = 0; j < lenght; j++){
            if(j == lenght - 1)
            {
                Console.Write("# \n");
                continue;
            }
            if(i == 0 || i == height-1)
            {
                Console.Write("##");
                continue;
            }
            if((j == 0) && (i != 0 || i != height-1))
            {
                Console.Write("# ");
                continue;
            }
            if(j == lenght/2 && i != 0 && i != height-1)
            {
                Console.Write("| ");
                continue;
            }
            if( i != 0 || i != height-1 || j != 0 ||j != lenght - 1)
            {
                Console.Write("  ");
                continue;
            }
        }
    }
}

static int Score(int height, int lenght, int score, int p1, int[] ballpos)
{
    if ((ballpos[1] == p1 - 2 || ballpos[1] == p1 - 1 || ballpos[1] == p1 || ballpos[1] == p1 + 1 || ballpos[1] == p1 + 2) && (ballpos[3] == 4 && ballpos[0] == 4 || ballpos[3] == 2 && ballpos[0] == 4) && ballpos[3] != 5)
        score++;
    string scr = "Score of this game is:";
    Console.CursorVisible = false;
    Console.SetCursorPosition(lenght - scr.Length/2, height );
    Console.Write(scr);
    Console.SetCursorPosition( lenght - (scr.Length / 2), height + 2);
    Console.Write(score);
    return score;
}

static void Player(int playerpos, int height, int lenght, int player2pos, int who)
{
    if (who == 1)
    {
        for (int i = 1; i < height - 1; i++)
        {
            Console.SetCursorPosition(4, i);
            Console.Write(" ");
        }
        for (int i = -2; i < 3; i++)
        {
            if (playerpos + i == 0)
                return;
            Console.SetCursorPosition(4, playerpos + i);
            Console.Write("█");
        }
    }
    else if (who == 2)
    {
        for (int i = 1; i < height - 1; i++)
        {
            Console.SetCursorPosition(lenght * 2 - 6, i);
            Console.Write(" ");
        }
        for (int i = -2; i < 3; i++)
        {
            if (player2pos + i == 0)
                return;
            Console.SetCursorPosition(lenght * 2 - 6, player2pos + i);
            Console.Write("█");
        }
    }
}

static int Move(ConsoleKeyInfo cki, int playerpos, int height, int player2pos)
{
    switch (cki.Key)
    {
        case ConsoleKey.W:
            if (playerpos == 3)
                return playerpos;
            playerpos--;
            return playerpos;
        case ConsoleKey.S:
            if (playerpos == height - 4)
                return playerpos;
            playerpos++;
            return playerpos;
        case ConsoleKey.UpArrow:
            if (player2pos == 3)
                return player2pos;
            player2pos--;
            return player2pos;
        case ConsoleKey.DownArrow:
            if (player2pos == height - 4)
                return player2pos;
            player2pos++;
            return player2pos;
        default:
            return playerpos;
    }
}

static int[] Ball(int lenght, int height, int[] ballpos, int playerpos, int player2pos)
{

    Console.SetCursorPosition(ballpos[0], ballpos[1]);
    Console.Write(" ");

    if(ballpos[0] == lenght)
    {
        for (int i = 1; i < height-1; i++)
        {
            Console.SetCursorPosition(lenght, i);
            Console.Write("|");
        }
    }
    if (ballpos[1] == height - 2 && ballpos[3] == 3) // From Down -> To Up ->
    {
        ballpos[3] = 1;
        ballpos[2] = 1;
    }
    if (ballpos[1] == height - 2 && ballpos[3] == 4) // From Down <- To Up <-
    {
        ballpos[3] = 2;
        ballpos[2] = 3;
    }
    if (ballpos[1] == 1 && ballpos[3] == 1) // From Up -> To Down ->
    {
        ballpos[3] = 3;
        ballpos[2] = 2;
    }
    if (ballpos[1] == 1 && ballpos[3] == 2) // From Up <- To Down <-
    {
        ballpos[3] = 4;
        ballpos[2] = 4;
    }
    if ((ballpos[1] == player2pos - 2 || ballpos[1] == player2pos - 1 || ballpos[1] == player2pos || ballpos[1] == player2pos + 1 || ballpos[1] == player2pos + 2) && ballpos[3] == 3 && ballpos[0] == lenght*2 - 6 ||
        (ballpos[1] == player2pos - 2 || ballpos[1] == player2pos - 1 || ballpos[1] == player2pos || ballpos[1] == player2pos + 1 || ballpos[1] == player2pos + 2) && ballpos[3] == 1 && ballpos[0] == lenght*2 - 6) // From Up -> To Up <-
    {
        if (ballpos[3] == 1)
        {
            ballpos[3] = 2;
            ballpos[2] = 3;
        }
        else
        {
            ballpos[3] = 4;
            ballpos[2] = 4;
        }
    }
    if (ballpos[0] == lenght * 2 - 2 && ballpos[3] == 1 || ballpos[0] == lenght * 2 - 2 && ballpos[3] == 3) // From Up -> To Up <-
    {
        ballpos[3] = 6;
        return ballpos;
    }
    if ((ballpos[1] == playerpos - 2 || ballpos[1] == playerpos - 1 || ballpos[1] == playerpos || ballpos[1] == playerpos + 1 || ballpos[1] == playerpos + 2) && ballpos[3] == 4 && ballpos[0] == 4 ||
        (ballpos[1] == playerpos - 2 || ballpos[1] == playerpos - 1 || ballpos[1] == playerpos || ballpos[1] == playerpos + 1 || ballpos[1] == playerpos + 2) && ballpos[3] == 2 && ballpos[0] == 4) // From Player 1 Succsess
    {
        if (ballpos[3] == 4){
            ballpos[3] = 3;
            ballpos[2] = 2; }
        else{
            ballpos[3] = 1;
            ballpos[2] = 1;}
    }
    if(ballpos[0] == 0 && ballpos[3] == 2 || ballpos[0] == 0 && ballpos[3] == 4) // Player 1 fail
    {
        ballpos[3] = 5;
        return ballpos;
    }

    if (ballpos[2] == 1) // Up ->
    {
        ballpos[0] += 2;
        ballpos[1]--;
    }
    if (ballpos[2] == 2) // Down ->
    {
        ballpos[0] += 2;
        ballpos[1]++;
    }
    if (ballpos[2] == 3) // Up <-
    {
        ballpos[0] -= 2;
        ballpos[1]--;
    }
    if (ballpos[2] == 4) // Down <-
    {
        ballpos[0] -=2 ;
        ballpos[1]++;
    }
     Console.SetCursorPosition(ballpos[0], ballpos[1]);
    Console.Write("0");

    return ballpos;
}
