Imports SampSharp.GameMode.SAMP
Imports SampSharp.GameMode.Events
Imports SampSharp.GameMode

Public Class GameMode
    Inherits BaseMode

    Overrides Function OnGameModeInit() As Boolean
        Console.WriteLine("GameModeInit from Visual basic")
        Return True
    End Function

    Private Sub MySender_Start(ByVal sender As Object, ByVal e As PlayerEventArgs) Handles Me.PlayerConnected
        e.Player.SendClientMessage(Color.White, "You connected to a VB gamemode!")
    End Sub
End Class
