Module modAppStructure


    Public Structure MDSParam

        'MDS Control Panel App and ID
        Dim MDSControlPanelAppPath As String
        Dim MDSControlPanelAppID As String


        'Restart PC Option
        Dim blnRestartPC As Boolean

        'Update Patches
        Dim blnReadCDOpt As Boolean
        Dim strCDDrive As String
        Dim strPatchesNm As String

        'Hardware Device Exe Path
        'MDS have 8 Devices
        Dim MDSAppPath As String
        Dim CardReaderAppPath As String
        Dim KeyPadAppPath As String
        Dim PrinterAppPath As String
        Dim BarcodeAppPath As String
        Dim UPSAppPath As String
        Dim BeeperAppPath As String
        Dim RSIAppPath As String

        'App Version ID
        Dim APPVersionID As String


        'App Login Password
        Dim APPLoginPassword As String

        'Vendor Software Path
        Dim RotoMDSToolsPath As String
        Dim RotoWrapperPath As String


        'Required User Login Control
        Dim blnRequiredAppLogin As Boolean


    End Structure

    Public udtMDSParam As MDSParam

End Module
