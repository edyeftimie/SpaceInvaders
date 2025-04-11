#include <iostream>
#include <list>

#include <conio.h>

#include <thread>
#include <chrono>

using namespace std;

class Object;
class Data;
class Player;
class Bullet;
class Screen;
class Input;
class Enemy;
class Logic;

class Object {
public:
    int positionX;
    int positionY;
    char** image;
    int dimX;
    int dimY;
    int dmg;
    int hp;
    Object() {
        dmg = 1;
    }
    Object(int dimX, int dimY) :Object() {
        this->dimX = dimX;
        this->dimY = dimY;
        image = new char* [dimX];
        for (int i = 0; i < dimX; i++) {
            image[i] = new char[dimY];
        }
        for (int i = 0; i < dimX; i++) {
            for (int j = 0; j < dimY; j++) {
                image[i][j] = ' ';
            }
        }
    }
    void move(int x, int y);
    virtual void hit() {}
};

class Data {
public:
    static const int PLAYER = 1;
    static const int ENEMY = 2;
    static const int BULLET = 3;
    list<Object*> lista;
    list<Bullet*> bullets;
    Player* player;
    int dimScreenX;
    int dimScreenY;
    Data(int x, int y) {
        dimScreenX = x;
        dimScreenY = y;
    }
    void add(Object* object, int type);
};

class Player :public Object {
public:
    Player(int x, int y, int dimX, int dimY) :Object(dimX, dimY) {
        positionX = x;
        positionY = y;
        hp = 3;
        image[0][1] = '#';
        for (int i = 0; i < 3; i++) {
            image[1][i] = '#';
        }
    }
    void fire(Data* data);
};

class Bullet :public Object {
public:
    int dmg;
    Bullet(Player* player, int dmg, int dimX, int dimY) :Object(dimX, dimY) {
        positionX = player->positionX;
        positionY = player->positionY + 1;
        this->dmg = dmg;
        image[0][0] = '*';
    }

};

class Screen {
public:
    int x;
    int y;
    Data* data;
    char** mat;
    char* toScreen;
    Screen(Data* data) {
        this->data = data;
        this->x = data->dimScreenX;
        this->y = data->dimScreenY;
        toScreen = new char[x * (y + 1) + 1];
        mat = new char* [x];
        for (int i = 0; i < x; i++) {
            mat[i] = new char[y];
        }
    }
    void update();
};

void Data::add(Object* object, int type) {
    if (type == BULLET) {
        bullets.push_front((Bullet*)object);
        return;
    }
    if (type == PLAYER) {
        player = (Player*)object;
        return;
    }
    lista.push_front(object);
}

void Screen::update() {
    //TODO: refactor code -> use one list to draw
    for (int i = 0; i < x; i++) {
        for (int j = 0; j < y; j++) {
            mat[i][j] = '.';
        }
    }
    for (Object* obj : data->lista) {
        for (int i = 0; i < obj->dimX; i++) {
            for (int j = 0; j < obj->dimY; j++) {
                mat[obj->positionX + i][obj->positionY + j] = obj->image[i][j];
            }
        }
    }
    for (Object* obj : data->bullets) {
        for (int i = 0; i < obj->dimX; i++) {
            for (int j = 0; j < obj->dimY; j++) {
                mat[obj->positionX + i][obj->positionY + j] = obj->image[i][j];
            }
        }
    }
    for (int i = 0; i < data->player->dimX; i++) {
        for (int j = 0; j < data->player->dimY; j++) {
            mat[data->player->positionX + i][data->player->positionY + j] = data->player->image[i][j];
        }
    }
    int k = 0;
    for (int i = 0; i < x; i++) {
        for (int j = 0; j < y; j++) {
            toScreen[k++] = mat[i][j];
        }
        toScreen[k++] = '\n';
    }
    toScreen[k] = '\0';
    system("cls");
    cout << toScreen;
}

class Input {
public:
    Data* data;
    Input(Data* data) {
        this->data = data;
    }
    void update();
    int kbhit();
};



void Input::update() {
    if (_kbhit()) {
        char key = getch();
        if (key == 'a') {
            data->player->positionY--;
        }
        if (key == 'd') {
            data->player->positionY++;
        }
        if (key == ' ') {
            data->player->fire(data);
        }
    }
}

void Object::move(int x, int y) {
    positionX = x;
    positionY = y;
}

class Enemy :public Object {
public:
    Enemy(int x, int y, int dimX, int dimY) :Object(dimX, dimY) {
        cout << dimX << ' ' << dimY << endl;
        hp = 1;
        image[0][0] = '@';
        for (int i = 0; i < dimY; i++) {
            image[1][i] = '@';
        }
        positionX = x;
        positionY = y;
    }
    void hit(int dmg);
    ~Enemy() {
        for (int i = 0; i < dimX; i++) {
            delete[] image[i];
        }
        delete[] image;
    }
};

void Enemy::hit(int dmg) {
    hp -= dmg;
}


void Player::fire(Data* data) {
    data->add(new Bullet(this, this->dmg, 1, 1), Data::BULLET);
}


class Logic {
private:
    bool pointBetween(Object* a, int x, int y);
public:
    Data* data;
    Logic(Data* data) {
        this->data = data;
    }
    void update();
    bool collisionDetected(Object* a, Object* b);
    void endGame();
};

void Logic::update() {
    auto obj = data->lista.begin();
    while (obj != data->lista.end()) {
        (*obj)->positionX++;
        if ((*obj)->positionX + (*obj)->dimX >= data->dimScreenX) {
            obj = data->lista.erase(obj);

        }
        else if (collisionDetected((*obj), data->player)) {
            endGame();
        }
        ++obj;
    }
    auto bullet = data->bullets.begin();
    while (bullet != data->bullets.end()) {
        (*bullet)->positionX--;
        if ((*bullet)->positionX < 0)
            bullet = data->bullets.erase(bullet);
        auto obj = data->lista.begin();
        while (obj != data->lista.end()) {
            if (collisionDetected((*bullet), (*obj))) {
                cout << "BULLET HIT" << endl;
                ((Enemy*)(*obj))->hit((*bullet)->dmg);
                if ((*obj)->hp == 0) {
                    obj = data->lista.erase(obj);
                }
                bullet = data->bullets.erase(bullet);
            }
            ++obj;
        }
        ++bullet;
    }
}

bool Logic::collisionDetected(Object* a, Object* b) {
    return (
        pointBetween(a, b->positionX, b->positionY) or
        pointBetween(a, b->positionX + b->dimX - 1, b->positionY) or
        pointBetween(a, b->positionX, b->positionY + b->dimY - 1) or
        pointBetween(a, b->positionX + b->dimX - 1, b->positionY + b->dimY - 1));

}

bool Logic::pointBetween(Object* a, int x, int y) {
    return (a->positionX <= x and x < (a->positionX + a->dimX) and a->positionY <= y and y < (a->positionY + a->dimY));
}

void Logic::endGame() {
    exit(0);
}

int main() {
    Data data(30, 30);
    data.add(new Enemy(0, 15, 2, 2), Data::ENEMY);
    data.add(new Player(28, 15, 2, 3), Data::PLAYER);
    Screen screen(&data);
    Logic logic(&data);
    Input input(&data);
    while (true) {
        this_thread::sleep_for(chrono::milliseconds(100));
        logic.update();
        screen.update();
        input.update();
    }
    return 0;
}
