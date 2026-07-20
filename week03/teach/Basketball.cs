/*
 * CSE 212 Lesson 6C 
 * 
 * This code will analyze the NBA basketball data and create a table showing
 * the players with the top 10 career points.
 * 
 * Note about columns:
 * - Player ID is in column 0
 * - Points is in column 8
 * 
 * Each row represents the player's stats for a single season with a single team.
 */

using Microsoft.VisualBasic.FileIO;

public class Basketball
{
    public static void Run()
    {
        var players = new Dictionary<string, int>();

        using var reader = new TextFieldParser("basketball.csv");
        reader.TextFieldType = FieldType.Delimited;
        reader.SetDelimiters(",");
        reader.ReadFields(); // ignore header row
        while (!reader.EndOfData)
        {
            var fields = reader.ReadFields()!;
            var playerId = fields[0];
            var points = int.Parse(fields[8]);

            if (players.ContainsKey(playerId))
            {
                players[playerId] += points;
            }
            else
            {
                players[playerId] = points;
            }
        }

        Console.WriteLine($"Players: {{{string.Join(", ", players)}}}");

        var topPlayers = players
            .OrderByDescending(pair => pair.Value)
            .Take(10)
            .Select(pair => pair.Key)
            .ToArray();

        Console.WriteLine("Rank  Player ID    Total Points");
        for (int i = 0; i < topPlayers.Length; i++)
        {
            string id = topPlayers[i];
            Console.WriteLine($"{i + 1,-6}{id,-12}{players[id]}");
        }
    }
}