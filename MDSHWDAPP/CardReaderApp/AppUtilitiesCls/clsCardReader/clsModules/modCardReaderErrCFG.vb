Imports System
Imports System.IO
Imports FileUtils
Imports clsCardReader.clsCardReaderControl


Module modCardReaderErrCFG

#Region "Cls Variable"
    Private strTitle As String = "modCardReaderErrCFG"
    Private objINIFile As IniFile = Nothing
    Private strErrMsg As String = String.Empty
    Dim strINIPath As String = String.Empty
#End Region

#Region "INI - Card Reader Error"

    Private Const CARDREADER_ERROR_SECT As String = "TRXERRCODE"

    Private Const CARDREADER_NOERROR As String = "NOERROR"
    Private Const CARDREADER_CARDCAPTURETIMEOUT As String = "CARDCAPTURE"
    Private Const CARDREADER_CARDCAPTUREJAM As String = "CARDJAM"
    Private Const CARDREADER_CARDUPDERR As String = "CARDUPDERROR"
    Private Const CARDREADER_INVALIDTRACK As String = "INVALIDTRACK"
    Private Const CARDREADER_ERRTRACKDATA As String = "ERRORTRACKDATA"

    Private Const CARDREADER_SMRTCRDGOOD As String = "SMRTCRDGOOD"
    Private Const CARDREADER_SMRTCRDCMDERR As String = "SMRTCRDCMDERR"
    Private Const CARDREADER_SMRTCRDNOTPRESENT As String = "SMRTCRDNOTPRESENT"
    Private Const CARDREADER_SMRTCRDFATAL As String = "SMRTCRDFATAL"
    Private Const CARDREADER_SCIFWARNING As String = "SCIFWARNING"
    Private Const CARDREADER_SCIFENCRPYTERR As String = "SCIFENCRPYTERR"
    Private Const CARDREADER_CARDNOTAVAILABLE As String = "CARDNOTAVAILABLE"


    Private Const CARDREADER_DEVICECODE_SECT As String = "DEVICECODE"

    Private Const CARDREADER_DEVICE_GRAPHICID As String = "DEVICE_GRAPHIC_ID"
    Private Const CARDREADER_DEVICE_NAME As String = "DEVICE_NAME"

    Private Const CARDREADER_DST_NOCARD As String = "DST_NOCARD"
    Private Const CARDREADER_DST_CARDINREADER As String = "DST_CARDINREADER"
    Private Const CARDREADER_DST_CARDSTAGE As String = "DST_CARDSTAGE"
    Private Const CARDREADER_DST_CARDCAPTURE As String = "DST_CARDCAPTURE"
    Private Const CARDREADER_DST_CARDEJECTFAIL As String = "DST_CARDEJECTFAIL"
    Private Const CARDREADER_DST_CARDEJECT As String = "DST_CARDEJECT"
    Private Const CARDREADER_DST_CARDBLOCK As String = "DST_CARDBLOCK"

    Private Const CARDREADER_EVT_WRAPDEV As String = "EVTTYPE_WRAPDEVICE"
    Private Const CARDREADER_EVT_TIMEOUT As String = "EVTTYPE_TIMEOUT"
    Private Const CARDREADER_EVT_DATAARRIVED As String = "EVTTYPE_DATAARRIVED"
    Private Const CARDREADER_EVT_ERROR As String = "EVTTYPE_ERROR"

    Private Const CARDREADER_DVST_READSTATE As String = "DVST_ERRIN_READSTATE"
    Private Const CARDREADER_DVST_LOCKSTATE As String = "DVST_ERRIN_LOCKSTATE"

    Private Const CARDREADER_ERST_NOERR As String = "ERST_NOERR"
    Private Const CARDREADER_ERST_READERERR As String = "ERST_READERERR"
    Private Const CARDREADER_ERST_READERFATAL As String = "ERST_READERFATAL"
    Private Const CARDREADER_ERST_READERWARN As String = "ERST_READERWARN"

    Private Const CARDREADER_DGST_STATUS As String = "DGST_STATUS"

    Private Const CARDREADER_SYST_STATUSNONEWSTATE As String = "SYST_STATUS_NONEWSTATE"
    Private Const CARDREADER_SYST_STATUSNOOVERFILL As String = "SYST_STATUS_NOOVERFILL"
    Private Const CARDREADER_SYST_STATUSOVERFILL As String = "SYST_STATUS_OVERFILL"

