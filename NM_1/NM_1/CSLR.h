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

	// ��������� ������� ����������� �������
	int get_size() const;

	/*
		�������� ����� ��� ������� cslr: size.bin, di.bim, autr.bin, altr.bin, jptr.bin, iptr.bin
		size.bin: ������ ������� (��� int)
		N
		di.bin: ��������� (��� double)
		A1 A2 ... AN
		autr.bin: ������� ����������� (��� double)
		A1 A2 ... AM
		altr.bin: ������ ����������� (��� double)
		A1 A2 ... AM
		jptr.bin: ��������� ������ ��������/����� (��� int)
		I1 I2 ... IM
		iptr.bin: ��������� ������ ������ �����/�������� (��� int)
		I1 I2 ... I(N + 1)

		�����: ������� � ����� jptr ���������� � 1
	*/
	// ������ ������ �� �������� ������ ��� ����������� ������� � ������� cslr
	void ReadFromBinFile(const std::string& path);

	// ������� ����������� ������� �� ������� clsr � ������� ������
	std::vector<std::vector<double>> ConvertToTightFormat() const;

	// ��������� ����������� ������� � ������� cslr �� ������
	std::vector<double> MultiplyOnVector(const std::vector<double>& x) const;

	// ��������� �� ������� 5.1
	void FirstIterativeProcedure() const;

	// ��������� �� ������� 5.2
	void SecondIterativeProcedure() const;
	
	// ���������� ��������� () - ������ � �������� (row, column) ����������� ������� � ������� cslr
	const double& operator()(const int& row, const int& column) const;
};

// ��������� ����������� ������� � ������� ������� �� ������
std::vector<double> MultiplyTightFormat(const std::vector<std::vector<double>>& A, const std::vector<double>& x);

// ���������� ���������� �����
double EuclideanNorm(const std::vector<double>& x);