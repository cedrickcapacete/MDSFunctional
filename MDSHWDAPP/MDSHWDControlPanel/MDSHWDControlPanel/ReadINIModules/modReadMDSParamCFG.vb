Imports System.IO

Module modReadMDSParamCFG

#Region "Variable"

    Private strTitle As String = "modReadMDSParamCFG"
    Private m_IniObj As clsINIFunc

#End Region


#Region "MDS HWD APP PATH CONTROL"


    '[MDSHWDCFG]
  

    Private Const MDSPARAM_SECT As String = "MDSHWDCFG"

    'MDS MAIN Application
    'AppPath="E:\MDSHWDAPP\MDSHWD\MDSHWDControlPanel\MDSHWDControlPanel\bin\Debug\MDSHWDControlPanel.exe"
    'AppID="MDSHWDControlPanel"

    Private Const MDS_MAINAPPPath As String = "AppPath"
    Private Const MDSMAINAPP_ID As String = "AppID"


    'MDS=""
    'CARDREADER=""
    'KEYPAD=""
    'PRINTER=""
    'BEEPER=""
    'UPS=""
    'RSI=""
    'BARCODE=""

    Private Const MDS_APPPATH As String = "MDS"
    Private Const CARDREADER_APPPATH As String = "CARDREADER"
    Private Const KEYPAD_APPPATH As String = "KEYPAD"
    Private Const PRINTER_APPPATH As String = "PRINTER"
    Private Const BEEPER_APPPATH As String = "BEEPER"
    Private Const UPS_APPPATH As String = "UPS"
    Private Const RSI_APPPATH As String = "RSI"
    Private Const BARCODE_APPPATH As String = "BARCODE"


    'Update Batches
    'ReadCDOpt=1
    'CDDrive="D:\"
    'UpdatePatches="MDSHWDUPDATE.bat"

    Private Const UPT_READCDOPT As String = "ReadCDOpt"
    Private Const UPT_CDDRIVE As String = "CDDrive"
    Private Const UPT_PATCHESNM As String = "UpdatePatches"

    'RestartPCOpt=1
    Private Const RESTARTPC_OPT As String = "RestartPCOpt"


    ';VENDOR Tools Path
    'RotoMDSTools
    'RotoWrapper
    Private Const ROTOTYPE_MDSTOOLS As String = "RotoMDSTools"
    Private Const ROTOTYPE_WRAPPER As String = "RotoWrapper"


    ';MDSHWD Required Login Option
    ';1=Enable 0=Disable
    'AppLoginOPT=0
    Private Const AppLogin_Opt As String = "AppLoginOPT"




#End Region

#Region "App Version"



    '    [APPVERSION]
    ';APP VERSION Control
    ';V - VERSION
    ';XX - identifies major release
    ';YY - identifies major country
    ';zz - identifies the point or maintenance release
    ';nn - identifies major patch release
    ';Sample: VXX.YY.ZZ.nn

    'APPVERSIONID="V1.01.01.01"

    Private Const APPVERSION_SEC As String = "APPVERSION"
    Private Const APPVERSION_ID As String = "APPVERSIONID"


#Region "Load INI Application Version and Control"

    Public Function LoadAppVersionID(ByVal strINIFile As String) As Boolean
        Dim blnRtn As Boolean
        Try

            strINIFile = strINIFile.Trim

            If File.Exists(strINIFile) = True Then
                If (m_IniObj Is Nothing) Then
                    m_IniObj = New clsINIFunc(strINIFile, False)
                End If

                m_IniObj.CurrentSection = APPVERSION_SEC

                With udtMDSParam
                    'Application Version ID
                    .APPVersionID = m_IniObj.GetStrVal(APPVERSION_ID, "V1.01.01.01")
                End With

                'Logger
                AppLogInfo("MDS App Version=" & udtMDSParam.APPVersionID.Trim)

                blnRtn = True
            Else
                AppLogWarn("LoadAppVersionID:" & strINIFile & " File Not Found")
                udtMDSParam.APPVersionID = "V1.01.01.01"
                blnRtn = False
            End If

            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in LoadAppVersionID: " & ex.Message)
            udtMDSParam.APPVersionID = "V1.01.01.01"
            Return False
        Finally
            m_IniObj = Nothing
        End Try
    End Function

#End Region

#End Region


#Region "APP Login Password"

    '[APPLOGINCFG]
    'APPLOGINPWSD="c/E/IA5njW3iG74ssk8N4g=="

    Private Const APPLOGINCFG_SEC As String = "APPLOGINCFG"
    Private Const APPLOGIN_PWSD As String = "APPLOGINPWSD"


#End Region




