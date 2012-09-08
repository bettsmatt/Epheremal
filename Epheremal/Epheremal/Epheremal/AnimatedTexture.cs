using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Epheremal.Model;
using Epheremal.Assets;
using Epheremal.Model.Levels;

using System.Diagnostics;

public class AnimatedTexture
{
    private int framecount;
    private float TimePerFrame;
    public static int Frame;
    private float TotalElapsed;
    private bool Paused;

    public AnimatedTexture(int FrameCount, int FramesPerSec)
    {
        framecount = FrameCount;
        TimePerFrame = (float)1 / FramesPerSec;
        Frame = 0;
        TotalElapsed = 0;
        Paused = false;
    }

    public void UpdateFrame(float elapsed)
    {
        if (Paused)
            return;
        TotalElapsed += elapsed;
        if (TotalElapsed > TimePerFrame)
        {
            Frame++;
            // Keep the Frame between 0 and the total frames, minus one.
            Frame = Frame % framecount;
            TotalElapsed -= TimePerFrame;
        }
    }

    public bool IsPaused
    {
        get { return Paused; }
    }
    public void Reset()
    {
        Frame = 0;
        TotalElapsed = 0f;
    }
    public void Stop()
    {
        Pause();
        Reset();
    }
    public void Play()
    {
        Paused = false;
    }
    public void Pause()
    {
        Paused = true;
    }

    public int GetFrame()
    {
        return Frame;
    }

}