using System;
using NAudio.Wave;
using System.Threading;

Random random = new Random(); // new seed
Console.CursorVisible = false; 
int height = Console.WindowHeight - 1;
int width = Console.WindowWidth - 5;
bool shouldExit = false;

// Music, be sure to add your own file path
string soundFilePath = @"YourFilePath\betterOffAlone.mp3";
WaveOutEvent outputDevice = null;

// Console position of the dog
int dogX = 0;
int dogY = 0;

// Console position of the sheep
int sheepX = 0;
int sheepY = 0;

// Console position of the pen
int penX = 0;
int penY = 0;

// How far dog and sheep can be, how far sheep and pen can be
int range = 5;
int penRange = 2;

string dogIcon = "dog";
string sheepIcon = "sheep";
string penIcon = "[==]";

InitializeGame();
while (!shouldExit) 
{
    if (TerminalResized()) 
    {
        Console.Clear();
        Console.Write("Console was resized. Program exiting.");
        shouldExit = true;
    } 
    else 
    {
        Move(1, false);
        if (HerdedSheep())
        {
            Console.Clear();
            Console.Write("Victory!"); // center this message
            shouldExit = true;
        }
        System.Threading.Thread.Sleep(25);
    }
}


// Returns true if the Terminal was resized 
bool TerminalResized() 
{
    return height != Console.WindowHeight - 1 || width != Console.WindowWidth - 5;
}

void ShowDog()
{
    Console.SetCursorPosition(dogX, dogY);
    Console.Write(dogIcon);
}

void ShowSheep() 
{
    // Update sheep position to a random location
    sheepX = random.Next(0, width - dogIcon.Length);
    sheepY = random.Next(0, height - 1);

    // Display the sheep at the location
    Console.SetCursorPosition(sheepX, sheepY);
    Console.Write(sheepIcon);
}

void ShowPen()
{
    // Update pen position to a random location
    penX = random.Next(0, width - penIcon.Length);;
    penY = random.Next(0, height - 1);

    // Display the pen at the location
    Console.SetCursorPosition(penX, penY);
    Console.Write(penIcon);
}

// Returns true if the sheep location is in range of pen location
bool HerdedSheep() 
{
    return (Math.Abs(sheepY - penY) < penRange) && (Math.Abs(sheepX - penX) < penRange);
}

void MoveSheep(int dogX, int dogY)
{   
    // Calculate the direction from the sheep to the dog
    int deltaX = sheepX - dogX;
    int deltaY = sheepY - dogY;

    // Determine the new position for the sheep based on the dog's position
    int newSheepX = sheepX + Math.Sign(deltaX) * 1;
    int newSheepY = sheepY + Math.Sign(deltaY) * 1;

    // Ensure the new position is within the bounds
    newSheepX = Math.Max(0, Math.Min(width - sheepIcon.Length, newSheepX));
    newSheepY = Math.Max(0, Math.Min(height - 1, newSheepY));

    Console.SetCursorPosition(sheepX, sheepY);
    for (int i = 0; i < sheepIcon.Length; i++) 
    {
        Console.Write(" ");
    }

    sheepX = newSheepX;
    sheepY = newSheepY;

    Console.SetCursorPosition(sheepX, sheepY);
    Console.Write(sheepIcon);
}

void Move(int speed = 1, bool otherKeysExit = false) 
{
    int lastX = dogX;
    int lastY = dogY;

    switch (Console.ReadKey(true).Key) {
        case ConsoleKey.UpArrow:
            dogY--; 
            break;
		case ConsoleKey.DownArrow: 
            dogY++; 
            break;
		case ConsoleKey.LeftArrow:  
            dogX -= speed; 
            break;
		case ConsoleKey.RightArrow: 
            dogX += speed; 
            break;
		case ConsoleKey.Escape:     
            shouldExit = true; 
            break;
        default:
            // Exit if any other keys are pressed
            shouldExit = otherKeysExit;
            break;
    }
    
    // Clear the previous position of the dog
    Console.SetCursorPosition(lastX, lastY);
    for (int i = 0; i < dogIcon.Length; i++) 
    {
        Console.Write(" ");
    }
    
    // Display the pen at the location (dog can clear pen otherwise)
    Console.SetCursorPosition(penX, penY);
    Console.Write(penIcon);

    // Keep dog position within the bounds of the Terminal window
    dogX = (dogX < 0) ? 0 : (dogX >= width ? width : dogX);
    dogY = (dogY < 0) ? 0 : (dogY >= height ? height : dogY);

    // Draw the player at the new location
    Console.SetCursorPosition(dogX, dogY);
    Console.Write(dogIcon);

    if (Math.Abs(dogX - sheepX) <= range && Math.Abs(dogY - sheepY) <= range)
    {
        MoveSheep(dogX, dogY);
    }

}

void playMusic(string soundFilePath)
{
        if (System.IO.File.Exists(soundFilePath))
        {
            outputDevice = new WaveOutEvent();
            
            using (var audioFile = new AudioFileReader(soundFilePath))
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {

                }
            }
        }
        else
        {
            Console.WriteLine("File not found: " + soundFilePath);
        }
}

void InitializeGame() 
{
    Console.Clear();
    // Start music playback in a separate thread
    System.Threading.Thread musicThread = new System.Threading.Thread(() => playMusic(soundFilePath));
    musicThread.Start();
    ShowDog();
    ShowSheep();
    ShowPen();
}