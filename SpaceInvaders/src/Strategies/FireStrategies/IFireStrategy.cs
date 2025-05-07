using System.Collections.Generic;

public interface IFireStrategy {
   public List<Bullet> fire (int x, int y, int damage, IBulletStrategy bulletStrategy, Character source); 
}