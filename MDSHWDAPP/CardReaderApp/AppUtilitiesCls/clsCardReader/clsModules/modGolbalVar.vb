Imports clsCardReader.clsCardReaderControl


Module modGolbalVar

#Region "App Layer Default Ini File"
    'CardReader Hardware
    Public Const CARDREADERHWDLAYERINIPATH As String = "AppIniFile\CARDREADERHWDCFG.ini"
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

    Public objPMPCOperation As New clsCardReaderSAM


#End Region




#Region "App Object"

    Public udtCARDREADERHWDCFG As CARDREADERHWDSTR
    Public udtCARDREADERTRXSTATERRORCODE As CARDREADERERROR
    Public udtCARDREADERDEVICECODE As CARDREADERDEVICE

#End Region

#Region "SAM Properties"

    Public Const SC_READ_CMD As Integer = 1
    Public Const SC_UNBLOCK_CMD As Integer = 2
    Public Const SC_UPDATE_TRACK_CMD As Integer = 3
    Public Const SC_UPDATE_TRACK1_CMD As Integer = 4
    Public Const SC_UPDATE_TRACK2_CMD As Integer = 5
    Public Const SC_UPDATE_TRACK3_CMD As Integer = 6
    Public Const SC_PARTIAL_UPDATE_TRACK_CMD As Integer = 7
    Public Const SC_PARTIAL_UPDATE_TRACK1_CMD As Integer = 8
    Public Const SC_PARTIAL_UPDATE_TRACK2_CMD As Integer = 9
    Public Const SC_PARTIAL_UPDATE_TRACK3_CMD As Integer = 10
    Public Const SC_SAM_SETPIN_CMD As Integer = 11
    Public Const SC_SAM_CHGPIN_CMD As Integer = 12
    Public Const SC_SUP_ACCESS_CMD As Integer = 13
    Public Const SUP_SAM_Select_MF_CMD As Integer = 14
    Public Const SUP_READ_CARD_SERIAL_CMD As Integer = 15
    Public Const SUP_READ_SAM_SERIAL_CMD As Integer = 16
    Public Const SC_SUBMIT_PIN_CMD As Integer = 17
    Public Const SC_CHANGE_PIN_CMD As Integer = 18
    Public Const SC_REVERT_OLDPIN_CMD As Integer = 19

    Structure SAMCommand
        Dim CLA() As Byte
        Dim INS() As Byte
        Dim P1() As Byte
        Dim P2() As Byte
        Dim TLEN() As Byte
        Dim TDATA() As Byte
    End Structure

    Structure SAMResponse
        Dim SW1() As Byte
        Dim SW2() As Byte
        Dim TCode() As Byte
        Dim RespLength() As Byte
        Dim RData() As Byte
    End Structure

    Public Const cmdCRDATR As Integer = 1
    Public Const cmdSAMATR As Integer = 2

    Public Const NOERROR As Integer = 0
    Public Const SAMERROR As Integer = 1
    Public Const ICCERROR As Integer = 2
    Public Const NOTONUSTERMINAL As Integer = 3

    Public udtTrackEF As clsCardReaderSAM.CARDEFTrack

    Public LastPMPCProcess As Integer

    Public SAMPINCode() As Byte

#End Region

End Module
