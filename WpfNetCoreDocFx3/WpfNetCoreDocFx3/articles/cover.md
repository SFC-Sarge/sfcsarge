# Retail Systems Software **(RSS)** Workstation Provisioning Solution

## Quick Start Notes:
The RSS Provisioning solution is used to build RSS Workstations and KIOSK's.
The RSS Provisioning solution is used by peronnel at the USPS Central Repair Facility (CRF) to build RSS Workstations for Retail Sales.

#### Solution Requirements
1. 16GB or 32GB USB Flash Drive. 


> Must support USB version 3.0 or greater.

2. Windows Preinstallation Environment (WinPE) version 1903 ADK.

> [Download the Windows ADK for Windows 10, version 1903](https://go.microsoft.com/fwlink/?linkid=2086042)

3. Visual Studio Enterprise 2019 version 16.4.2 or higher.


> [Download the Visual Studio Enterprise 2019](https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Enterprise&rel=16)

4. WASP Barcode USB attached Scanner (either model is supported).
 
    * **WLS8600**
    * **WLS3408**

5. Barcode Scan Sheet.

    Scan Sheet provided default values for provisioning Solution.

6. Provisioning Windows Server with share. 

    Provides downloadable files and directories to copy to the RSS Workstation/KIOSK during provisioning.

7. JSON Server to provide downloadable configuration files to the provisioning solution:

   * **RssImageX64Processor-NetCoreConfig.xml**
   * **RssImageX64Processor-NetCoreConfigSchema.xsd**

    Configuration files are used by the provisioning solution to customize 
and control the workstation provisioning processes.

8. Deployed Provisioned Hardware used for RSS Workstations or KIOSK:

    * **NCR XR8 Model Workstation**
    * **IBM Self-Service Kiosk (SSK) desktop hardware**

9. Fiber Network connection between Provisioning Server and Provisioning Switches located on the Provisioning Rack.

10. Category 5/6 Network connection between Provisioning Switches and Workstation Hardware.
11. RSS Provisioning Solution was developed by Perspecta for the United States Postal Service (USPS).




