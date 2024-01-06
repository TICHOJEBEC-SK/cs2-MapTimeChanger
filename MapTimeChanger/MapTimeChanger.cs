namespace MapTimeChanger;

using System;
using System.IO;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Timers;

public class PluginInfo : BasePlugin
{
    private Timer mapChangeTimer;

    public override string ModuleName => "MapTimeChanger";
    public override string ModuleVersion => "1.0";
    public override string ModuleAuthor => "TICHOJEBEC";
    public override string ModuleDescription => "Map changer, based on a timer.";

    private string FilePath => Path.Join(ModuleDirectory, "maps.txt");

    public override void Load(bool hotReload)
    {
        if (!File.Exists(FilePath))
            File.Create(FilePath);

        AddTimer(2700, () =>//2700 45 min
        {
            ChangeMap();
        }, TimerFlags.REPEAT);

        Console.WriteLine("MapTimeChanger is loaded");
    }
    private void ChangeMap()
    {
        Console.WriteLine("Changing map...");

        try
        {
            string[] mapIdArray = File.ReadAllLines(FilePath);

            if (mapIdArray.Length == 0)
            {
                Console.WriteLine("No map IDs found in file.");
                return;
            }

            string randomMapId = mapIdArray[new Random().Next(mapIdArray.Length)];

            Console.WriteLine($"Selected random map ID: {randomMapId}");
            
            Server.ExecuteCommand($"host_workshop_map {randomMapId}");

            Console.WriteLine($"Executed command: host_workshop_map {randomMapId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error changing map: {ex.Message}");
        }
    }
}    