Imports System
Imports System.IO
Imports FileUtils


Module modCashDepositCFG

#Region "Cls Variable"
    'Private strTitle As String = "modReadCashDepositCFG"
    Private objINIFile As IniFile = Nothing

#End Region

#Region "INI Session"

    Private Const CASHDEPOSITOR_SEC As String = "CASHDEPOSITORSETTING"
    Private Const CASHDEPOSITOR_ENABLE As String = "ENABLE"
    Private Const CASHDEPOSITOR_COMPORT As String = "Comport"

    Private Const CASHDEPOSITOR_TRACEFILE As String = "TraceFile"
    Private Const CASHDEPOSITOR_CHEQUEIMAGE As String = "ChequeImage"
    Private Const CASHDEPOSITOR_TEMPLATEFILE As String = "TemplateFile"
    Private Const CASHDEPOSITOR_OPERATIONMODE As String = "OperationMode"

    Private Const CASHDEPOSITOR_INSERTITEMTIMEOUT As String = "InsertItemTimeOut"
    Private Const CASHDEPOSITOR_CLEANFEEDERTIMEOUT As String = "CleanFeederTimeOut"
    Private Const CASHDEPOSITOR_REPOSITIONDOCTIMEOUT As String = "RepositionDocTimeOut"
    Private Const CASHDEPOSITOR_TAKERETURNITEMTIMEOUT As String = "TakeReturnItemTimeOut"

    Private Const CASHDEPOSITOR_MACHINESTARTUPTIMEOUT As String = "MachineStartupTimeout"

    Private Const CASHDEPOSITOR_FORCEACCEPTCHEQUE As String = "ForceAcceptCheque"


    'MIXModeForceAcceptCheque=0
    Private Const CASHDEPOSITOR_MIXModeForceAcceptCheque As String = "MIXModeCheckChequeMICR"



    ';Min Required Length
    'MICRMinLen=20

    ';Num of ? MICR Reject Control
    ';Default Value:20
    'MICRERRRejectCnt=20

    Private Const MICR_MINLEN As String = "MICRMinLen"
    Private Const MICR_MAXMARK As String = "MICRERRRejectCnt"



    ';MDS Door Sensor Checking
    ';Default 3 sec
    'MDSDoorCheckInterval=3

    Private Const MDSDOOR_CHECKINTERVAL As String = "MDSDoorCheckInterval"

    ';IMG Conversion
    ';JEPG and TIFF File
    'IMGConversion=1

    Private Const MDSIMG_CONVERSION As String = "IMGConversion"


    ';Kiosk CardReader Mode
    ';1=ON,2=OFF,3=FAST,4=SLOW

    'MDSLightFlashing=1
    'MDSCardRearderLightFlashing=4

    Private Const MDSLIGHTMODE As String = "MDSLightFlashing"
    Private Const MDSCRLIGHTMODE As String = "MDSCardRearderLightFlashing"


    'NEW 
    ';-------- New Changes 11July2016 : MDS Control ------------
    ';WFS_STAT_DEVHWERROR
    ';WFS_SYSE_HARDWARE_ERROR (Missing reply to periodic ping.)
    ';WFS_EXECUTE_COMPLETE(WFS_CMD_CIM_RESET) returned WFS_ERR_CONNECTION_LOST
    ';Seperator By = "|"
    'MDSAutoReconnectCTRFlag="WFS_SYSE_HARDWARE_ERROR (Missing reply to periodic ping.)"
    Private Const MDSAutoReconnectCTR_Flag As String = "MDSAutoReconnectCTRFlag"





#End Region


#Region "CASH INI SECTION"
    Private Const CASH_DEVICECODE_SECT As String = "CASHMODE"

    Private Const CASH_DEVICEGRAPHIC_ID As String = "DEVICE_GRAPHIC_ID"
    Private Const CASH_DEVICENAME_ID As String = "DEVICE_NAME"

    Private Const CASH_EVENTTYPE_WRAP_DEVICE As String = "EVTTYPE_WRAPDEVICE"
    Private Const CASH_EVENTTYPE_TIMEOUT As String = "EVTTYPE_TIMEOUT"
    Private Const CASH_EVENTTYPE_DATAARRIVED As String = "EVTTYPE_DATAARRIVED"
    Private Const CASH_EVENTTYPE_ERROR As String = "EVTTYPE_ERROR"

    Private Const CASH_DVST_ERRINDEVSTATE As String = "DVST_ERRIN_DEVSTATE"

    Private Const CASH_ERRORSTATE_NOERROR As String = "ERST_NOERR"
    Private Const CASH_ERRORSTATE_DEPOSITORERR As String = "ERST_CDERR"
    Private Const CASH_ERRORSTATE_DEPOSITOREFATAL As String = "ERST_CDFATAL"
    Private Const CASH_ERRORSTATE_DEPOSITOREWARN As String = "ERST_CDWARN"

    Private Const CASH_DEVICESTATUS_DATA As String = "DGST_STATUS"
    Private Const CASH_SYSTEMSTATUS_NOSTATE As String = "SYST_STATUS_NOSTATE"

