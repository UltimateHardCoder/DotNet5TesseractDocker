#!/bin/sh
imagePath=$1
txtFilePath=$2

command="tesseract $imagePath $txtFilePath -l eng"
eval " $command"
