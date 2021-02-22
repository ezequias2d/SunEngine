// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace SunEngine
{
    public static class SunMath
    {
        public static readonly float PI = (float)Math.PI;
        public static readonly float Deg2RadF = (float)(Math.PI / 180d);
        public static readonly float Rad2DegF = (float)(180d / Math.PI);
        public static readonly double Deg2RadD = (Math.PI / 180d);
        public static readonly double Rad2DegD = (180d / Math.PI);

        public static readonly float Epsilon = float.Epsilon;

        public static float ToRadians(float degrees) => degrees * Deg2RadF;
        public static double ToRadians(double degress) => degress * Deg2RadD;

        public static Vector3 ToRadians(this Vector3 degrees) => degrees * Deg2RadF;

        public static float ToDegrees(float radians) => radians * Rad2DegF;
        public static double ToDegrees(double radians) => radians * Rad2DegD;


        public static Vector3 ToDegrees(this Vector3 radians) => radians * Rad2DegF;

        public static bool Approximately(float a, float b)
        {
            if (a == 0 || b == 0)
                return Math.Abs(a - b) <= Epsilon;
            return Math.Abs(a - b) / Math.Abs(a) <= Epsilon &&
                Math.Abs(a - b) / Math.Abs(b) <= Epsilon;
        }

        public static int Floor(float value)
        {
            int i = (int)value;
            if (i > value)
                i--;
            return i;
        }

        public static int Ceiling(float value)
        {
            int i = (int)value;
            if (i < value)
                i++;
            return i;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Round(float value)
        {
            int i = (int)value;

            if (value - i > 0.5f)
                i++;

            return i;
        }

        public static Vector3 ToEulerAngles(this Quaternion q)
        {
            Vector3 euler;

            // if the input quaternion is normalized, this is exactly one. Otherwise, this acts as a correction factor for the quaternion's not-normalizedness
            float unit = (q.X * q.X) + (q.Y * q.Y) + (q.Z * q.Z) + (q.W * q.W);

            // this will have a magnitude of 0.5 or greater if and only if this is a singularity case
            float test = q.X * q.W - q.Y * q.Z;

            if (test > 0.4995f * unit) // singularity at north pole
            {
                euler.X = (float)(Math.PI) / 2;
                euler.Y = 2f * (float)Math.Atan2(q.Y, q.X);
                euler.Z = 0;
            }
            else if (test < -0.4995f * unit) // singularity at south pole
            {
                euler.X = -(float)(Math.PI) / 2;
                euler.Y = -2f * (float)Math.Atan2(q.Y, q.X);
                euler.Z = 0;
            }
            else // no singularity - this is the majority of cases
            {
                euler.X = (float)Math.Asin(2f * (q.W * q.X - q.Y * q.Z));
                euler.Y = (float)Math.Atan2(2f * q.W * q.Y + 2f * q.Z * q.X, 1 - 2f * (q.X * q.X + q.Y * q.Y));
                euler.Z = (float)Math.Atan2(2f * q.W * q.Z + 2f * q.X * q.Y, 1 - 2f * (q.Z * q.Z + q.X * q.X));
            }

            return euler * Rad2DegF;
        }

        public static Quaternion ToQuaternion(this Vector3 eulerAngles)
        {
            Vector3 eulerAnglesRadians = eulerAngles * Deg2RadF;
            return Quaternion.CreateFromYawPitchRoll(eulerAnglesRadians.Y, eulerAnglesRadians.X, eulerAnglesRadians.Z);
        }
    }
}
