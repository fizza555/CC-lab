#include <iostream>
#include <vector>
#include <cstdlib>
#include <ctime>

#ifdef _WIN32
#include <windows.h>
#else
#include <unistd.h>
#endif

class Entity {
public:
    virtual void update() = 0;
};

class CopCar : public Entity {
public:
    int x, y;
    CopCar() : x(5), y(5) {}

    void moveUp() { if (y > 0) y--; }
    void moveDown() { if (y < 9) y++; }  // Assuming screen height is 10
    void moveLeft() { if (x > 0) x--; }
    void moveRight() { if (x < 9) x++; } // Assuming screen width is 10
    void shoot() { std::cout << "Shooting!\n"; }

    void update() { // Removed 'override' for compatibility
        // Implement movement and shooting logic based on input
    }
};

class Criminal : public Entity {
public:
    int x, y;
    Criminal(int startX) : x(startX), y(0) {}

    void update() { // Removed 'override' for compatibility
        y++;
        if (y >= 10) { // Assuming screen height is 10 for simplicity
            std::cout << "Criminal caught. Game Over!\n";
            exit(0);
        }
    }
};

class Game {
public:
    CopCar copCar;
    std::vector<Criminal> criminals;
    double speed;
    int criminalSpawnRate;
    int maxCriminals;

    Game(double spd, int spawnRate, int maxCrim)
        : speed(spd), criminalSpawnRate(spawnRate), maxCriminals(maxCrim) {
        std::srand(std::time(0));
    }

    void spawnCriminal() {
        if (criminals.size() < maxCriminals) {
            criminals.push_back(Criminal(std::rand() % 10));
        }
    }

    void update() {
        copCar.update();
        for (int i = 0; i < criminals.size(); ++i) {
            criminals[i].update();
        }

        if (std::rand() % 100 < criminalSpawnRate) {
            spawnCriminal();
        }

        speed += 0.01; // Increase speed over time
    }

    void draw() {
        // Clear the screen
#ifdef _WIN32
        system("cls");
#else
        system("clear");
#endif

        char screen[10][10] = { ' ' };

        // Place cop car
        screen[copCar.y][copCar.x] = 'C';

        // Place criminals
        for (int i = 0; i < criminals.size(); ++i) {
            screen[criminals[i].y][criminals[i].x] = 'X';
        }

        // Draw screen
        for (int i = 0; i < 10; ++i) {
            for (int j = 0; j < 10; ++j) {
                std::cout << screen[i][j];
            }
            std::cout << '\n';
        }
    }

    void run() {
        while (true) {
            update();
            draw();
            // Add delay based on speed
#ifdef _WIN32
            Sleep(static_cast<int>(1000 / speed)); // For Windows
#else
            usleep(static_cast<int>(1000000 / speed)); // For Unix/Linux
#endif
        }
    }
};

int main() {
    Game game(1.0, 5, 10);
    game.run();
    return 0;
}

