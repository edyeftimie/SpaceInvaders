using System.Collections.Generic;

public interface IFireStrategy {
   public BulletCollection fire (int x, int y, int damage, string bulletStrategyType, Character source); 
}