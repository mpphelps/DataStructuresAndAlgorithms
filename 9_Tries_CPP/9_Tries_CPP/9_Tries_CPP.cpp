#include <iostream>
#include <list>
#include <unordered_map>

class Trie
{
private:
	struct Node
	{
		char _value;
		std::unordered_map<char, Node*>* _children = new std::unordered_map<char, Node*>{};
		bool isEndOfWord = false;
		Node() : Node(0) {}
		Node(char value) : _value(value)
		{
			//for (auto& i : _children) i = nullptr;
		}
		~Node()
		{
			delete _children;
		}
		bool HasChild(const char ch)
		{
			return _children->find(ch) != _children->end();
		}
		void AddChild(const char ch)
		{
			Node* node = new Node(ch);
			_children->insert({ ch, node });
		}
		Node* GetChild(const char ch)
		{
			return _children->at(ch);
		}
		int GetDirectChildrenQuantity()
		{
			int quantity = 0;
			for (auto i = _children->begin(); i != _children->end(); i++)
			{
				quantity++;
			}
			return quantity;
		}
		void InorderPrint()
		{
			for (auto i = _children->begin(); i != _children->end(); i++)
			{
				if (i->second->isEndOfWord)
					std::cout << i->first << '*' << std::endl;
				else 
					std::cout << i->first << std::endl;
				_children->at(i->first)->InorderPrint();
			}
		}
		void PostOrderPrint()
		{
			for (auto i = _children->begin(); i != _children->end(); i++)
			{
				_children->at(i->first)->PostOrderPrint();
				if (i->second->isEndOfWord)
					std::cout << i->first << '*' << std::endl;
				else
					std::cout << i->first << std::endl;
			}
		}
		void Remove(const std::string& word, int wordIndex)
		{
			if (!HasChild(word[wordIndex])) return;
			Node* child = GetChild(word[wordIndex]);

			// if we are not at end of word
			if (wordIndex < word.length()-1) {
				child->Remove(word, wordIndex+1);
			}

			// end of word but still further words
			if (child->isEndOfWord && wordIndex == word.length() - 1)
			{
				if (child->_children->empty())
					_children->erase(word[wordIndex]);
				else child->isEndOfWord = false;
			}
			else if (wordIndex < word.length() - 1)
			{
				if (child->isEndOfWord)
				{
					return;
				}
				if (child->_children->empty())
				{
					_children->erase(word[wordIndex]);
					return;
				}
			}
		}
		void AutoComplete(const std::string& word, int i, std::vector<std::string>* words)
		{
			if(HasChild(word[i]))
			{
				GetChild(word[i])->AutoComplete(word, i+1, words);
			}

			// Now we are at the node where word is
			// We need to get a list of words from here to the next end of word and return that list to the user

			if (i == word.length()) GetAutoCompleteWordList(words, word);

			
		}
		void GetAutoCompleteWordList(std::vector<std::string>* words, std::string currentWord)
		{
			for (auto j = _children->begin(); j != _children->end(); j++)
			{
				currentWord.push_back(j->first);
				if (j->second->isEndOfWord) {
					words->push_back(currentWord);
					std::cout << j->first << '*' << std::endl;
				}
				if(!j->second->_children->empty())
				{
					j->second->GetAutoCompleteWordList(words, currentWord);
					std::cout << j->first << std::endl;
				}
				currentWord.pop_back();
			}
		}
		bool Contains(const std::string& word, int i)
		{
			if (i == word.length())
			{
				return true;
			}
			if (HasChild(word[i]))
			{
				return GetChild(word[i])->Contains(word, i + 1);
			}
			return false;
			
		}
		int CountWords()
		{
			int wordCount = 0;

			if (isEndOfWord)
				wordCount++;

			for (auto i = _children->begin(); i != _children->end(); i++)
				wordCount += _children->at(i->first)->CountWords();

			return wordCount;
		}
		void LongestCommonPrefix(std::string* prefix)
		{
			if(_value!=NULL) prefix->push_back(_value);
			if (GetDirectChildrenQuantity() == 0 || GetDirectChildrenQuantity() > 1 || isEndOfWord) return;
			for (auto i = _children->begin(); i != _children->end(); i++)
			{
				_children->at(i->first)->LongestCommonPrefix(prefix);
			}
		}
	};
	Node* _root;
public:
	Trie()
	{
		_root = new Node();
	}
	void Insert(const std::string& word)
	{
		Node* currentNode = _root;
		for (const char ch : word)
		{
			if (!currentNode->HasChild(ch))
				currentNode->AddChild(ch);
			currentNode = currentNode->GetChild(ch);
		}
		currentNode->isEndOfWord = true;
	}
	bool Contains(const std::string& word)
	{
		Node* currentNode = _root;
		for (const char ch : word)
		{
			if (currentNode->HasChild(ch)) currentNode = currentNode->GetChild(ch);
			else return false;
		}
		return currentNode->isEndOfWord;
	}
	bool Contains2 (const std::string& word)
	{
		return _root->Contains(word, 0);
	}
	void InorderPrint()
	{
		_root->InorderPrint();
	}
	void PostOrderPrint()
	{
		_root->PostOrderPrint();
	}
	void Remove(const std::string& word, Node* currentNode, int wordIndex)
	{
		// if we are not at end of word and child has letter
		if (wordIndex < word.length() && currentNode->HasChild(word[wordIndex]))
		{
			// make currentnode = child and increment wordindex, recurse
			currentNode = currentNode->GetChild(word[wordIndex]);
			wordIndex++;
			Remove(word, currentNode, wordIndex);
		}
		wordIndex--;

		// end of word but still further words
		if (currentNode->isEndOfWord && wordIndex == word.length()-1)
		{
			if (currentNode->_children->empty())
				delete currentNode;
			else currentNode->isEndOfWord = false;
		}
		else if (wordIndex < word.length() - 1)
		{
			if(currentNode->isEndOfWord)
			{
				return;
			}
			if (currentNode->_children->empty())
			{
				delete currentNode;
				return;
			}
		}
	}
	void Remove(const std::string& word)
	{
		_root->Remove(word, 0);
	}
	std::vector<std::string> AutoComplete(const std::string& word)
	{
		auto words = new std::vector<std::string>;
		_root->AutoComplete(word, 0, words);
		return *words;
	}
	static int Index(const char character)
	{
		const int index = character - 'a';
		if (index < 0 || index > 26)
		{
			throw new std::invalid_argument("invalid character");
		}
		return index;
	}
	int CountWords()
	{
		return _root->CountWords();
	}
	std::string LongestCommonPrefix()
	{
		std::string* prefix = new std::string {""};
		_root->LongestCommonPrefix(prefix);
		return *prefix;
	}
};


