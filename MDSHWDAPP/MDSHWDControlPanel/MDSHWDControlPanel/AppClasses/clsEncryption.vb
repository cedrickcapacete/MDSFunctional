Imports System
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

Public Class clsEncryption

    Private strTitle As String = "clsEncryption"
    Dim strErrMsg As String = String.Empty


    'Key Encryption Function 
    Public Function Encrypt(ByVal strEncrypt As String) As String
        Dim strEncryptKey As String = String.Empty
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
        Dim plainTextBytes As Byte()
        Dim objpassword As PasswordDeriveBytes
        Dim keyBytes As Byte()
        Dim symmetricKey As RijndaelManaged
        Dim encryptor As ICryptoTransform
        Dim memoryStream As MemoryStream
        Dim cryptoStream As CryptoStream
        Dim cipherTextBytes As Byte()

        Try

            plainText = strEncrypt    ' original plaintext

            passPhrase = "Pas5pr@se"        ' can be any string
            saltValue = "s@1tValue"        ' can be any string
            hashAlgorithm = "SHA1"             ' can be "MD5"
            passwordIterations = 2                  ' can be any number
            initVector = "@1B2c3D4e5F6g7H8" ' must be 16 bytes
            keySize = 256                ' can be 192 or 128


            ' Convert strings into byte arrays.
            ' Let us assume that strings only contain ASCII codes.
            ' If strings include Unicode characters, use Unicode, UTF7, or UTF8 
            ' encoding.
            initVectorBytes = Encoding.ASCII.GetBytes(initVector)

            saltValueBytes = Encoding.ASCII.GetBytes(saltValue)

            ' Convert our plaintext into a byte array.
            ' Let us assume that plaintext contains UTF8-encoded characters.
            plainTextBytes = Encoding.UTF8.GetBytes(plainText)

            ' First, we must create a password, from which the key will be derived.
            ' This password will be generated from the specified passphrase and 
            ' salt value. The password will be created using the specified hash 
            ' algorithm. Password creation can be done in several iterations.
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

            ' Generate encryptor from the existing key bytes and initialization 
            ' vector. Key size will be defined based on the number of the key 
            ' bytes.
            encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)

            ' Define memory stream which will be used to hold encrypted data.
            memoryStream = New MemoryStream()

            ' Define cryptographic stream (always use Write mode for encryption).
            cryptoStream = New CryptoStream(memoryStream, _
                                            encryptor, _
                                            CryptoStreamMode.Write)
            ' Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)

            ' Finish encrypting.
            cryptoStream.FlushFinalBlock()

            ' Convert our encrypted data from a memory stream into a byte array.
            cipherTextBytes = memoryStream.ToArray()

            ' Close both streams.
            memoryStream.Close()
            cryptoStream.Close()

            ' Convert encrypted data into a base64-encoded string.
            cipherText = Convert.ToBase64String(cipherTextBytes)

            ' Return encrypted string.
            Encrypt = cipherText

            strEncryptKey = Encrypt

            Return strEncryptKey


        Catch ex As Exception
            'strErrMsg = "Error in clsEncryption_Encrypt Function."
            'strErrMsg += ControlChars.NewLine & ex.Message
            'MsgBox(strErrMsg, MsgBoxStyle.Critical, strTitle)
            strEncryptKey = ""
            Return strEncryptKey
        End Try

    End Function



End Class
