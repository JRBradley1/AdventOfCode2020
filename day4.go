package main

import (
	"fmt"
	"log"
	"regexp"
	"strconv"
	"strings"
)

func main() {
	input := `iyr:2013 hcl:#ceb3a1
	hgt:151cm eyr:2030
	byr:1943 ecl:grn`

	passports := splitByEmptyNewLine(input)
	fmt.Println("passport count:", len(passports))
	validPassports := 0

	for _, passport := range passports {
		fmt.Println("\r\nStarting new passport")
		if isValidInt(passport, "byr:", 1920, 2002, 4) && isValidInt(passport, "iyr:", 2010, 2020, 4) &&
			isValidInt(passport, "eyr:", 2020, 2030, 4) && hasValidHeight(passport) && hasValidHair(passport) &&
			hasValidEye(passport) && hasValidPid(passport) {
			fmt.Println("valid passport:", passport)
			validPassports++
		}
	}

	fmt.Println("valid passports:", validPassports)
}

func splitByEmptyNewLine(str string) []string {
	return strings.Split(str, "\n\t\n")
}

func isValidInt(passport string, fieldExpected string, min int, max int, expectedNum int) bool {
	index := strings.Index(passport, fieldExpected)
	if index < 0 {
		return false
	}

	value, _ := strconv.Atoi(passport[index+4 : index+4+expectedNum])
	return value >= min && value <= max
}

func hasValidHeight(passport string) bool {
	index := strings.Index(passport, "hgt:")
	if index < 0 {
		return false
	}
	isCm := strings.Index(passport, "cm") > 0
	isIn := strings.Index(passport, "in") > 0
	if isCm && isIn {
		log.Fatal("Unexpected Input, has both cm and in:", passport)
	}
	if !isCm && !isIn {
		return false
	}
	heightEnd := 0
	if isCm {
		heightEnd = index + 7
	} else if isIn {
		heightEnd = index + 6
	}

	height, err := strconv.Atoi(passport[index+4 : heightEnd])
	if err != nil {
		return false
	}
	return (isCm && height >= 150 && height <= 193) || (isIn && height >= 59 && height <= 76)
}

func hasValidHair(passport string) bool {
	index := strings.Index(passport, "hcl:")
	if index < 0 {
		return false
	}
	if index+11 > len(passport) {
		return false
	}
	hair := passport[index+4 : index+11]
	regex := regexp.MustCompile("#[0-9a-f]{6}")
	result := regex.MatchString(hair)
	return result
}

func hasValidEye(passport string) bool {
	index := strings.Index(passport, "ecl:")
	if index < 0 {
		return false
	}

	eye := passport[index+4 : index+7]
	validColors := []string{"amb", "blu", "brn", "gry", "grn", "hzl", "oth"}
	return contains(validColors, eye)
}

func hasValidPid(passport string) bool {
	index := strings.Index(passport, "pid:")
	if index < 0 {
		return false
	}
	if index+13 > len(passport) {
		return false
	}
	_, err := strconv.Atoi(passport[index+4 : index+13])
	emptySpaceString := " "
	if index+14 < len(passport) && string(passport[index+14]) == emptySpaceString {
		return false
	}

	if err != nil {
		return false
	}
	return true
}

func contains(s []string, str string) bool {
	for _, v := range s {
		if v == str {
			return true
		}
	}

	return false
}