int main()
{
	const auto trie = new Trie();
	std::cout << "Adding car" << std::endl;
	trie->Insert("car");
	trie->InorderPrint();
	std::cout << "Adding care" << std::endl;
	trie->Insert("care");
	trie->InorderPrint();
	std::cout << "Adding cart" << std::endl;
	trie->Insert("cart");
	trie->InorderPrint();
/*	std::cout << "Adding dog" << std::endl;
	trie->Insert("dog");
	trie->InorderPrint()*/;
	std::cout << "Adding carpenter" << std::endl;
	trie->Insert("carpenter");
	trie->InorderPrint();
	//std::cout << "Removing cart" << std::endl;
	//trie->Remove("cart");
	//trie->InorderPrint();
	//std::cout << "Removing car" << std::endl;
	//trie->Remove("car");
	//trie->InorderPrint();
	//std::cout << "Removing care" << std::endl;
	//trie->Remove("care");
	//trie->InorderPrint();
	//std::cout << "Removing dog" << std::endl;
	//trie->Remove("dog");
	//trie->InorderPrint();
	//std::cout << "Removing dog" << std::endl;
	//trie->Remove("dog");
	//trie->InorderPrint();
	//std::cout << "Adding c" << std::endl;
	//trie->Insert("c");
	//trie->InorderPrint();
	//std::cout << "Removing c" << std::endl;
	//trie->Remove("c");
	//trie->InorderPrint();

	/*std::cout << "Contains dog? " << trie->Contains("dog") << std::endl;
	std::cout << "Contains canada? " << trie->Contains("canada") << std::endl;
	std::cout << "Contains can? " << trie->Contains("can") << std::endl;
	std::cout << "Contains ca? " << trie->Contains("ca") << std::endl;
	std::cout << "Contains ''? " << trie->Contains("") << std::endl;*/
	//trie->PostOrderPrint();
	std::cout << "Auto Complete car: " << std::endl;
	auto words = trie->AutoComplete("car");
	for (auto word : words)
	{
		std::cout << word << std::endl;
	}

	std::cout << "Contains car: " << trie->Contains2("car") << std::endl;
	std::cout << "Word Count: " << trie->CountWords() << std::endl;
	std::cout << "Longest Common Prefix: " << trie->LongestCommonPrefix() << std::endl;
	
}
