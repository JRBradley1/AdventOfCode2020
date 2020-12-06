from itertools import islice
from functools import reduce

input = [
"........#.............#........",
"...#....#...#....#.............",
".#..#...#............#.....#..#"
]

movements = [[1, 1], [3, 1], [5, 1], [7, 1], [1, 2]]
movementResults = []
for movement in movements:
    treeCount = 0        
    currentPositionRightPosition = 0
    runNextLine = movement[1] == 1
    for line in islice(input, 1, None):
        if runNextLine == False:
            runNextLine = True
            continue
        currentPositionRightPosition += movement[0]
        if currentPositionRightPosition >= len(line):
            currentPositionRightPosition = currentPositionRightPosition - len(line)
        if line[currentPositionRightPosition] == "#":
            treeCount += 1
        if movement[1] != 1:
            runNextLine = False
    print(treeCount)
    movementResults.append(treeCount)

print(reduce((lambda x, y: x * y), movementResults))
