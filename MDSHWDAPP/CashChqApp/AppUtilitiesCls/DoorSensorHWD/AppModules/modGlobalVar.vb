Module modGlobalVar

#Region "App Layer Default Ini File"
    'Hookup Ini File
    Public Const MDSHWDINIPATH As String = "AppIniFile\DOORHWDCFG.ini"
#End Region

#Region "Enum"

    Enum DoorStatus
        DoorClosed = 0
        DoorOpened = 1
        DoorMaintenaceMode = 2
        DoorSupervisorMode = 3
    End Enum

#End Region




#Region "Variable"

    Public strErrMsg As String = String.Empty
    Public strLogEvt As String = String.Empty
    Public strLogMsg As String = String.Empty
    Public strSelMsgErr As String = String.Empty
    Public strLogIniPath As String = String.Empty

    'AppLog CFG - clsLoggerControl
    Public objAppLog As clsAppLogger.clsAppLoggerControl

    'AppLayer INI CFG - clsAPPLayerINI
    Public objAppLayerINI As New clsAppLayer.clsAppLayerControl

#End Region


End Module
