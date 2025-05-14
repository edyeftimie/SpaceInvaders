using System.Collections.Generic;

public interface IFireStrategy {
   public Collection<Bullet> fire (int x, int y, int damage, string bulletStrategyType, Character source); 
}