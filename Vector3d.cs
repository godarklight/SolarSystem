using System;
public struct Vector3d : Integratable<Vector3d>
{
    public double X;
    public double Y;
    public double Z;

    public Vector3d(double X, double Y, double Z)
    {
        this.X = X;
        this.Y = Y;
        this.Z = Z;
    }

    public Vector3d Add(Vector3d rhs)
    {
        Vector3d retVal;
        retVal.X = X + rhs.X;
        retVal.Y = Y + rhs.Y;
        retVal.Z = Z + rhs.Z;
        return retVal;
    }


    public static Vector3d operator +(Vector3d lhs, Vector3d rhs)
    {
        return lhs.Add(rhs);
    }

    public Vector3d Subtract(Vector3d rhs)
    {
        Vector3d retVal;
        retVal.X = X - rhs.X;
        retVal.Y = Y - rhs.Y;
        retVal.Z = Z - rhs.Z;
        return retVal;
    }

    public static Vector3d operator -(Vector3d lhs, Vector3d rhs)
    {
        return lhs.Subtract(rhs);
    }

    public Vector3d Multiply(double rhs)
    {
        Vector3d retVal;
        retVal.X = X * rhs;
        retVal.Y = Y * rhs;
        retVal.Z = Z * rhs;
        return retVal;
    }

    public Vector3d Multiply(Vector3d rhs)
    {
        Vector3d retVal;
        retVal.X = X * rhs.X;
        retVal.Y = Y * rhs.Y;
        retVal.Z = Z * rhs.Z;
        return retVal;
    }

    public static Vector3d operator *(Vector3d lhs, double rhs)
    {
        return lhs.Multiply(rhs);
    }

    public static Vector3d operator *(Vector3d lhs, Vector3d rhs)
    {
        return lhs.Multiply(rhs);
    }

    public Vector3d Divide(double rhs)
    {
        Vector3d retVal;
        retVal.X = X / rhs;
        retVal.Y = Y / rhs;
        retVal.Z = Z / rhs;
        return retVal;
    }

    public Vector3d Divide(Vector3d rhs)
    {
        Vector3d retVal;
        retVal.X = X / rhs.X;
        retVal.Y = Y / rhs.Y;
        retVal.Z = Z / rhs.Z;
        return retVal;
    }

    public static Vector3d operator /(Vector3d lhs, double rhs)
    {
        return lhs.Divide(rhs);
    }

    public static Vector3d operator /(Vector3d lhs, Vector3d rhs)
    {
        return lhs.Divide(rhs);
    }

    public double length
    {
        get
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }
    }

    public Vector3d normal
    {
        get
        {
            double normalLength = length;
            if (normalLength < double.Epsilon)
            {
                return new Vector3d(1, 0, 0);
            }
            return this / normalLength;
        }
    }

    public override string ToString()
    {
        return $"[{Math.Round(X, 3)}, {Math.Round(Y, 3)}, {Math.Round(Z, 3)}]";
    }
}