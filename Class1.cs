// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Runtime.InteropServices;
using Z.Expressions;

namespace NativeLibrary
{
    public class Class1
    {
        // Use common prefix for all entrypoints to avoid symbol name collisions
        [UnmanagedCallersOnly(EntryPoint = "aotsample_add")]
        public static int Add(int a, int b)
        {
            var x = "1+2".Execute<int>(); 

            return a + b + x;
        }

        [UnmanagedCallersOnly(EntryPoint = "aotsample_write_line")]
        public static int WriteLine(IntPtr pString)
        {
            // The marshalling code is typically auto-generated by a custom tool in larger projects.
            try
            {
                // UnmanagedCallersOnly methods only accept primitive arguments. The primitive arguments
                // have to be marshalled manually if necessary.
                string str = Marshal.PtrToStringAnsi(pString);

                Console.WriteLine(str);
            }
            catch
            {
                // Exceptions escaping out of UnmanagedCallersOnly methods are treated as unhandled exceptions.
                // The errors have to be marshalled manually if necessary.
                return -1;
            }
            return 0;
        }

        [UnmanagedCallersOnly(EntryPoint = "aotsample_sumstring")]
        public static IntPtr sumstring(IntPtr first, IntPtr second)
        {
            // Parse strings from the passed pointers 
            string my1String = Marshal.PtrToStringAnsi(first);
            string my2String = Marshal.PtrToStringAnsi(second);

            // Concatenate strings 
            string sum = my1String + my2String;

            // Assign pointer of the concatenated string to sumPointer
            IntPtr sumPointer = Marshal.StringToCoTaskMemAnsi(sum);

            // Return pointer
            return sumPointer;
        }
    }
}
