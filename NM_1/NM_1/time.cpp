#include "CSLR.h"
#include <time.h>
#include <chrono>

using std::cout;

int main()
{
	srand(time(NULL));
	
	CSLR matrix;
	try
	{
		matrix.ReadFromBinFile("..\\ChM_PRACTICA_1\\Matrix2");
	}
	catch (std::exception& error)
	{
		cout << error.what();
	}

	std::vector<std::vector<double>> A;
	A = matrix.ConvertToTightFormat();

	std::vector<double> x(matrix.get_size());

	for (int i = 0; i < x.size(); i++)
		x[i] = rand();

	std::chrono::steady_clock::time_point start, end;
	std::chrono::duration<double> duration_cslr(0), duration_tight(0);

	int count_times = 1000;

	for (int i = 0; i < count_times; i++)
	{
		start = std::chrono::high_resolution_clock::now();
		matrix.MultiplyOnVector(x);
		end = std::chrono::high_resolution_clock::now();

		duration_cslr += end - start;

		start = std::chrono::high_resolution_clock::now();
		MultiplyTightFormat(A, x);
		end = std::chrono::high_resolution_clock::now();

		duration_tight += end - start;
	}

	duration_cslr /= count_times;
	duration_tight /= count_times;

	cout << " Multiply cslr - format matrix on vector: " << duration_cslr.count() << "\n";
	cout << " Multiply tight - format matrix on vector: " << duration_tight.count() << "\n";

	return 0;
}