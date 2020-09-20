#pragma once

#include <iostream>
#include <vector>
#include <fstream>
#define eps 1e-17

class CSLR
{
private:
	std::vector<double> di;
	std::vector<double> altr;
	std::vector<double> autr;
	std::vector<int> jptr;
	std::vector<int> iptr;

public:
	CSLR();

	CSLR(const std::vector<std::vector<double>>& data);

	// получение размера разреженной матрицы
	int get_size() const;

	/*
		бинарные файлы для формата cslr: size.bin, di.bim, autr.bin, altr.bin, jptr.bin, iptr.bin
		size.bin: размер матрицы (тип int)
		N
		di.bin: диагональ (тип double)
		A1 A2 ... AN
		autr.bin: верхний треугольник (тип double)
		A1 A2 ... AM
		altr.bin: нижний треугольник (тип double)
		A1 A2 ... AM
		jptr.bin: индексный массив столбцов/строк (тип int)
		I1 I2 ... IM
		iptr.bin: индексный массив начала строк/столбцов (тип int)
		I1 I2 ... I(N + 1)

		ВАЖНО: индексы в файле jptr начинаются с 1
	*/
	// чтение данных из бинарных файлов для разреженной матрицы в формате cslr
	void ReadFromBinFile(const std::string& path);

	// перевод разреженной матрицы из формата clsr в плотный формат
	std::vector<std::vector<double>> ConvertToTightFormat() const;

	// умножение разреженной матрицы в формате cslr на вектор
	std::vector<double> MultiplyOnVector(const std::vector<double>& x) const;

	// процедура из задания 5.1
	void FirstIterativeProcedure() const;

	// процедура из задания 5.2
	void SecondIterativeProcedure() const;
	
	// перегрузка оператора () - доступ к элементу (row, column) разреженной матрицы в формате cslr
	const double& operator()(const int& row, const int& column) const;
};

// умножение разреженной матрицы в плотном формате на вектор
std::vector<double> MultiplyTightFormat(const std::vector<std::vector<double>>& A, const std::vector<double>& x);

// вычисление евклидовой нормы
double EuclideanNorm(const std::vector<double>& x);