#End Region

#Region "CHEQUE INI SECTION"

    Private Const CHEQUE_DEVICECODE_SECT As String = "CHEQUEMODE"

    Private Const CHEQUE_DEVICEGRAPHIC_ID As String = "DEVICE_GRAPHIC_ID"
    Private Const CHEQUE_DEVICENAME_ID As String = "DEVICE_NAME"

    Private Const CHEQUE_EVENTTYPE_WRAP_DEVICE As String = "EVTTYPE_WRAPDEVICE"
    Private Const CHEQUE_EVENTTYPE_TIMEOUT As String = "EVTTYPE_TIMEOUT"
    Private Const CHEQUE_EVENTTYPE_DATAARRIVED As String = "EVTTYPE_DATAARRIVED"
    Private Const CHEQUE_EVENTTYPE_ERROR As String = "EVTTYPE_ERROR"

    Private Const CHEQUE_DVST_ERRINDEVSTATE As String = "DVST_ERRIN_DEVSTATE"

    Private Const CHEQUE_ERRORSTATE_NOERROR As String = "ERST_NOERR"
    Private Const CHEQUE_ERRORSTATE_DEPOSITORERR As String = "ERST_CDERR"
    Private Const CHEQUE_ERRORSTATE_DEPOSITOREFATAL As String = "ERST_CDFATAL"
    Private Const CHEQUE_ERRORSTATE_DEPOSITOREWARN As String = "ERST_CDWARN"

    Private Const CHEQUE_DEVICESTATUS_DATA As String = "DGST_STATUS"
    Private Const CHEQUE_SYSTEMSTATUS_NOSTATE As String = "SYST_STATUS_NOSTATE"

#End Region

#Region "DOORSENSOR INI SECTION"
    Private Const DOOR_DEVICECODE_SECT As String = "DOORSENSOR"

    Private Const DOOR_DEVICEGRAPHIC_ID As String = "DEVICE_GRAPHIC_ID"
    Private Const DOOR_DEVICENAME_ID As String = "DEVICE_NAME"

    Private Const DOOR_EVENTTYPE_WRAP_DEVICE As String = "EVTTYPE_WRAPDEVICE"
    Private Const DOOR_EVENTTYPE_TIMEOUT As String = "EVTTYPE_TIMEOUT"
    Private Const DOOR_EVENTTYPE_DATAARRIVED As String = "EVTTYPE_DATAARRIVED"
    Private Const DOOR_EVENTTYPE_ERROR As String = "EVTTYPE_ERROR"

#End Region

