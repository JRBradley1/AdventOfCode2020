$inputs = @(
'BBFFFBBLLR'
'BBFFFFFLRL'
)

[System.Collections.ArrayList]$seats = @()

foreach($boardingPass in $inputs) {
    $minRow = 0
    $maxRow = 127
    $minColumn = 0
    $maxColumn = 7

    $rowData = $boardingPass.substring(0,7)
    $index = 0
    $rowNumber = 0
    foreach($rowChar in $rowData.ToCharArray()) {
        if ($index -eq 6) {
            $rowNumber = If ($rowChar -eq 'F') { $minRow } Else { $maxRow }
            continue
        }
        $minRow = If ($rowChar -eq 'F') { $minRow } Else { $minRow + [math]::Round(($maxRow - $minRow) / 2) }
        $maxRow = If ($rowChar -eq 'B') { $maxRow } Else { $maxRow - [math]::Round(($maxRow - $minRow) / 2) }
        $index++
    }
    $index = 0
    $columnNumber = 0
    $columnData = $boardingPass.substring(7, 3)
    foreach($columnChar in $columnData.ToCharArray()) {
        if ($index -eq 2) {
            $columnNumber = If ($columnChar -eq 'L') { $minColumn } Else { $maxColumn }
            continue
        }
        $minColumn = If ($columnChar -eq 'L') { $minColumn } Else { $minColumn + [math]::Round(($maxColumn - $minColumn) / 2) }
        $maxColumn = If ($columnChar -eq 'R') { $maxColumn } Else { $maxColumn - [math]::Round(($maxColumn - $minColumn) / 2) }
        $index++
    }
    $seatId = $rowNumber * 8 + $columnNumber
    $seats += $seatId
}

for ($i = 100; $i -lt 861; $i++) {
    If ($seats -notcontains $i -and $seats -contains ($i + 1) -and $seats -contains ($i - 1)) {
        Write-Output "My Seat: $i"
    }
}
