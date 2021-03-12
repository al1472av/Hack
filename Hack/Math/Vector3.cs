using System.Runtime.InteropServices;

namespace Hack.Math
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3
    {
        public float X, Y, Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3 operator +(Vector3 left, Vector3 right)
            => new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        public static Vector3 operator -(Vector3 left, Vector3 right)
            => new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        public override string ToString() => $"X:{X} Y:{Y} Z:{Z}";
    }
}