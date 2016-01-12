
Namespace ValidationLayer

    Public Class Validations
        Public Function Getpassword(ByVal Password As String) As String
            Const encdtext As String = "Thusithamelaniinokavishwa"
            Dim x As Object
            Dim l As Short
            Dim Char_Renamed As Integer
            Try
                l = Len(encdtext)
                For x = 1 To Len(Password)
                    Char_Renamed = Asc(Mid(encdtext, (x Mod l) - l * CShort((x Mod l) = 0), 1))
                    Mid(Password, x, 1) = Chr(Asc(Mid(Password, x, 1)) Xor Char_Renamed)
                Next
                Return Password
            Catch ex As Exception
                Return ""
            End Try
        End Function
        Public Function TextValidation(ByVal crControl As String, ByVal strVAL As String, ByVal mandetory As Boolean) As String
            If mandetory = True Then
                If strVAL <> String.Empty Then
                    If strVAL <> "" Then
                        Dim x As Integer
                        Dim cval As String
                        For x = 1 To strVAL.Length
                            cval = Mid(strVAL, x, 1)
                            If cval = "~" Or cval = "`" Or cval = "!" Or cval = "^" Then
                                Return crControl & " Have  Invalied letter Input"
                            End If
                        Next
                    Else
                        Return crControl & " Cannot Be a Blank"
                    End If
                Else
                    Return crControl & " Is a Mandetory Field"
                End If
            Else
                Dim x As Integer
                Dim cval As String
                For x = 1 To strVAL.Length
                    cval = Mid(strVAL, x, 1)
                    If cval = "~" Or cval = "`" Or cval = "!" Or cval = "^" Or cval = "*" Then
                        Return crControl & " Invalied letter Input"
                    End If
                Next
            End If
            Return "True"
        End Function
        Public Function NumbersValidate(ByVal crControl As String, ByVal strval As String, ByVal mandotory As Boolean) As String
            Dim Lenthstr As Integer
            Dim X As Integer
            Dim curChar As String
            Lenthstr = Len(strval)
            Dim countdes As Integer
            countdes = 0
            If mandotory = True Then
                If strval = String.Empty Then
                    Return crControl & " Blank Field"
                End If
                If strval = "" Then
                    Return crControl & " Blank Field"
                End If
            End If

            For X = 1 To Lenthstr
                If Mid(strval, X, 1) = "-" Then
                    If X <> 1 Then

                        Return crControl & " Not a Valied Number"
                    End If
                End If
                curChar = Mid(strval, X, 1)
                If curChar = "." Then
                    countdes = countdes + 1
                    If countdes > 1 Then
                        Return crControl & " Not a Valied Number"
                    End If
                End If
                If Asc(curChar) >= 48 And Asc(curChar) <= 57 Or Asc(curChar) = 45 Or curChar = "." Then
                    NumbersValidate = "True"
                Else
                    Return crControl & " Not a Valied Number"
                End If
            Next
            Return "True"
        End Function

        
        'Public Interface Ivalidation
        '    Function NumbersValidate(ByVal strval As String, ByVal mandotory As Boolean) As String
        '    Function TextValidation(ByVal strVAL As String, ByVal mandetory As Boolean) As String
        '    Function Getpassword(ByVal Password As String) As String
        'End Interface
    End Class
End Namespace

