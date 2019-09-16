Imports System
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Printing

Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD


Public Class clsReceiptPrinterControl

#Region "NDS+ Receipt Printer Data"

    Private PrintDoc As New PrintDocument
    Private prtController As PrintController

    Structure ReplyReceiptPrinterHWDSTR
        Dim strReceiptData As String
        Dim strImageFileName As String
        'Dim strDeviceStatus As String
        Dim strTxnStatus As String
        Dim strErrSeverity As String
        Dim strSupplyStatus As String
        Dim strMStatus As String
    End Structure

    Dim udtReplyReceiptPrinterHWDSTR As ReplyReceiptPrinterHWDSTR

#End Region

#Region "Private Variable"

    Private m_intCurrentY As Integer


#End Region

#Region "Receipt Printer Control"
    Structure RECEIPTPRINTERHWDSTR
        Dim strPrinterModel As String
        Dim blnPrinterStatus As Boolean
        Dim blnPaperOK As Boolean
        Dim blnPaperLow As Boolean
        Dim blnPaperEnd As Boolean
        Dim blnPaperJam As Boolean
        Dim blnPrinterFail As Boolean
        'Dim intReceiptPrinterTimeout As Integer
    End Structure

    Structure RECEIPTPRINTERSETTINGSTR
        Dim DefaultCustomPrinterName As String
        Dim ReceiptTopMargin As Integer
        Dim ReceiptLeftMargin As Integer
        Dim ReceiptRightMargin As Integer
        Dim ReceiptBottomMargin As Integer
        Dim PaperWidth As Integer
        Dim PaperHeight As Integer
        Dim PaperX1stColumn As Integer
        Dim PaperX2ndColumn As Integer
        Dim PaperX3rdColumn As Integer


        Dim blnBypassUnknownStatus As Boolean
        Dim strPaperOKCD As String
        Dim strPaperJamCD As String
        Dim strPaperLowCD As String
        Dim strPaperOutCD As String
        Dim strPrintDWValue As String


    End Structure

    Structure RECEIPTPRINTERPROPERTIESSTR
        Dim HeaderImgHeightPixel As Integer

        'HEADER TEXT PROPERTIES
        Dim HeaderTextFontType As String
        Dim HeaderTextFontSize As Integer
        Dim HeaderTextFontBold As String
        Dim HeaderTextFontItalic As String

        'TITLE TEXT PROPERTIES
        Dim TitleTextFontType As String
        Dim TitleTextFontSize As Integer
        Dim TitleTextFontBold As String
        Dim TitleTextFontItalic As String

        'BODY TEXT PROPERTIES
        Dim BodyTextFontType As String
        Dim BodyTextFontSize As Integer
        Dim BodyTextFontBold As String
        Dim BodyTextFontItalic As String
        Dim BodyTextMiddleMargin As Integer

        'FOOTER TEXT PROPERTIES
        Dim FooterTextFontType As String
        Dim FooterTextFontSize As Integer
        Dim FooterTextFontBold As String
        Dim FooterTextFontItalic As String
    End Structure

    Structure RECEIPTPRINTERLOOKUPDATASTR
        Dim DeviceGraphicID As String
        Dim DeviceName As String

        'Device Properties
        Dim TxnStatusPrtSuccess As String
        Dim TxnStatusPrtNotComplete As String
        Dim TxnStatusDeviceNotCfg As String
        Dim TxnStatusCancelSideway As String

        'Error Severity
        Dim ErrSvrtyPrtOk As String
        Dim ErrSvrtyPrtError As String
        Dim ErrSvrtyPrtFatal As String
        Dim ErrSvrtyPrtWarning As String

        'Diagnos Status
        Dim MStatus As String
        Dim MStatusData As String

        'Supply Status
        Dim SufficientPaper As String
        Dim PaperLow As String
        Dim PaperExh As String

        'Ribbon
        Dim RibbonOK As String
        Dim RibbonReplaceRecommend As String
        Dim RibbonReplaceMandatory As String

        'Print Head
        Dim PrintHeadOK As String
        Dim PrintHeadReplaceRecommend As String
        Dim PrintHeadReplaceMandatory As String

        'Knife
        Dim KnifeOK As String
        Dim KnifeReplaceRecommend As String

        'iDeviceState
        Dim DeviceStatePrint As String
        Dim DeviceStateNoPrint As String

        'Event Type
        Dim EvtDeviceErr As String
        Dim EvtDeviceReady As String
        Dim EvtDeviceWrap As String
    End Structure

    ReadOnly Property RECEIPTPRINTERHWDInfo() As RECEIPTPRINTERHWDSTR
        Get
            Return udtRECEIPTPRINTERHWDCFG
        End Get
    End Property

    ReadOnly Property RECEIPTPRINTERSETTINGInfo() As RECEIPTPRINTERSETTINGSTR
        Get
            Return udtRECEIPTPRINTERSETTINGCFG
        End Get
    End Property

    ReadOnly Property RECEIPTPRINTERPROPERTIESInfo() As RECEIPTPRINTERPROPERTIESSTR
        Get
            Return udtRECEIPTPRINTERPROPERTIESCFG
        End Get
    End Property

    ReadOnly Property RECEIPTPRINTERLOOKUPDATAInfo() As RECEIPTPRINTERLOOKUPDATASTR
        Get
            Return udtRECEIPTPRINTERLOOKUPDATACFG
        End Get
    End Property


    ReadOnly Property HwdReceiptPrinterName() As String
        Get
            Return udtRECEIPTPRINTERSETTINGCFG.DefaultCustomPrinterName
        End Get
    End Property

