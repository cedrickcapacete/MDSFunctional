Imports System
Imports System.Text
Imports clsAppMainDirectory.clsAppMainDirectoryControl.MDSHWD


Public Class clsKeypadControl
    'Inherits clsEPPKeypad

   
#Region "Keypad Control"
    Structure KEYPADHWDSTR
        Dim strKeypadModel As String
        Dim blnKeypadStatus As Boolean
        Dim blnHideMode As Boolean
        Dim blnEncryptMode As Boolean
        'Dim blnPaperEnd As Boolean
        'Dim blnPaperJam As Boolean
        'Dim blnPrinterFail As Boolean
        Dim intKeypadTimeOut As Integer
        'Dim intReceiptPrinterTimeout As Integer
    End Structure

    Structure KEYPADSETTINGSTR
        Dim strKeypadModel As String
        Dim Comport As String
        Dim Baudrate As Integer
        Dim Timeout As Integer
    End Structure

    Structure KEYPADLOOKUPDATASTR
        Dim DeviceGraphicID As String
        Dim DeviceName As String

        'Device Properties
        Dim TxnStatusPrtSuccess As String
        Dim TxnStatusPrtNotComplete As String
        Dim TxnStatusDeviceNotCfg As String
        Dim TxnStatusCancelSideway As String

        'Error Severity
        Dim ErrSvrtyKeypadOk As String
        Dim ErrSvrtyKeypadError As String
        Dim ErrSvrtyKeypadFatal As String
        Dim ErrSvrtyKeypadWarning As String

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
        Dim EvtDeviceTimeout As String

        'EPP Key
        Dim EPPCOMKEY As String
        Dim EPPMACKEY As String
        Dim EPPMASTERKEY As String

    End Structure

    Public Return_KEY_STROKE As String

    ReadOnly Property KEYPADHWDInfo() As KEYPADHWDSTR
        Get
            Return udtKEYPADHWDCFG
        End Get
    End Property

    ReadOnly Property KEYPADSETTINGInfo() As KEYPADSETTINGSTR
        Get
            Return udtKEYPADSETTINGCFG
        End Get
    End Property

    ReadOnly Property KEYPADLOOKUPDATAInfo() As KEYPADLOOKUPDATASTR
        Get
            Return udtKEYPADLOOKUPDATACFG
        End Get
    End Property

    ReadOnly Property HSMHWDInfo() As KEYPADHWDSTR
        Get
            Return udtHSMHWDCFG
        End Get
    End Property

    ReadOnly Property HSMSETTINGInfo() As KEYPADSETTINGSTR
        Get
            Return udtHSMSETTINGCFG
        End Get
    End Property

    ReadOnly Property HSMLOOKUPDATAInfo() As KEYPADLOOKUPDATASTR
        Get
            Return udtKEYPADLOOKUPDATACFG
        End Get
    End Property


    ReadOnly Property GetKeyStroke() As String
        Get
            Return Return_KEY_STROKE
        End Get
    End Property

    ReadOnly Property GetMacedData() As String
        Get
            Return objSagemEPP.GetMACedData
        End Get
    End Property

    ReadOnly Property GetCheckSumedData() As String
        Get
            Return objSagemEPP.GetCheckSumedData
        End Get
    End Property
#End Region


#Region "Local Variable"

    Private delegateASYNC_EVT As async_event_func_pointer

#End Region

#Region "Return Keeypad Timeout"
    Property ReturnKeypadTimeout() As Integer
        Get
            Return udtKEYPADHWDCFG.intKeypadTimeOut
        End Get
        Set(ByVal value As Integer)
            udtKEYPADHWDCFG.intKeypadTimeOut = value
        End Set
    End Property
#End Region


#Region "Property"

    ReadOnly Property KeypadModelInfo() As String
        Get
            Return udtKEYPADSETTINGCFG.strKeypadModel.Trim
        End Get
    End Property

    ReadOnly Property KeypadComportInfo() As String
        Get
            Return udtKEYPADSETTINGCFG.Comport.Trim
        End Get
    End Property

