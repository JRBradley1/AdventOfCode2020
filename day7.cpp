// AocDay7.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

using namespace std;
#include <iostream>
#include <vector>
#include <string>

const vector<string> input = {
	"light plum bags contain 1 faded blue bag.",
	"muted salmon bags contain 4 faded lavender bags, 4 posh magenta bags.",
	"wavy gray bags contain 2 dotted teal bags."
};

vector<string> colorsAdded;

vector<string> getNextBags(string nextBagColors)
{
	vector<string> nextBags;
	string delimiter = ", ";
	auto start = 0U;
	auto end = nextBagColors.find(delimiter);
	if (end == string::npos && nextBagColors != "no other bags.")
	{
		// single requirement
		nextBags.push_back(nextBagColors);
		return nextBags;
	}

	if (nextBagColors == "no other bags.")
	{
		return nextBags;
	}

	bool isLast = false;
	while (end != string::npos)
	{
		// found the delimiter, multiple other bags.
		nextBags.push_back(nextBagColors.substr(start, end - start));
		start = end + delimiter.length();
		end = nextBagColors.find(delimiter, start);
		if (end == string::npos && !isLast)
		{
			isLast = true;
			end = nextBagColors.length();
		}
	}

	return nextBags;
}

int countBags(string bagColor)
{
	int count = 0;
	for (string bagRequirements : input)
	{
		if (bagRequirements.find(bagColor) != string::npos && bagRequirements.find(bagColor) == 0)
		{
			//found it, lets increment the count and then see what bags contain it.
			if (bagColor != "shiny gold")
			{
				count++;
			}

			string nextBagColors = bagRequirements.substr(bagRequirements.find("contain ") + 8);
			vector<string> nextBagColorsArray = getNextBags(nextBagColors);

			for (string nextBagColor : nextBagColorsArray)
			{
				int multiplier = stoi(nextBagColor.substr(0, 1));
				nextBagColor.replace(0, 2, "");
				auto realColor = nextBagColor.substr(0, nextBagColor.rfind(" "));
				cout << realColor << ":" << multiplier << "\n";
				int bagCount = countBags(realColor);
				int newCount = multiplier * bagCount;
				if (bagColor == "mirrored bronze")
				{
					cout << newCount << " " << realColor;
				}
				count += newCount;
			}
		}
	}

	return count;
}

int main()
{
	string bagColor = "shiny gold";
	int total = countBags(bagColor);
    cout << "Total Count: " << total;
}