#End Region

#Region "Receipt Printer Event"


#End Region

#Region "InitCls/Close Object -  Control"

    'Init Class Object
    Public Function InitRECEIPTPRINTERControl() As Boolean
        Dim strLogIniPath As String = String.Empty
        Dim strRECEIPTPRINTERHWDIniPath As String = String.Empty
        Dim strRECEIPTPRINTERPROPERTIESIniPath As String = String.Empty
        Dim strRECEIPTPRINTERLOOKUPDATAIniPath As String = String.Empty

        Try

            'Input - Default AppLayer\xxxxx.ini
            If objAppLayerINI.ReadAppLayerINICFGFile(HWDLAYERINIPATH, THERMALPRINTER) = True Then

                'Read INI File
                'Log Ini File
                '1.Receipt Printer Setting
                '2.Receipt Printer Properties Setting
                '3.Receipt Printer Lookup Data

                With objAppLayerINI.udtClsAppLayerIniCFG
                    strLogIniPath = .strLogINIPath.Trim
                    strRECEIPTPRINTERHWDIniPath = .strAppLayerINIPath1.Trim
                    strRECEIPTPRINTERPROPERTIESIniPath = .strAppLayerINIPath2.Trim
                    strRECEIPTPRINTERLOOKUPDATAIniPath = .strAppLayerINIPath3.Trim
                End With

                'Layer Class
                InitLog(strLogIniPath)
                strLogEvt = "Receipt Printer Layer Class Init Ok"
                AppLogInfo(strLogEvt)

                'Reference
                'LOG ININ PATH
                '2.DB Parameter
                AppLogInfo("Logger INI Path:" & strLogIniPath)
                AppLogInfo("Receipt Printer HWD INI PATH:" & strRECEIPTPRINTERHWDIniPath.Trim)
                AppLogInfo("Receipt Printer Properties INI PATH:" & strRECEIPTPRINTERPROPERTIESIniPath.Trim)
                AppLogInfo("Receipt Printer Lookup Data INI PATH:" & strRECEIPTPRINTERLOOKUPDATAIniPath.Trim)

                'Init Layer Classes
                '1.Receipt Printer Setting
                If clsReadCUSTOMPrinterSetting() = False Then
                    AppLogWarn("Read CUSTOM Printer Hardware Setting Failed")
                Else
                    With udtRECEIPTPRINTERSETTINGCFG
                        AppLogInfo("Read CUSTOM Printer Hardware Setting OK")
                        AppLogInfo("Custom Printer Name:" & .DefaultCustomPrinterName)
                        AppLogInfo("Left Margin:" & .ReceiptLeftMargin)
                        AppLogInfo("Top Margin:" & .ReceiptTopMargin)
                        AppLogInfo("Bottom Margin:" & .ReceiptBottomMargin)
                        AppLogInfo("Right Margin:" & .ReceiptRightMargin)

                        'Printer Status
                        AppLogInfo("Bypass Unknown Printer Status:" & .blnBypassUnknownStatus)
                        AppLogInfo("Paper Ok Value:" & .strPaperOKCD)
                        AppLogInfo("Paper Jammed Value:" & .strPaperJamCD)
                        AppLogInfo("Paper Low Value:" & .strPaperLowCD)
                        AppLogInfo("Paper Out Value:" & .strPaperOutCD)
                        AppLogInfo("Printer DW Value:" & .strPrintDWValue)

                    End With
                End If

                '2.Receipt Printer Properties Setting
                If clsReadCUSTOMPrinterPropSetting() = False Then
                    AppLogWarn("Read Printer Properties Setting Failed")
                Else
                    AppLogInfo("Read Printer Properties Setting OK")
                End If

                '3.Receipt Printer Lookup Data Setting
                If clsReadReceiptPrinterLookupDataSetting() = False Then
                    AppLogWarn("Read Receipt Printer Lookup Data Setting Failed")
                Else
                    AppLogInfo("Read Receipt Printer Lookup Data Setting OK")
                End If

                blnInitAppLayer = True
                Return True

            Else
                blnInitAppLayer = False
                Return False
            End If

        Catch ex As Exception
            blnInitAppLayer = False
            Return False
        End Try
    End Function

