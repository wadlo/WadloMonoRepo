using Godot;
using System;
using System.Diagnostics;

public partial class TestComponentCache : Node
{
    public override void _Ready()
    {
        GD.Print("There should be 4 Node2Ds:");
        GD.Print(ECS.GetChildrenOfType<Node2D>(this).Count);
        GD.Print(ECS.GetInstance().childCache.Keys);
        RunBenchmark();
    }

    /**
    One assumption we make is that every cached operation will be running 1000 times over its lifetime.
    This is roughly 60 times a second for 16 seconds. If something isn't going to run this many times over
    its lifetime, caching should probably be off.
    */
    public void RunBenchmark(int iterations = 1000)
    {
        Stopwatch sw;

        GD.Print("Running cached benchmark");
        GC.Collect();
        sw = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)
        {
            ECS.GetChildrenOfType<Node2D>(this, true);
        }
        GC.Collect();
        sw.Stop();
        GD.Print("Cached benchmark ", sw.ElapsedTicks.ToString());

        GD.Print("Running uncached benchmark");
        GC.Collect();
        sw = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)
        {
            ECS.GetChildrenOfType<Node2D>(this, false);
        }
        GC.Collect();
        sw.Stop();
        GD.Print("Uncached benchmark ", sw.ElapsedTicks.ToString());
    }
}
