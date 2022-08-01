#include <iostream>

class BigOfunctions {
public:

	/*
	Best performance to worst performance:
	O(1) -- O(log n) -- O(n) -- O(n^2) -- O(2^n)
	*/
	
	// O(1): Size of input doesn't matter, will always run once
	static void Print_O1(int numbers[]) {
		std::cout << "\nO(1): " << std::endl;
		std::cout << numbers[0] << std::endl;
	}

	// O(n): Size of input matters here, n represents size of the input.
	static void Print_On(int numbers[], int size) {
		std::cout << "\nO(n): " << std::endl;
		for (int i = 0; i < size; i++)
		{
			std::cout << numbers[i] << std::endl;
		}
	}

	// O(n^2): The algorithm cost increases quadratically with the size of the data input
	static void Print_On2(int numbers[], int size) {
		std::cout << "\nO(n^2): " << std::endl;
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				std::cout << numbers[j] << std::endl;
			}
		}
	}

	// O(log n): More efficient than linear or quadtratic time.  
	// This function uses binary search which 
	// TO DO, flesh out this function
	static int BinarySearch_Ologn(int numbers[], int size, int searchNum) {
		std::cout << "\nO(log n): " << std::endl;
		int left = 0;
		int right = size-left;
		if (numbers[right / 2] > searchNum) {
			left = right / 2;
		}
		else if (numbers[right / 2] < searchNum) {
			right = right / 2;
		}
		else return 0;
	}


	// O(2^n): The algorithm cost increases exponetially with the size of the data.
	static void Print_O2n(int numbers[], int size) {
	}


	/*
	* Space time complexity is similarly described using O(x).  This is used to
	* describe the amount of memory consumed be performing an algorithm
	*/

};


int main()
{
	int myNumbers[13] = { 1,2,3,4,5,6,7,8,9,10,11,12,13 };
	int myNumbersSize = sizeof(myNumbers) / sizeof(myNumbers[0]);
	BigOfunctions::Print_O1(myNumbers);
	BigOfunctions::Print_On(myNumbers, myNumbersSize);
	BigOfunctions::Print_On2(myNumbers, myNumbersSize);
	//BigOfunctions::BinarySearch_Ologn(myNumbers, myNumbersSize, 10);
}


