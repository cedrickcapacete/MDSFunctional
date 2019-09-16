Imports System
Imports System.IO
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD

Public Class clsUPSControl


#Region "UPS Control"
    Structure UPSSETTINGSTR
        Dim blnEnableUPS As Boolean
        Dim strMonitoringPath As String
        Dim lngCheckInterval As Long
        Dim strTextSplitter As String
        Dim intUPSCommLost As Integer
        Dim intUPSCommEstablished As Integer
        Dim intUPSServiceStart As Integer
        Dim intUPSServiceStop As Integer
        Dim intUPSBatteryMode As Integer
        Dim intUPSPowerSupplyUp As Integer
        Dim blnUPSConn As Boolean
    End Structure

    Structure UPSLOOKUPDATASTR
        Dim DeviceGraphicID As String
        Dim DeviceName As String

        'Device Properties
        Dim TxnStatusACPowerMode As String
        Dim TxnStatusBatteryMode As String
        'Dim TxnStatusDeviceNotCfg As String
        'Dim TxnStatusCancelSideway As String

        'Error Severity
        Dim ErrSvrtyUPSOk As String
        Dim ErrSvrtyUPSError As String
        Dim ErrSvrtyUPSFatal As String
        Dim ErrSvrtyUPSWarning As String

        'Supply Status
        'Power mode
        Dim PowerUP As String
        Dim PowerOnBattery As String
        Dim PowerDown As String

        'Diagnos Status
        Dim MStatus As String
        Dim MStatusData As String

        'iDeviceState
        Dim DeviceStatePowerSupply As String
        Dim DeviceStateBattery As String

        'Event Type
        Dim EvtDeviceErr As String
        Dim EvtDeviceReady As String
        Dim EvtDeviceWrap As String
    End Structure


    ReadOnly Property UPSSETTINGInfo() As UPSSETTINGSTR
        Get
            Return udtUPSHWDCFG
        End Get
    End Property

    ReadOnly Property UPSLOOKUPDATAInfo() As UPSLOOKUPDATASTR
        Get
            Return udtUPSLOOKUPDATACFG
        End Get
    End Property


    ReadOnly Property GetUPSErrorCode() As Integer
        Get
            Return objUPS.GetErrorCode
        End Get
    End Property

#End Region

#Region "InitCls/Close Object -  Control"

    'Init Class Object
    Public Function InitUPSControl() As Boolean
        Dim strLogIniPath As String = String.Empty
        Dim strUPSHWDIniPath As String = String.Empty
        Dim strUPSLOOKUPDATAIniPath As String = String.Empty

        Try

            'Input - Default AppLayer\xxxxx.ini
            If objAppINI.ReadAppLayerINICFGFile(UPSHWDLAYERINIPATH, UPS) = True Then

                'Read INI File
                'Log Ini File
                '1.UPS Setting
                '2.UPS Lookup Data

                With objAppINI.udtClsAppLayerIniCFG
                    strLogIniPath = .strLogINIPath.Trim
                    strUPSHWDIniPath = .strAppLayerINIPath1.Trim
                    strUPSLOOKUPDATAIniPath = .strAppLayerINIPath2.Trim
                End With

                'Layer Class
                InitLog(strLogIniPath)
                strLogEvt = "UPS Layer Class Init Ok"
                AppLogInfo(strLogEvt)

                'Reference
                'LOG ININ PATH
                '1.UPS Setting
                '2.UPS Lookup Data
                AppLogInfo("Logger INI Path:" & strLogIniPath)
                AppLogInfo("UPS HWD INI PATH:" & strUPSHWDIniPath.Trim)
                AppLogInfo("UPS Lookup Data INI PATH:" & strUPSLOOKUPDATAIniPath.Trim)

                'Init Layer Classes
                '1.UPS Setting
                If ReadUPSHWDSetting(strUPSHWDIniPath) = False Then
                    AppLogWarn("Read UPS Hardware Setting Failed")
                Else
                    With udtUPSHWDCFG
                        AppLogInfo("Read UPS Hardware Setting OK")
                        AppLogInfo("UPS Enable: " & .blnEnableUPS)
                        AppLogInfo("Monitoring Path: " & .strMonitoringPath)
                        AppLogInfo("Check Interval: " & .lngCheckInterval)
                        AppLogInfo("Log Splitter:" & .strTextSplitter)
                        AppLogInfo("UPS Communication Lost Flag: " & .intUPSCommLost)
                        AppLogInfo("UPS Service Stopped Flag: " & .intUPSServiceStop)
                        AppLogInfo("UPS Battery Mode Flag: " & .intUPSBatteryMode)
                        AppLogInfo("UPS AC Power Mode Flag: " & .intUPSPowerSupplyUp)
                    End With
                End If


                '2.UPS Lookup Data Setting
                If ReadUPSLookupDataSetting(strUPSLOOKUPDATAIniPath) = False Then
                    AppLogWarn("Read UPS Lookup Data Setting Failed")
                Else
                    AppLogInfo("Read UPS Lookup Data Setting OK")
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

#Region "UPS Control Function"

    Public Function InitUPSHWD() As Boolean
        Try
            If objUPS.InitUPSCommunication = True Then
                udtUPSHWDCFG.blnUPSConn = True
                Return True
            Else
                udtUPSHWDCFG.blnUPSConn = False
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in InitUPSHWD. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function CloseUPSHWD() As Boolean
        Try
            udtUPSHWDCFG.blnUPSConn = False
            Return True
        Catch ex As Exception
            AppLogErr("Error in CloseUPSHWD. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function IsUPSBatteryMode() As Boolean
        Try
            If objUPS.UPSBatteryMode() Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in UPSPowerMode. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

#End Region


End Class