#Region "Read Ini File"

    Public Function ReadCASHDEPOSITORSetting() As Boolean
        Dim intDoorCheckInterval As Integer = 3

        Try

            'Defautl INI File Path
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = CASHDEPOSITOR_SEC
                With udtDEPOSITORHWDCFG
                    .blnCASHDEPOSITORENABLE = objINIFile.GetBoolVal(CASHDEPOSITOR_ENABLE, False)
                    .CASHDEPOSITORPortNum = objINIFile.GetIntVal(CASHDEPOSITOR_COMPORT, 3)

                    .CASHDEPOSITOROperationMode = objINIFile.GetStrVal(CASHDEPOSITOR_OPERATIONMODE, String.Empty)

                    .CASHDEPOSITORTraceFilePath = objINIFile.GetStrVal(CASHDEPOSITOR_TRACEFILE, String.Empty)
                    .CASHDEPOSITORChequeImagePath = objINIFile.GetStrVal(CASHDEPOSITOR_CHEQUEIMAGE, String.Empty)
                    .CASHDEPOSITORTemplateFilePath = objINIFile.GetStrVal(CASHDEPOSITOR_TEMPLATEFILE, String.Empty)

                    .CASHDEPOSITORInsertItemTimeout = objINIFile.GetIntVal(CASHDEPOSITOR_INSERTITEMTIMEOUT, 0)
                    .CASHDEPOSITORTakeReturnTimeout = objINIFile.GetIntVal(CASHDEPOSITOR_TAKERETURNITEMTIMEOUT, 0)
                    .CASHDEPOSITORCleanFeederTimeout = objINIFile.GetIntVal(CASHDEPOSITOR_CLEANFEEDERTIMEOUT, 0)
                    .CASHDEPOSITORRepositonDocTimeout = objINIFile.GetIntVal(CASHDEPOSITOR_REPOSITIONDOCTIMEOUT, 0)

                    .CASHDEPOSITORMachineStartupTimeout = objINIFile.GetIntVal(CASHDEPOSITOR_MACHINESTARTUPTIMEOUT, 0)

                    .blnCASHDEPOSITORForceAcceptChq = objINIFile.GetBoolVal(CASHDEPOSITOR_FORCEACCEPTCHEQUE, False)


                    'NEW 03/04/2017
                    .blnMIXModeMICRCheck = objINIFile.GetBoolVal(CASHDEPOSITOR_MIXModeForceAcceptCheque, False)


                    'MICR
                    .intMinMICRLEN = objINIFile.GetIntVal(MICR_MINLEN, 20)
                    .intMaxErrMark = objINIFile.GetIntVal(MICR_MAXMARK, 15)


                    'Door Check interval
                    '1sec = 1000
                    intDoorCheckInterval = objINIFile.GetIntVal(MDSDOOR_CHECKINTERVAL, 3)
                    .DOORCheckInterval = intDoorCheckInterval * 1000

                    .blnIMGConversion = objINIFile.GetBoolVal(MDSIMG_CONVERSION, True)

                    'Light Mode
                    '.strMDSLightMode = objINIFile.GetStrVal(MDSLIGHTMODE, "1")
                    '.strMDSCardReaderLightMode = objINIFile.GetStrVal(MDSCRLIGHTMODE, "4")


                    'New
                    .strMDSErrStatusReply = objINIFile.GetStrVal(MDSAutoReconnectCTR_Flag, String.Empty)


                End With
                Return True
            Else
                'Ini File Path Not Found
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in clsMDSXFS-ReadCASHDEPOSITORSetting. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function


    Public Function UpdateMDSSetting(ByVal strEnableOpt As String, ByVal strForeChqOpt As String, ByVal strComportNo As String, ByVal strInsertItemTMOut As String, ByVal strRepositionTMOut As String, ByVal strCleanFeederTMOut As String, ByVal strTakeReturnTMOut As String, ByVal strTraceLogPath As String, ByVal strChqImagePath As String, ByVal strChqTemplatePath As String) As Boolean
        Try

            'Defautl INI File Path
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File 
                With objINIFile
                    .CurrentSection = CASHDEPOSITOR_SEC

                    .SetVal(CASHDEPOSITOR_ENABLE, strEnableOpt)
                    .SetVal(CASHDEPOSITOR_FORCEACCEPTCHEQUE, strForeChqOpt)
                    .SetVal(CASHDEPOSITOR_COMPORT, strComportNo)

                    .SetVal(CASHDEPOSITOR_INSERTITEMTIMEOUT, strInsertItemTMOut)
                    .SetVal(CASHDEPOSITOR_CLEANFEEDERTIMEOUT, strCleanFeederTMOut)
                    .SetVal(CASHDEPOSITOR_REPOSITIONDOCTIMEOUT, strRepositionTMOut)
                    .SetVal(CASHDEPOSITOR_TAKERETURNITEMTIMEOUT, strTakeReturnTMOut)

                    .SetVal(CASHDEPOSITOR_TRACEFILE, strTraceLogPath)
                    .SetVal(CASHDEPOSITOR_CHEQUEIMAGE, strChqImagePath)
                    .SetVal(CASHDEPOSITOR_TEMPLATEFILE, strChqTemplatePath)
                End With

                AppLogInfo("UpdateMDSSetting Success")
                Return True
            Else
                AppLogWarn("File Not Exist:" & strINIPath)
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in UpdateMDSSetting. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function


#End Region

