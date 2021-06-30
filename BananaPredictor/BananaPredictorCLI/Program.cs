using System;
using System.Collections.Generic;
using BananaPredictorCLI.Osu;

namespace BananaPredictorCLI
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            Console.Clear();
            for (int i = 0; i < 20; i++)
            {
                Console.Write("+");
            }
            Console.WriteLine();
            Console.WriteLine("K3's BananaPredictor");
            for (int i = 0; i < 20; i++)
            {
                Console.Write("+");
            }

            string[] command;
            
            string tpath = null;

            StoringInfoDict spinnerAOR = new();

            while (true)
            {
                Console.WriteLine();
                command = Console.ReadLine().Split(' ');
                
                switch (command[0].ToLower())
                {
                    case "tpath":
                        tpath = command[1];
                        Console.WriteLine("Target path: " + tpath);
                        break;

                    case "add":
                        // Adding - 
                        // add
                        //      Start Time:
                        //      End Time:
                        //      Distance:
                        //      Only Spinner (T/F):
                        //      Inverted (T/F):
                        //      Start Left Pos:
                        //      End Left Pos:
                        //      Start Right Pos:
                        //      End Right Pos:
                        //      Added Spinner!
                        //      id for Spinner is: 1234
                        int s = 0, e = 0, d = 0, o = 0, inv = 1, sl = 0, el = 0, sr = 0, er = 0;
                        /*// TODO: Fix this later, using bad programming for now
                        int[] iLoop = {s, e, d, o, inv, sl, el, sr, er};
                        bool[] iBool = {true, e <= s, d <= 0, true, true, true, el <= sl, true, er <= sr};      // TODO: While loop not working; fix this
                        string[] iString = {"Start Time", "End Time", "Distance", "Only Spinner (T/F)", "Inverted(T/F)", "Start Left Pos", "End Left Pos", "Start Right Pos", "End Right Pos"};                
                        
                        for (int i = 0; i < iLoop.Length; i++)
                            while (iBool[i])
                                try
                                {
                                    Console.WriteLine(iString[i] + ":");
                                    if (i == 0 || i == 1)
                                    {
                                        string[] temp = Console.ReadLine().Split(":");
                                        iLoop[i] = Int32.Parse(temp[0]) * 60 * 100 + Int32.Parse(temp[1]) * 1000 + Int32.Parse(temp[2]);
                                    }
                                    else
                                        iLoop[i] = Int32.Parse(Console.ReadLine());
                                    
                                    if (iBool[i].ToString().ToLower() == "true")
                                        break;
                                } catch (System.FormatException)
                                {
                                    Console.WriteLine("Not valid input. Try again.");
                                }*/
                        Console.WriteLine("Start Time:");
                        string[] temp = Console.ReadLine().Split(":");
                        s = Int32.Parse(temp[0]) * 60 * 100 + Int32.Parse(temp[1]) * 1000 + Int32.Parse(temp[2]);
                        
                        Console.WriteLine("End Time:");
                        temp = Console.ReadLine().Split(":");
                        e = Int32.Parse(temp[0]) * 60 * 100 + Int32.Parse(temp[1]) * 1000 + Int32.Parse(temp[2]);
                        
                        Console.WriteLine("Distance:");
                        d = Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Only Spinner (T:1/F:0):");
                        o = Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Inverted (T:1/F:0):");
                        inv = Int32.Parse(Console.ReadLine());
                        
                        Console.WriteLine("Start Left Pos:");
                        sl = Int32.Parse(Console.ReadLine());
                        
                        Console.WriteLine("End Left Pos:");
                        el = Int32.Parse(Console.ReadLine());
                        
                        Console.WriteLine("Start Right Pos:");
                        sr = Int32.Parse(Console.ReadLine());
                        
                        Console.WriteLine("End Right Pos:");
                        er = Int32.Parse(Console.ReadLine());

                        List<int> listMap = new();
                        List<int> listSpin = new();

                        listMap.Add(s);
                        listMap.Add(e);
                        listMap.Add(d);
                        listMap.Add(o);
                        listMap.Add(inv);

                        listSpin.Add(sl);
                        listSpin.Add(el);
                        listSpin.Add(sr);
                        listSpin.Add(er);

                        Random rnd = new();
                        int id = rnd.Next(s + e + d + sl + el + sr + er, int.MaxValue);

                        spinnerAOR.Add(id, listMap, listSpin);

                        Console.WriteLine("New spinner added - id: " + id);

                        break;
                        
                    case "edit":
                        // Editing -
                        // edit -id 1234 -s 300 -e 3000     |   Edits start and end time
                        // edit -id 1234 -d 2               |   Edits distance
                        // edit -id 1234 -o false           |   Edits only spinner
                        // edit -id 1234 -i true            |   Edits inverted
                        // edit -id 1234 -sl 31             |   Edits StartLeftPos
                        // edit -id 1234 -el 124            |   Edits EndLeftPos
                        // edit -id 1234 -sr 423            |   Edits StartRightPos
                        // edit -id 1234 -er 156            |   Edits EndRightPos
                        // edit -id 1234 -s 300 -e 3000 -d 2 -o false -i true -sl 31 -el 124 -sr 423 -er 156    |   Can be combined into one full command
                        break;

                    case "remove":
                        // Removing -
                        // remove -id 2931                  |   Removes item
                        spinnerAOR.Remov(Int32.Parse(command[1]));
                        break;

                    case "run":
                        if (!(string.IsNullOrEmpty(tpath) && string.IsNullOrWhiteSpace(tpath))
                            && spinnerAOR.Count > 1)
                        {
                            BananaSpinPredictor bs = new();
                            bs.SpinnerPredictor(tpath, spinnerAOR);
                            Console.WriteLine("Finished!");
                        }
                        break;

                    case "info":
                        Console.WriteLine("Target: " + tpath);
                        Console.WriteLine("Spinner List: ");
                        foreach (KeyValuePair<int, StoringInfo> kvp in spinnerAOR)
                        {
                            Console.WriteLine("id: " + kvp.Key);
                            foreach (var item in kvp.Value.MapRelated)
                                Console.WriteLine("\t" + item);
                            foreach (var item in kvp.Value.SpinnerRelated)
                                Console.WriteLine("\t" + item);
                        }
                        break;

                    case "help":
                        Console.WriteLine("TODO - Check the github README file for the time being");
                        break;

                    case "exit":
                        Console.Clear();
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Not a valid command. Type help for more information.");
                        break;
                }
            }
        }
    }
}
