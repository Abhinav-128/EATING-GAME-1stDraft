using System;

Random random = new Random();
Console.CursorVisible = false;
int height = Console.WindowHeight -1;
int width = Console.WindowWidth -5 ;
bool gameExit = false;

int playerX =0;
int playerY =0;

int foodX = 0;
int foodY = 0;

string[] states = { "('-')", "(^-^)", "(X_X)" };
string[] foods = { "@@@@@", "$$$$$", "#####" };

string Cplayer = states[0];

int food = 0;
int score = 0;

GameStart();
while(!gameExit)
{
   /* if (TerminalResized())
    {
        Console.Clear();
        Console.WriteLine("Console was resized, Program Exit");
        gameExit = true;
    }*/  
        if (PlayerIsFast())
        {
            Move(1, false);
        }
        else if (PlayerIsSick())
        {
            FreezePlayer();
        }
        else
        {
            Move(othersKeyExit: false);
        }
        
        if(GotFood()) 
        {
            score++;
            ChangePlayer();
            ShowFood();
        }
    
}
if(gameExit)
{
    //Score();
    Console.WriteLine($"Your final score is {score}");
}

/*bool TerminalResized()
{
    return height != Console.WindowHeight -1 || width != Console.WindowWidth -5;
}*/
void ShowFood()
{
    food = random.Next(0, foods.Length);

    foodX = random.Next(0, width - Cplayer.Length);
    foodY = random.Next(0, height-1);

    Console.SetCursorPosition(foodX, foodY);
    Console.Write(foods[food]);

}
void GameStart()
{
    Console.Clear();
    ShowFood();
    Console.SetCursorPosition(0,0);
    Console.Write(Cplayer);
}
bool GotFood()
{
    return playerY ==foodY && playerX ==foodX;
}

bool PlayerIsSick()
{
    return Cplayer.Equals(states[2]);
}

bool PlayerIsFast()
{
    return Cplayer.Equals(states[1]);
}

void ChangePlayer()
{
   // food = random.Next(0, foods.Length - 1);
    Cplayer = states[food];
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(Cplayer);
}

/*void Score()
{
    while (!gameExit)
    {
        if (food == 0)

        {
            score += 1;
        }
        if (food == 1) 
        {
            score += 2;
        }
        if(food==2)
        {
            score += 3;
        }
    }
}*/

void Move(int speed = 1, bool othersKeyExit = false)
{
    int lastX = playerX;
    int lastY = playerY;

    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.UpArrow:
            playerY--;
            break;
        case ConsoleKey.DownArrow:
            playerY++;
            break;
        case ConsoleKey.LeftArrow:
            playerX-=speed;
            break;
        case ConsoleKey.RightArrow:
            playerX+=speed;
        break;
        case ConsoleKey.Escape:
            gameExit = true;
            break;
        default:
            gameExit = othersKeyExit;
            break;
    }
    Console.SetCursorPosition(lastX, lastY);
    for(int i =0; i<Cplayer.Length; i++)
    {
        Console.Write(" ");
    }
    if(playerX<0)
    {
        playerX = 0;
    }
    else if(playerX>width)
    {
        playerX = width;
    }
    if(playerY<0)
    {
        playerY = 0;
    }
    else if(playerY>height)
    {
        playerY = height;
    }
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(Cplayer);
}
void FreezePlayer()
{
    System.Threading.Thread.Sleep(1000);
    Cplayer = states[0];
}