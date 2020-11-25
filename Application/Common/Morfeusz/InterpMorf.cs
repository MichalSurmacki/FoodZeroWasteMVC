using System.Runtime.InteropServices;

namespace Application.Common.Morfeusz
{
    [StructLayout(LayoutKind.Sequential)]
    public struct InterpMorf
    {
        public int p;

        public int k;

        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public string forma;

        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public string haslo;

        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public string interp;
    }
}
