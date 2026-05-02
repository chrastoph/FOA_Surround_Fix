#!/bin/bash

count=0

for f in Directory.Build.props */*csproj
 do
  xmllint -noout ${f}
  e=$?
  echo "Syntax Check on [${f}] returned ${e}"
  count=$[count+e]
done

echo "Files with Errors: ${count}"
