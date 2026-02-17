# TF8040 Samples
Examples of all features from TF8040.

# System requirements
The TF8040 samples are made for TwinCAT Build 4026 based systems:
- TwinCAT.Standard.XAE >= v4026.3
- TwinCAT.HMI.Engineering >= v14.1
- TF8040.BuildingAutomation.XAE >= v6.1

# Download
To use the samples clone or download the repository.
Inside the repository you find the following samples:
- [Concept samples](/Concepts/)
    - [Documentation](/Documentation/Concept-Samples/Concept-Samples.md)
- [Template samples](/Templates/)
    - [Documentation](/Documentation/Template-Samples/Template-Samples.md)

# Configuration
## PLC
Individual settings within the TF8040-Concept-Samples-PLC-Solution must be adapted to the hardware used.
The settings affect the project settings and the I/O.

> **INFO**
    All necessary steps are described in [Starting a project](https://infosys.beckhoff.com/english.php?content=../content/1033/tf8040_tc3_buildingautomation/10466962059.html&id=).
    Individual settings that have already been made do not need to be taken into account any further.

## HMI

### Preparing the PLC
To run the HMI, the PLC must be configured and started.

### Installing the TwinCAT HMI
To load the sample project, the [TwinCAT 3 HMI Engineering](https://www.beckhoff.com/de-de/produkte/automation/twincat/texxxx-twincat-3-engineering/te2000.html) must first be downloaded and installed.

> **INFO**	
Further information on the required steps can be found in the documentation for the TwinCAT 3 HMI Engineering in the section [installation](https://infosys.beckhoff.com/content/1031/te2000_tc3_hmi_engineering/26697081717888648971.html?id=4943381260988253553).

### Download
Once the PLC is activated and running, the sample solution can be opened.

### License development system
In order for the HMI samples to be executable, the TC3 HMI server license of the TF2000 must be licensed on the target system.

### Server configuration
No further configuration is necessary if the runtime is activated on the PC on which the HMI project is also started.

If not, the configuration must be adjusted. The procedure for this is described in the tutorial [Generic HMI](https://infosys.beckhoff.com/english.php?content=../content/1033/tf8040_tc3_buildingautomation/10503142283.html&id=).

# Further information
- [PLC programming](https://infosys.beckhoff.com/english.php?content=../content/1033/tf8040_tc3_buildingautomation/13934606987.html&id=)