using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;

public class AnimatedSprite : Sprite
{
    public float secondsPerFrame { get; private set; }
    public Vector2i spriteSize { get; private set; }
    public int frameCount { get; private set; }
    Vector2i upperLeftCorner;

    float timer;
    bool pause;

    public AnimatedSprite(Texture spriteSheet, float secondsPerFrame, int frameCount, Vector2i spriteSize)
        : this(spriteSheet, secondsPerFrame, frameCount, spriteSize, new Vector2i(0, 0))
    {
    }

    public AnimatedSprite(Texture spriteSheet, float secondsPerFrame, int frameCount, Vector2i spriteSize, Vector2i upperLeftCorner)
        : base(spriteSheet)
    {
        this.secondsPerFrame = secondsPerFrame;
        this.frameCount = frameCount;
        this.spriteSize = spriteSize;
        this.upperLeftCorner = upperLeftCorner;
        RestartAnimation();
    }

    /// <summary>start or restart the animation</summary>
    public void RestartAnimation()
    {
        timer = 0F;
        pause = false;
    }

    public void PauseAnimation()
    {
        pause = true;
    }

    public void ResumeAnimation()
    {
        pause = false;
    }


    public Sprite UpdateFrame(float deltaTime)
    {
        if (!pause)
        {
            timer = (timer + deltaTime) % ((float)frameCount * secondsPerFrame);
        }

        int currentFrame = (int)(timer / secondsPerFrame);
        
        TextureRect = new IntRect(upperLeftCorner.X + (currentFrame * spriteSize.X), upperLeftCorner.Y, spriteSize.X, spriteSize.Y);
        return this;

    }
}