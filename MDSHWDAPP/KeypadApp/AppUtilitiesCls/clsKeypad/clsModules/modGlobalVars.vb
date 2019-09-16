Imports clsKeypad.clsKeypadControl

Module modGlobalVars


#Region "App Layer Default Ini File"
    'Hardware
    Public Const HWDLAYERINIPATH As String = "AppIniFile\EPPHWDCFG.ini"
#End Region


#Region "Cls Variable"

    'AppLog CFG - clsLoggerControl
    Public objAppLog As clsAppLogger.clsAppLoggerControl

    Public strAppLogINIFile As String = String.Empty
    Public strLogEvt As String = String.Empty
    Public strErrMsg As String = String.Empty

    Public strFileINIPath As String = String.Empty

    Public Const APP_DEF_TXT_FLD_SEP As String = "|"


    'AppLayer INI CFG - clsAPPLayerINI
    Public objAppLayerINI As New clsAppLayer.clsAppLayerControl

    Public blnInitAppLayer As Boolean


#End Region



#Region "App Object"

    Public udtKEYPADHWDCFG As KEYPADHWDSTR
    Public udtKEYPADSETTINGCFG As KEYPADSETTINGSTR
    Public udtKEYPADLOOKUPDATACFG As KEYPADLOOKUPDATASTR

    Public udtHSMHWDCFG As KEYPADHWDSTR
    Public udtHSMSETTINGCFG As KEYPADSETTINGSTR
    Public udtHSMLOOKUPDATACFG As KEYPADLOOKUPDATASTR

    Public objSagemEPP As clsEPPKeypad
    Public objPrinter As clsKeypadControl.KEYPADHWDSTR
    Public bytGlobalTxPacket(99) As Byte
    Public bytGlobalRxPacket(99) As Byte
    Public blnReturn As Boolean
#End Region


#Region "Sagem EPP"

    'global variable
    Public EPP_COMPORT_HANDLER As IntPtr = IntPtr.Zero
    Public RC As Short
    Public KEY_STROKE As String = ""
    Public PIN_BLOCK As String = ""
    Public CHECK_SUM As String = ""
    Public MAC_DATA As String = ""
    'Public KEY_COUNTER As Integer = 1

    'parameter type
    Public Const CHARACTERS As Integer = 1
    Public Const INTEGERS As Integer = 2
    Public Const LONG_INTEGERS As Integer = 3

    'epp response code
    Public Const RC_OK As Short = 0
    Public Const RC_COMPORTNEVEROPENED As Short = 11
    Public Const RC_CLOSEERR As Short = 2
#End Region



End Module