#Region "Load MDS Hardware App Path and Control"

    Public Function LoadMDSHardwareAppPath(ByVal strINIFile As String) As Boolean
        Dim blnRtn As Boolean
        Try

            strINIFile = strINIFile.Trim

            If File.Exists(strINIFile) = True Then
                If (m_IniObj Is Nothing) Then
                    m_IniObj = New clsINIFunc(strINIFile, False)
                End If

                m_IniObj.CurrentSection = MDSPARAM_SECT

                With udtMDSParam

                    'App ID
                    .MDSControlPanelAppPath = m_IniObj.GetStrVal(MDS_MAINAPPPath, "")
                    .MDSControlPanelAppID = m_IniObj.GetStrVal(MDSMAINAPP_ID, "")

                    'Update Control
                    .blnReadCDOpt = m_IniObj.GetBoolVal(UPT_READCDOPT, True)
                    .strCDDrive = m_IniObj.GetStrVal(UPT_CDDRIVE, "D:\")
                    .strPatchesNm = m_IniObj.GetStrVal(UPT_PATCHESNM, "MDSHWDUPDATE.bat")

                    'Restart PC
                    .blnRestartPC = m_IniObj.GetBoolVal(RESTARTPC_OPT, True)

                    'HWD EXE Path
                    .MDSAppPath = m_IniObj.GetStrVal(MDS_APPPATH, "")
                    .CardReaderAppPath = m_IniObj.GetStrVal(CARDREADER_APPPATH, "")
                    .KeyPadAppPath = m_IniObj.GetStrVal(KEYPAD_APPPATH, "")
                    .PrinterAppPath = m_IniObj.GetStrVal(PRINTER_APPPATH, "")
                    .BeeperAppPath = m_IniObj.GetStrVal(BEEPER_APPPATH, "")
                    .RSIAppPath = m_IniObj.GetStrVal(RSI_APPPATH, "")
                    .UPSAppPath = m_IniObj.GetStrVal(UPS_APPPATH, "")
                    .BarcodeAppPath = m_IniObj.GetStrVal(BARCODE_APPPATH, "")

                    'Vendor Path
                    .RotoMDSToolsPath = m_IniObj.GetStrVal(ROTOTYPE_MDSTOOLS, "")
                    .RotoWrapperPath = m_IniObj.GetStrVal(ROTOTYPE_WRAPPER, "")

                    'Required App Login 
                    .blnRequiredAppLogin = m_IniObj.GetBoolVal(AppLogin_Opt, True)

                End With


                'Logger
                AppLogInfo("==== Start Load MDS Hardware Control Panel Parameter and Setting ====")

                With udtMDSParam

                    'App ID and Path
                    AppLogInfo("MDS Control Panel Path=" & .MDSControlPanelAppPath.Trim)
                    AppLogInfo("MDS Control Panel AppID=" & .MDSControlPanelAppID.Trim)

                    'Update Patches Control
                    AppLogInfo("Patches ReadCDOpt=" & .blnReadCDOpt)
                    AppLogInfo("Patches CDDrive=" & .strCDDrive.Trim)
                    AppLogInfo("Patches Name=" & .strPatchesNm.Trim)

                    'Restart PC
                    AppLogInfo("RestartPCOpt=" & .blnRestartPC)
                    AppLogInfo("App RequiredLogin Opt=" & .blnRequiredAppLogin)

                    'Hardware Path
                    AppLogInfo("MDS App Path=" & .MDSAppPath.Trim)
                    AppLogInfo("Card Reader App Path=" & .CardReaderAppPath.Trim)
                    AppLogInfo("KeyPad App Path=" & .KeyPadAppPath.Trim)
                    AppLogInfo("Printer App Path=" & .PrinterAppPath.Trim)
                    AppLogInfo("Beeper App Path=" & .BeeperAppPath.Trim)
                    AppLogInfo("RSI App Path=" & .RSIAppPath.Trim)
                    AppLogInfo("UPS App Path=" & .UPSAppPath.Trim)
                    AppLogInfo("Barcode App Path=" & .BarcodeAppPath.Trim)

                    'Vedor Path
                    AppLogInfo("Rototype MDSTools Path=" & .RotoMDSToolsPath.Trim)
                    AppLogInfo("Rototype Wrapper Path=" & .RotoWrapperPath.Trim)


                End With
                

                blnRtn = True
            Else
                AppLogWarn("LoadMDSHardwareAppPath:" & strINIFile & " File Not Found")
                blnRtn = False
            End If

            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in LoadMDSHardwareAppPath: " & ex.Message)
            Return False
        Finally
            m_IniObj = Nothing
        End Try
    End Function


#End Region


#Region "Load INI Application Login Password"

    Public Function LoadAppLoginPassword(ByVal strINIFile As String) As Boolean
        Dim blnRtn As Boolean
        Try

            strINIFile = strINIFile.Trim

            If File.Exists(strINIFile) = True Then
                If (m_IniObj Is Nothing) Then
                    m_IniObj = New clsINIFunc(strINIFile, False)
                End If

                m_IniObj.CurrentSection = APPLOGINCFG_SEC

                With udtMDSParam
                    'Application Login Password
                    .APPLoginPassword = m_IniObj.GetStrVal(APPLOGIN_PWSD, "")
                End With

                blnRtn = True
            Else
                udtMDSParam.APPLoginPassword = ""
                blnRtn = False
            End If

            Return blnRtn
        Catch ex As Exception
            AppLogErr("Error in LoadAppLoginPassword: " & ex.Message)
            udtMDSParam.APPLoginPassword = ""
            Return False
        Finally
            m_IniObj = Nothing
        End Try
    End Function

#End Region


End Module
