#include "CSLR.h"

CSLR::CSLR() {}

CSLR::CSLR(const std::vector<std::vector<double>>& data)
{
	int size = data.size();
	int count_iptr = 0;
	iptr.push_back(1);
	iptr.push_back(1);

	di.push_back(data[0][0]);

	for (int i = 1; i < size; i++)
	{
		di.push_back(data[i][i]);
		count_iptr = 0;

		for (int j = 0; j < i; j++)
		{
			if (data[i][j] != 0.0)
			{
				altr.push_back(data[i][j]);
				autr.push_back(data[j][i]);
				jptr.push_back(j);
				count_iptr++;
			}
		}

		iptr.push_back(iptr[i] + count_iptr);
	}
}

int CSLR::get_size() const
{
	return di.size();
}

void CSLR::ReadFromBinFile(const std::string& path)
{
	std::ifstream in;

	int N = 0;

	in.open(path + "\\size.bin", std::ios::binary);
	if (in.is_open())
	{
		in.read((char*)& N, sizeof(int));

		in.close();
	}
	else
		throw std::exception("File size.bin not found");

	in.open(path + "\\di.bin", std::ios::binary);
	if (in.is_open())
	{
		di.resize(N);

		for (int i = 0; i < N; i++)
			in.read((char *)& di[i], sizeof(double));

		in.close();
	}
	else
		throw std::exception("File di.bin not found");

	in.open(path + "\\iptr.bin", std::ios::binary);
	if (in.is_open())
	{
		iptr.resize(N + 1);

		for (int i = 0; i < N + 1; i++)
			in.read((char *)& iptr[i], sizeof(int));

		in.close();
	}
	else
		throw std::exception("File iptr.bin not found");
	
	int size = iptr[N] - 1;
	autr.resize(size);
	altr.resize(size);
	jptr.resize(size);

	in.open(path + "\\autr.bin", std::ios::binary);
	if (in.is_open())
	{
		for (int i = 1; i < size; i++)
			in.read((char*)& autr[i], sizeof(double));

		in.close();
	}
	else
		throw std::exception("File autr.bin not found");

	in.open(path + "\\altr.bin", std::ios::binary);
	if (in.is_open())
	{
		for (int i = 1; i < size; i++)
			in.read((char*)& altr[i], sizeof(double));

		in.close();
	}
	else
		throw std::exception("File altr.bin not found");

	in.open(path + "\\jptr.bin", std::ios::binary);
	if (in.is_open())
	{
		int index;

		for (int i = 0; i < size; i++)
		{
			in.read((char*)& index, sizeof(int));
			jptr[i] = index - 1;
		}

		in.close();
	}
	else
		throw std::exception("File jptr.bin not found");
}

std::vector<std::vector<double>> CSLR::ConvertToTightFormat() const
{
	int size = di.size();
	std::vector<std::vector<double>> tight_format_matrix(size, std::vector<double>(size));

	tight_format_matrix[0][0] = di[0];

	for (int row = 1; row < size; row++)
	{
		tight_format_matrix[row][row] = di[row];

		for (int j = iptr[row] - 1; j < iptr[row + 1] - 1; j++)
		{
			tight_format_matrix[row][jptr[j]] = altr[j];
			tight_format_matrix[jptr[j]][row] = autr[j];
		}
	}

	return tight_format_matrix;
}

std::vector<double> CSLR::MultiplyOnVector(const std::vector<double>& x) const
{
	int size = x.size();
	std::vector<double> res(size);

	res[0] = di[0] * x[0];

	for (int row = 1; row < size; row++)
	{
		res[row] = di[row] * x[row];

		for (int j = iptr[row] - 1; j < iptr[row + 1] - 1; j++)
		{
			res[row] += x[jptr[j]] * altr[j];
			res[jptr[j]] += x[row] * autr[j];
		}
	}

	return res;
}

void CSLR::FirstIterativeProcedure() const
{
	std::vector<double> x(di.size()), y;
	x[0] = 1.0;
	double norm = 0.0;

	while (!isinf(norm))
	{
		y = MultiplyOnVector(x);
		norm = EuclideanNorm(y);

		std::cout << norm << " " << x[0] << "\n";
		x[0] *= 10;
	}
}

void CSLR::SecondIterativeProcedure() const
{
	std::vector<double> x(di.size()), y;
	x[0] = 1.0;
	double norm = 0.1;

	while (norm != 0.0)
	{
		y = MultiplyOnVector(x);
		norm = EuclideanNorm(y);

		std::cout << norm << " " << x[0] << "\n";
		x[0] /= 10;
	}
}

const double& CSLR::operator()(const int& row, const int& column) const
{
	if (row >= di.size() && column >= di.size())
		throw std::exception("Out of range");

	// диагональный элемент
	if (row == column)
		return di[row];

	// нижний треугольник
	if (row > column)
	{
		// число элементов в строке
		int count_elements = iptr[row + 1] - iptr[row];

		if (count_elements == 0)
			return 0;

		// ищем столбец column в массиве jptr
		for (int ind = iptr[row] - 1, k = 0; k < count_elements; ind++, k++)
			if (column == (jptr[ind]))
				return altr[iptr[row] - 1 + k];
	}

	// верхний треугольник
	if (row < column)
	{
		// число элементов в столбце
		int Num = iptr[column + 1] - iptr[column];

		if (Num == 0)
			return 0;

		// ищем строку row в массиве jptr
		for (int ind = iptr[column] - 1, k = 0; k < Num; ind++, k++)
			if (row == (jptr[ind]))
				return autr[iptr[column] - 1 + k];

	}
}

std::vector<double> MultiplyTightFormat(const std::vector<std::vector<double>>& A, const std::vector<double>& x)
{
	int size = x.size();
	std::vector<double> res(size);

	for (int i = 0; i < size; i++)
		for (int j = 0; j < size; j++)
			res[i] += A[i][j] * x[j];

	return res;
}

double EuclideanNorm(const std::vector<double>& x)
{
	double res = 0.0;

	for (auto& x_i : x)
		res += x_i * x_i;

	return std::sqrt(res);
}