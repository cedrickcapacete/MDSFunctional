Imports System
Imports System.IO
Imports FileUtils
Imports clsKeypad.clsKeypadControl

Module modReadKeypadCFG

#Region "Cls Variable"
    Private strTitle As String = "modReadReceiptPrinterCFG"
    Private objINIFile As IniFile = Nothing
    Private strErrMsg As String = String.Empty
    Dim strINIPath As String = String.Empty
#End Region


#Region "Ini - File Info Session - Keypad"

#Region "CUSTOM PRINTER SETTING"
    '[KEYPADSETTING]
    'COMPORT=1
    'BAUDRATE=38400

    Private Const KEYPADHWD_SEC As String = "KEYPADSETTING"
    'Ini File - Log Session Key 
    Private Const KEYPAD_MODEL As String = "DefaultKeypad"
    Private Const KEYPAD_COMPORT As String = "COMPORT"
    Private Const KEYPAD_BAUDRATE As String = "BAUDRATE"
    Private Const KEYPAD_TIMEOUT As String = "TIMEOUT"

#End Region

#Region "Keypad LOOKUP DATA"
    '[PRINTERLOOKUPDATA]

    'DEVICE_GRAPHIC_ID=G
    'DEVICE_NAME=RECEIPTPRT

    ';Device Property
    ';Transaction Status
    'TXN_ST_PRT_SUCCESS=0
    'TXN_ST_PRT_NOT_COMPLETE=1
    'TXN_ST_DEVICE_NOT_CFG=2
    'TXN_ST_CANCEL_SIDEWAY=4

    ';Error Severity
    'ERST_PRT_OK=0
    'ERST_PRT_ERROR=1
    'ERST_PRT_WARNING=2
    'ERST_PRT_FATAL=3

    ';Diagnos Status
    'M_STATUS=""
    'M_STATUS_DATA=""

    ';Supply Status
    ';Paper Supply
    'SUFFICIENT_PAPER=1
    'PAPER_LOW=2
    'PAPER_EXH=3

    ';Ribon
    'RIBBON_OK=1
    'RIBBON_REPLACE_RECOMMEND=2
    'RIBBON_REPLACE_MANDATORY

    ';Print Head
    'PRINT_HEAD_OK=1
    'PRINT_HEAD_REPLACE_RECOMMEND=2
    'PRINT_HEAD_REPLACE_MANDATORY=3

    ';Knife
    'KNIIFE_OK=1
    'KNIFE_REPLACE_RECOMMEND=2


    ';Value for iDeviceState
    ';Device State
    'DST_STARTPINPADSTATE=1
    'DST_NOPINPADSTATE=0

    ';value for strEventType
    'EVTTYPE_DEVICEERROR="KEYPADFAILED"
    'EVTTYPE_DEVICEWRAP="KEYPADWRAP"
    'EVTTYPE_DEVICEREADY="KEYPADCOMPLETE"
    'EVTTYPE_DEVICETIMEOUT="KEYPADTIMEOUT"

    Private Const KEYPADLOOKUPDATA_SEC As String = "KEYPADLOOKUPDATA"

    'Ini File - Log Session Key 
    Private Const INI_KEY_DEVICE_GRAPHIC_ID As String = "DEVICE_GRAPHIC_ID"
    Private Const INI_KEY_DEVICE_NAME As String = "DEVICE_NAME"
    Private Const INI_KEY_TXN_STATUS_SUCCESS_PRINT As String = "TXN_ST_PRT_SUCCESS"
    Private Const INI_KEY_TXN_STATUS_PRINT_NOT_COMPLETE As String = "TXN_ST_PRT_NOT_COMPLETE"
    Private Const INI_KEY_TXN_STATUS_DEVICE_NOT_CFG As String = "TXN_ST_DEVICE_NOT_CFG"
    Private Const INI_KEY_TXN_STATUS_CANCEL_SIDEWAY As String = "TXN_ST_CANCEL_SIDEWAY"
    Private Const INI_KEY_ERR_SEV_OK As String = "ERST_PRT_OK"
    Private Const INI_KEY_ERR_SEV_ERROR As String = "ERST_PRT_ERROR"
    Private Const INI_KEY_ERR_SEV_WARNING As String = "ERST_PRT_WARNING"
    Private Const INI_KEY_ERR_SEV_FATAL As String = "ERST_PRT_FATAL"
    Private Const INI_KEY_DIAGNOS_MSTATUS As String = "M_STATUS"
    Private Const INI_KEY_DIAGNOS_MSTATUSDATA As String = "M_STATUS_DATA"
    Private Const INI_KEY_PAPER_SUFFICIENT As String = "SUFFICIENT_PAPER"
    Private Const INI_KEY_PAPER_LOW As String = "PAPER_LOW"
    Private Const INI_KEY_PAPER_EXH As String = "PAPER_EXH"
    Private Const INI_KEY_RIBBON_OK As String = "RIBBON_OK"
    Private Const INI_KEY_RIBBON_REPLACE_RECOMMEND As String = "RIBBON_REPLACE_RECOMMEND"
    Private Const INI_KEY_RIBBON_REPLACE_MANDATORY As String = "RIBBON_REPLACE_MANDATORY"
    Private Const INI_KEY_PRINTHEAD_OK As String = "PRINT_HEAD_OK"
    Private Const INI_KEY_PRINTHEAD_REPLACE_RECOMMEND As String = "PRINT_HEAD_REPLACE_RECOMMEND"
    Private Const INI_KEY_PRINTHEAD_REPLACE_MANDATORY As String = "PRINT_HEAD_REPLACE_MANDATORY"
    Private Const INI_KEY_KNIFE_OK As String = "KNIIFE_OK"
    Private Const INI_KEY_KNIFE_REPLACE_RECOMMEND As String = "KNIFE_REPLACE_RECOMMEND"
    Private Const INI_KEY_DEVICESTATE_PRINT As String = "DST_STARTPINPADSTATE"
    Private Const INI_KEY_DEVICESTATE_NOPRINT As String = "DST_NOPINPADSTATE"
    Private Const INI_KEY_EVT_DEVICE_ERROR As String = "EVTTYPE_DEVICEERROR"
    Private Const INI_KEY_EVT_DEVICE_READY As String = "EVTTYPE_DEVICEREADY"
    Private Const INI_KEY_EVT_DEVICE_WRAP As String = "EVTTYPE_DEVICEWRAP"
    Private Const INI_KEY_EVT_DEVICE_TIMEOUT As String = "EVTTYPE_DEVICETIMEOUT"
    Private Const INI_KEY_EPP_COMKEY As String = "EPP_KEY_COMKEY"
    Private Const INI_KEY_EPP_MACKEY As String = "EPP_KEY_MACKEY"
    Private Const INI_KEY_EPP_MASTERKEY As String = "EPP_KEY_MASTERKEY"

