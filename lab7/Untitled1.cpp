#include <iostream>
#include <vector>
using namespace std;

int main() {
  
    char terminals[] = {'p', 'q', 'r', 's'};

  
    char nonTerminals[] = {'Z', 'Y', 'X', 'D'};

   
    char productions[][5] = {
        {'Z', 'q', 'Z', ' ', ' '},
        {'Y', 'r', 'D', ' ', ' '},
        {'X', 'p', 'Y', ' ', ' '},
        {'D', 's', ' ', ' ', ' '}
    };

   
    cout << "Terminal Symbols: ";
    for (int i = 0; i < sizeof(terminals) / sizeof(terminals[0]); ++i) {
        cout << terminals[i] << " ";
    }
    cout << endl;

   
    cout << "Non-Terminal Symbols: ";
    for (int i = 0; i < sizeof(nonTerminals) / sizeof(nonTerminals[0]); ++i) {
        cout << nonTerminals[i] << " ";
    }
    cout << endl;

   
    cout << "Production Rules:" << endl;
    for (int i = 0; i < 4; ++i) {
        cout << productions[i][0] << " -> ";
        for (int j = 1; j < 5; ++j) {
            if (productions[i][j] != ' ') {
                cout << productions[i][j];
            }
        }
        cout << endl;
    }

    return 0;
}

