#!/usr/bin/python3

import os
import sys
from pathlib import Path as p

cwd = os.getcwd()
binFdr = cwd + "/bin"
srcFdr = cwd + "/src"

if os.system('csc -target:library -out:bin/BaseGame.dll src/BaseGame/*.cs') == 0:
    print("===== Compiled BaseGame.dll\n")
    files = os.listdir(srcFdr)
    for file in files:
        if not os.path.isfile(os.path.join(srcFdr, file)):
            continue
        fileName = p(file).stem
        if os.system(f'csc -target:exe -out:bin/{fileName}.exe src/{fileName}.cs -r:bin/BaseGame.dll -warn:0') == 0:
            print(f'===== Compiled {fileName}.cs\n')
        else:
            print(f'===== Failed to compile {fileName}.cs\n')
else:
    print('===== Compilation failed\n')

