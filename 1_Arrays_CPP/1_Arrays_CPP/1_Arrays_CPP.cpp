#include <iostream>
#include <vector>

// This class is similar to the vector class in that it extends base array type

template <typename T>
class Array {
private:
    int _count = 0;
    T* _array;
    int _arraySize;
public:
    Array()
        : _arraySize(0)
    {
        _array = new T[0];
    }
    Array(const int size)
        : _arraySize(size)
    {
        _array = new T[size];
    }
    ~Array() {
        delete[] _array;
    }

    int GetCount() { return _count;  }

    void Print() {
        for (int i = 0; i < _count; i++)
        {
            std::cout << _array[i] << std::endl;
        }
    }

    void Insert(T element) {
        if (_count == _arraySize) {
            T *array2 = new T[_arraySize + 1];
            for (int i = 0; i < _arraySize; i++)
            {
                array2[i] = _array[i];
            }
            array2[_arraySize] = element;
            delete[] _array;
            _array = array2;
            _arraySize++;
            _count++;
        }
        else {
            _array[_count] = element;
            _count++;
        }
    }

    void InsertAt(T element, int index) {
        std::cout << "Inserting " << element << " at index " << index << std::endl;
        if (_count == _arraySize) {
            T* array2 = new T[_arraySize + 1];
            for (int i = 0; i < _arraySize; i++)
            {
                array2[i] = _array[i];
            }
            delete[] _array;
            _array = array2;
            _arraySize++;
            _count++;
        }
        for (int i = index; i < _count; i++)
        {
            _array[i + 1] = _array[i];
        }
        _array[index] = element;
        _count++;
    }

    void RemoveAt(int index) {
        try
        {
            if (index < 0 || index >= _count) throw std::out_of_range("Invalid Index");
            std::cout << "removing: nums[" << index << "] = " << _array[index] << std::endl;
            for (int i = index; i < _count; i++)
            {
                _array[index] = _array[index + 1];
            }
            _count--;
        }
        catch (const std::exception&)
        {
            std::cout << "Invalid index: " << index << std::endl;
        }
    }

    T ElementAt(int index) {
        return _array[index];
    }

    int IndexOf(T element) {
        for (int i = 0; i < _count; i++)
        {
            if (_array[i] == element) return i;
        }
        return -1;
    }

    bool Contains(T element) {
        for (int i = 0; i < _count; i++)
        {
            if (_array[i] == element) return true;
        }
        return false;
    }

    T Max() {
        T max = _array[0];
        for (int i = 0; i < _count; i++)
        {
            if (_array[i] > max) max = _array[i];
        }
        return max;
    }

    T Min() {
        T min = _array[0];
        for (int i = 0; i < _count; i++)
        {
            if (_array[i] < min) min = _array[i];
        }
        return min;
    }

    void Reverse() {
        std::cout << "Reversing Array" << std::endl;
        T* array2 = new T[_arraySize];
        for (int i = 0; i < _count; i++)
        {
            array2[i] = _array[_count-1-i];
        }
        delete[] _array;
        _array = array2;
    }

    void Intersect(Array<T>& comparedWithArray, Array<T>& intersectionResultArray) {
        for (int i = 0; i < this->GetCount(); i++)
        {
            if (comparedWithArray.Contains(_array[i])) {
                intersectionResultArray.Insert(_array[i]);
            }
        }
    }
};

int main()
{
    
    std::cout << "Stack Array Practice" << std::endl;
    int nums[5] = { 1,2,3,4,5 };
    nums[3] = 1000;
    for (int i = 0; i < 5; i++)
    {
        std::cout << "Address of element " << i << ": " << nums + i << " has value of: " << nums[i] << std::endl;
    }

    std::cout << "\nVector Practice" << std::endl;
    const int size = 5;
    std::vector<int> myVector(size, 0);
    //Removes last element
    myVector.pop_back();
    //Adds an element to end
    myVector.push_back(3);

    std::cout << "\nCustom Array Class Practice" << std::endl;
    Array<int> myNums(3);
    myNums.Print();
    myNums.Insert(2);
    myNums.Insert(2);
    myNums.Insert(3);
    myNums.Insert(10);
    myNums.Print();
    std::cout << "Index of 10: " << myNums.IndexOf(10) << std::endl;
    myNums.RemoveAt(3);
    myNums.RemoveAt(-2);
    myNums.Print();
    myNums.Reverse();
    myNums.Print();
    myNums.InsertAt(5, 2);
    myNums.Print();
    std::cout << "myNums.Max(): " << myNums.Max() << std::endl;
    std::cout << "myNums.Min(): " << myNums.Min() << std::endl;
    Array<int> myNums2(5);
    Array<int> myIntersection;
    myNums2.Insert(2);
    myNums2.Insert(10);
    myNums2.Print();
    myNums.Intersect(myNums2, myIntersection);
    std::cout << "Intersection of: " << std::endl;
    std::cout << "myNums" << std::endl;
    myNums.Print();
    std::cout << "myNums2" << std::endl;
    myNums2.Print();
    std::cout << "Result" << std::endl;
    myIntersection.Print();
}
