using System;
using System.Numerics;
using System.Drawing;

using AStarSharp;

internal class Program
{
    static void Main(string[] args)
    {
        Node node = new Node(new Vector2(0, 0), true);
        Image map = Image.FromFile("maze.png");
        Console.WriteLine(map);
    }
}
