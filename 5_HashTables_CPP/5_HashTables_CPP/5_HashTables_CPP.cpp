#include <array>
#include <iostream>
#include <map>
#include <unordered_map>
#include <unordered_set>
#include <list>


class HashTable
{
private:
	struct KeyValuePair
	{
		int _key;
		std::string _value;
		bool operator == (const KeyValuePair& s) const { return _key == s._key && _value == s._value; }
	};
	int _table_size;
	std::list<KeyValuePair> LLArray[10];

public:
	HashTable(int size) : _table_size(size)
	{
		//LLArray = new std::list<KeyValuePair>[size];
	}
	void Put(int key, std::string value)
	{
		// i = 0  for first check.  if an item exists at hash index, then increment i and try again
		// hash1(key) = key % table_size
		// hash2(key) = prime - (key % prime)  // prime should be smaller than size of table
		// index = (hash1) + i * hash2) % table_size
		KeyValuePair pair = { key, value };
		int index = Hash(key);
		for (auto node : LLArray[index])
		{
			if (node._key == key)
			{
				node._value = value;
				return;
			}
		}
		LLArray[index].push_back(pair);
	}
	std::string Get(int key)
	{
		int index = Hash(key);
		for (auto node : LLArray[index])
		{
			if (node._key == key) return node._value;
		}
		return "";
	}
	void Remove(int key)
	{
		int index = Hash(key);
		for (auto node : LLArray[index])
		{
			if (node._key == key) {
				LLArray[index].remove(node);
				break;
			}
		}
	}
	bool Contains(int key)
	{
		int index = Hash(key);
		for (auto node : LLArray[index])
		{
			if (node._key == key) return true;
		}
		return false;
	}
	int Hash(int key)
	{
		int prime = 7;
		int hash1 = key % _table_size;
		int hash2 = prime - (key % prime);
		int i = 0;
		int index = (hash1 + i * hash2) % _table_size;
		return hash1;
	}
};

char FindFirstNonRepeatedChar(std::string str)
{
	std::unordered_map<char, int> map;
	for (int i = 0; i < str.length(); i++)
	{
		if (map.find(str[i]) == map.end())
		{
			map.insert({ str[i], 1 });
		}
		else
		{
			map.at(str[i]) += 1;
		}
	}
	for (int i = 0; i < str.length(); i++)
	{
		if (map.at(str[i]) == 1)
		{
			return str[i];
		}
	}
	return 0;
}

char FindFirstRepeatedChar(const std::string& str)
{
	std::unordered_set<char> set;
	for (char c : str)
	{
		if (set.find(c) == set.end())
			set.insert(c);
	}
	for (char c : str)
	{
		if (set.find(c) != set.end())
			return c;
	}
	return 0;
}

int FindMostRepeatedInt(std::array<int, 10> nums)
{
	std::unordered_map<int,int> map;
	for (int i = 0; i<nums.size(); i++)
	{
		auto it = map.find(nums[i]); 
		if (it != map.end()) it->second += 1;
		else map.insert({ nums[i], 1 });
	}
	std::pair<int, int> itMax {0,0};
	for(auto it : map)
	{
		if (it.second > itMax.second) {
			itMax.first = it.first;
			itMax.second = it.second;
		}
	}
	return itMax.first;
}

int CountPairsWithDiff(std::array<int, 10> nums, int k)
{
	std::unordered_set<int> set;
	for (auto num : nums)
	{
		set.insert(num);
	}
	set.erase(0);

	int count = 0;
	for(auto num : nums)
	{
		if (set.find(num + k) != set.end()) count++;
		if (set.find(num - k) != set.end()) count++;
		set.erase(num);
	}
	return count;
}

std::pair<int,int> TwoSum(std::array<int, 4> nums, int target)
{
	std::pair<int, int> indices;
	std::unordered_map<int, int> map;
	for (int i = 0; i < nums.size(); i++)
		map.insert({ nums[i], i });

	for (auto num : nums)
	{
		auto it = map.find(target - num);
		if (it != map.end()) return { map.find(num)->second, it->second };
	}
	return { -1,-1 };
}

