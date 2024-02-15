#include "iostream"

std::string TriangleType(double a, double b, double c)
{
    if (a < 0 || b < 0 || c < 0) {
        return "Неизвестная ошибка";
    }

    if (a == 0 || b == 0 || c == 0) {
        return "Не треугольник";
    }

    if (a + b > c && a + c > b && b + c > a)
    {
        if (a == b && b == c)
        {
            return "Равносторонний";
        }
        else if (a == b || a == c || b == c)
        {
            return "Равнобедренный";
        }
        else
        {
            return "Обычный";
        }
    }
    else
    {
        return "Не треугольник";
    }
}
