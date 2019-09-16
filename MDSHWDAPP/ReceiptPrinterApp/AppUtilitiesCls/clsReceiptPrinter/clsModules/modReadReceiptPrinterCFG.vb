Imports System
Imports System.IO
Imports FileUtils
Imports clsReceiptPrinter.clsReceiptPrinterControl

Module modReadReceiptPrinterCFG

#Region "Cls Variable"
    Private strTitle As String = "modReadReceiptPrinterCFG"
    Private objINIFile As IniFile = Nothing
    Private strErrMsg As String = String.Empty
    Dim strINIPath As String = String.Empty
#End Region


#Region "Ini - File Info Session - Receipt Printer"

#Region "CUSTOM PRINTER SETTING"
    ' [CUSTOMPRINTERSETTING]
    ';Default Printer Setting
    'DefaultPrinter="Custom VKP80 (200dpi)"

    ';Receipt Margin Setting
    'RECEIPTMARGINTop=0
    'RECEIPTMARGINLeft=20
    'RECEIPTMARGINRight=10
    'RECEIPTMARGINBottom=0

    ';Paper Setting
    'PAPERWidth=290
    'PAPERHeight=0

    'PAPER POSITIONING
    '1stColumnXValue=20
    '2ndColumnXValue=110
    '3rdColumnXValue=200

    Private Const CUSTOMPRINTERHWD_SEC As String = "CUSTOMPRINTERSETTING"
    'Ini File - Log Session Key 
    Private Const CUSTOMPHWD_DefaultPrinter As String = "DefaultPrinter"

    Private Const RECEIPTMARGIN_TOP As String = "RECEIPTMARGINTop"
    Private Const RECEIPTMARGIN_LEFT As String = "RECEIPTMARGINLeft"
    Private Const RECEIPTMARGIN_RIGHT As String = "RECEIPTMARGINRight"
    Private Const RECEIPTMARGIN_BOTTOM As String = "RECEIPTMARGINBottom"

    Private Const PRINTERPAPER_WIDTH As String = "PAPERWidth"
    Private Const PRINTERPAPER_HEIGHT As String = "PAPERHeight"

    Private Const PRINTERPAPER_1STCOLUMNXVALUE As String = "1stColumnXValue"
    Private Const PRINTERPAPER_2NDCOLUMNXVALUE As String = "2ndColumnXValue"
    Private Const PRINTERPAPER_3RDCOLUMNXVALUE As String = "3rdColumnXValue"

    'Private Const RECEIPTPRINTER_TIMEOUT As String = "HWDTIMEOUTINTERVAL"


    ';Printer Status Value

    ';1=Bypass 0=As Error
    'BypassUnknownStates=1

    'PaperOKCD="8388608,2147483648,0"
    'PaperJammedCD="8388609,1,419430,4194336"
    'PaperOutCD="8388609,1,5,2147483649"
    'PaperLowCD="8388612,4,2147483680,2158679008"

    'dwReturnErrCode = 280

    Private Const BYPASS_UNKNOWNPRNTCD As String = "BypassUnknownStates"

    Private Const PAPEROK_CD As String = "PaperOKCD"
    Private Const PAPERJAM_CD As String = "PaperJammedCD"
    Private Const PAPEROUT_CD As String = "PaperOutCD"
    Private Const PAPERLOW_CD As String = "PaperLowCD"

    Private Const PRNTDW_VALUE As String = "dwReturnErrCode"




#End Region

#Region "PRINTER PROPERTIES SETTING"
    '   [PrinterHWDSetting]
    Private Const PRINTERHWD_SEC As String = "PrinterHWDSetting"

    'Ini File - Log Session Key 
    Private Const PHWD_HEADERIMGHEIGHTPIXEL As String = "HeaderImgHeightPixel"

    Private Const PHWD_HEADERTEXTFONTYPE As String = "HeaderTextFontType"
    Private Const PHWD_HEADERTEXTFONTSIZE As String = "HeaderTextFontSize"
    Private Const PHWD_HEADERTEXTFONTBOLD As String = "HeaderTextFontBold"
    Private Const PHWD_HEADERTEXTFONTITALIC As String = "HeaderTextFontItalic"

    Private Const PHWD_TITLETEXTFONTYPE As String = "TitleTextFontType"
    Private Const PHWD_TITLETEXTFONTSIZE As String = "TitleTextFontSize"
    Private Const PHWD_TITLETEXTFONTBOLD As String = "TitleTextFontBold"
    Private Const PHWD_TITLETEXTFONTITALIC As String = "TitleTextFontItalic"

    Private Const PHWD_BODYTEXTFONTYPE As String = "BodyTextFontType"
    Private Const PHWD_BODYTEXTFONTSIZE As String = "BodyTextFontSize"
    Private Const PHWD_BODYTEXTFONTBOLD As String = "BodyTextFontBold"
    Private Const PHWD_BODYTEXTFONTITALIC As String = "BodyTextFontItalic"
    Private Const PHWD_BODYTEXTMIDLEMARGIN As String = "BodyTextMiddleMargin"

    Private Const PHWD_FOOTERTEXTFONTYPE As String = "FooterTextFontType"
    Private Const PHWD_FOOTERTEXTFONTSIZE As String = "FooterTextFontSize"
    Private Const PHWD_FOOTERTEXTFONTBOLD As String = "FooterTextFontBold"
    Private Const PHWD_FOOTERTEXTFONTITALIC As String = "FooterTextFontItalic"
