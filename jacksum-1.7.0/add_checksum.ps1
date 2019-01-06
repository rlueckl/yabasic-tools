param (
  [string]$in = $(throw "-in is required."),
  [string]$out = "use_default"
)

# Allow PowerShell to run unsigned scripts:
# Set-ExecutionPolicy remotesigned

$inpath = (Resolve-Path $in).Path
$outstr = java -jar jacksum.jar -E hex -a crc32_bzip2 $inpath

$bytes = @()

for ($i=6; $i -ge 0; $i-=2) {
  # Extract one byte of the checksum from jacksum's output
  $bytestr = $outstr.Substring($i,2)

  # Convert the hex value to an integer
  $bytehex = [Convert]::ToInt32($bytestr, 16)

  # Add the byte of CRC32 Bzip2 to our array
  $bytes += $bytehex
}

# Add the original file
$bytes += [System.IO.File]::ReadAllBytes($inpath)

if ($out -eq "use_default") {
  # Default is the same filename/location but with "_crc" before the extension
  $infn = Get-Item $inpath
  $outfn = $infn.DirectoryName + "\" + $infn.BaseName + "_crc" + $infn.Extension
}
else {
  $outfn = $out
}

# Write everything to the destination
[io.file]::WriteAllBytes($outfn, $bytes)
