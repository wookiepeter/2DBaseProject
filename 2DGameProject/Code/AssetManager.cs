﻿using System.Collections.Generic;
using SFML.Graphics;

public class AssetManager
{
    static Dictionary<TextureName, Texture> textures = new Dictionary<TextureName, Texture>();

    public static Texture GetTexture(TextureName textureName)
    {
        if (textures.Count == 0)
        {
            LoadTextures(); 
        }
        return textures[textureName];
    }

    static void LoadTextures()
    {
        textures.Add(TextureName.WhitePixel, new Texture("Textures/pixel.png"));
        textures.Add(TextureName.MainMenuBackground, new Texture("Textures/MainMenu_Background.jpg"));
        textures.Add(TextureName.Crop, new Texture("Textures/Plant1.png"));

    }

    public enum TextureName
    {
        WhitePixel,
        MainMenuBackground,
        Crop,
    }
}
