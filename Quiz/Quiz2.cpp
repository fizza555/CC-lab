#include <iostream>
#include <string>

using namespace std;

bool parseG(const string& input) {
    if (input.empty()) {
        cout << "Accepted." << endl;
        return true;
    }

    char currentFloor = 'G';
    int upCount = 0;
    int downCount = 0;

    for (size_t i = 0; i < input.size(); ++i) {
        char c = input[i];
        if (c == 'u') {
            if (currentFloor == 'L' || (upCount == 3 && c == 'u')) {
                cout << "Rejected." << endl;
                return false;
            }
            currentFloor++;
            upCount++;
        } else if (c == 'd') {
            if (currentFloor == 'G' || (downCount == 3 && c == 'd')) {
                cout << "Rejected." << endl;
                return false;
            }
            currentFloor--;
            downCount++;
        } else {
            cout << "Invalid character: " << c << endl;
            return false;
        }
    }

    cout << "Accepted." << endl;
    return true;
}

int main() {
    string input;
    cout << "Enter input string: ";
    cin >> input;

    if (input.length() > 6) {
        cout << "Rejected: More than 3 floor movements." << endl;
        return 0;
    }

    if (parseG(input)) {
        cout << "Input string is accepted." << endl;
    } else {
        cout << "Input string is rejected." << endl;
    }

    return 0;
}

