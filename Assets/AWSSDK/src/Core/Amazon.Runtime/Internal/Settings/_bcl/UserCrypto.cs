﻿//
// Copyright 2014-2015 Amazon.com, Inc. or its affiliates. All Rights Reserved.
//
//
// Licensed under the AWS Mobile SDK for Unity Developer Preview License Agreement (the "License").
// You may not use this file except in compliance with the License.
// A copy of the License is located in the "license" file accompanying this file.
// See the License for the specific language governing permissions and limitations under the License.
//
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace Amazon.Runtime.Internal.Settings
{
    public class UserCrypto
    {
        public static string Decrypt(string encrypted)
        {
            List<Byte> dataIn = new List<byte>();
            for (int i = 0; i < encrypted.Length; i = i + 2)
            {
                byte data = Convert.ToByte(encrypted.Substring(i, 2), 16);
                dataIn.Add(data);
            }

            CryptProtectFlags flags = CryptProtectFlags.CRYPTPROTECT_UI_FORBIDDEN;
            DATA_BLOB encryptedBlob = ConvertData(dataIn.ToArray());
            DATA_BLOB unencryptedBlob = new DATA_BLOB();
            DATA_BLOB dataOption = new DATA_BLOB();

            try
            {

                CRYPTPROTECT_PROMPTSTRUCT prompt = new CRYPTPROTECT_PROMPTSTRUCT();
                if (!CryptUnprotectData(ref encryptedBlob, "psw", ref dataOption, IntPtr.Zero, ref prompt, flags, ref unencryptedBlob))
                {
                    int errCode = Marshal.GetLastWin32Error();
                    throw new Exception("CryptProtectData failed.", new Win32Exception(errCode));
                }

                byte[] outData = new byte[unencryptedBlob.cbData];
                Marshal.Copy(unencryptedBlob.pbData, outData, 0, outData.Length);

                string unencrypted = Encoding.Unicode.GetString(outData);
                return unencrypted;
            }
            finally
            {
                if (encryptedBlob.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(encryptedBlob.pbData);
                if (unencryptedBlob.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(unencryptedBlob.pbData);
            }
        }

        public static string Encrypt(string unencrypted)
        {
            CryptProtectFlags flags = CryptProtectFlags.CRYPTPROTECT_UI_FORBIDDEN;
            DATA_BLOB unencryptedBlob = ConvertData(Encoding.Unicode.GetBytes(unencrypted));
            DATA_BLOB encryptedBlob = new DATA_BLOB();
            DATA_BLOB dataOption = new DATA_BLOB();

            try
            {
                CRYPTPROTECT_PROMPTSTRUCT prompt = new CRYPTPROTECT_PROMPTSTRUCT();
                if (!CryptProtectData(ref unencryptedBlob, "psw", ref dataOption, IntPtr.Zero, ref prompt, flags, ref encryptedBlob))
                {
                    int errCode = Marshal.GetLastWin32Error();
                    throw new Exception("CryptProtectData failed.", new Win32Exception(errCode));
                }

                byte[] outData = new byte[encryptedBlob.cbData];
                Marshal.Copy(encryptedBlob.pbData, outData, 0, outData.Length);


                StringBuilder encrypted = new StringBuilder();
                for (int i = 0; i <= outData.Length - 1; i++)
                {
                    encrypted.Append(
                        Convert.ToString(outData[i], 16).PadLeft(2, '0').ToUpper());
                }

                string encryptedPassword = encrypted.ToString().ToUpper();
                return encryptedPassword;
            }
            finally
            {
                if (unencryptedBlob.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(unencryptedBlob.pbData);
                if (encryptedBlob.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(encryptedBlob.pbData);
            }
        }

        static DATA_BLOB ConvertData(byte[] data)
        {
            DATA_BLOB blob = new DATA_BLOB();
            blob.pbData = Marshal.AllocHGlobal(data.Length);
            blob.cbData = data.Length;
            Marshal.Copy(data, 0, blob.pbData, data.Length);

            return blob;
        }


        #region PInvoke Declarations
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct DATA_BLOB
        {
            public int cbData;
            public IntPtr pbData;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CRYPTPROTECT_PROMPTSTRUCT
        {
            public int cbSize;
            public CryptProtectPromptFlags dwPromptFlags;
            public IntPtr hwndApp;
            public String szPrompt;
        }

        [Flags]
        private enum CryptProtectPromptFlags
        {
            // prompt on unprotect
            CRYPTPROTECT_PROMPT_ON_UNPROTECT = 0x1,

            // prompt on protect
            CRYPTPROTECT_PROMPT_ON_PROTECT = 0x2
        }

        [Flags]
        private enum CryptProtectFlags
        {
            // for remote-access situations where ui is not an option
            // if UI was specified on protect or unprotect operation, the call
            // will fail and GetLastError() will indicate ERROR_PASSWORD_RESTRICTION
            CRYPTPROTECT_UI_FORBIDDEN = 0x1,

            // per machine protected data -- any user on machine where CryptProtectData
            // took place may CryptUnprotectData
            CRYPTPROTECT_LOCAL_MACHINE = 0x4,

            // force credential synchronize during CryptProtectData()
            // Synchronize is only operation that occurs during this operation
            CRYPTPROTECT_CRED_SYNC = 0x8,

            // Generate an Audit on protect and unprotect operations
            CRYPTPROTECT_AUDIT = 0x10,

            // Protect data with a non-recoverable key
            CRYPTPROTECT_NO_RECOVERY = 0x20,


            // Verify the protection of a protected blob
            CRYPTPROTECT_VERIFY_PROTECTION = 0x40
        }

        [DllImport("Crypt32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CryptProtectData(
            ref DATA_BLOB pDataIn,
            String szDataDescr,
            ref DATA_BLOB pOptionalEntropy,
            IntPtr pvReserved,
            ref CRYPTPROTECT_PROMPTSTRUCT pPromptStruct,
            CryptProtectFlags dwFlags,
            ref DATA_BLOB pDataOut
        );

        [DllImport("Crypt32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CryptUnprotectData(
            ref DATA_BLOB pDataIn,
            String szDataDescr,
            ref DATA_BLOB pOptionalEntropy,
            IntPtr pvReserved,
            ref CRYPTPROTECT_PROMPTSTRUCT pPromptStruct,
            CryptProtectFlags dwFlags,
            ref DATA_BLOB pDataOut
        );
        #endregion
    }
}
