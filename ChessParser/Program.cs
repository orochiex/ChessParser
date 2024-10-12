using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class ChessNotationParser
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Введите шахматную нотацию:");
        string input = Console.ReadLine();
        var moves = ParseMoves(input);

        for (int i = 0; i < moves.Count; i++)
        {
            ProcessMove(moves[i], i);
        }
    }

    public static List<string> ParseMoves(string notation)
    {
        var movesList = new List<string>();
        string pattern = @"(\s*([KQRBN]?[a-h]?[1-8]?x?(сх)?[a-h][1-8](\+|#)?|O-O(-O)?))";
        var matches = Regex.Matches(notation, pattern);

        foreach (Match match in matches)
        {
            movesList.Add(match.Groups[2].Value.Trim());
        }

        return movesList;
    }

    public static void ProcessMove(string move, int index)
    {
        string color = index % 2 == 0 ? "белый" : "черный";
        string piece = GetPieceName(move);
        string position = GetPosition(move);
        string note = GetNotation(move);
        string capture = GetCapture(move);

        if (move == "O-O" || move == "O-O-O")
        {
            Console.WriteLine($"Рокировка {color} короля.");
        }
        else
        {
            string description = $"{color} фигура {piece} сходила на позицию {position} {capture}";
            if (!string.IsNullOrEmpty(note))
            {
                description += $" ({note})";
            }
            Console.WriteLine(description + ".");
        }
    }

    public static string GetPieceName(string move)
    {
        if (string.IsNullOrEmpty(move)) return "пешка";

        char pieceChar = move[0];

        switch (pieceChar)
        {
            case 'K': return "король";
            case 'Q': return "королева";
            case 'R': return "ладья";
            case 'N': return "конь";
            case 'B': return "слон";
            default: return "пешка";
        }
    }

    public static string GetPosition(string move)
    {
        return move.Length > 1 ? move[^2..] : move;
    }

    public static string GetNotation(string move)
    {
        if (move.EndsWith("+"))
            return "шах";
        else if (move.EndsWith("#"))
            return "мат";
        return string.Empty;
    }

    public static string GetCapture(string move)
    {
        if (move.Contains("x"))
            return " (взятие)";
        return string.Empty;
    }
}
