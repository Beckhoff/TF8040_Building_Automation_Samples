import os.path
import sys

if len(sys.argv) != 2:
    print('Invalid parameters passed!\nE.g. call "py set_versions.py 1.1.10"')
    exit()

version = sys.argv[1]

stringParts = version.split("-")
versionParts = stringParts[0].split(".")
major = 1
minor = int(versionParts[0])
revision = int(versionParts[1])
build = int(versionParts[2])
versionString = str(major) + '.' + str(minor) + '.' + str(revision) + '.' + str(build)

def replaceLineInFile(linePattern, newString, filePath):
    if len(version) <= 0 or len(filePath) <= 0:
        print('Invalid input for setVersion function!')
        return    

    if os.path.exists(filePath):
        print("Reading file + " + filePath)
        with open(filePath, 'r') as f:
            lines = f.readlines()
            newLines = []
            for l in lines:
                if l.find(linePattern) > -1:
                    newLines.append(newString)
                else:
                    newLines.append(l)            
            f.close()
        with open(filePath, 'w') as f:
            f.writelines(newLines)
            f.close()
            print('Updated version of "' + filePath + '" to ' + versionString)
    else:
        print('File "' + filePath + '" was not found!')

def setHmiProjVersion(version, projFilePath):
    if len(version) <= 0 or len(projFilePath) <= 0:
        print('Invalid input for setVersion function!')
        return
    
    replaceLineInFile('<HmiVersion>', '\t<HmiVersion>' + versionString + '</HmiVersion>\n', projFilePath)

def setPlcProjVersion(version, projFilePath):
    if len(version) <= 0 or len(projFilePath) <= 0:
        print('Invalid input for setVersion function!')
        return
    
    replaceLineInFile('<ProjectVersion>', '\t<ProjectVersion>' + versionString + '</ProjectVersion>\n', projFilePath)

setHmiProjVersion(versionString, os.path.relpath('../Conecpt Samples/HMI/TF8040 Concept Samples HMI/TF8040 Concept Samples HMI.hmiproj'))
setHmiProjVersion(versionString, os.path.relpath('../Template Samples/HMI/TF8040 Template Samples HMI/TF8040 Template Samples HMI.hmiproj'))

setPlcProjVersion(versionString, os.path.relpath('../Conecpt Samples/PLC/TF8040 Concept Samples PLC/Samples/Samples.plcproj'))
setPlcProjVersion(versionString, os.path.relpath('../Template Samples/PLC/TF8040 Template Samples PLC/TF8040 Template Samples PLC/TF8040 Template Samples PLC.plcproj'))