#End Region



#Region "InitCls/Close Object -  Control"
    Public Function InitKEYPADControl() As Boolean
        Dim strLogIniPath As String = String.Empty
        Dim strKEYPADHWDIniPath As String = String.Empty
        'Dim strRECEIPTPRINTERPROPERTIESIniPath As String = String.Empty
        Dim strKEYPADLOOKUPDATAIniPath As String = String.Empty

        Try

            'Input - Default AppLayer\xxxxx.ini
            If objAppLayerINI.ReadAppLayerINICFGFile(HWDLAYERINIPATH, EPP) = True Then

                'Read INI File
                'Log Ini File
                '1.Keypad Setting
                '2.Keypad Properties Setting
                '3.Keypad Lookup Data

                With objAppLayerINI.udtClsAppLayerIniCFG
                    strLogIniPath = .strLogINIPath.Trim
                    strKEYPADHWDIniPath = .strAppLayerINIPath1.Trim
                    strKEYPADLOOKUPDATAIniPath = .strAppLayerINIPath2.Trim
                End With

                'Layer Class
                InitLog(strLogIniPath)
                strLogEvt = "Keypad Layer Class Init Ok"
                AppLogInfo(strLogEvt)

                'Reference
                'LOG ININ PATH
                '2.DB Parameter
                AppLogInfo("Logger INI Path:" & strLogIniPath)
                AppLogInfo("Keypad HWD INI PATH:" & strKEYPADHWDIniPath.Trim)
                'AppLogInfo("Keypad Properties INI PATH:" & strRECEIPTPRINTERPROPERTIESIniPath.Trim)
                AppLogInfo("Keypad Lookup Data INI PATH:" & strKEYPADLOOKUPDATAIniPath.Trim)

                'Init Layer Classes
                '1.Keypad Setting
                If clsReadKeypadSetting() = False Then
                    AppLogWarn("Read Sagem Keypad Hardware Setting Failed")
                Else
                    With udtKEYPADSETTINGCFG
                        AppLogInfo("Read Sagem Keypad Hardware Setting OK")
                        AppLogInfo("Model:" & .strKeypadModel)
                        AppLogInfo("Comport:" & .Comport)
                        AppLogInfo("Baudrate:" & .Baudrate)
                        AppLogInfo("Timeout:" & .Timeout)
                        'AppLogInfo("Top Margin:" & .ReceiptTopMargin)
                        'AppLogInfo("Bottom Margin:" & .ReceiptBottomMargin)
                        'AppLogInfo("Right Margin:" & .ReceiptRightMargin)
                    End With
                End If

                ''2.Keypad Properties Setting
                'If clsReadSagemKeypadPropSetting() = False Then
                '    AppLogWarn("Read Printer Properties Setting Failed")

                'Else
                '    AppLogInfo("Read Printer Properties Setting OK")
                '    'Else
                '    '    With udtRECEIPTPRINTERPROPERTIESCFG
                '    '        AppLogInfo("PrinterHWD HeaderImgHeightPixel:" & .HeaderImgHeightPixel)
                '    '    End With
                'End If

                '3.Keypad Lookup Data Setting
                If clsReadKeypadLookupDataSetting() = False Then
                    AppLogWarn("Read Keypad Lookup Data Setting Failed")
                Else
                    With udtKEYPADSETTINGCFG
                        AppLogInfo("Read Keypad Lookup Data Setting OK")
                    End With
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

    Public Function InitHSMControl() As Boolean
        Dim strLogIniPath As String = String.Empty
        Dim strHSMHWDIniPath As String = String.Empty
        'Dim strRECEIPTPRINTERPROPERTIESIniPath As String = String.Empty
        Dim strHSMLOOKUPDATAIniPath As String = String.Empty

        Try

            'Input - Default AppLayer\xxxxx.ini
            If objAppLayerINI.ReadAppLayerINICFGFile(HWDLAYERINIPATH, EPP) = True Then

                'Read INI File
                'Log Ini File
                '1.Keypad Setting
                '2.Keypad Properties Setting
                '3.Keypad Lookup Data

                With objAppLayerINI.udtClsAppLayerIniCFG
                    strLogIniPath = .strLogINIPath.Trim
                    strHSMHWDIniPath = .strAppLayerINIPath3.Trim
                    strHSMLOOKUPDATAIniPath = .strAppLayerINIPath4.Trim
                End With

                'Layer Class
                InitLog(strLogIniPath)
                strLogEvt = "HSM Layer Class Init Ok"
                AppLogInfo(strLogEvt)

                'Reference
                'LOG ININ PATH
                '2.DB Parameter
                AppLogInfo("Logger INI Path:" & strLogIniPath)
                AppLogInfo("HSM HWD INI PATH:" & strHSMHWDIniPath.Trim)
                'AppLogInfo("Keypad Properties INI PATH:" & strRECEIPTPRINTERPROPERTIESIniPath.Trim)
                AppLogInfo("HSM Lookup Data INI PATH:" & strHSMLOOKUPDATAIniPath.Trim)

                'Init Layer Classes
                '1.Keypad Setting
                If clsReadHSMSetting() = False Then
                    AppLogWarn("Read Sagem Keypad Hardware Setting Failed")
                Else
                    With udtKEYPADSETTINGCFG
                        AppLogInfo("Read Sagem Keypad Hardware Setting OK")
                        AppLogInfo("Model:" & .strKeypadModel)
                        AppLogInfo("Comport:" & .Comport)
                        AppLogInfo("Baudrate:" & .Baudrate)
                        AppLogInfo("Timeout:" & .Timeout)
                        'AppLogInfo("Top Margin:" & .ReceiptTopMargin)
                        'AppLogInfo("Bottom Margin:" & .ReceiptBottomMargin)
                        'AppLogInfo("Right Margin:" & .ReceiptRightMargin)
                    End With
                End If

                ''2.Keypad Properties Setting
                'If clsReadSagemKeypadPropSetting() = False Then
                '    AppLogWarn("Read Printer Properties Setting Failed")

                'Else
                '    AppLogInfo("Read Printer Properties Setting OK")
                '    'Else
                '    '    With udtRECEIPTPRINTERPROPERTIESCFG
                '    '        AppLogInfo("PrinterHWD HeaderImgHeightPixel:" & .HeaderImgHeightPixel)
                '    '    End With
                'End If

                '3.Keypad Lookup Data Setting
                If clsReadKeypadLookupDataSetting() = False Then
                    AppLogWarn("Read HSM Lookup Data Setting Failed")
                Else
                    With udtKEYPADSETTINGCFG
                        AppLogInfo("Read HSM Lookup Data Setting OK")
                    End With
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


