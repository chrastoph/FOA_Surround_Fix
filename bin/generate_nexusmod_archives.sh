#!/bin/bash
#
# generate 7z's for upload to nexusmods.
# 

version="v1.2.0"
dst=".artifacts"
pid="$$"
target="${1}"

if [ ! -n "${target}" ]
 then
  echo "No Target Specified"
  echo "Usage: $0 (5|6)"
  echo " -- 5 for BepInEx5 / Mono Branch Only"
  echo " -- 6 for BepInEx5 / Both Main and Mono Branches"
  echo
  exit 0
else
  echo "Generating Archives for BepInEx (${1})"
fi

if [ ! -d ${dst} ]
 then
  echo "No ${dst}, something is up"
  exit 1
fi


# Target Filenames:
#  - "FOA_Surround_Fix - IL2CPP - BepInEx 6 - v1.1.1.7z"
#  - "FOA_Surround_Fix - Mono - BepInEx 6 - v1.1.1.7z"
#  - "FOA_Surround_Fix - Mono - BepInEx 5 - v1.1.1.7z"
#
# Archive Structure
# "FOA_Surround_Fix - IL2CPP - BepInEx 6 - v1.1.1"
#   "Plugins"
#     "FOA_Surround_Fix.IL2CPP.dll"  

# Its only 3 files, not going to bother automating

mkdir -vp $dst/$pid
mkdir -vp $dst/nexusmods

if [ "z${target}" == "z6" ]
 then
  echo "Creating Archives for BepInEx6 / IL2CPP"
  s=".artifacts/mod.IL2CPP/bin/Debug/net6.0/FOA_Surround_Fix.IL2CPP.dll"
  f="FOA_Surround_Fix - IL2CPP - BepInEx ${target} - ${version}"
  t="${dst}/${pid}/${f}"
  if [ ! -e ${s} ] 
   then
    echo "Missing Source: $s"
    exit 1
  else 
    mkdir -vp "${t}/plugins"
    cp -v "${s}" "${t}/plugins"
    find "${t}" -type f
    [ -e "./$dst/nexusmods/${f}.7z" ] && rm -v "./$dst/nexusmods/${f}.7z"
    7z a -t7z -m0=lzma -mx=9 -mfb=64 -md=32m -ms=on "./$dst/nexusmods/${f}.7z" "./${t}*"
  fi

  echo "Creating Archives for BepInEx6 / Mono"
  s=".artifacts/mod.Mono/bin/Debug/netstandard2.1/FOA_Surround_Fix.Mono.dll"
  f="FOA_Surround_Fix - Mono - BepInEx ${target} - ${version}"
  t="${dst}/${pid}/${f}"
  if [ ! -e ${s} ] 
   then
    echo "Missing Source: $s"
    exit 1
  else 
    mkdir -vp "${t}/plugins"
    cp -v "${s}" "${t}/plugins"
    find "${t}" -type f
    [ -e "./$dst/nexusmods/${f}.7z" ] && rm -v "./$dst/nexusmods/${f}.7z"
    7z a -t7z -m0=lzma -mx=9 -mfb=64 -md=32m -ms=on "./$dst/nexusmods/${f}.7z" "./${t}*"
  fi

elif [ "z${target}" == "z5" ]
 then
  echo "Creating Archives for BepInEx5 / Mono"
  s=".artifacts/mod.Mono_bepinex5/bin/Debug/netstandard2.1/FOA_Surround_Fix.bepinex5.Mono.dll"
  f="FOA_Surround_Fix - Mono - BepInEx ${target} - ${version}"
  t="${dst}/${pid}/${f}"
  if [ ! -e ${s} ] 
   then
    echo "Missing Source: $s"
    exit 1
  else 
    mkdir -vp "${t}/plugins"
    cp -v "${s}" "${t}/plugins"
    find "${t}" -type f
    [ -e "./$dst/nexusmods/${f}.7z" ] && rm -v "./$dst/nexusmods/${f}.7z"
    7z a -t7z -m0=lzma -mx=9 -mfb=64 -md=32m -ms=on "./$dst/nexusmods/${f}.7z" "./${t}*"
  fi
fi

echo "Cleanup"
rm -rfv ${dst}/$pid

echo "Final Targets"
echo "------------------------"
echo "---"
ls -1 $dst/nexusmods/*7z
echo "---"
for z in $dst/nexusmods/*7z
 do
  7z l "${z}"
done
echo "------------------------"
exit 0
