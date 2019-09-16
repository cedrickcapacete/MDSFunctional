Module modGlobalVar

#Region "App Layer Default Ini File"
    'CardReader - Hookup Ini File
    Public Const CARDREADERHOOKUPINIPATH As String = "AppIniFile\CARDREADERHOOKUPCFG.ini"
#End Region


#Region "App Golbal Variable/Object "

    Public strErrMsg As String = String.Empty
    Public strLogEvt As String = String.Empty

    'AppLog CFG - clsLoggerControl
    Public objAppLog As clsAppLogger.clsAppLoggerControl

    Public CardCaptureCount As Integer

    'AppLayer INI CFG - clsAPPLayerINI
    Public objAppLayerINI As New clsAppLayer.clsAppLayerControl



#End Region

#Region "Structure Variable"


    Public udtCardReader As CardReaderHWDSTATUS




#End Region


#Region "Card Reader Device State"

    Public Enum CurrentState
        NoCard = 0
        CardInReader = 1
        CardStage = 2
        Captured = 3
        EjectFailed = 4
        Ejected = 5
        Blocked = 6
    End Enum

#End Region

#Region "CARDREADER Property Value"

    'Device States
    Public DVST_ERRIN_READSTATE As String
    Public DVST_ERRIN_LOCKSTATE As String

    'Error Severity
    Public ERST_NOERR As String
    Public ERST_READERERR As String
    Public ERST_READERFATAL As String
    Public ERST_READERWARN As String

    'Diagnostic Status -DGST
    Public DGST_STATUS As String

    'Supplies Status - SYST
    Public SYST_STATUS_NONEWSTATE As String
    Public SYST_STATUS_NOOVERFILL As String
    Public SYST_STATUS_OVERFILL As String

#End Region

#Region "Golbal APP Structure"

    Structure APPSYSPATHSTR
        Dim APPMAINDIRVE As String
    End Structure

    Public udtAPPSYSPATH As APPSYSPATHSTR

#End Region




#Region "Hardware CARDREADER Const Value"
    Public CardReaderDeviceGraphicId As String
    Public CardReaderDeviceName As String
#End Region



#Region "CARDREADER CardStatus"

    Public Const POSITIVEREPLY As String = "PositiveReply"
    Public Const NEGATIVEREPLY As String = "NegativeReply"
    Public Const RECVREPLYFAIL As String = "ReplyReceivingFailure"
    Public Const CMDCANCEL As String = "CommandCancellation"
    Public Const REPLYTIMEOUT As String = "ReplyTimeout"

    Public Const NOCARD As String = "0"
    Public Const CARDATGATE As String = "1"
    Public Const CARDINSIDE As String = "2"

#End Region

#Region "CARDREADER Const Status"

    Public DST_NOCARD As String
    Public DST_CARDINREADER As String
    Public DST_CARDSTAGE As String
    Public DST_CARDCAPTURE As String
    Public DST_CARDEJECTFAIL As String
    Public DST_CARDEJECT As String
    Public DST_CARDBLOCK As String

#End Region

#Region "CARDREADER Const Event Type"

    Public EVTTYPE_WRAPDEVICE As String
    Public EVTTYPE_TIMEOUT As String
    Public EVTTYPE_DATAARRIVED As String

#End Region


#Region "Property Tmp Variable and Control"

    Public strProTxnStatus As String = String.Empty
    Public strProErrSeverity As String = String.Empty
    Public strProDignosticStatus As String = String.Empty
    Public strProSupplyStatus As String = String.Empty

#End Region

#Region "Function - GenDateTimeStamp"

    Public Function GenDateTimeStamp() As String
        Dim strDateTimeStamp As String = String.Empty
        Dim strDay As String = String.Empty
        Dim strMonth As String = String.Empty
        Dim strYear As String = String.Empty

        Dim strHH As String = String.Empty
        Dim strMM As String = String.Empty
        Dim strSS As String = String.Empty

        Dim strTmpdate As String = String.Empty
        Dim strTmptime As String = String.Empty

        Try

            strDay = DateTime.Now.Day.ToString("00")
            strMonth = DateTime.Now.Month.ToString("00")
            strYear = DateTime.Now.Year.ToString("0000")

            strHH = DateTime.Now.Hour.ToString("00")
            strMM = DateTime.Now.Minute.ToString("00")
            strSS = DateTime.Now.Second.ToString("00")

            strTmpdate = strDay & strMonth & strYear
            strTmptime = strHH & strMM & strSS

            strDateTimeStamp = strTmpdate.Trim & strTmptime.Trim

            Return strDateTimeStamp
        Catch ex As Exception
            AppLogErr("Error in GenDateTimeStamp:" & ex.Message)
            Return "9999"
        End Try
    End Function


#End Region


End Module
