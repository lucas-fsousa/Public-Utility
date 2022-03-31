#ifndef SHARED_LIB_H
#define SHARED_LIB_H

#include <iostream>
#include <string>
#include <cstdlib>
#include <cstdio>
#include <cstring>
#include <array>
#include <windows.h>

using namespace std;

#ifdef __cplusplus
    extern "C" {
#endif

#ifdef BUILD_MY_DLL
    #define SHARED_LIB __declspec(dllexport)
#else
    #define SHARED_LIB __declspec(dllimport)
#endif

void SHARED_LIB GetDesktopResolution(char *newStr);

#ifdef __cplusplus
    }

#endif
#endif