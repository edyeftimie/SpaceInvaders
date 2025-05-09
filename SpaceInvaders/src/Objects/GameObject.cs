using Microsoft.Xna.Framework.Graphics;

public class GameObject {
    public int x {get; set;}
    public int y {get; set;}
    public int width {get;}
    public int height {get;}
    public Texture2D Texture {get;}

    public GameObject (int x, int y, int width, int height, Texture2D Texture) {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        this.Texture = Texture;
    } 

    public bool move (int x, int y) {
        //validator todo
        this.x += x;
        this.y += y;
        return true;
    }
}