#End Region

#Region "Clear INI"

    Private Sub CLSCARDREADERHWDERRSTR()
        Try
            'Clear CardReader Hardware Structure
            With udtCARDREADERTRXSTATERRORCODE
                .CARDRDRTRXSTATNOERR = "0"
                .CARDRDRTRXSTATCARDCAPTURETIMEOUT = "1"
                .CARDRDRTRXSTATCARDCAPTUREJAM = "2"
                .CARDRDRTRXSTATINVALIDTRACK = "4"
                .CARDRDRTRXSTATERRTRCKDT = "7"

                .CARDRDRSMRTCARDGOOD = "0"
                .CARDRDRSMRTCARDCMDERR = "1"
                .CARDRDRSMRTCARDNOPRSNT = "3"
                .CARDRDRSMRTCARDFATALSMRTCARD = "8"
                .CARDRDRSMRTCARDSCIFWARN = "9"
                .CARDRDRSMRTCARDSCIFENCRYPTERR = ":"
                .CARDRDRSMRTCARDOUTPOSITION = ";"
            End With

        Catch ex As Exception
            strErrMsg = "Error in CLSCARDREADERHWDSTR. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub
#End Region

#Region "clsCARDREADERHWDERRORCODE"

    Public Function clsReadCARDREADERErrorCode() As Boolean
        Dim strAppPath As String = String.Empty
        Dim strAppName As String = String.Empty
        Try

            'Defautl INI File Path
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath2.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = CARDREADER_ERROR_SECT
                With udtCARDREADERTRXSTATERRORCODE
                    .CARDRDRTRXSTATNOERR = objINIFile.GetStrVal(CARDREADER_NOERROR, "0")
                    .CARDRDRTRXSTATCARDCAPTURETIMEOUT = objINIFile.GetStrVal(CARDREADER_CARDCAPTURETIMEOUT, "1")
                    .CARDRDRTRXSTATCARDCAPTUREJAM = objINIFile.GetStrVal(CARDREADER_CARDCAPTUREJAM, "2")
                    .CARDRDRTRXSTATCARDUPDERR = objINIFile.GetStrVal(CARDREADER_CARDUPDERR, "3")
                    .CARDRDRTRXSTATINVALIDTRACK = objINIFile.GetStrVal(CARDREADER_INVALIDTRACK, "4")

                    .CARDRDRSMRTCARDGOOD = objINIFile.GetStrVal(CARDREADER_SMRTCRDGOOD, "0")
                    .CARDRDRSMRTCARDCMDERR = objINIFile.GetStrVal(CARDREADER_SMRTCRDCMDERR, "1")
                    .CARDRDRSMRTCARDNOPRSNT = objINIFile.GetStrVal(CARDREADER_SMRTCRDNOTPRESENT, "3")
                    .CARDRDRSMRTCARDFATALSMRTCARD = objINIFile.GetStrVal(CARDREADER_SMRTCRDFATAL, "8")
                    .CARDRDRSMRTCARDSCIFWARN = objINIFile.GetStrVal(CARDREADER_SCIFWARNING, "9")
                    .CARDRDRSMRTCARDSCIFENCRYPTERR = objINIFile.GetStrVal(CARDREADER_SCIFENCRPYTERR, ":")
                    .CARDRDRSMRTCARDOUTPOSITION = objINIFile.GetStrVal(CARDREADER_SCIFWARNING, ";")
                End With
                Return True
            Else
                'Ini File Path Not Found
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in clsReadCARDREADERErrorCode. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function

    Public Function clsReadCARDREADERDeviceNDCCode() As Boolean
        Dim strAppPath As String = String.Empty
        Dim strAppName As String = String.Empty
        Try

            'Defautl INI File Path
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath2.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = CARDREADER_DEVICECODE_SECT
                With udtCARDREADERDEVICECODE

                    .CARDRDR_DEVGRAPHICID = objINIFile.GetStrVal(CARDREADER_DEVICE_GRAPHICID, "D")
                    .CARDRDR_DEVGRAPHICNAME = objINIFile.GetStrVal(CARDREADER_DEVICE_NAME, "CARDREADER")

                    .CARDRDR_DSTNOCARD = objINIFile.GetStrVal(CARDREADER_DST_NOCARD, "0")
                    .CARDRDR_DSTCARDINREADER = objINIFile.GetStrVal(CARDREADER_DST_CARDINREADER, "1")
                    .CARDRDR_DSTCARDSTAGE = objINIFile.GetStrVal(CARDREADER_DST_CARDSTAGE, "2")
                    .CARDRDR_DSTCARDCAPTURE = objINIFile.GetStrVal(CARDREADER_DST_CARDCAPTURE, "3")
                    .CARDRDR_DSTCARDEJECTFAIL = objINIFile.GetStrVal(CARDREADER_DST_CARDEJECTFAIL, "4")
                    .CARDRDR_DSTCARDEJECT = objINIFile.GetStrVal(CARDREADER_DST_CARDEJECT, "5")
                    .CARDRDR_DSTCARDBLOCK = objINIFile.GetStrVal(CARDREADER_DST_CARDBLOCK, "5")

                    .CARDRDR_EVTTYPE_DATAARRIVED = objINIFile.GetStrVal(CARDREADER_EVT_DATAARRIVED, "DATAARRIVED")
                    .CARDRDR_EVTTYPE_TIMEOUT = objINIFile.GetStrVal(CARDREADER_EVT_TIMEOUT, "TIMEOUT")
                    .CARDRDR_EVTTYPE_ERROR = objINIFile.GetStrVal(CARDREADER_EVT_ERROR, "ERROR")
                    .CARDRDR_EVTTYPE_WRAPDEVICE = objINIFile.GetStrVal(CARDREADER_EVT_WRAPDEV, "WRAPDEVICE")

                    .CARDRDR_DVST_ERRIN_READSTATE = objINIFile.GetStrVal(CARDREADER_DVST_READSTATE, "0")
                    .CARDRDR_DVST_ERRIN_LOCKSTATE = objINIFile.GetStrVal(CARDREADER_DVST_LOCKSTATE, "1")

                    .CARDRDR_ERST_NOERR = objINIFile.GetStrVal(CARDREADER_ERST_NOERR, "0")
                    .CARDRDR_ERST_READERERR = objINIFile.GetStrVal(CARDREADER_ERST_READERERR, "1")
                    .CARDRDR_ERST_READERFATAL = objINIFile.GetStrVal(CARDREADER_ERST_READERFATAL, "2")
                    .CARDRDR_ERST_READERWARN = objINIFile.GetStrVal(CARDREADER_ERST_READERWARN, "3")

                    .CARDRDR_DGST_STATUS = objINIFile.GetStrVal(CARDREADER_DGST_STATUS, "0000000000000000")

                    .CARDRDR_SYST_STATUSNONEWSTATE = objINIFile.GetStrVal(CARDREADER_SYST_STATUSNONEWSTATE, "0")
                    .CARDRDR_SYST_STATUSNOOVERFILL = objINIFile.GetStrVal(CARDREADER_SYST_STATUSNOOVERFILL, "1")
                    .CARDRDR_SYST_STATUSOVERFILL = objINIFile.GetStrVal(CARDREADER_SYST_STATUSOVERFILL, "0")

                End With
                Return True
            Else
                'Ini File Path Not Found
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in clsReadCARDREADERErrorCode. ErrInfo:" & ex.Message
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
