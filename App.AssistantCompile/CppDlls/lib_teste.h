#ifndef LIB_TESTE_H
#define LIB_TESTE_H

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

#ifdef MY_DLLIMPORT
#define LIB_TESTE_H __declspec(dllexport)
#else
#define LIB_TESTE_H __declspec(dllimport)
#endif

  void LIB_TESTE_H GetDesktopResolution(char* newStr);

#ifdef __cplusplus
}

#endif
#endif