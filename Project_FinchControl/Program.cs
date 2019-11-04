using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control
    // Description: 
    // Application Type: Console
    // Author: 
    // Dated Created: 
    // Last Modified: 
    //
    // **************************************************

    class Program
    {
        public enum Command
        {
            NONE,
            MOVEFORWARD,
            MOVEBACKWARD,
            STOPMOTORS,
            WAIT,
            TURNRIGHT,
            TURNLEFT,
            LEDON,
            LEDOFF,
            DONE
        }

        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMainMenu();
            DisplayClosingScreen();
        }

        static void SetTheme()
        {
            string dataPath = @"Data\Theme.txt";
            string[] colors = new string[2];
            string[] colorString = new string[2];

            string foregroundColorString;
            string backgroundColorString;

            Console.Write("What background color would you like? ");
            backgroundColorString = Console.ReadLine();
            Console.WriteLine();

            Console.Write("What foreground color would you like? ");
            foregroundColorString = Console.ReadLine();
            Console.WriteLine();
                       
            colors[0] = backgroundColorString;
            colors[1] = foregroundColorString;

            File.WriteAllLines(dataPath, colors);

            ConsoleColor foregroundColor;
            ConsoleColor backgroundColor;

            colorString = File.ReadAllText(dataPath).Split();
            backgroundColorString = colorString[0];
            foregroundColorString = colorString[1];

            Enum.TryParse(foregroundColorString, out foregroundColor);
            Enum.TryParse(backgroundColorString, out backgroundColor);

            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
        }

        //
        // display main menu
        //
        static void DisplayMainMenu()
        {
            //
            // instantiate a Finch object
            // 
            Finch finchRobot = new Finch();

            bool finchRobotConnected = false;
            bool quitApplication = false;
            string menuChoice;

            do
            {

                DisplayScreenHeader("Main Menu");

                //
                // get the user's menu choice
                //
                Console.WriteLine("a) Connect Finch Robot");
                Console.WriteLine("b) Talent Show");
                Console.WriteLine("c) Data Recorder");
                Console.WriteLine("d) Alarm System");
                Console.WriteLine("e) User Programming");
                Console.WriteLine("f) Disconnect Finch Robot");
                Console.WriteLine("q) Quit");
                Console.Write("Enter Choice: ");
                menuChoice = Console.ReadLine().ToLower().Trim();


                //
                // process user's choice
                //
                switch (menuChoice)
                {
                    case "a":
                        finchRobotConnected = DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        if (finchRobotConnected)
                        {
                            DisplayTalentShow(finchRobot);
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Please return to the Main Menu and connect to the Finch robot.");
                            DisplayContinuePrompt();
                        }
                        break;
                    case "c":
                        if (finchRobotConnected)
                        {
                            DisplayDataRecorder(finchRobot);
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Please return to the Main Menu and connect to the Finch robot.");
                            DisplayContinuePrompt();
                        }
                        break;

                    case "d":
                        if (finchRobotConnected)
                        {
                            DisplayAlarmSystem(finchRobot);
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Please return to the Main Menu and connect to the Finch robot.");
                            DisplayContinuePrompt();
                        }
                        break;

                    case "e":
                        if (finchRobotConnected)
                        {
                            DisplayUserProgramming(finchRobot);
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Please return to the Main Menu and connect to the Finch robot.");
                            DisplayContinuePrompt();
                        }
                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine("\t**********************");
                        Console.WriteLine("\tPlease indicate your choice with a letter.");
                        Console.WriteLine("\t**********************");

                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        //
        // display connect finch robot
        //
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            bool finchRobotConnected = false;

            DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("Ready to connect to the Finch Robot. Please be sure to connect the USB cable to the robot and the computer.");
            DisplayContinuePrompt();
            Console.Clear();
            Console.WriteLine();

            finchRobotConnected = finchRobot.connect();

            if (finchRobotConnected)
            {
                finchRobot.setLED(0, 255, 0);
                finchRobot.noteOn(15000);
                finchRobot.wait(250);
                finchRobot.noteOff();
                finchRobot.setLED(0, 0, 0);

                Console.WriteLine();
                Console.WriteLine("Finch robot is now connected.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Unable to connect to the Finch robot.");
            }

            DisplayContinuePrompt();

            return finchRobotConnected;
        }

        //
        // display talent show
        //
        static void DisplayTalentShow(Finch finchRobot)
        {
            /*//
            // variables
            //
            int redHue;
            int blueHue;
            int greenHue;
            int lightTime;
            */

            DisplayScreenHeader("Talent Show");

            Console.WriteLine("The Finch robot is ready to show you its talent");
            DisplayContinuePrompt();
            Console.WriteLine();
            Console.WriteLine("Performing...");
            Console.WriteLine();

            //
            // red light, forward motion 5 seconds
            //

            DisplayShowOne(finchRobot);
            //
            // green light, turn counterclockwise 180 degrees
            //
            DisplayShowTwo(finchRobot);

            //
            // blue light, forward motion 5 seconds
            // 
            DisplayShowThree(finchRobot);


            /*
            for (int lightLevel = 0; lightLevel < 255; lightLevel++)
            {
                finchRobot.setLED(redHue, greenHue, blueHue);
            }
            */

            Console.WriteLine("Performance complete.");
            finchRobot.wait(500);

            DisplayContinuePrompt();

            DisplayMainMenu();
        }

        //
        // light red, forward motion 3 seconds, tone on 1 second, tone off, light off
        //

        static void DisplayShowOne(Finch finchRobot)
        {
            finchRobot.setLED(255, 0, 0);

            finchRobot.setMotors(255, 255);

            finchRobot.wait(3000);

            finchRobot.setMotors(0, 0);

            finchRobot.noteOn(4435);

            finchRobot.wait(1000);

            finchRobot.noteOff();

            finchRobot.setLED(0, 0, 0);
        }

        //
        // light green, turn 3 seconds, note on 2 seconds, note off, light off
        //
        static void DisplayShowTwo(Finch finchRobot)
        {
            finchRobot.setLED(0, 255, 0);

            finchRobot.setMotors(0, 150);

            finchRobot.wait(3000);

            finchRobot.setMotors(0, 0);

            finchRobot.noteOn(10000);

            finchRobot.wait(2000);

            finchRobot.noteOff();

            finchRobot.setLED(0, 0, 0);
        }

        //
        // light blue, reverse 3 seconds, note on 1.5 seconds, note off, light off
        //
        static void DisplayShowThree(Finch finchRobot)
        {
            finchRobot.setLED(0, 0, 255);

            finchRobot.setMotors(-255, -255);

            finchRobot.wait(3000);

            finchRobot.setMotors(0, 0);

            finchRobot.noteOn(220);

            finchRobot.wait(1500);

            finchRobot.noteOff();

            finchRobot.setLED(0, 0, 0);
        }

        /* user input for color of light and duration
        static int GetLightDuration()
        {
            //
            // variables
            //
            int lightTime;
            bool IsNumber;

            Console.Write("How long to leave the light on: ");
            IsNumber = int.TryParse(Console.ReadLine(), out lightTime);

            return lightTime;

        }

        static int GetGreenHue()
        {
            bool IsNumber;
            int green;

            Console.WriteLine("Choose green hue (0 - 255): ");
            IsNumber = int.TryParse(Console.ReadLine(), out green);

            return green;
        }

        static int GetBlueHue()
        {
            bool IsNumber;
            int blue;

            Console.WriteLine("Choose blue hue (0 - 255): ");
            IsNumber = int.TryParse(Console.ReadLine(), out blue);

            return blue;
        }

        static int GetRedHue()
        {
            bool IsNumber;
            int red;

            Console.WriteLine("Choose red hue (0 - 255): ");
            IsNumber = int.TryParse(Console.ReadLine(), out red);

            return red;
        }
        */
        
        //
        // display data recorder
        //
        static void DisplayDataRecorder(Finch finchRobot)
        {
            double dataPointFrequency;
            int numberOfDataPoints;

            DisplayScreenHeader("Data Recorder");

            //
            // give the user some info about what is going to happen
            //

            Console.WriteLine("I need to know how often to take a recording and how many recordings to make. We will get those recordings and echo them back to you.");

            dataPointFrequency = DisplayGetDataPointFrequency();
            numberOfDataPoints = DisplayGetNumberOfDataPoints();

            double[] temperatures = new double[numberOfDataPoints];

            DisplayGetData(numberOfDataPoints, dataPointFrequency, temperatures, finchRobot);

            DisplayData(temperatures);

            DisplayContinuePrompt();
        }

        static void DisplayData(double[] temperatures)
        {
            DisplayScreenHeader("Temperatures");

            Console.WriteLine("The temperature readings are as follows: ");
            Console.WriteLine();

            for (int index = 0; index < temperatures.Length; index++)
            {
                foreach (double temp in temperatures)
                {
                    ConvertCelsiusToFahrenheit(temp);
                }
                Console.WriteLine($"Temperature {index + 1}: {temperatures[index]}");
            }

            //DisplayContinuePrompt();
        }

        static void DisplayGetData(
            int numberOfDataPoints, 
            double dataPointFrequency, 
            double[] temperatures, 
            Finch finchRobot)
        {
            DisplayScreenHeader("Get Temperatures");

            // give the user info and a prompt
            Console.WriteLine($"The Finch Robot is now going to take {numberOfDataPoints} data points.");

            finchRobot.wait(2000);
            Console.Clear();

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                temperatures[index] = finchRobot.getTemperature();
                ConvertCelsiusToFahrenheit(temperatures[index]);
                int milliseconds = (int)(dataPointFrequency * 1000);
                finchRobot.wait(milliseconds);

                Console.WriteLine($"Temperature {index + 1}: {temperatures[index]}");
            }

            DisplayContinuePrompt();
        }

        static double ConvertCelsiusToFahrenheit(double celciusTemp)
        {
            double Fahrenheit;

            Fahrenheit = celciusTemp * 1.8 + 32;

            return Fahrenheit;

        }

        static int DisplayGetNumberOfDataPoints()
        {
            int numberOfDataPoints;

            DisplayScreenHeader("Number of Data Points");

            Console.Write("Enter the Number of Data Points: ");
            int.TryParse(Console.ReadLine(), out numberOfDataPoints);

            DisplayContinuePrompt();

            return numberOfDataPoints;
        }

        static double DisplayGetDataPointFrequency()
        {
            double dataPointFrequency;

            DisplayScreenHeader("Data Point Frequency");

            Console.Write("Enter Frequency of Recordings (in seconds): ");
            double.TryParse(Console.ReadLine(), out dataPointFrequency);

            DisplayContinuePrompt();

            return dataPointFrequency;
        }
        //
        // display alar system
        //
        static void DisplayAlarmSystem(Finch finchRobot)
        {
            string alarmType;
            int maxSeconds;
            double threshold;
            bool thresholdExceeded;

            DisplayScreenHeader("Alarm System");
            Console.WriteLine("**********************************************************");
            Console.WriteLine("* The finch will now ask which type of alarm to display, *");
            Console.WriteLine("* How long to monitor that particular alarm type,        *");
            Console.WriteLine("* and the threshold to compare against.                  *");
            Console.WriteLine("**********************************************************");

            DisplayContinuePrompt();

            alarmType = DisplayGetAlarmType();
            maxSeconds = DisplayGetMaxSeconds();
            threshold = DisplayGetThreshold(finchRobot, alarmType);
            thresholdExceeded = MonitorCurrentLevels(finchRobot, threshold, maxSeconds, alarmType);

            if (thresholdExceeded)
            {
                if (alarmType == "light")
                {
                    Console.WriteLine($"Maximum {alarmType} Level Exceeded");
                }
                else
                {
                    Console.WriteLine($"Maximum {alarmType} Exceeded");
                }

                //
                // Red light and sound when threshold is exceeded
                //

                finchRobot.setLED(255, 0, 0);
                finchRobot.noteOn(3200);

                finchRobot.wait(500);

                finchRobot.setLED(0, 0, 0);
                finchRobot.noteOff();
            }
            else
            {
                Console.WriteLine("Maximum Time Exceeded");
            }

            DisplayContinuePrompt();
        }

        /*static bool MonitorCurrentLightLevels(Finch finchRobot, double threshold, int maxSeconds)
        {
            bool thresholdExceeded = false;
            int currentLightLevel;
            double seconds = 0;


            while (!thresholdExceeded && seconds <= maxSeconds)
            {
                finchRobot.setLED(0, 255, 0);

                currentLightLevel = finchRobot.getLeftLightSensor();

                DisplayScreenHeader($"Monitor Light Levels");
                Console.WriteLine($"Maximum Light Level: {threshold}");
                Console.WriteLine($"Current Light Level: {currentLightLevel}");

                if (currentLightLevel > threshold)
                {
                    thresholdExceeded = true;
                }

                finchRobot.wait(500);
                seconds += 0.5;
            }

            finchRobot.setLED(0, 0, 0);
            return thresholdExceeded;
        }*/

        /*static bool MonitorCurrentTemperatureLevels(Finch finchRobot, double threshold, int maxSeconds, string alarmType)
        {
            bool thresholdExceeded = false;
            int currentLevel;
            double seconds = 0;

            while (!thresholdExceeded && seconds <= maxSeconds)
            {
                finchRobot.setLED(0, 255, 0);

                currentLevel = (int)finchRobot.getTemperature();

                DisplayScreenHeader($"Monitor {alarmType} Levels");
                Console.WriteLine($"Maximum {alarmType} Level: {threshold}");
                Console.WriteLine($"Current {alarmType} Level: {currentLevel}");

                if (currentLevel > threshold)
                {
                    thresholdExceeded = true;
                }

                finchRobot.wait(500);
                seconds += 0.5;
            }

            return thresholdExceeded;
        }*/

        static bool MonitorCurrentLevels(Finch finchRobot, double threshold, int maxSeconds, string alarmType)
        {
            bool thresholdExceeded = false;
            int currentLevel = 0;
            double seconds = 0;

            while (!thresholdExceeded && seconds <= maxSeconds)
            {
                if (alarmType == "temperature")
                {
                    currentLevel = (int)finchRobot.getTemperature();
                }
                else if (alarmType == "light")
                {
                    currentLevel = (int) finchRobot.getLeftLightSensor();
                }
                finchRobot.setLED(0, 255, 0);

                DisplayScreenHeader($"Monitor {alarmType} Levels");
                Console.WriteLine($"Maximum {alarmType} Level: {threshold}");
                Console.WriteLine($"Current {alarmType} Level: {currentLevel}");

                if (currentLevel > threshold)
                {
                    thresholdExceeded = true;
                }

                finchRobot.wait(500);
                seconds += 0.5;
            }

            return thresholdExceeded;
        }

        //
        // user chooses the upper threshold. Exceeding this number will trigger the alarm
        //
        static double DisplayGetThreshold(Finch finchRobot, string alarmType)
        {
            double threshold = 0;
            bool ValidNumber = false;

            DisplayScreenHeader("Threshold Value");

            switch (alarmType)
            {
                case "light":
                    Console.WriteLine("\t\tAlarm type: Light");
                    Console.WriteLine();

                    Console.Write($"Current Light Level:  {finchRobot.getLeftLightSensor()}");
                    Console.WriteLine();

                    while (!ValidNumber)
                    {
                    Console.Write("Enter Maximum Light Level [0 - 255]: ");

                        if (double.TryParse(Console.ReadLine(), out threshold))
                        {
                            ValidNumber = true;
                        }
                        else
                        {
                            Console.WriteLine("Not a number. Try again.");
                        }
                    }

                    Console.WriteLine($"Threshold set to {threshold}");
                    Console.WriteLine();

                    DisplayContinuePrompt();
                    break;

                case "temperature":
                    Console.WriteLine("\t\tAlarm type: Temperature");
                    Console.WriteLine();

                    Console.Write($"Current Temperature Level:  {finchRobot.getTemperature()}");
                    Console.WriteLine();

                    while (!ValidNumber)
                    {
                    Console.Write("Enter Maximum Temperature Level [0 - 255]: ");

                        if (double.TryParse(Console.ReadLine(), out threshold))
                        {
                            ValidNumber = true;
                        }
                        else
                        {
                            Console.WriteLine("Not a number. Try again.");
                        }
                    }
                    Console.WriteLine($"Threshold set to {threshold}");
                    break;

                default:
                    break;
            }

            return threshold;
        }

        //
        // user chooses how long to monitor
        //
        static int DisplayGetMaxSeconds()
        {
            bool ValidNumber = false;
            int maxSeconds = 0;
            // todo - ***** Add validation to user response *****
            while (!ValidNumber)
            {
            Console.Write("Enter Maximum Number of Seconds: ");

                if (int.TryParse(Console.ReadLine(), out maxSeconds))
                {
                    ValidNumber = true;
                }
                else
                {
                    Console.WriteLine("Not a number. Try again.");
                }
            }

            Console.WriteLine($"Setting Maximum Number of Seconds to {maxSeconds}");

            DisplayContinuePrompt();

            return maxSeconds;
        }

        //
        // user chooses alarm type to monitor
        //
        static string DisplayGetAlarmType()
        {
            string alarmType = "";
            bool ValidAlarmType = false;
            while (!ValidAlarmType)
            {
            Console.Write("Enter Alarm Type [light or temperature]: ");
            alarmType = Console.ReadLine();

                if (alarmType == "light")
                {
                    ValidAlarmType = true;
                }
                else if (alarmType == "temperature")
                {
                    ValidAlarmType = true;
                }
                else
                {
                    Console.WriteLine("Unrecognized Alarm Type. Please choose \"light\" or \"temperature\"");
                }
            }
            return alarmType;
        }

        //
        // display user programming
        //
        static void DisplayUserProgramming(Finch finchRobot)
        {               
            string menuChoice;
            bool quitApplication = false;

            (int motorSpeed, int ledBrightness, int waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            List<Command> commands = new List<Command>();

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get the user's menu choice
                //
                Console.WriteLine("a) Set Command Parameters");
                Console.WriteLine("b) Add Commands");
                Console.WriteLine("c) View Commands");
                Console.WriteLine("d) Execute Commands");
                Console.WriteLine("e) Write Commands to Data File");
                Console.WriteLine("f) Read Commands from Data File");
                Console.WriteLine("q) Quit");
                Console.Write("Enter Choice: ");
                menuChoice = Console.ReadLine().ToLower().Trim();

                //
                // process user's choice
                //
                switch (menuChoice.ToLower())
                {
                    case "a":
                       commandParameters = DisplayGetCommandParameters();
                        break;

                    case "b":
                        DisplayGetFinchCommands(commands);
                        break;
                    case "c":
                        DisplayFinchCommands(commands);
                        break;

                    case "d":
                        DisplayExecuteFinchCommands(finchRobot, commands, commandParameters);
                        break;

                    case "e":
                        DisplayWriteUserProgrammingData(commands);
                        break;

                    case "f":
                        commands = DisplayReadUserProgrammingData();
                        break;

                    case "q":
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine("\t**********************");
                        Console.WriteLine("\tPlease indicate your choice with a letter.");
                        Console.WriteLine("\t**********************");

                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        static List<Command> DisplayReadUserProgrammingData()
        {
            string dataPath = @"Data\Data.txt";
            List<Command> commands = new List<Command>();
            string[] commandsString;

            DisplayScreenHeader("Load Commands from Data File");

            Console.WriteLine("Ready to load the commands from the data file.");
            Console.WriteLine();

            commandsString = File.ReadAllLines(dataPath);

            //
            // create a list of Command
            //
            Command command;
            foreach (string commandString in commandsString)
            {
                Enum.TryParse(commandString, out command);

                commands.Add(command);
            }

            Console.WriteLine();
            Console.WriteLine("Commands loaded successfully.");

            DisplayContinuePrompt();

            return commands;
        }

        static void DisplayWriteUserProgrammingData(List<Command> commands)
        {
            string dataPath = @"Data\Data.txt";
            List<string> commandsString = new List<string>();

            DisplayScreenHeader("Save Commands to the Data File");

            Console.WriteLine("Ready to save the commands to the data file.");

            DisplayContinuePrompt();

            //
            // create list of command strings
            //
            foreach (Command command in commands)
            {
                commandsString.Add(command.ToString());
            }

            File.WriteAllLines(dataPath, commandsString.ToArray());

            Console.WriteLine();
            Console.WriteLine("Commands successfully saved.");

            DisplayContinuePrompt();
        }

        static void DisplayExecuteFinchCommands(
            Finch finchRobot,
            List<Command> commands,
            (int motorSpeed, int ledBrightness, int waitSeconds) commandParameters)            
        {
            int motorSpeed = commandParameters.motorSpeed;
            int ledBrightness = commandParameters.ledBrightness;
            int waitSeconds = commandParameters.waitSeconds;

            DisplayScreenHeader("Execute Finch Command");

            Console.WriteLine("The Finch Robot will now execute the commands you provided.");
            Console.WriteLine();
            DisplayContinuePrompt();

            foreach (Command command in commands)
            {
                switch (command)
                {
                    case Command.NONE:
                        break;

                    case Command.MOVEFORWARD:
                        Console.WriteLine("MOVEFORWARD");
                        finchRobot.setMotors(motorSpeed, motorSpeed);
                        break;

                    case Command.MOVEBACKWARD:
                        Console.WriteLine("MOVEBACKWARD");
                        finchRobot.setMotors(-motorSpeed, -motorSpeed);
                        break;

                    case Command.STOPMOTORS:
                        Console.WriteLine("STOPMOTORS");
                        finchRobot.setMotors(0, 0);
                        break;

                    case Command.WAIT:
                        Console.WriteLine("WAIT");
                        finchRobot.wait(waitSeconds * 1000);
                        break;

                    case Command.TURNRIGHT:
                        Console.WriteLine("TURNRIGHT");
                        finchRobot.setMotors(motorSpeed, 0);
                        break;

                    case Command.TURNLEFT:
                        Console.WriteLine("TURNLEFT");
                        finchRobot.setMotors(0, motorSpeed);
                        break;

                    case Command.LEDON:
                        Console.WriteLine("LEDON");
                        finchRobot.setLED(ledBrightness, ledBrightness, ledBrightness);
                        break;

                    case Command.LEDOFF:
                        Console.WriteLine("LEDOFF");
                        finchRobot.setLED(0, 0, 0);
                        break;

                    case Command.DONE:
                        Console.WriteLine("DONE");
                        break;

                    default:
                        break;
                }
            }

            DisplayContinuePrompt();
        }

        static void DisplayFinchCommands(List<Command> commands)
        {
            DisplayScreenHeader("Finch Robot Commands");

            foreach (Command command in commands)
            {
                Console.WriteLine(command);
            }

            DisplayContinuePrompt();
        }

        static void DisplayGetFinchCommands(List<Command> commands)
        {
            Command command = Command.NONE;

            DisplayScreenHeader("Finch Robot Commands");

            Console.WriteLine("************************************************************************");
            Console.WriteLine("* Here is where you will enter commands.                               *");
            Console.WriteLine("* After each command, press the Enter key to add more commands.        *");
            Console.WriteLine("* After entering your final command, type DONE, to return to the menu. *");
            Console.WriteLine("* At the menu, use the C option to review your commands, and           *");
            Console.WriteLine("* use the D option to execute your command block.                      *");
            Console.WriteLine("************************************************************************");
            Console.WriteLine();

            while (command != Command.DONE)
            {
                Console.WriteLine("Valid commands are: ");
                Console.WriteLine();
                Console.WriteLine("MOVEFORWARD, MOVEBACKWARD, STOPMOTORS,");
                Console.WriteLine("WAIT, TURNRIGHT, TURNLEFT,");
                Console.WriteLine("LEDON, LEDOFF, DONE");

                Console.Write("Enter command: ");
                Enum.TryParse(Console.ReadLine().ToUpper(), out command);
                Console.WriteLine();
                

                commands.Add(command);

            }

            // echo commands

            DisplayContinuePrompt();
        }

        static (int motorSpeed, int ledBrightness, int waitSeconds) DisplayGetCommandParameters()
        {
            (int motorSpeed, int ledBrightness, int waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            //
            // Get Motor Speed values
            //
            string userInput;
            bool intTryParseResult;
            
            userInput = "0";
            intTryParseResult = false;

            if (int.Parse(userInput) < 1 || int.Parse(userInput) > 255)
            {
                while (!intTryParseResult)
                {
                    Console.Write("Enter Motor Speed [1 - 255]: ");
                    userInput = Console.ReadLine();
                    intTryParseResult = int.TryParse(userInput, out commandParameters.motorSpeed);
                    if (intTryParseResult)
                    {
                        int.TryParse(userInput, out commandParameters.motorSpeed);
                        while (commandParameters.motorSpeed < 1 || commandParameters.motorSpeed > 255)
                        {
                            Console.WriteLine("Value not within acceptable parameter. Please try again.");
                            Console.Write("Enter Motor Speed [1 - 255]: ");
                            int.TryParse(Console.ReadLine(), out commandParameters.motorSpeed);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not an integer. Try again.");
                    }
                }
            }

            //
            // get LED Brightness value
            //
            userInput = "0";
            intTryParseResult = false;
            if (int.Parse(userInput) < 1 || int.Parse(userInput) > 255)
            {
                while (!intTryParseResult)
                {
                    Console.Write("Enter LED Brightness [1 - 255]: ");
                    userInput = Console.ReadLine();
                    intTryParseResult = int.TryParse(userInput, out commandParameters.ledBrightness);
                    if (intTryParseResult)
                    {
                        int.TryParse(userInput, out commandParameters.ledBrightness);
                        while (commandParameters.ledBrightness < 1 || commandParameters.ledBrightness > 255)
                        {
                            Console.WriteLine("Value not within acceptable parameter. Please try again.");
                            Console.Write("Enter LED Brightness [1 - 255]: ");
                            int.TryParse(Console.ReadLine(), out commandParameters.ledBrightness);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not an integer. Try again.");
                    }
                }
            }

            //
            // get wait time
            //
            userInput = "0";
            intTryParseResult = false;
            if (int.Parse(userInput) < 1 || int.Parse(userInput) > 255)
            {
                while (!intTryParseResult)
                {
                    Console.Write("Enter Wait time in Seconds [1 - 255]: ");
                    userInput = Console.ReadLine();
                    intTryParseResult = int.TryParse(userInput, out commandParameters.waitSeconds);
                    if (intTryParseResult)
                    {
                        int.TryParse(userInput, out commandParameters.waitSeconds);
                        while (commandParameters.waitSeconds < 1 || commandParameters.waitSeconds > 255)
                        {
                            Console.WriteLine("Value not within acceptable parameter. Please try again.");
                            Console.Write("Enter Wait time in Seconds [1 - 255]: ");
                            int.TryParse(Console.ReadLine(), out commandParameters.waitSeconds);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not an integer. Try again.");
                    }
                }
            }

            // echo values to user
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"Motor Speed is set to: {commandParameters.motorSpeed}");
            Console.WriteLine($"LED Brightness is set to: {commandParameters.ledBrightness}");
            Console.WriteLine($"The wait time is set to: {commandParameters.waitSeconds} second(s)");
            Console.WriteLine();

            DisplayContinuePrompt();

            return commandParameters;
        }

        //
        // display disconnect finch robot
        //
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine();
            Console.WriteLine("Ready to disconnect the Finch robot.");
            DisplayContinuePrompt();
            Console.Clear();

            finchRobot.disConnect();

            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(250);
            finchRobot.setLED(0, 0, 0);

            Console.WriteLine();
            Console.WriteLine("Finch robot is now disconnected.");

            DisplayContinuePrompt();
        }


        /// <summary>
        /// display welcome screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        #region HELPER METHODS

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}