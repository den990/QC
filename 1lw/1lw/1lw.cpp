#include <iostream>
#include <fstream>
#include <locale>
#include "TriangleType.hpp"
#include <stdlib.h>

int main(int argc, char* argv[])
{
    setlocale(LC_ALL, "RUS");

    if (argc != 4) {
        std::cout << "Неверное количество аргументов. Используйте triangle.exe a b c." << std::endl;
        return 1;
    }

    double a = atof(argv[1]);
    double b = atof(argv[2]);
    double c = atof(argv[3]);

    std::string result = TriangleType(a, b, c);
    std::cout << result << std::endl;
}