#Region "Device Code - INI File Setting"

    Public Function ReadDEPOSITORCASHDEVCODE() As Boolean
      
        Try
            'Defautl INI File Path
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath2.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = CASH_DEVICECODE_SECT

                With udtCASHMODEDevCode
                    .CASHDEVDeviceID = objINIFile.GetStrVal(CASH_DEVICEGRAPHIC_ID, String.Empty)
                    .CASHDEVDeviceName = objINIFile.GetStrVal(CASH_DEVICENAME_ID, String.Empty)

                    .CASHDEVEventWrap = objINIFile.GetStrVal(CASH_EVENTTYPE_WRAP_DEVICE, String.Empty)
                    .CASHDEVEventDataArrive = objINIFile.GetStrVal(CASH_EVENTTYPE_DATAARRIVED, String.Empty)
                    .CASHDEVEventError = objINIFile.GetStrVal(CASH_EVENTTYPE_ERROR, String.Empty)
                    .CASHDEVEventTimeout = objINIFile.GetStrVal(CASH_EVENTTYPE_TIMEOUT, String.Empty)

                    .CASHDEVErrDevState = objINIFile.GetStrVal(CASH_DVST_ERRINDEVSTATE, String.Empty)

                    .CASHDEVErrorStateNoError = objINIFile.GetStrVal(CASH_ERRORSTATE_NOERROR, String.Empty)
                    .CASHDEVErrorStateError = objINIFile.GetStrVal(CASH_ERRORSTATE_DEPOSITORERR, String.Empty)
                    .CASHDEVErrorStateFatal = objINIFile.GetStrVal(CASH_ERRORSTATE_DEPOSITOREFATAL, String.Empty)
                    .CASHDEVErrorStateWarning = objINIFile.GetStrVal(CASH_ERRORSTATE_DEPOSITOREWARN, String.Empty)

                    .CASHDEVErrorDGSTStatus = objINIFile.GetStrVal(CASH_DEVICESTATUS_DATA, String.Empty)
                    .CASHDEVErrorSysStatNoState = objINIFile.GetStrVal(CASH_SYSTEMSTATUS_NOSTATE, String.Empty)
                End With

                Return True
            Else
                'Ini File Path Not Found
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in clsMDSXFS-ReadDEPOSITORCASHDEVCODE. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function

    Public Function ReadDEPOSITORCHEQUEDEVCODE() As Boolean
      
        Try
            'Defautl INI File Path
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath2.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = CHEQUE_DEVICECODE_SECT

                With udtCHQMODEDevCode
                    .CHQDEVDeviceID = objINIFile.GetStrVal(CHEQUE_DEVICEGRAPHIC_ID, String.Empty)
                    .CHQDEVDeviceName = objINIFile.GetStrVal(CHEQUE_DEVICENAME_ID, String.Empty)

                    .CHQDEVEventWrap = objINIFile.GetStrVal(CHEQUE_EVENTTYPE_WRAP_DEVICE, String.Empty)
                    .CHQDEVEventDataArrive = objINIFile.GetStrVal(CHEQUE_EVENTTYPE_DATAARRIVED, String.Empty)
                    .CHQDEVEventError = objINIFile.GetStrVal(CHEQUE_EVENTTYPE_ERROR, String.Empty)
                    .CHQDEVEventTimeout = objINIFile.GetStrVal(CHEQUE_EVENTTYPE_TIMEOUT, String.Empty)

                    .CHQDEVErrDevState = objINIFile.GetStrVal(CHEQUE_DVST_ERRINDEVSTATE, String.Empty)

                    .CHQDEVErrorStateNoError = objINIFile.GetStrVal(CHEQUE_ERRORSTATE_NOERROR, String.Empty)
                    .CHQDEVErrorStateError = objINIFile.GetStrVal(CHEQUE_ERRORSTATE_DEPOSITORERR, String.Empty)
                    .CHQDEVErrorStateFatal = objINIFile.GetStrVal(CHEQUE_ERRORSTATE_DEPOSITOREFATAL, String.Empty)
                    .CHQDEVErrorStateWarning = objINIFile.GetStrVal(CHEQUE_ERRORSTATE_DEPOSITOREWARN, String.Empty)

                    .CHQDEVErrorDGSTStatus = objINIFile.GetStrVal(CHEQUE_DEVICESTATUS_DATA, String.Empty)
                    .CHQDEVErrorSysStatNoState = objINIFile.GetStrVal(CHEQUE_SYSTEMSTATUS_NOSTATE, String.Empty)
                End With

                Return True
            Else
                'Ini File Path Not Found
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in clsMDSXFS-ReadDEPOSITORCHEQUEDEVCODE. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function

    Public Function ReadDOORSENSORDEVCODE() As Boolean
       
        Try
            'Defautl INI File Path
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath2.Trim

            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = DOOR_DEVICECODE_SECT

                With udtDOORSENSORDevCode
                    .DOORDEVDeviceID = objINIFile.GetStrVal(DOOR_DEVICEGRAPHIC_ID, String.Empty)
                    .DOORDEVDeviceName = objINIFile.GetStrVal(DOOR_DEVICENAME_ID, String.Empty)

                    .DOORDEVEventWrap = objINIFile.GetStrVal(DOOR_EVENTTYPE_WRAP_DEVICE, String.Empty)
                    .DOORDEVEventDataArrive = objINIFile.GetStrVal(DOOR_EVENTTYPE_DATAARRIVED, String.Empty)
                    .DOORDEVEventError = objINIFile.GetStrVal(DOOR_EVENTTYPE_ERROR, String.Empty)
                    .DOORDEVEventTimeout = objINIFile.GetStrVal(DOOR_EVENTTYPE_TIMEOUT, String.Empty)
                End With

                Return True
            Else
                'Ini File Path Not Found
                Return False
            End If

        Catch ex As Exception
            strErrMsg = "Error in clsMDSXFS-ReadDOORSENSORDEVCODE. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
            Return False
        Finally
            If Not (objINIFile Is Nothing) Then
                objINIFile = Nothing
            End If
        End Try
    End Function

