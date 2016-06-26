using System.Collections.Generic;
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
        textures.Add(TextureName.EndScreen, new Texture("Textures/EndScreen.jpg"));
        textures.Add(TextureName.Crop, new Texture("Textures/Plant1.png"));
        textures.Add(TextureName.Farmer1Running, new Texture("Textures/farmer01_running.png"));
        textures.Add(TextureName.Farmer1Jumping, new Texture("Textures/farmer01_jumping.png"));
        textures.Add(TextureName.Farmer2Running, new Texture("Textures/farmer02_running.png"));
        textures.Add(TextureName.Farmer2Jumping, new Texture("Textures/farmer02_jumping.png"));


    }

    public enum TextureName
    {
        WhitePixel,
        MainMenuBackground,
        EndScreen,
        Crop,
        Farmer1Running,
        Farmer1Jumping,
        Farmer2Running,
        Farmer2Jumping,
    }
}
