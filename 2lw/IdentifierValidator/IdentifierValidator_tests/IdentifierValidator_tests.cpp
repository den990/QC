#define CATCH_CONFIG_MAIN
#include "../../../../OOP/catch/catch.hpp"
#include "../IdentifierValidator/IdentifierValidator.hpp"
#include <locale>

TEST_CASE("IdentifierValidator check")
{
	setlocale(LC_ALL, "RUS");
	REQUIRE(IdentifierValidator::IsValid("abc123"));
	REQUIRE(IdentifierValidator::IsValid("Abc123"));
	REQUIRE(IdentifierValidator::IsValid("ABC123"));
	REQUIRE(IdentifierValidator::IsValid("_identifier"));
	REQUIRE(IdentifierValidator::IsValid("a"));
	REQUIRE(IdentifierValidator::IsValid("valid_identifier123_valid"));
	REQUIRE(IdentifierValidator::IsValid("valid-identifier123_valid"));
	REQUIRE(IdentifierValidator::IsValid("valid-identifier123-valid"));
	REQUIRE(IdentifierValidator::IsValid("__dssdsdssd"));
	REQUIRE(IdentifierValidator::IsValid("_-valid"));


	REQUIRE(!IdentifierValidator::IsValid("123invalid"));
	REQUIRE(!IdentifierValidator::IsValid("-invalid"));
	REQUIRE(!IdentifierValidator::IsValid(""));
	REQUIRE(!IdentifierValidator::IsValid("too_long_identifier_to_make_it_invalid_too_long_identifier_to_make_it_invalid_too_long_identifier_to_make_it_invalid_too_long_identifier_to_make_it_invalid_too_long_identifier_to_make_it_invalid"));
	REQUIRE(!IdentifierValidator::IsValid("\n"));
	REQUIRE(!IdentifierValidator::IsValid(" "));
	REQUIRE(!IdentifierValidator::IsValid("валовалва"));
	REQUIRE(!IdentifierValidator::IsValid("-валовалва"));
	REQUIRE(!IdentifierValidator::IsValid("_валовалва"));
}
