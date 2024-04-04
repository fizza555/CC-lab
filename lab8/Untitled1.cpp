#include <iostream>
#include <string>

using namespace std;

bool check(const string& str) {
    int state = 0;
    for (int i = 0; i < str.length(); ++i) {
        char c = str[i];
        switch (state) {
            case 0:
                if (isalpha(c) || c == '_')
                    state = 1;
                else
                    return false;
                break;
            case 1:
                if (isalnum(c) || c == '_')
                    state = 1;
                else
                    return false;
                break;
        }
    }
    return state == 1;
}

int main() {
    string variableName;
    cout << "Enter a variable name: ";
    cin >> variableName;
    
    if (check(variableName))
        cout << "Its valid" << endl;
    else
        cout << "Invalid " << endl;
    
    return 0;
}

