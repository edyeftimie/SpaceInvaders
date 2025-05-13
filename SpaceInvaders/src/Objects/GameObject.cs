using Microsoft.Xna.Framework.Graphics;

public class GameObject {
    public int x {get; set;}
    public int y {get; set;}
    public int width {get;}
    public int height {get;}
    public Texture2D Texture {get;}
    public int middleX { 
        get { return x + width/2; }
    }
    public int middleY {
        get { return y + height/2; }
    }

    public GameObject (int x, int y, int width, int height, Texture2D Texture) {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        this.Texture = Texture;
        // this.middleX = this.x + width/2;
        // this.middleY = this.y + height/2;
    } 

    public bool move (int x, int y) {
        //validator todo
        this.x += x;
        this.y += y;
        return true;
    }
}