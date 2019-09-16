Imports MDSXFSWrapper.clsMDSXFS

Module modGolbalVar

#Region "Object Variable"

    'AppLog CFG - clsLoggerControl
    Public objAppLog As clsAppLogger.clsAppLoggerControl

    'AppLayer INI CFG - clsAPPLayerINI
    Public objAppLayerINI As New clsAppLayer.clsAppLayerControl


#End Region


#Region "Cls Variable"

   
    Public strAppLogINIFile As String = String.Empty
    Public strLogEvt As String = String.Empty
    Public strErrMsg As String = String.Empty

    Public strFileINIPath As String = String.Empty
    Public strINIPath As String = String.Empty

    Public blnInitAppLayer As Boolean

    Public LastState As MDS_Wrapper_Control.MDSControl.UserNotificationOptions

#End Region


#Region "App User Define Structure"

    Public udtDEPOSITORHWDCFG As DEPOSITORCTR
    Public udtCASHMODEDevCode As CASHDEVICECODE
    Public udtCHQMODEDevCode As CHEQUEDEVICECODE
    Public udtDOORSENSORDevCode As DOORSENSORDEVICECODE

    Public intMDSDoorStatus As Integer = 0

#End Region

End Module