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
        textures.Add(TextureName.MainMenuBackground, new Texture("Textures/MainMenu_Background.png"));
        textures.Add(TextureName.EndScreen, new Texture("Textures/EndScreen.png"));
        textures.Add(TextureName.Drop, new Texture("Textures/Tropfe.png"));
        textures.Add(TextureName.Crop, new Texture("Textures/Plant1.png"));
        textures.Add(TextureName.Crop2, new Texture("Textures/Plant2.png"));
        textures.Add(TextureName.Crop3, new Texture("Textures/Plant3.png"));
        textures.Add(TextureName.Crop4, new Texture("Textures/Plant4.png"));
        textures.Add(TextureName.Island, new Texture("Textures/Island.png"));
        textures.Add(TextureName.ingameBackGround, new Texture("Textures/background.png"));
        textures.Add(TextureName.Blossom, new Texture("Textures/Blossom.png"));

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
        Drop,
        Crop,
        Crop2,
        Crop3,
        Crop4,
        Island,
        ingameBackGround,
        Blossom,
        
        Farmer1Running,
        Farmer1Jumping,
        Farmer2Running,
        Farmer2Jumping,
    }
}