#End Region

#Region "RECEIPT PRINTER LOOKUP DATA"
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
    'DST_PRINTSTATE=1
    'DST_NOPRINTSTATE=0

    ';value for strEventType
    'EVTTYPE_DEVICEERROR="PRINTFAILED"
    'EVTTYPE_DEVICEWRAP="PRINTERWRAP"
    'EVTTYPE_DATAARRIVED="PRINTCOMPLETE"

    Private Const PRTLOOKUPDATA_SEC As String = "PRINTERLOOKUPDATA"

    'Ini File - Log Session Key 
    Private Const INI_KEY_DEVICE_GRAPHIC_ID As String = "DEVICE_GRAPHIC_ID"
    Private Const INI_KEY_DEVICE_NAME As String = "DEVICE_GRAPHIC_ID"
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
    Private Const INI_KEY_DEVICESTATE_PRINT As String = "DST_PRINTSTATE"
    Private Const INI_KEY_DEVICESTATE_NOPRINT As String = "DST_NOPRINTSTATE"
    Private Const INI_KEY_EVT_DEVICE_ERROR As String = "EVTTYPE_DEVICEERROR"
    Private Const INI_KEY_EVT_DEVICE_READY As String = "EVTTYPE_DEVICEREADY"
    Private Const INI_KEY_EVT_DEVICE_WRAP As String = "EVTTYPE_DEVICEWRAP"

#End Region

#End Region


#Region "Clear INI Structure Setting"

    Private Sub CLSRECEIPTPRINTERHWDSTR()
        Try
            'Clear CUSTOM Printer HWD Setting Structure
            With udtRECEIPTPRINTERHWDCFG
                .strPrinterModel = ""
                .blnPrinterStatus = False
                .blnPaperOK = False
                .blnPaperLow = False
                .blnPaperJam = False
                .blnPaperEnd = False
            End With

        Catch ex As Exception
            strErrMsg = "Error in CLSCUSTOMPrinterHWDSettingSTR. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub

    Private Sub CLSRECEIPTPRINTERSETTINGSTR()
        Try
            'Clear CUSTOM Printer HWD Setting Structure
            With udtRECEIPTPRINTERSETTINGCFG
                .DefaultCustomPrinterName = "Custom VKP80 (200dpi)"
                .ReceiptTopMargin = 0
                .ReceiptLeftMargin = 10
                .ReceiptRightMargin = 10
                .ReceiptBottomMargin = 0
                .PaperWidth = 290
                .PaperHeight = 0
                .PaperX1stColumn = 20
                .PaperX2ndColumn = 110
                .PaperX3rdColumn = 200

                'Printer Status
                .blnBypassUnknownStatus = False
                .strPaperOKCD = "8388608,2147483648,0"
                .strPaperJamCD = "8388609,1,419430,4194336"
                .strPaperOutCD = "8388609,1,5,2147483649"
                .strPaperLowCD = "8388612,4,2147483680,2158679008"
                .strPrintDWValue = "280"



            End With

        Catch ex As Exception
            strErrMsg = "Error in CLSCUSTOMPrinterHWDSettingSTR. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub

    Private Sub CLSPrinterHWDSettingSTR()
        Try
            'Clear Printer HWD Setting Structure
            With udtRECEIPTPRINTERPROPERTIESCFG
                .HeaderImgHeightPixel = 20
            End With

        Catch ex As Exception
            strErrMsg = "Error in CLSPrinterHWDSettingSTR. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub

    Private Sub CLSRECEIPTPRINTERLOOKUPDATASTR()
        Try
            'Clear Receipt Printer Lookup Data Setting Structure
            With udtRECEIPTPRINTERLOOKUPDATACFG
                .DeviceGraphicID = "G"
                .DeviceName = "RECEIPTPRT"

                .TxnStatusPrtSuccess = "0"
                .TxnStatusPrtNotComplete = "1"
                .TxnStatusDeviceNotCfg = "2"
                .TxnStatusCancelSideway = "4"

                .ErrSvrtyPrtOk = "1"
                .ErrSvrtyPrtError = "2"
                .ErrSvrtyPrtWarning = "3"
                .ErrSvrtyPrtFatal = "4"

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
            strErrMsg = "Error in CLSRECEIPTPRINTERLOOKUPDATASTR. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub

