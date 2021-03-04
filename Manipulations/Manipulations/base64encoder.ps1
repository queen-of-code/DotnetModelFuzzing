
$files = Get-ChildItem "fuzzdb" -Recurse *.txt
foreach ($f in $files){

$outfile = $f.FullName + ".base64"

echo $outfile

$input = Get-Content $f.FullName -Raw

$input.contains("`n")
$input.contains("`r")

#$input = $input -replace [System.Environment]::NewLine, "NEWLINEOMG"
#$input = $input -replace '`r`n', "NEWLINEOMG"
#$input = $input -replace '`n', "NEWLINEOMG"
#$input = $input -replace "`r", "NEWLINEOMG"
$base = [Convert]::ToBase64String([Text.Encoding]::UTF8.GetBytes($input))

#echo $base

Set-Content -Path $outfile $base

}
