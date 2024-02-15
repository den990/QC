#include "iostream"

std::string TriangleType(double a, double b, double c)
{
    if (a < 0 || b < 0 || c < 0) {
        return "����������� ������";
    }

    if (a == 0 || b == 0 || c == 0) {
        return "�� �����������";
    }

    if (a + b > c && a + c > b && b + c > a)
    {
        if (a == b && b == c)
        {
            return "��������������";
        }
        else if (a == b || a == c || b == c)
        {
            return "��������������";
        }
        else
        {
            return "�������";
        }
    }
    else
    {
        return "�� �����������";
    }
}
