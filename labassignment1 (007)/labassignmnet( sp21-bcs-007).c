#include <stdio.h>
#include <ctype.h>
#include <string.h>

void keyword_identifier(char str[20]) {
    char keywords[11][10] = {"for", "while", "do", "int", "float", "char", "double", "static", "switch", "case", "if"};
    int i, flag = 0;

    for (i = 0; i < 11; ++i) {
        if (strcmp(keywords[i], str) == 0) {
            printf("\n%s is a keyword", str);
            flag = 1;
            break;
        }
    }
    if (flag == 0 && strcmp("", str) != 0) 
        printf("\n%s is an identifier", str);
}

void operator(char c) {
    if (c == '+' || c == '-' || c == '*' || c == '/' || c == '%')
        printf("\n%c is an arithmetic operator", c);
    else if (c == '=' || c == '>' || c == '<' || c == '!')
        printf("\n%c is a logical operator", c);
}

int main() {
    FILE *f1, *f2, *f3;
    char c, str[20], st1[10];
    int num[100], lineno = 0, tokenvalue = 0, i = 0, j = 0, k = 0;
    int last_was_space = 0; 

    printf("\nEnter the C program:\n");
    f1 = fopen("input", "w");

    while ((c = getchar()) != EOF)
        putc(c, f1);

    fclose(f1);
    f1 = fopen("input", "r");
    f2 = fopen("identifier", "w");
    f3 = fopen("specialchar", "w");

    while ((c = getc(f1)) != EOF) {
        if (isdigit(c)) {
            tokenvalue = c - '0';
            c = getc(f1);
            while (isdigit(c)) {
                tokenvalue = tokenvalue * 10 + c - '0';
                c = getc(f1);
            }
            num[i++] = tokenvalue;
            ungetc(c, f1);
        } else if (isalpha(c)) {
            putc(c, f2);
            c = getc(f1);
            while (isdigit(c) || isalpha(c) || c == '_' || c == '$') {
                putc(c, f2);
                c = getc(f1);
            }
            putc(' ', f2);
            ungetc(c, f1);
        } else if (c == ' ' || c == '\t') {
            last_was_space = 1;
        } else if (c == '\n') {
            lineno++;
        } else if (c == '+' || c == '-' || c == '*' || c == '/' || c == '%') {
            operator(c);
            putc(c, f3);
            last_was_space = 0; 
        } else {
            if (!last_was_space) { 
                putc(' ', f2); 
            }
            putc(c, f3);
            operator(c);
            last_was_space = 0; 
        }
    }

    fclose(f2);
    fclose(f3);
    fclose(f1);

    printf("\nThe identifiers are:\n");
    f2 = fopen("identifier", "r");
    k = 0;
    while ((c = getc(f2)) != EOF) {
        if (c != ' ') {
            str[k++] = c;
        } else {
            str[k] = '\0';
            keyword_identifier(str);
            k = 0;
        }
    }
    fclose(f2);

    f3 = fopen("specialchar", "r");
    printf("\nSpecial characters are: ");
    while ((c = getc(f3)) != EOF)
        printf("%c", c);

    printf("\nTotal no. of lines are: %d\n", lineno);

    fclose(f3);

    return 0;
}

