#include <iostream>
class IdentifierValidator
{
public:
	static bool IsValid(const std::string& identifier)
	{
        if (identifier.length() >= 1 && identifier.length() <= 128 && (isalpha(identifier[0]) || identifier[0] == '_')) 
        {
            for (char c : identifier) 
            {
                if (!(isalnum(c) || c == '_' || c == '-')) 
                {
                    return false;
                }
            }
            return true;
        }
        return false;
	}
};