#End Region

#Region "Update INI File Action - MDS Timeout"

    Public Sub WriteInsertFeederTimeout(ByVal value As Integer)
        Try
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim
            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = CASHDEPOSITOR_SEC
                objINIFile.SetVal(CASHDEPOSITOR_INSERTITEMTIMEOUT, value)
                AppLogInfo("WriteInsertFeederTimeout :" & value)
            End If
        Catch ex As Exception
            strErrMsg = "Error in clsMDSXFS-WriteInsertFeederTimeout. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub

    Public Sub WriteCleanFeederTimeout(ByVal value As Integer)
        Try
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim
            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = CASHDEPOSITOR_SEC
                objINIFile.SetVal(CASHDEPOSITOR_CLEANFEEDERTIMEOUT, value)
                AppLogInfo("WriteCleanFeederTimeout :" & value)
            End If
        Catch ex As Exception
            strErrMsg = "Error in clsMDSXFS-WriteCleanFeederTimeout. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub

    Public Sub WriteRepositionTimeout(ByVal value As Integer)
        Try
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim
            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = CASHDEPOSITOR_SEC
                objINIFile.SetVal(CASHDEPOSITOR_REPOSITIONDOCTIMEOUT, value)
                AppLogInfo("WriteRepositionTimeout :" & value)
            End If
        Catch ex As Exception
            strErrMsg = "Error in clsMDSXFS-WriteRepositionTimeout. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub

    Public Sub WriteTakeReturnedItemTimeout(ByVal value As Integer)
        Try
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim
            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = CASHDEPOSITOR_SEC
                objINIFile.SetVal(CASHDEPOSITOR_TAKERETURNITEMTIMEOUT, value)
                AppLogInfo("WriteTakeReturnedItemTimeout :" & value)
            End If
        Catch ex As Exception
            strErrMsg = "Error in clsMDSXFS-WriteTakeReturnedItemTimeout. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub

    Public Sub WriteMachineStartupTimeout(ByVal value As Integer)
        Try
            strINIPath = objAppLayerINI.udtClsAppLayerIniCFG.strAppLayerINIPath1.Trim
            If (File.Exists(strINIPath)) Then
                objINIFile = New IniFile(strINIPath, False)
                'App Info Ini File
                objINIFile.CurrentSection = CASHDEPOSITOR_SEC
                objINIFile.SetVal(CASHDEPOSITOR_MACHINESTARTUPTIMEOUT, value)
                AppLogInfo("WriteMachineStartupTimeout :" & value)
            End If
        Catch ex As Exception
            strErrMsg = "Error in clsMDSXFS-WriteMachineStartupTimeout. ErrInfo:" & ex.Message
            AppLogErr(strErrMsg)
        End Try
    End Sub
#End Region

End Module