#Region "KEYPAD Read INI Setting"

    Public Function ReadKEYPADHWDCFG() As Boolean
        Try
            'Read Keypad HWD CFG
            If clsReadKeypadHWDSetting() = True Then
                AppLogInfo("Read Keypad HWD CFG Info OK")
                Return True
            Else
                AppLogWarn("Read Keypad HWD CFG Info Failed")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in  ReadRECEIPTPRINTERHWDCFG. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function UpdateKEYPADHWDCFG(ByVal strKeypadModel As String, ByVal blnHideMode As Boolean, ByVal blnEncryptMode As Boolean, ByVal blnKeypadStatus As Boolean, ByVal intKeypadTimeOut As Integer) As Boolean
        Try
            'Read Keypad HWD CFG
            If clsUpdateKeypadHWDSetting(strKeypadModel, blnHideMode, blnEncryptMode, blnKeypadStatus, intKeypadTimeOut) = True Then
                AppLogInfo("Read Keypad HWD CFG Info OK")
                Return True
            Else
                AppLogWarn("Read Keypad HWD CFG Info Failed")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in  ReadRECEIPTPRINTERHWDCFG. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function


    Public Function UpdateKEYPADHWDSetting(ByVal strKeypadModel As String, ByVal strKeypadComport As String) As Boolean
        Try
            'Read Keypad HWD CFG
            If clsUpdateKeypadValue(strKeypadModel, strKeypadComport) = True Then
                AppLogInfo("Update Keypad Setting CFG Info OK")
                Return True
            Else
                AppLogWarn("Update Keypad Setting CFG Info Failed")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in UpdateKEYPADHWDSetting. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function


    Public Function UpdateKeypadTimeout(ByVal intKeypadTimeOut As Integer) As Boolean
        Try
            'Read Keypad HWD CFG
            If clsUpdateKeypadTimeout(intKeypadTimeOut) = True Then
                AppLogInfo("Read Keypad HWD CFG Info OK")
                Return True
            Else
                AppLogWarn("Read Keypad HWD CFG Info Failed")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in  ReadRECEIPTPRINTERHWDCFG. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function CloseKEYPADHWDCFG() As Boolean
        Try
            If clsCloseKeypadHWDSetting() Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in  ReadRECEIPTPRINTERHWDCFG. ErrInfo:" & ex.Message)
            Return False
        End Try

    End Function