#End Region

#End Region


#Region "Clear INI Structure Setting"

    Private Sub CLSKEYPADHWDSTR()
        Try
            'Clear CUSTOM Printer HWD Setting Structure
            With udtKEYPADHWDCFG
                .strKeypadModel = ""
                .blnEncryptMode = False
                .blnHideMode = False
                .blnKeypadStatus = True
                .intKeypadTimeOut = 10000
            End With

        Catch ex As Exception
            strErrMsg = "Error in CLSKEYPADHWDSTR. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub

    Private Sub CLSKEYPADSETTINGSTR()
        Try
            'Clear CUSTOM Printer HWD Setting Structure
            With udtKEYPADSETTINGCFG
                .strKeypadModel = "Sagem EPP INT1217-4010"
                .Comport = 2  'COM1
                .Baudrate = 38400
            End With

        Catch ex As Exception
            strErrMsg = "Error in CLSKEYPADSETTINGSTR. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub

    'Private Sub CLSKEYPADLOOKUPDATASTR()
    '    Try
    '        'Clear Keypad Lookup Data Setting Structure
    '        With udtKEYPADLOOKUPDATACFG
    '            .DeviceGraphicID = "Q"
    '            .DeviceName = "PINPAD"

    '            .TxnStatusPrtSuccess = "0"
    '            .TxnStatusPrtNotComplete = "1"
    '            .TxnStatusDeviceNotCfg = "2"
    '            .TxnStatusCancelSideway = "4"

    '            .ErrSvrtyKeypadOk = "1"
    '            .ErrSvrtyKeypadError = "2"
    '            .ErrSvrtyKeypadWarning = "3"
    '            .ErrSvrtyKeypadFatal = "4"

    '            .MStatus = ""
    '            .MStatusData = ""

    '            .SufficientPaper = "1"
    '            .PaperLow = "2"
    '            .PaperExh = "3"

    '            .RibbonOK = "1"
    '            .RibbonReplaceRecommend = "2"
    '            .RibbonReplaceMandatory = "3"

    '            .PrintHeadOK = "1"
    '            .PrintHeadReplaceRecommend = "2"
    '            .PrintHeadReplaceMandatory = "3"

    '            .KnifeOK = "1"
    '            .KnifeReplaceRecommend = "2"

    '            .DeviceStateNoPrint = "0"
    '            .DeviceStatePrint = "1"

    '            .EvtDeviceErr = "PRINTFAILED"
    '            .EvtDeviceReady = "PRINTCOMPLETE"
    '            .EvtDeviceWrap = "PRINTERWRAP"
    '            .EvtDeviceTimeout = "KEYPADTIMEOUT"

    '            .EPPCOMKEY = ""
    '            .EPPMACKEY = ""
    '            .EPPMASTERKEY = ""
    '        End With

    '    Catch ex As Exception
    '        strErrMsg = "Error in CLSKEYPADLOOKUPDATASTR. ErrInfo:" & ex.Message
    '        AppLogErr(strErrMsg)
    '    End Try
    'End Sub

    Private Sub CLSHSMHWDSTR()
        Try
            'Clear Sagem EPP HWD Setting Structure
            With udtHSMHWDCFG
                .strKeypadModel = ""
                .blnEncryptMode = False
                .blnHideMode = False
                .blnKeypadStatus = True
                .intKeypadTimeOut = 10000
            End With

        Catch ex As Exception
            strErrMsg = "Error in CLSHSMHWDSTR. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub

    Private Sub CLSHSMSETTINGSTR()
        Try
            'Clear Sagem EPP HWD Setting Structure
            With udtHSMSETTINGCFG
                .strKeypadModel = "Sagem EPP INT1217-4010"
                .Comport = 2  'COM1
                .Baudrate = 38400
            End With

        Catch ex As Exception
            strErrMsg = "Error in CLSHSMSETTINGSTR. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub

    Private Sub CLSHSMLOOKUPDATASTR()
        Try
            'Clear Keypad Lookup Data Setting Structure
            With udtHSMLOOKUPDATACFG
                .DeviceGraphicID = "Q"
                .DeviceName = "PINPAD"

                .TxnStatusPrtSuccess = "0"
                .TxnStatusPrtNotComplete = "1"
                .TxnStatusDeviceNotCfg = "2"
                .TxnStatusCancelSideway = "4"

                .ErrSvrtyKeypadOk = "1"
                .ErrSvrtyKeypadError = "2"
                .ErrSvrtyKeypadWarning = "3"
                .ErrSvrtyKeypadFatal = "4"

                .MStatus = ""
                .MStatusData = ""

                .SufficientPaper = "1"
                .PaperLow = "2"
                .PaperExh = "3"

                .RibbonOK = "1"
                .RibbonReplaceRecommend = "2"
                .RibbonReplaceMandatory = "3"

                .PrintHeadOK = "1"
                .PrintHeadReplaceRecommend = "2"
                .PrintHeadReplaceMandatory = "3"

                .KnifeOK = "1"
                .KnifeReplaceRecommend = "2"

                .DeviceStateNoPrint = "0"
                .DeviceStatePrint = "1"

                .EvtDeviceErr = "PRINTFAILED"
                .EvtDeviceReady = "PRINTCOMPLETE"
                .EvtDeviceWrap = "PRINTERWRAP"
            End With

        Catch ex As Exception
            strErrMsg = "Error in CLSHSMLOOKUPDATASTR. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub

