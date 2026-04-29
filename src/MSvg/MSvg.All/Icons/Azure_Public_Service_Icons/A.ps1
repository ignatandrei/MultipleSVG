cls
Write-Host "Start"
$files = Get-ChildItem -Path "." -Filter "*.svg"
$files.Length
# $files |% {
#     $index = $_.Name.IndexOf("icon-service")
#     $number = $_.Name.Substring(0,$index-1)
#     $newName = $_.Name.Substring($index+13)
#     $newName =$newName.Substring(0,$newName.Length-4)  
#     $newName =$newName + "-"+ $number +".svg"
#     $newName 
#     Rename-Item $_.Name $newName 
# }

$files |% {
    $index = $_.Name.IndexOf("(Classic)")
    if ($index  -gt  0 ){
        $newName=  $_.Name.Replace("(Classic)","Classic")
        Write-Host $newName
        Rename-Item $_.Name $newName 
    } 
}
$files |% {
    $index = $_.Name.IndexOf("(Deprecated)")
    if ($index  -gt  0 ){
        $newName=  $_.Name.Replace("(Deprecated)","Deprecated")
        Write-Host $newName
        Rename-Item $_.Name $newName 
    } 
}
$files |% {
    $index = $_.Name.IndexOf("(WAF)")
    if ($index  -gt  0 ){
        $newName=  $_.Name.Replace("(WAF)","WAF")
        Write-Host $newName
        Rename-Item $_.Name $newName 
    } 
}