#End Region


#Region "KEYPAD Control Function"

    Public Function InitKEYPADHWD(ByVal strDevice As String) As Boolean
        objSagemEPP = New clsEPPKeypad

        Try
            If strDevice = "KEYPAD" Then
                If objSagemEPP.OpenEPPComport(udtKEYPADSETTINGCFG.Comport, udtKEYPADSETTINGCFG.Baudrate, strDevice) = True Then
                    udtKEYPADHWDCFG.blnKeypadStatus = True
                    udtHSMHWDCFG.blnKeypadStatus = True
                    Return True

                Else
                    Return False
                End If
            Else
                If objSagemEPP.OpenEPPComport(udtHSMSETTINGCFG.Comport, udtHSMSETTINGCFG.Baudrate, strDevice) = True Then
                    udtKEYPADHWDCFG.blnKeypadStatus = True
                    udtHSMHWDCFG.blnKeypadStatus = True
                    Return True

                Else
                    Return False
                End If
            End If


        Catch ex As Exception
            AppLogErr("Error in InitKEYPADHWD. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function CloseKEYPADHWD() As Boolean
        objSagemEPP = New clsEPPKeypad

        Try
            If objSagemEPP.CloseEPPComport Then
                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            AppLogErr("Error in CloseKEYPADHWD. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

    Public Function Send_CmdStartClearTextMode() As Boolean
        Try
            AppLogInfo("clsKeyControl. Send_CmdStartClearTextMode")

            If objSagemEPP.CMD_Set_Clear_Text() Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Send_CmdStopClearTextMode() As Boolean
        Try
            If objSagemEPP.CMD_STOP_PIN() Then
                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Send_CmdStartEncTextMode() As Boolean
        Try
            If objSagemEPP.CMD_START_PIN() Then
                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Send_CmdStopEncTextMode() As Boolean
        Try
            If objSagemEPP.CMD_STOP_PIN() Then
                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Send_CmdStartSupervisorMode() As Boolean
        Try
            If objSagemEPP.CMD_START_KEY() Then
                Return True

            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Send_CmdCallKeyPress() As Boolean

        Try
            delegateASYNC_EVT = AddressOf objSagemEPP.CMD_KEY_DETECTED

            SetAsync_event_func(delegateASYNC_EVT)
            GC.KeepAlive(GetAsync_event_func)
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Send_CmdCallHexKeyPress() As Boolean

        Try
            delegateASYNC_EVT = AddressOf objSagemEPP.CMD_HEX_KEY_DETECTED

            SetAsync_event_func(delegateASYNC_EVT)
            GC.KeepAlive(GetAsync_event_func)
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Send_CmdCallPINPress() As Boolean
        Try
            delegateASYNC_EVT = AddressOf objSagemEPP.CMD_KEY_DETECTED

            SetAsync_event_func(delegateASYNC_EVT)
            GC.KeepAlive(GetAsync_event_func)
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Send_CmdSetMasterKey(ByVal strMasterKey As String) As Boolean

        Try
            If objSagemEPP.CMD_SET_KEY(strMasterKey) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Send_CmdTestEpp() As Boolean
        Try
            If objSagemEPP.CMD_TEST_EPP() Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Send_CmdClearAllRegister() As Boolean
        Try
            If objSagemEPP.CMD_CLEAN_DATA() Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Send_CmdDisablePassword() As Boolean
        Try
            If objSagemEPP.CMD_SET_CONFIGURATION() Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Send_CmdWriteMPA(ByVal strKeyIndex As String, ByVal strIndicator As String, ByVal strEncKeyIndex As String, ByVal strWorkingKeyLen As String, ByVal strWorkingKey As String) As Boolean
        Dim ascii As New ASCIIEncoding
        Dim bytKeyIndex() As Byte
        'Dim bytWorkingKey() As Byte
        Dim bytWorkingKeyLen() As Byte
        Dim bytEncKeyIndex() As Byte
        Dim bytIndicator(1) As Byte

        Try
            bytKeyIndex = ascii.GetBytes(strKeyIndex)

            Dim len As Integer = strWorkingKey.Length \ 2
            Dim bytWorkingKey(len - 1) As Byte
            For i As Integer = 0 To len - 1
                bytWorkingKey(i) = Convert.ToByte(strWorkingKey.Substring(i * 2, 2), 16)
            Next

            'bytWorkingKey = ascii.GetBytes(strWorkingKey)
            bytWorkingKeyLen = ascii.GetBytes(strWorkingKeyLen)
            bytEncKeyIndex = ascii.GetBytes(strEncKeyIndex)
            bytIndicator(0) = Left(strIndicator, 1)
            bytIndicator(1) = Right(strIndicator, 1)

            If objSagemEPP.CMD_WRITE_MPA(bytKeyIndex, bytWorkingKey, bytEncKeyIndex, bytIndicator, bytWorkingKeyLen) Then
                Return True

            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Send_CmdCheckSum(ByVal strKeyIndex As String) As Boolean
        Dim ascii As New ASCIIEncoding
        Dim bytVarKey() As Byte


        Try
            bytVarKey = ascii.GetBytes(strKeyIndex)


            If objSagemEPP.CMD_CHECK_SUM(strKeyIndex, "") Then
                Return True

            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Send_CmdValidateCheckSum(ByVal strKeyIndex As String, ByVal strKVV As String) As Boolean
        Dim ascii As New ASCIIEncoding
        Dim bytVarKey() As Byte


        Try
            bytVarKey = ascii.GetBytes(strKeyIndex)


            If objSagemEPP.CMD_CHECK_SUM(strKeyIndex, strKVV) Then
                Return True

            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Send_CmdCalcCBCMAC(ByVal strMacString As String, ByVal strKeyIndex As String) As String
        Dim ascii As New ASCIIEncoding
        Dim bytVar() As Byte
        Dim bytData() As Byte
        Dim strReturnMacedData As String = ""

        Try
            bytVar = ascii.GetBytes(strKeyIndex)
            bytData = ascii.GetBytes(strMacString)

            If objSagemEPP.CMD_CALC_CBC_MAC(bytVar, bytData) Then
                strReturnMacedData = objSagemEPP.GetMACedData
            End If

            Return strReturnMacedData

        Catch ex As Exception
            Return strReturnMacedData
        End Try
    End Function

    Public Function Send_CmdVerifyCBCMAC(ByVal strMacString As String, ByVal strMacData As String) As Boolean
        Dim ascii As New ASCIIEncoding
        Dim bytVar() As Byte
        Dim bytData() As Byte
        Dim strReturnMacedData As String = ""

        Try
            bytVar = ascii.GetBytes("M11")
            bytData = ascii.GetBytes(strMacString)

            If objSagemEPP.CMD_CALC_CBC_MAC(bytVar, bytData) Then
                strReturnMacedData = objSagemEPP.GetMACedData

                If strReturnMacedData = strMacData Then
                    Return True

                Else
                    Return False
                End If

            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Send_CmdReadPIN(ByVal intFormatType As Integer, ByVal strParam As String, ByVal strKeyIndex As String) As String
        Dim ascii As New ASCIIEncoding
        Dim bytKeyIndex() As Byte
        Dim bytFormatType(1) As Byte
        'Dim bytParam1(5) As Byte
        'Dim bytParam2(0) As Byte
        Dim bytParam3() As Byte = New Byte() {}
        Dim strEncPINBlock As String = ""
        Dim strReturnEncPINBlock As String = ""

        Try
            bytKeyIndex = ascii.GetBytes(strKeyIndex)

            bytFormatType(0) = 0
            bytFormatType(1) = intFormatType

            If intFormatType = "1" Then
                'bytParam1(0) = strParam.Substring(0, 2)
                'bytParam1(1) = strParam.Substring(2, 2)
                'bytParam1(2) = strParam.Substring(4, 2)
                'bytParam1(3) = strParam.Substring(6, 2)
                'bytParam1(4) = strParam.Substring(8, 2)
                'bytParam1(5) = strParam.Substring(10, 2)

                Dim len As Integer = strParam.Length \ 2
                Dim bytParam1(len - 1) As Byte
                For i As Integer = 0 To len - 1
                    bytParam1(i) = Convert.ToByte(strParam.Substring(i * 2, 2), 16)
                Next

                If objSagemEPP.CMD_READ_PIN(bytFormatType, bytParam1, bytKeyIndex, strEncPINBlock) Then
                    strReturnEncPINBlock = strEncPINBlock
                End If

            ElseIf intFormatType = "2" Or intFormatType = "3" Or intFormatType = "5" Or intFormatType = "6" Then
                'bytParam2(0) = strParam

                Dim len As Integer = strParam.Length
                Dim bytParam2(len - 1) As Byte
                For i As Integer = 0 To len - 1
                    bytParam2(i) = Convert.ToByte(strParam.Substring(i * 2, 1), 16)
                Next

                If objSagemEPP.CMD_READ_PIN(bytFormatType, bytParam2, bytKeyIndex, strEncPINBlock) Then
                    strReturnEncPINBlock = strEncPINBlock
                End If

            ElseIf intFormatType = "4" Then
                If objSagemEPP.CMD_READ_PIN(bytFormatType, bytParam3, bytKeyIndex, strEncPINBlock) Then
                    strReturnEncPINBlock = strEncPINBlock
                End If
            End If

            Return strReturnEncPINBlock

        Catch ex As Exception
            Return strReturnEncPINBlock
        End Try
    End Function

#End Region


#Region "HSM Read INI Setting"

    Public Function ReadHSMHWDCFG() As Boolean
        Try
            'Read Keypad HWD CFG
            If clsReadHSMHWDSetting() = True Then
                AppLogInfo("Read HSM HWD CFG Info OK")
                Return True
            Else
                AppLogWarn("Read HSM HWD CFG Info Failed")
                Return False
            End If
        Catch ex As Exception
            AppLogErr("Error in  ReadHSMHWDCFG. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function


#End Region


#Region "HSM Control Function"

    Public Function InitHSMHWD(ByVal strDevice As String) As Boolean
        objSagemEPP = New clsEPPKeypad

        Try
            If objSagemEPP.OpenEPPComport(udtHSMSETTINGCFG.Comport, udtHSMSETTINGCFG.Baudrate, strDevice) = True Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            AppLogErr("Error in InitHSMHWD. ErrInfo:" & ex.Message)
            Return False
        End Try
    End Function

#End Region


#Region "Log Event"
    Public Sub WriteLogStartDeviceHSM(ByVal strFormatType As String, ByVal strPAN As String, ByVal strPADString As String)
        Try
            AppLogInfo("StartDevice() HSM is OK")
            AppLogInfo("PIN Block Format Type: " & strFormatType)
            AppLogInfo("PAN value: " & strPAN)
            AppLogInfo("PAD String value: " & strPADString)
        Catch ex As Exception
            AppLogErr("StartDevice() HSM is Failed")
        End Try
    End Sub

    Public Sub WriteLogStopDeviceHSM()
        Try
            AppLogInfo("StopDevice() HSM is OK")

        Catch ex As Exception
            AppLogErr("StopDevice() HSM is Failed")
        End Try
    End Sub

    Public Sub WriteLogWrapDeviceHSM(ByVal strFormatType As String, ByVal strPAN As String, ByVal strPADString As String)
        Try
            AppLogInfo("WrapDevice() HSM is OK")
            AppLogInfo("PIN Block Format Type: " & strFormatType)
            AppLogInfo("PAN value: " & strPAN)
            AppLogInfo("PAD String value: " & strPADString)
        Catch ex As Exception
            AppLogErr("WrapDevice() HSM is Failed")
        End Try
    End Sub

    Public Sub WriteLogWakeDeviceHSM(ByVal strFormatType As String, ByVal strPAN As String, ByVal strPADString As String)
        Try
            AppLogInfo("WakeDevice() HSM is OK")
            AppLogInfo("PIN Block Format Type: " & strFormatType)
            AppLogInfo("PAN value: " & strPAN)
            AppLogInfo("PAD String value: " & strPADString)
        Catch ex As Exception
            AppLogErr("WakeDevice() HSM is Failed")
        End Try
    End Sub

    Public Sub WriteLogLockDeviceHSM()
        Try
            AppLogInfo("LockDevice() HSM is OK")

        Catch ex As Exception
            AppLogErr("LockDevice() HSM is Failed")
        End Try
    End Sub

    Public Sub WriteLogUnlockDeviceHSM()
        Try
            AppLogInfo("UnlockDevice() HSM is OK")

        Catch ex As Exception
            AppLogErr("UnlockDevice() HSM is Failed")
        End Try
    End Sub

    Public Sub WriteLogDiagnosticDeviceHSM(ByVal strTxnStatus As String, ByVal strErrSeverity As String, ByVal strSupplyStatus As String)
        Try
            AppLogInfo("DiagnosticDevice() HSM is OK")
            AppLogInfo("TxnStatus: " & strTxnStatus)
            AppLogInfo("ErrorSeverity: " & strErrSeverity)
            AppLogInfo("SupplyStatus: " & strSupplyStatus)
            AppLogInfo("MStatus: " & "")
        Catch ex As Exception
            AppLogErr("DiagnosticDevice() HSM is Failed")
        End Try
    End Sub

    Public Sub WriteLogGetPinBlockHSM(ByVal strFormatType As String, ByVal strPAN As String, ByVal strPADString As String)
        Try
            AppLogInfo("GetPinBlock() HSM is OK")
            AppLogInfo("PIN Block Format Type: " & strFormatType)
            AppLogInfo("PAN value: " & strPAN)
            AppLogInfo("PAD String value: " & strPADString)
        Catch ex As Exception
            AppLogErr("GetPinBlock() HSM is Failed")
        End Try
    End Sub
#End Region

End Class
