//#define CATCH_CONFIG_MAIN
//#include "../../../OOP/catch/catch.hpp"
//#include "../1lw/TriangleType.hpp"
//#include <sstream>
//#include "locale"
//
//
//
//TEST_CASE("Test With good parametrs")
//{
//	setlocale(LC_ALL, "RUS");
//	std::string str = "2 3 4\n 5 6 7\n 7 8 9\n";
//	std::istringstream sstrm(str);
//	double a, b, c;
//	while (sstrm >> a && sstrm >> b && sstrm >> c)
//	{
//		std::string result = TriangleType(a, b, c);
//		REQUIRE(result == "Обычный");
//	}
//}
//
//TEST_CASE("Test with ravnobedr parametrs")
//{
//	setlocale(LC_ALL, "RUS");
//	std::string str = "2 2 3\n 5 5 7\n 7 7 9\n";
//	std::istringstream sstrm(str);
//	double a, b, c;
//	while (sstrm >> a && sstrm >> b && sstrm >> c)
//	{
//		std::string result = TriangleType(a, b, c);
//		REQUIRE(result == "Равнобедренный");
//	}
//}
//
//TEST_CASE("Test with ravnostoron parametrs")
//{
//	setlocale(LC_ALL, "RUS");
//	std::string str = "2 2 2\n 5 5 5\n 7 7 7\n";
//	std::istringstream sstrm(str);
//	double a, b, c;
//	while (sstrm >> a && sstrm >> b && sstrm >> c)
//	{
//		std::string result = TriangleType(a, b, c);
//		REQUIRE(result == "Равносторонний");
//	}
//}
//
//TEST_CASE("Test with bad parametrs")
//{
//	setlocale(LC_ALL, "RUS");
//	std::string str = "ab ab ab\n ac ac ab\n c b d\n 2 2 10\n 5 5 100\n 300 300 3";
//	std::istringstream sstrm(str);
//	double a, b, c;
//	while (sstrm >> a && sstrm >> b && sstrm >> c)
//	{
//		std::string result = TriangleType(a, b, c);
//		REQUIRE(result == "Не треугольник");
//	}
//}
#include <iostream>
#include <fstream>
#include <string>
#include <locale>
#include <sstream>
#include <vector>
#include <Windows.h>
#include "../1lw/TriangleType.hpp"

int main()
{
	setlocale(LC_ALL, "ru_RU_cp1251");

	std::ifstream fileInput("test_case.txt");
	std::ofstream fileOutput("result.txt");
	//очистка временного файла
	std::ofstream clearFile("temp.txt", std::ios::trunc);
	clearFile.close();  // Закрываем файл после очистки

	if (!fileInput)
	{
		std::cout << "Файл не открыт" << std::endl;
		return 1;
	}

	std::vector<std::string> inputResults;
	std::string str, str1;

	while (getline(fileInput, str))
	{
		std::istringstream sstrm(str);
		std::string a, b, c;
		sstrm >> a >> b >> c >> str;
		while (sstrm >> str1)
		{
			str += " " + str1;
		}
		inputResults.push_back(str);
		std::string command = "D:\\source\\repos\\QC\\1lw\\x64\\Debug\\1lw.exe " + a + " " + b + " " + c + " >> temp.txt";
		system(command.c_str());
	}

	std::ifstream fileTemp("temp.txt");
	size_t index = 0;

	while (getline(fileTemp, str))
	{
		if (str == inputResults[index])
		{
			fileOutput << "success" << std::endl;
		}
		else
		{
			fileOutput << "error" << std::endl;
		}
		index++;
	}
	return 0;
}