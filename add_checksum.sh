#!/bin/bash

# How to use in Vim:
# :w | ! ./add_checksum.sh %

ORIG=$1

if [ -z $ORIG ]; then
  echo "usage: $0 filename"
  echo "for example: BESCES-50008DEV.bas"
  exit 1
fi

NOEXT=$(echo $ORIG | sed "s/\.bas//g")

CRC=$(jacksum -E hex -a crc32_bzip2 $ORIG | sed "s/^\(..\)\(..\)\(..\)\(..\).*/\\\x\4\\\x\3\\\x\2\\\x\1/g")
echo -n -e $CRC | cat - $ORIG > $NOEXT && echo "done." || echo "failed."

