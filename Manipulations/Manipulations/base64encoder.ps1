
$files = Get-ChildItem "fuzzdb" -Recurse *.txt
foreach ($f in $files){

$outfile = $f.FullName + ".base64"

echo $outfile

$input = Get-Content $f.FullName
$base = [Convert]::ToBase64String([Text.Encoding]::Unicode.GetBytes($input))

#echo $base

Set-Content -Path $outfile $base

}
