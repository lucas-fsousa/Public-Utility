#include "shared_lib.h"

#define STRING(num)

void GetDesktopResolution(char *newStr){
    int width = GetSystemMetrics(SM_CXSCREEN);
    int height = GetSystemMetrics(SM_CYSCREEN);
    string x = to_string(width);
    string y = to_string(height);
    string xy = x + ";" + y;
    strcpy(newStr, xy.c_str());
}

int main(){
    char str[20];
    GetDesktopResolution(str);
    cout << str;
    return 0;
}