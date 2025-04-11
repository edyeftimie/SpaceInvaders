Design patterns:
    - game loop pattern:
    - component pattern
        -> spaceship
        -> enemies
        -> bullets
        -> ability boxes
    - state pattern:
        -> game states (start, palying, game over)
        -> enemy states (moving, shooting, destroyed)
        -> bullet states (created, moving, hit something)
    - factory pattern:
        -> differen types of enemies
        -> bullet variations
        -> ability power-ups
    - observer pattern:
        -> tracking score
        -> handling collisions
        -> managing game events
    - singleton pattern:
        -> game manager
        -> score keeper
    - strategy pattern:
        -> shooting strategies (single, triple)
        -> movement strategies (straight down, zig-zag, diagonal)
    - decorator pattern:
        -> armour to enemy
        -> speed boost 
Object:
    position x
    position y
    image
    dim x
    dim y
    health
    deal dmg

Bullet -> Object:
    health
    dmg
    bullet strategy

Bullet factory:
    create bullet(type)
    _bullettype.kdfsl
    _bullettype.dsjflka

BulletStrategy:
    speed
    update()

StraightBulletStrategy : BulletStrategy
    target
    update
ZigZagBulletStrategy : BulletStrategy
    timeBeforeSwitch
    update
DiagonalBulletStrategy: BulletStrategy
    spreadAngle
    update

IFire:
    fire
Player -> Object, IFire:
    singleton
Enemy -> Object, IFire:
    hit(dmg)
    ~
