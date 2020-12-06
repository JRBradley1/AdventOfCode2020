const input = [
'4-5 r: rrrjr',
'9-10 x: pxcbpxxwkqjttx'
]

const checkInputs = () => {
    let validInputCounts = 0;
    for (let i = 0; i < input.length; i++) {
        isInputValid(input[i]) && validInputCounts++;
    }

    console.log(`valid passwords: ${validInputCounts}`)
}

const isInputValid = (input) => {
    const dashIndex = input.indexOf('-');
    const spaceIndex = input.indexOf(' ');
    const colonIndex = input.indexOf(':');
    const positionOne = parseInt(input.substring(0, dashIndex));
    const positionTwo = parseInt(input.substring(dashIndex + 1, spaceIndex));
    const letterToCheck = input.substring(spaceIndex + 1, spaceIndex + 2);
    const password = input.substring(colonIndex + 2);
    const positionOneChar = password[positionOne - 1];
    const positionTwoChar = password[positionTwo - 1];
    console.log(`position1: ${positionOneChar}, position2: ${positionTwoChar}`)
    const isValid = (positionOneChar === letterToCheck && positionTwoChar != letterToCheck)
                    || (positionOneChar != letterToCheck && positionTwoChar === letterToCheck);
    if (!isValid)
    {
        console.log(`invalid input: ${input}`);
    }
    return isValid;
}

checkInputs();