#End Region

#Region "RECEIPT PRINTER Read INI Setting"

    Public Function ReadRECEIPTPRINTERHWDCFG() As Boolean
        Try
            'Read Receipt Printer HWD CFG
            If clsReadReceiptPrinterHWDSetting() = True Then
                AppLogInfo("Read Receipt Printer HWD CFG Info OK")
                Return True
            Else
                AppLogWarn("Read Receipt Printer HWD CFG Info Failed")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in  ReadRECEIPTPRINTERHWDCFG. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function


#End Region

#Region "RECEIPT PRINTER Control Function"

    Public Function InitRECEIPTPRINTERHWD() As Boolean
        objCustomPrinter = New clsCustomPrinter

        Try
            If objCustomPrinter.InitPrinter = True Then
                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            AppLogErr("Error in InitRECEIPTPRINTERHWD. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

#End Region

#Region "Receipt Print Function - StartPrint"

    Public Function StartPrint(ByVal udtPrinterSTR As ReplyReceiptPrinterHWDSTR) As Boolean
        Try
            udtReplyReceiptPrinterHWDSTR = udtPrinterSTR
            AddHandler PrintDoc.PrintPage, AddressOf PrintMDSCashDepositAdviceSlip_Custom
            prtController = New StandardPrintController()
            PrintDoc.PrintController = prtController
            PrintDoc.Print()
            RemoveHandler PrintDoc.PrintPage, AddressOf PrintMDSCashDepositAdviceSlip_Custom

            Return True

        Catch ex As Exception
            AppLogErr("Error in StartPrint. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function


#End Region

#Region "Print Job"

    Private Sub PrintMDSCashDepositAdviceSlip_Custom(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
        PrintMDSCashDepositSlip_Custom(ev, udtReplyReceiptPrinterHWDSTR)
    End Sub

    Private Function PrintMDSCashDepositSlip_Custom(ByVal ev As PrintPageEventArgs, ByVal ReplySTRData As ReplyReceiptPrinterHWDSTR) As Boolean
        Dim intCurrentX As Integer = 0
        'Dim objChqOCBCInfo As ADChqInfoListSTR


        Dim GetMiddleMargin As Integer = 0
        Dim GetLeftMargin As Integer = 0
        Dim GetFontName As String = String.Empty
        Dim GetFontSize As Integer = 0
        Dim GetFontBold As String = "N"
        Dim GetFontItalic As String = "N"
        Dim GetPrinterWidth As Integer
        Dim GetPrinterHeight As Integer
        Dim GetRightMargin As Integer
        Dim Get1stColumnX As Integer
        Dim Get2ndColumnX As Integer
        Dim Get3rdColumnX As Integer


        Dim strImageName As String = ""
        Dim strErrMsg As String = String.Empty
        Dim ReceiptData As String = ""
        Dim ReceiptImg As String = ""
        Dim ReceiptRawData() As String

        Dim blnFirsData As Boolean = False

        Try
            m_intCurrentY = 1
            intCurrentX = 0

            SetPrintoutProp_Custom(True, GetLeftMargin, GetMiddleMargin, GetFontName, GetFontSize, GetFontBold, GetFontItalic, GetPrinterWidth, GetPrinterHeight, GetRightMargin, "", Get1stColumnX, Get2ndColumnX, Get3rdColumnX)


            ReceiptRawData = ReplySTRData.strReceiptData.Split("|")

            For i = 0 To (ReceiptRawData.Length - 1)
                If ReceiptRawData(i).IndexOf(".bmp") > 0 Or ReceiptRawData(i).IndexOf(".gif") > 0 Or ReceiptRawData(i).IndexOf(".jpeg") > 0 Or ReceiptRawData(i).IndexOf(".jpg") > 0 Then
                    ReceiptImg = ReceiptRawData(i)
                    PrintHeadLogo_Custom(ev, ReceiptImg)
                    'objCustomPrinter.CurrYInfo = m_intCurrentY
                Else
                    ReceiptData = ReceiptRawData(i)
                    PrintReceiptData(ev, True, GetLeftMargin, GetFontName, GetFontSize, GetFontBold, GetFontItalic, ReceiptData)
                    ' objCustomPrinter.CurrYInfo = m_intCurrentY
                End If

                objCustomPrinter.CurrYInfo = m_intCurrentY
            Next

            objCustomPrinter.CurrYInfo = 1
            ''print head logo
            'If ReplySTRData.strImageFileName.Length > 0 Then
            '    PrintHeadLogo_Custom(ev, ReplySTRData.strImageFileName)
            'End If

            'If ReplySTRData.strReceiptData.Length > 0 Then
            '    PrintReceiptData(ev, True, GetLeftMargin, GetFontName, GetFontSize, GetFontBold, GetFontItalic, ReplySTRData.strReceiptData)
            '    objCustomPrinter.CurrYInfo = m_intCurrentY
            'End If

            'objCustomPrinter.CurrYInfo = 1

            Return True
        Catch ex As Exception
            strErrMsg = "Print Cheque Deposit Slip Class - " & ex.Message
            'objCLSLog.AppEvtLog(LOGERROR, strErrMsg)
            Return False
        End Try
    End Function

#End Region

#Region "Custom Printer Utilities"

    Private Sub PrintHeadLogo_Custom(ByVal ev As PrintPageEventArgs, ByVal strImage As String)
        Dim GetHeaderIMG As String = String.Empty
        Dim GetHeaderImgHeightPixel As Integer = 0

        objCustomPrinter.CurrYInfo = m_intCurrentY
        GetHeaderIMG = strImage
        '"E:\MDSHWDLayerControlPanel\ReceiptPrinterApp\Image\OCBCReceipt.bmp"
        GetHeaderImgHeightPixel = 10
        'objCustomPrinter.BlankLine(ev.Graphics)
        objCustomPrinter.DoPrintReceiptLogo(ev.Graphics, GetHeaderIMG)
        'objCustomPrinter.BlankLine(ev.Graphics)
        m_intCurrentY = objCustomPrinter.CurrYInfo

    End Sub

    Private Sub SetPrintoutProp_Custom(ByVal bEpson As Boolean, ByRef GetLeftMargin As Integer, ByRef GetMiddleMargin As Integer, ByRef GetFontName As String, ByRef GetFontSize As Integer, ByRef GetFontBold As String, ByRef GetFontItalic As String, ByRef GetPrinterWidth As Integer, ByRef GetPrinterHeight As Integer, ByRef GetRightMargin As Integer, ByRef strNotAvailableImg As String, ByRef Get1stColumnX As Integer, ByRef Get2ndColumnX As Integer, ByRef Get3rdColumnX As Integer)
        GetLeftMargin = udtRECEIPTPRINTERSETTINGCFG.ReceiptLeftMargin
        GetFontName = "Arial"
        GetFontSize = 8
        GetFontBold = "N"
        GetFontItalic = "N"
        GetPrinterWidth = udtRECEIPTPRINTERSETTINGCFG.PaperWidth
        GetPrinterHeight = udtRECEIPTPRINTERSETTINGCFG.PaperHeight

    End Sub

    Private Sub PrintReceiptData(ByVal ev As PrintPageEventArgs, ByVal bEpson As Boolean, _
                               ByVal intLeftMargin As Integer, ByVal strFontName As String, _
                               ByVal intFontSize As Integer, ByVal bBoldFont As String, ByVal bFontItalic As String, _
                               ByVal strData As String)

        objCustomPrinter.DoPrintReceiptData(ev.Graphics, strData, intLeftMargin, strFontName, intFontSize, bBoldFont, bFontItalic)
        m_intCurrentY = objCustomPrinter.CurrYInfo

    End Sub

#End Region


End Class
