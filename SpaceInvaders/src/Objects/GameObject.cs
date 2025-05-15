using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders;

public class GameObject {
    public int x {get; set;}
    public int y {get; set;}
    public int width {get;}
    public int height {get;}
    public Texture2D Texture {get;}
    private IValidator <GameObject> _mapBoundaryValidator;
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
        _mapBoundaryValidator = MapBoundaryValidator.Instance;
    } 


    public bool move (int x, int y) {
        GameObject clone = Clone ();
        clone.forceMove (x, y);
        bool isValidMove = _mapBoundaryValidator.IsValid (clone);

        if (!isValidMove) {
            Logger.Error ("GameObject.move: Move prevented - " + _mapBoundaryValidator.ErrorMessage);
            return false;
        } else {
            forceMove (x, y);
            return true;
        }
        // return isValidMove;
    }

    public void forceMove (int x, int y) {
        this.x += x;
        this.y += y;
    }

    public GameObject Clone () {
        return new GameObject(this.x, this.y, this.width, this. height, this.Texture);
    }
}