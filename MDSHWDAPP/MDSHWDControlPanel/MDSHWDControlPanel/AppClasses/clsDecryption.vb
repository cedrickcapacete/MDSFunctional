﻿Imports System
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

Public Class clsDecryption

    Private strTitle As String = "clsDecryption"
    Dim strErrMsg As String = ""

    'Key Decryption Function 
    Public Function Decrypt(ByVal strDecrypt As String) As String
        Dim strDecryptKey As String = String.Empty
        Dim plainText As String = String.Empty
        Dim cipherText As String = String.Empty

        Dim passPhrase As String = String.Empty
        Dim saltValue As String = String.Empty
        Dim hashAlgorithm As String = String.Empty
        Dim passwordIterations As Integer
        Dim initVector As String = String.Empty
        Dim keySize As Integer
        Dim intKeySize As Integer

        Dim initVectorBytes As Byte()
        Dim saltValueBytes As Byte()
        Dim cipherTextBytes As Byte()
        Dim objpassword As PasswordDeriveBytes
        Dim keyBytes As Byte()
        Dim symmetricKey As RijndaelManaged
        Dim memoryStream As MemoryStream
        Dim decryptor As ICryptoTransform
        Dim cryptoStream As CryptoStream
        Dim plainTextBytes As Byte()
        Dim decryptedByteCount As Integer


        Try


            cipherText = strDecrypt    ' encryption text

            passPhrase = "Pas5pr@se"        ' can be any string
            saltValue = "s@1tValue"        ' can be any string
            hashAlgorithm = "SHA1"             ' can be "MD5"
            passwordIterations = 2                  ' can be any number
            initVector = "@1B2c3D4e5F6g7H8" ' must be 16 bytes
            keySize = 256                ' can be 192 or 128


            ' Convert strings defining encryption key characteristics into byte
            ' arrays. Let us assume that strings only contain ASCII codes.
            ' If strings include Unicode characters, use Unicode, UTF7, or UTF8
            ' encoding.

            initVectorBytes = Encoding.ASCII.GetBytes(initVector)


            saltValueBytes = Encoding.ASCII.GetBytes(saltValue)

            ' Convert our ciphertext into a byte array.

            cipherTextBytes = Convert.FromBase64String(cipherText)

            ' First, we must create a password, from which the key will be 
            ' derived. This password will be generated from the specified 
            ' passphrase and salt value. The password will be created using
            ' the specified hash algorithm. Password creation can be done in
            ' several iterations.

            objpassword = New PasswordDeriveBytes(passPhrase, _
                                               saltValueBytes, _
                                               hashAlgorithm, _
                                               passwordIterations)

            ' Use the password to generate pseudo-random bytes for the encryption
            ' key. Specify the size of the key in bytes (instead of bits).

            intKeySize = keySize / 8
            keyBytes = objpassword.GetBytes(intKeySize)

            ' Create uninitialized Rijndael encryption object.

            symmetricKey = New RijndaelManaged()

            ' It is reasonable to set encryption mode to Cipher Block Chaining
            ' (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC

            ' Generate decryptor from the existing key bytes and initialization 
            ' vector. Key size will be defined based on the number of the key 
            ' bytes.

            decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)

            ' Define memory stream which will be used to hold encrypted data.

            memoryStream = New MemoryStream(cipherTextBytes)

            ' Define memory stream which will be used to hold encrypted data.

            cryptoStream = New CryptoStream(memoryStream, _
                                            decryptor, _
                                            CryptoStreamMode.Read)

            ' Since at this point we don't know what the size of decrypted data
            ' will be, allocate the buffer long enough to hold ciphertext;
            ' plaintext is never longer than ciphertext.

            ReDim plainTextBytes(cipherTextBytes.Length)

            ' Start decrypting.

            decryptedByteCount = cryptoStream.Read(plainTextBytes, _
                                                   0, _
                                                   plainTextBytes.Length)

            ' Close both streams.
            memoryStream.Close()
            cryptoStream.Close()

            ' Convert decrypted data into a string. 
            ' Let us assume that the original plaintext string was UTF8-encoded.
            plainText = Encoding.UTF8.GetString(plainTextBytes, _
                                                0, _
                                                decryptedByteCount)

            ' Return decrypted string.
            Decrypt = plainText

            strDecryptKey = Decrypt

            Return strDecryptKey

        Catch ex As Exception
            'strErrMsg = "Error in clsDecryption_Decrypt Function."
            'strErrMsg += ControlChars.NewLine & ex.Message
            'MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
            strDecryptKey = ""
            Return strDecryptKey
        End Try

    End Function

End Class
