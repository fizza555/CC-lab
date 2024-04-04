#include <iostream>
#include <string>
using namespace std;

const int MAX = 100;

class Node {
    string identifier, scope, type;
    int lineNo;
    Node* next;

public:
    Node() {
        next = NULL;
    }

    Node(string key, string value, string type, int lineNo) {
        this->identifier = key;
        this->scope = value;
        this->type = type;
        this->lineNo = lineNo;
        next = NULL;
    }

    void print() {
        cout << "Identifier's Name: " << identifier << "\nType: " << type << "\nScope: " << scope << "\nLine Number: " << lineNo << endl;
    }

    friend class SymbolTable;
};

class SymbolTable {
    Node* head[MAX];

public:
    SymbolTable() {
        for (int i = 0; i < MAX; i++)
            head[i] = NULL;
    }

    int hashf(string id);
    bool insert(string id, string scope, string Type, int lineno);
    string find(string id);
    bool deleteRecord(string id);
    bool modify(string id, string scope, string Type, int lineno);
};

int SymbolTable::hashf(string id) {
    int asciiSum = 0;
    for (int i = 0; i < id.length(); i++)
        asciiSum = asciiSum + id[i];
    return (asciiSum % 100);
}

bool SymbolTable::modify(string id, string s, string t, int l) {
    int index = hashf(id);
    Node* start = head[index];
    if (start == NULL)
        return false;
    while (start != NULL) {
        if (start->identifier == id) {
            start->scope = s;
            start->type = t;
            start->lineNo = l;
            return true;
        }
        start = start->next;
    }
    return false;
}

bool SymbolTable::deleteRecord(string id) {
    int index = hashf(id);
    Node* tmp = head[index];
    Node* par = head[index];
    if (tmp == NULL)
        return false;
    if (tmp->identifier == id && tmp->next == NULL) {
        tmp->next = NULL;
        delete tmp;
        return true;
    }
    while (tmp->identifier != id && tmp->next != NULL) {
        par = tmp;
        tmp = tmp->next;
    }
    if (tmp->identifier == id && tmp->next != NULL) {
        par->next = tmp->next;
        tmp->next = NULL;
        delete tmp;
        return true;
    }
    else {
        par->next = NULL;
        tmp->next = NULL;
        delete tmp;
        return true;
    }
    return false;
}

string SymbolTable::find(string id) {
    int index = hashf(id);
    Node* start = head[index];
    if (start == NULL)
        return "-1";
    while (start != NULL) {
        if (start->identifier == id) {
            start->print();
            return start->scope;
        }
        start = start->next;
    }
    return "-1";
}

bool SymbolTable::insert(string id, string scope, string Type, int lineno) {
    int index = hashf(id);
    Node* p = new Node(id, scope, Type, lineno);
    if (head[index] == NULL) {
        head[index] = p;
        cout << "\n" << id << " inserted";
        return true;
    }
    else {
        Node* start = head[index];
        while (start->next != NULL)
            start = start->next;
        start->next = p;
        cout << "\n" << id << " inserted";
        return true;
    }
    return false;
}

int main() {
    SymbolTable st;
    string id, scope, Type, check;
    int lineno;
    char choice;
    cout << "**** SYMBOL_TABLE ****\n";

    do {
        cout << "\nEnter Identifier: ";
        cin >> id;
        cout << "Enter Scope: ";
        cin >> scope;
        cout << "Enter Type: ";
        cin >> Type;
        cout << "Enter Line Number: ";
        cin >> lineno;

        if (st.insert(id, scope, Type, lineno))
            cout << " -successfully";
        else
            cout << "\nFailed to insert.\n";

        cout << "\nDo you want to insert another identifier? (Y/N): ";
        cin >> choice;
    } while (choice == 'Y' || choice == 'y');

    cout << "\nEnter the identifier to search: ";
    cin >> id;
    check = st.find(id);
    if (check != "-1")
        cout << "Identifier Is present\n";
    else
        cout << "\nIdentifier Not Present\n";

    cout << "\nEnter the identifier to delete: ";
    cin >> id;
    if (st.deleteRecord(id))
        cout << id << " Identifier is deleted\n";
    else
        cout << "\nFailed to delete\n";

    cout << "\nEnter the identifier to modify: ";
    cin >> id;
    cout << "Enter new Scope: ";
    cin >> scope;
    cout << "Enter new Type: ";
    cin >> Type;
    cout << "Enter new Line Number: ";
    cin >> lineno;
    if (st.modify(id, scope, Type, lineno))
        cout << "\n" << id << " Identifier updated\n";

    cout << "\nEnter the identifier to search: ";
    cin >> id;
    check = st.find(id);
    if (check != "-1")
        cout << "Identifier Is present\n";
    else
        cout << "\nIdentifier Not Present";

    return 0;
}