#End Region


#Region "Read Ini File"

    Public Function clsUpdateKeypadHWDSetting(ByVal strKeypadModel As String, ByVal blnHideMode As Boolean, ByVal blnEncryptMode As Boolean, ByVal blnKeypadStatus As Boolean, ByVal intKeypadTimeOut As Integer) As Boolean
        Try
            With udtKEYPADHWDCFG
                .strKeypadModel = strKeypadModel
                .blnHideMode = blnHideMode
                .blnEncryptMode = blnEncryptMode
                .blnKeypadStatus = blnKeypadStatus
                .intKeypadTimeOut = intKeypadTimeOut
            End With

            Return True
        Catch ex As Exception
            strErrMsg = "Error in clsUpdateKeypadHWDSetting. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        End Try
    End Function

    Public Function clsUpdateKeypadTimeout(ByVal intKeypadTimeout) As Boolean
        Try
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim
            intKeypadTimeout = intKeypadTimeout * 1000

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = KEYPADHWD_SEC
                objINIFile.SetVal(KEYPAD_TIMEOUT, intKeypadTimeout)

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            strErrMsg = "Error in clsUpdateKeypadTimeout. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        End Try
    End Function

    Public Function clsUpdateKeypadValue(ByVal strKeypadNm As String, ByVal strKeypadComport As String) As Boolean
        Try
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = KEYPADHWD_SEC
                objINIFile.SetVal(KEYPAD_MODEL, strKeypadNm)
                objINIFile.SetVal(KEYPAD_COMPORT, strKeypadComport)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            strErrMsg = "Error in clsUpdateKeypadValue. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        End Try
    End Function


    Public Function clsReadKeypadHWDSetting() As Boolean
        Dim strAppPath As String = String.Empty
        Dim strAppName As String = String.Empty

        Try

            'Clear CUSTOM Printer Structure
            CLSKEYPADHWDSTR()

            'Defautl INI File Path - CustomPrint.INI
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = KEYPADHWD_SEC
                With udtKEYPADHWDCFG
                    .strKeypadModel = objINIFile.GetStrVal(KEYPAD_MODEL, String.Empty)
                    .blnHideMode = False
                    .blnEncryptMode = False
                    .blnKeypadStatus = False
                    .intKeypadTimeOut = objINIFile.GetStrVal(KEYPAD_TIMEOUT, String.Empty)
                End With
                Return True
            Else
                'Ini File Path Not Found
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in clsReadKeypadHWDSetting. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function

    Public Function clsReadKeypadSetting() As Boolean
        Dim strAppPath As String = String.Empty
        Dim strAppName As String = String.Empty

        Try

            'Clear CUSTOM Printer Structure
            CLSKEYPADSETTINGSTR()

            'Defautl INI File Path - CustomPrint.INI
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = KEYPADHWD_SEC
                With udtKEYPADSETTINGCFG
                    .strKeypadModel = objINIFile.GetStrVal(KEYPAD_MODEL, String.Empty)
                    .Comport = "COM" & objINIFile.GetStrVal(KEYPAD_COMPORT, String.Empty)
                    .Baudrate = objINIFile.GetStrVal(KEYPAD_BAUDRATE, String.Empty)
                    .Timeout = objINIFile.GetStrVal(KEYPAD_TIMEOUT, String.Empty)

                    '.DefaultCustomPrinterName = objINIFile.GetStrVal(CUSTOMPHWD_DefaultPrinter, String.Empty)
                    '.ReceiptTopMargin = objINIFile.GetIntVal(RECEIPTMARGIN_TOP, 0)
                    '.ReceiptLeftMargin = objINIFile.GetIntVal(RECEIPTMARGIN_LEFT, 0)
                    '.ReceiptRightMargin = objINIFile.GetIntVal(RECEIPTMARGIN_RIGHT, 0)
                    '.ReceiptBottomMargin = objINIFile.GetIntVal(RECEIPTMARGIN_BOTTOM, 0)
                    '.PaperWidth = objINIFile.GetIntVal(PRINTERPAPER_WIDTH, 0)
                    '.PaperHeight = objINIFile.GetIntVal(PRINTERPAPER_HEIGHT, 0)
                    '.PaperX1stColumn = objINIFile.GetIntVal(PRINTERPAPER_1STCOLUMNXVALUE, 0)
                    '.PaperX2ndColumn = objINIFile.GetIntVal(PRINTERPAPER_2NDCOLUMNXVALUE, 0)
                    '.PaperX3rdColumn = objINIFile.GetIntVal(PRINTERPAPER_3RDCOLUMNXVALUE, 0)
                End With
                Return True
            Else
                'Ini File Path Not Found
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in clsReadKeypadSetting. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function

    Public Function clsReadKeypadLookupDataSetting() As Boolean
        Dim strAppPath As String = String.Empty
        Dim strAppName As String = String.Empty

        Try

            'Clear Printer Structure
            'CLSKEYPADLOOKUPDATASTR()

            'Defautl INI File Path - OCPrinterHWDSetting.INI
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath2.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = KEYPADLOOKUPDATA_SEC
                With udtKEYPADLOOKUPDATACFG
                    .DeviceGraphicID = objINIFile.GetStrVal(INI_KEY_DEVICE_GRAPHIC_ID, "")
                    .DeviceName = objINIFile.GetStrVal(INI_KEY_DEVICE_NAME, "")

                    'TRANSACTION STATUS
                    .TxnStatusPrtSuccess = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_SUCCESS_PRINT, "")
                    .TxnStatusPrtNotComplete = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_PRINT_NOT_COMPLETE, "")
                    .TxnStatusDeviceNotCfg = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_DEVICE_NOT_CFG, "")
                    .TxnStatusCancelSideway = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_CANCEL_SIDEWAY, "")

                    'ERROR SEVERITY
                    .ErrSvrtyKeypadOk = objINIFile.GetStrVal(INI_KEY_ERR_SEV_OK, "")
                    .ErrSvrtyKeypadError = objINIFile.GetStrVal(INI_KEY_ERR_SEV_ERROR, "")
                    .ErrSvrtyKeypadWarning = objINIFile.GetStrVal(INI_KEY_ERR_SEV_WARNING, "")
                    .ErrSvrtyKeypadFatal = objINIFile.GetStrVal(INI_KEY_ERR_SEV_FATAL, "")

                    'DIAGNOSIS STATUS
                    .MStatus = objINIFile.GetStrVal(INI_KEY_DIAGNOS_MSTATUS, "")
                    .MStatusData = objINIFile.GetStrVal(INI_KEY_DIAGNOS_MSTATUSDATA, "")

                    'SUPPLY STATUS
                    'PAPER SUPPLY
                    .SufficientPaper = objINIFile.GetStrVal(INI_KEY_PAPER_SUFFICIENT, "")
                    .PaperLow = objINIFile.GetStrVal(INI_KEY_PAPER_LOW, "")
                    .PaperExh = objINIFile.GetStrVal(INI_KEY_PAPER_EXH, "")

                    'RIBBON STATUS
                    .RibbonOK = objINIFile.GetStrVal(INI_KEY_RIBBON_OK, "")
                    .RibbonReplaceRecommend = objINIFile.GetStrVal(INI_KEY_RIBBON_REPLACE_RECOMMEND, "")
                    .RibbonReplaceMandatory = objINIFile.GetStrVal(INI_KEY_RIBBON_REPLACE_MANDATORY, "")

                    'PRINTHEAD STATUS
                    .PrintHeadOK = objINIFile.GetStrVal(INI_KEY_PRINTHEAD_OK, "")
                    .PrintHeadReplaceRecommend = objINIFile.GetStrVal(INI_KEY_PRINTHEAD_REPLACE_RECOMMEND, "")
                    .PrintHeadReplaceMandatory = objINIFile.GetStrVal(INI_KEY_PRINTHEAD_REPLACE_MANDATORY, "")

                    'KNIFE STATUS
                    .KnifeOK = objINIFile.GetStrVal(INI_KEY_KNIFE_OK, "")
                    .KnifeReplaceRecommend = objINIFile.GetStrVal(INI_KEY_KNIFE_REPLACE_RECOMMEND, "")

                    'DEVICE STATE
                    .DeviceStateNoPrint = objINIFile.GetStrVal(INI_KEY_DEVICESTATE_NOPRINT, "")
                    .DeviceStatePrint = objINIFile.GetStrVal(INI_KEY_DEVICESTATE_PRINT, "")

                    'EVENT TYPE
                    .EvtDeviceErr = objINIFile.GetStrVal(INI_KEY_EVT_DEVICE_ERROR, "")
                    .EvtDeviceReady = objINIFile.GetStrVal(INI_KEY_EVT_DEVICE_READY, "")
                    .EvtDeviceWrap = objINIFile.GetStrVal(INI_KEY_EVT_DEVICE_WRAP, "")
                    .EvtDeviceTimeout = objINIFile.GetStrVal(INI_KEY_EVT_DEVICE_TIMEOUT, "")

                    'EPP KEY
                    .EPPCOMKEY = objINIFile.GetStrVal(INI_KEY_EPP_COMKEY, "")
                    .EPPMACKEY = objINIFile.GetStrVal(INI_KEY_EPP_MACKEY, "")
                    .EPPMASTERKEY = objINIFile.GetStrVal(INI_KEY_EPP_MASTERKEY, "")

                End With
                Return True
            Else
                'Ini File Path Not Found
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in clsReadReceiptPrinterLookupDataSetting. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function

    Public Function clsReadHSMHWDSetting() As Boolean
        Dim strAppPath As String = String.Empty
        Dim strAppName As String = String.Empty

        Try

            'Clear CUSTOM Printer Structure
            CLSHSMHWDSTR()

            'Defautl INI File Path - CustomPrint.INI
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = KEYPADHWD_SEC
                With udtHSMHWDCFG
                    .strKeypadModel = objINIFile.GetStrVal(KEYPAD_MODEL, String.Empty)
                    .blnHideMode = False
                    .blnEncryptMode = False
                    .blnKeypadStatus = False
                    .intKeypadTimeOut = objINIFile.GetStrVal(KEYPAD_TIMEOUT, String.Empty)
                End With
                Return True
            Else
                'Ini File Path Not Found
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in clsReadHSMHWDSetting. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function

    Public Function clsReadHSMSetting() As Boolean
        Dim strAppPath As String = String.Empty
        Dim strAppName As String = String.Empty

        Try

            'Clear CUSTOM Printer Structure
            CLSHSMSETTINGSTR()

            'Defautl INI File Path - CustomPrint.INI
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = KEYPADHWD_SEC
                With udtKEYPADSETTINGCFG
                    .strKeypadModel = objINIFile.GetStrVal(KEYPAD_MODEL, String.Empty)
                    .Comport = "COM" & objINIFile.GetStrVal(KEYPAD_COMPORT, String.Empty)
                    .Baudrate = objINIFile.GetStrVal(KEYPAD_BAUDRATE, String.Empty)
                    .Timeout = objINIFile.GetStrVal(KEYPAD_TIMEOUT, String.Empty)

                    '.DefaultCustomPrinterName = objINIFile.GetStrVal(CUSTOMPHWD_DefaultPrinter, String.Empty)
                    '.ReceiptTopMargin = objINIFile.GetIntVal(RECEIPTMARGIN_TOP, 0)
                    '.ReceiptLeftMargin = objINIFile.GetIntVal(RECEIPTMARGIN_LEFT, 0)
                    '.ReceiptRightMargin = objINIFile.GetIntVal(RECEIPTMARGIN_RIGHT, 0)
                    '.ReceiptBottomMargin = objINIFile.GetIntVal(RECEIPTMARGIN_BOTTOM, 0)
                    '.PaperWidth = objINIFile.GetIntVal(PRINTERPAPER_WIDTH, 0)
                    '.PaperHeight = objINIFile.GetIntVal(PRINTERPAPER_HEIGHT, 0)
                    '.PaperX1stColumn = objINIFile.GetIntVal(PRINTERPAPER_1STCOLUMNXVALUE, 0)
                    '.PaperX2ndColumn = objINIFile.GetIntVal(PRINTERPAPER_2NDCOLUMNXVALUE, 0)
                    '.PaperX3rdColumn = objINIFile.GetIntVal(PRINTERPAPER_3RDCOLUMNXVALUE, 0)
                End With
                Return True
            Else
                'Ini File Path Not Found
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in clsReadKeypadSetting. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function

    Public Function clsReadHSMLookupDataSetting() As Boolean
        Dim strAppPath As String = String.Empty
        Dim strAppName As String = String.Empty

        Try

            'Clear Printer Structure
            'CLSKEYPADLOOKUPDATASTR()

            'Defautl INI File Path - OCPrinterHWDSetting.INI
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath2.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = KEYPADLOOKUPDATA_SEC
                With udtHSMLOOKUPDATACFG
                    .DeviceGraphicID = objINIFile.GetStrVal(INI_KEY_DEVICE_GRAPHIC_ID, "")
                    .DeviceName = objINIFile.GetStrVal(INI_KEY_DEVICE_NAME, "")

                    'TRANSACTION STATUS
                    .TxnStatusPrtSuccess = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_SUCCESS_PRINT, "")
                    .TxnStatusPrtNotComplete = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_PRINT_NOT_COMPLETE, "")
                    .TxnStatusDeviceNotCfg = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_DEVICE_NOT_CFG, "")
                    .TxnStatusCancelSideway = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_CANCEL_SIDEWAY, "")

                    'ERROR SEVERITY
                    .ErrSvrtyKeypadOk = objINIFile.GetStrVal(INI_KEY_ERR_SEV_OK, "")
                    .ErrSvrtyKeypadError = objINIFile.GetStrVal(INI_KEY_ERR_SEV_ERROR, "")
                    .ErrSvrtyKeypadWarning = objINIFile.GetStrVal(INI_KEY_ERR_SEV_WARNING, "")
                    .ErrSvrtyKeypadFatal = objINIFile.GetStrVal(INI_KEY_ERR_SEV_FATAL, "")

                    'DIAGNOSIS STATUS
                    .MStatus = objINIFile.GetStrVal(INI_KEY_DIAGNOS_MSTATUS, "")
                    .MStatusData = objINIFile.GetStrVal(INI_KEY_DIAGNOS_MSTATUSDATA, "")

                    'SUPPLY STATUS
                    'PAPER SUPPLY
                    .SufficientPaper = objINIFile.GetStrVal(INI_KEY_PAPER_SUFFICIENT, "")
                    .PaperLow = objINIFile.GetStrVal(INI_KEY_PAPER_LOW, "")
                    .PaperExh = objINIFile.GetStrVal(INI_KEY_PAPER_EXH, "")

                    'RIBBON STATUS
                    .RibbonOK = objINIFile.GetStrVal(INI_KEY_RIBBON_OK, "")
                    .RibbonReplaceRecommend = objINIFile.GetStrVal(INI_KEY_RIBBON_REPLACE_RECOMMEND, "")
                    .RibbonReplaceMandatory = objINIFile.GetStrVal(INI_KEY_RIBBON_REPLACE_MANDATORY, "")

                    'PRINTHEAD STATUS
                    .PrintHeadOK = objINIFile.GetStrVal(INI_KEY_PRINTHEAD_OK, "")
                    .PrintHeadReplaceRecommend = objINIFile.GetStrVal(INI_KEY_PRINTHEAD_REPLACE_RECOMMEND, "")
                    .PrintHeadReplaceMandatory = objINIFile.GetStrVal(INI_KEY_PRINTHEAD_REPLACE_MANDATORY, "")

                    'KNIFE STATUS
                    .KnifeOK = objINIFile.GetStrVal(INI_KEY_KNIFE_OK, "")
                    .KnifeReplaceRecommend = objINIFile.GetStrVal(INI_KEY_KNIFE_REPLACE_RECOMMEND, "")

                    'DEVICE STATE
                    .DeviceStateNoPrint = objINIFile.GetStrVal(INI_KEY_DEVICESTATE_NOPRINT, "")
                    .DeviceStatePrint = objINIFile.GetStrVal(INI_KEY_DEVICESTATE_PRINT, "")

                    'EVENT TYPE
                    .EvtDeviceErr = objINIFile.GetStrVal(INI_KEY_EVT_DEVICE_ERROR, "")
                    .EvtDeviceReady = objINIFile.GetStrVal(INI_KEY_EVT_DEVICE_READY, "")
                    .EvtDeviceWrap = objINIFile.GetStrVal(INI_KEY_EVT_DEVICE_WRAP, "")
                    .EvtDeviceTimeout = objINIFile.GetStrVal(INI_KEY_EVT_DEVICE_TIMEOUT, "")
                End With
                Return True
            Else
                'Ini File Path Not Found
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in clsReadReceiptPrinterLookupDataSetting. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function

    Public Function clsCloseKeypadHWDSetting() As Boolean
        Try
            With udtKEYPADHWDCFG
                .blnKeypadStatus = False
            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

End Module