int main()
{

    // Hash Table Notes
    /* Hash table can:
     *  Insert O(1)
     *  Lookup O(1)
     *  Delete O(1)
     *  all these operations run O(1) because hash function says where in memory to lookup
     *
     *                      | map             | unordered_map
	 *		---------------------------------------------------------
	 *		Ordering        | increasing  order   | no ordering
	 *		                | (by default)        |
	 *
	 *		Implementation  | Self balancing BST  | Hash Table
	 *		                | like Red-Black Tree |  
	 *
	 *		search time     | log(n)              | O(1) -> Average 
	 *		                |                     | O(n) -> Worst Case
	 *
	 *		Insertion time  | log(n) + Rebalance  | Same as search
	 *		                      
	 *		Deletion time   | log(n) + Rebalance  | Same as search
	 *
	 *		C++ hashmap is depricated and should only use either map or unordered map
	 *		Only use map when need data ordered.
	 *
	 *		The reason why a hashmap is so fast is because it converts the key into a hash
	 *		This hash then can be used as an index to access an element of an array.
	 *		It is possible that their could be a collision when allocating items on the array,
	 *		in that two distinct keys generate the same hash.  There are two methods for
	 *		handling collisions in a hashmap.  First is with chaining, which makes
	 *		each element in the array a linked list.  Each node in list contains a key value pair.
	 *		If there is a collision, then the key value pair is added to the linked list chain.
	 *		The second option is with open addressing.  This means, that when a collision occurs,
	 *		there is an algorithm that is used to determine the next location to try and insert they key/value
	 *		pair into the array.  There is linear probing, quadratic probing, and double hashing probing.
     */
	auto print_key_value = [](const auto& key, const auto& value) {
		std::cout << "Key:[" << key << "] Value:[" << value << "]\n";
	};
	// Unordered Map on Stack
	/*std::cout << "Unordered Map on Stack" << std::endl;
	* std::unordered_map<int, std::string> map;
	* map.insert(std::make_pair(1, "Michael"));
	* map[2] = "Steve";
	* map.insert({ 2, "Bob" }); // does not work for reassigning key
	* for (auto& pair : map)
	* 	print_key_value(pair.first, pair.second);
	*/
	// Unordered Map on Heap
	/*std::cout << "\nUnordered Map on Heap" << std::endl;
	* auto map1 = new std::unordered_map<int, std::string>;
	* map1->insert(std::make_pair(1, "Michael"));
	* map1->insert(std::make_pair(2, "Steve"));
	* map1->insert({3, "Bob"});
	* map1->insert(std::make_pair(1, "Steve")); // doesn't work with reassigning key
	* map1->insert({ 1, "alkmd" }); // doesn't work with reassigning key
	* auto it = map1->find(1);  // Iterator works for reassigning key
	* if (it != map1->end()) it->second = "John";
	* map1->at(1) = "Joe"; //  At() works for reassigning key
	* // (&map1)[3] = "Bob";  // Can't figure out to add items to map by this way yet
	* map1->erase(3);
	* for (auto& pair : *map1)
	*	print_key_value(pair.first, pair.second);
	*/

	// Repeated Char Functions
	/*
	* std::cout << "First non repeated char of 'a green apple': " << FindFirstNonRepeatedChar("a green apple") << std::endl;
	* std::cout << "First repeated char of 'a green apple': " << FindFirstRepeatedChar("a green apple") << std::endl;
	*/
	/*std::array<int, 10> nums = { 1,2,3,4,4,3,4,4,2,2 };
	std::cout << "Most repeated of nums = { 1,2,3,4,4,3,4,4,2,2 } = " << FindMostRepeatedInt(nums) << std::endl;*/
	/*std::array<int, 10> nums = { 1, 7, 5, 9, 2, 12, 3 };
	std::cout << "CountPairsWithDiff = { 1, 7, 5, 9, 2, 12, 3 }, k = 2 is = " << CountPairsWithDiff(nums,2) << std::endl;*/
	std::array<int, 4> nums = { 2,7,11,15 };
	std::pair<int, int> pair = TwoSum(nums, 9);
	std::cout << "TwoSum = { 2,7,11,15 }, target = 9 is = [" << pair.first << ", " << pair.second << "]" << std::endl;
	// HashTable
	/*
	 * put(k, v)
	 * get(k) : v
	 * remove(k)
	 * k: int
	 * v: string
	 * collisions: chaining
	 */
	/*HashTable myTable(10);
	myTable.Put(1, "Hello");
	myTable.Put(11, "World");
	std::cout << "I added {1, 'Hello'} to a hashmap" << std::endl;
	std::cout << "{key: 1} has a value of:  " << myTable.Get(1) << std::endl;
	myTable.Remove(1);
	std::cout << "I removed {1, 'Hello'} from hashmap" << std::endl;
	std::cout << "{key: 1} has a value of:  " << myTable.Get(1) << std::endl;*/

}


