Public Class clsDelegateCommand
    'Implements ICommand

    'Private ReadOnly _canExecute As Predicate(Of Object)
    'Private ReadOnly _execute As Action(Of Object)
    'Public Event CanExecutedChanged As EventHandler

    'Public Sub New(ByVal execute As Action(Of Object))
    'End Sub

    'Public Sub New(ByVal execute As Action(Of Object), ByVal canExecute As Predicate(Of Object))
    '    _execute = execute
    '    _canExecute = canExecute
    'End Sub

    'Public Function CanExecute(ByVal parameter As Object) As Boolean
    '    If _canExecute Is Nothing Then
    '        Return True
    '    Else
    '        Return _canExecute(parameter)
    '    End If
    'End Function

    'Public Sub Execute(ByVal parameter As Object)
    '    _execute(parameter)
    'End Sub

    'Public Sub CanExecuteChanged()
    '    If (Not CanExecuteChanged() Is Nothing) Then
    '        CanExecuteChanged(Me, EventArgs.Empty)
    '    End If
    'End Sub
End Class
