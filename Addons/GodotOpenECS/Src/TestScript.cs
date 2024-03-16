using Godot;
using System;

public partial class TestScript : Node
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready() { }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    public static int Add(int a, int b)
    {
        return a + b;
    }
}