#End Region


#Region "Read Ini File"

    Public Function clsReadReceiptPrinterHWDSetting() As Boolean
        Dim strAppPath As String = String.Empty
        Dim strAppName As String = String.Empty

        Try

            'Clear CUSTOM Printer Structure
            CLSRECEIPTPRINTERHWDSTR()

            'Defautl INI File Path - CustomPrint.INI
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = CUSTOMPRINTERHWD_SEC
                With udtRECEIPTPRINTERHWDCFG
                    .strPrinterModel = objINIFile.GetStrVal(CUSTOMPHWD_DefaultPrinter, "Custom VKP80II (200dpi)")
                    .blnPrinterStatus = False
                    .blnPaperOK = False
                    .blnPaperLow = False
                    .blnPaperJam = False
                    .blnPaperEnd = False
                    '.intReceiptPrinterTimeout = objINIFile.GetStrVal(RECEIPTPRINTER_TIMEOUT, String.Empty)
                End With
                Return True
            Else
                'Ini File Path Not Found
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in clsReadCUSTOMPrinterHWDSetting. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function

    Public Function clsReadCUSTOMPrinterSetting() As Boolean
        Dim strAppPath As String = String.Empty
        Dim strAppName As String = String.Empty

        Try

            'Clear CUSTOM Printer Structure
            CLSRECEIPTPRINTERSETTINGSTR()

            'Defautl INI File Path - CustomPrint.INI
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = CUSTOMPRINTERHWD_SEC

                With udtRECEIPTPRINTERSETTINGCFG

                    .DefaultCustomPrinterName = objINIFile.GetStrVal(CUSTOMPHWD_DefaultPrinter, "Custom VKP80II (200dpi)")
                    .ReceiptTopMargin = objINIFile.GetIntVal(RECEIPTMARGIN_TOP, 0)
                    .ReceiptLeftMargin = objINIFile.GetIntVal(RECEIPTMARGIN_LEFT, 0)
                    .ReceiptRightMargin = objINIFile.GetIntVal(RECEIPTMARGIN_RIGHT, 0)
                    .ReceiptBottomMargin = objINIFile.GetIntVal(RECEIPTMARGIN_BOTTOM, 0)
                    .PaperWidth = objINIFile.GetIntVal(PRINTERPAPER_WIDTH, 0)
                    .PaperHeight = objINIFile.GetIntVal(PRINTERPAPER_HEIGHT, 0)
                    .PaperX1stColumn = objINIFile.GetIntVal(PRINTERPAPER_1STCOLUMNXVALUE, 0)
                    .PaperX2ndColumn = objINIFile.GetIntVal(PRINTERPAPER_2NDCOLUMNXVALUE, 0)
                    .PaperX3rdColumn = objINIFile.GetIntVal(PRINTERPAPER_3RDCOLUMNXVALUE, 0)


                    'Printer Status
                    .blnBypassUnknownStatus = objINIFile.GetBoolVal(BYPASS_UNKNOWNPRNTCD, False)
                    .strPaperOKCD = objINIFile.GetStrVal(PAPEROK_CD, "8388608,2147483648,0")
                    .strPaperJamCD = objINIFile.GetStrVal(PAPERJAM_CD, "8388609,1,419430,4194336")
                    .strPaperOutCD = objINIFile.GetStrVal(PAPEROUT_CD, "8388609,1,5,2147483649")
                    .strPaperLowCD = objINIFile.GetStrVal(PAPERLOW_CD, "8388612,4,2147483680,2158679008")
                    .strPrintDWValue = objINIFile.GetStrVal(PRNTDW_VALUE, "280")

                End With







                Return True
            Else
                'Ini File Path Not Found
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in clsReadCUSTOMPrinterHWDSetting. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function

    Public Function clsReadCUSTOMPrinterPropSetting() As Boolean
        Dim strAppPath As String = String.Empty
        Dim strAppName As String = String.Empty

        Try

            'Clear Printer Structure
            CLSPrinterHWDSettingSTR()

            'Defautl INI File Path - OCPrinterHWDSetting.INI
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath2.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = PRINTERHWD_SEC
                With udtRECEIPTPRINTERPROPERTIESCFG
                    .HeaderImgHeightPixel = objINIFile.GetStrVal(PHWD_HEADERIMGHEIGHTPIXEL, 20)

                    'HEADER TEXT PROPERTIES
                    .HeaderTextFontType = objINIFile.GetStrVal(PHWD_HEADERTEXTFONTYPE, "Arial")
                    .HeaderTextFontSize = objINIFile.GetStrVal(PHWD_HEADERTEXTFONTSIZE, 10)
                    .HeaderTextFontBold = objINIFile.GetStrVal(PHWD_HEADERTEXTFONTBOLD, "Y")
                    .HeaderTextFontItalic = objINIFile.GetStrVal(PHWD_HEADERTEXTFONTITALIC, "N")

                    'TITLE TEXT PROPERTIES
                    .TitleTextFontType = objINIFile.GetStrVal(PHWD_TITLETEXTFONTYPE, "Arial")
                    .TitleTextFontSize = objINIFile.GetStrVal(PHWD_TITLETEXTFONTSIZE, 9)
                    .TitleTextFontBold = objINIFile.GetStrVal(PHWD_TITLETEXTFONTBOLD, "N")
                    .TitleTextFontItalic = objINIFile.GetStrVal(PHWD_TITLETEXTFONTITALIC, "N")

                    'BODY TEXT PROPERTIES
                    .BodyTextFontType = objINIFile.GetStrVal(PHWD_BODYTEXTFONTYPE, "Arial")
                    .BodyTextFontSize = objINIFile.GetStrVal(PHWD_BODYTEXTFONTSIZE, 8)
                    .BodyTextFontBold = objINIFile.GetStrVal(PHWD_BODYTEXTFONTBOLD, "N")
                    .BodyTextFontItalic = objINIFile.GetStrVal(PHWD_BODYTEXTFONTITALIC, "N")
                    .BodyTextMiddleMargin = objINIFile.GetStrVal(PHWD_BODYTEXTMIDLEMARGIN, 120)

                    'FOOTER TEXT PROPERTIES
                    .FooterTextFontType = objINIFile.GetStrVal(PHWD_FOOTERTEXTFONTYPE, "Arial")
                    .FooterTextFontSize = objINIFile.GetStrVal(PHWD_FOOTERTEXTFONTSIZE, 9)
                    .FooterTextFontBold = objINIFile.GetStrVal(PHWD_FOOTERTEXTFONTBOLD, "N")
                    .FooterTextFontItalic = objINIFile.GetStrVal(PHWD_FOOTERTEXTFONTITALIC, "N")
                End With
                Return True
            Else
                'Ini File Path Not Found
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in clsReadPrinterHWDSetting. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function

    Public Function clsReadReceiptPrinterLookupDataSetting() As Boolean
        Dim strAppPath As String = String.Empty
        Dim strAppName As String = String.Empty

        Try

            'Clear Printer Structure
            CLSRECEIPTPRINTERLOOKUPDATASTR()

            'Defautl INI File Path - OCPrinterHWDSetting.INI
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath3.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = PRTLOOKUPDATA_SEC
                With udtRECEIPTPRINTERLOOKUPDATACFG
                    .DeviceGraphicID = objINIFile.GetStrVal(INI_KEY_DEVICE_GRAPHIC_ID, "")
                    .DeviceName = objINIFile.GetStrVal(INI_KEY_DEVICE_NAME, "")

                    'TRANSACTION STATUS
                    .TxnStatusPrtSuccess = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_SUCCESS_PRINT, "")
                    .TxnStatusPrtNotComplete = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_PRINT_NOT_COMPLETE, "")
                    .TxnStatusDeviceNotCfg = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_DEVICE_NOT_CFG, "")
                    .TxnStatusCancelSideway = objINIFile.GetStrVal(INI_KEY_TXN_STATUS_CANCEL_SIDEWAY, "")

                    'ERROR SEVERITY
                    .ErrSvrtyPrtOk = objINIFile.GetStrVal(INI_KEY_ERR_SEV_OK, "")
                    .ErrSvrtyPrtError = objINIFile.GetStrVal(INI_KEY_ERR_SEV_ERROR, "")
                    .ErrSvrtyPrtWarning = objINIFile.GetStrVal(INI_KEY_ERR_SEV_WARNING, "")
                    .ErrSvrtyPrtFatal = objINIFile.GetStrVal(INI_KEY_ERR_SEV_FATAL, "")

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

#End Region

End Module
