using System;
using System.Collections.Generic;
public static class Calculator
{
    public static double CircularOrbitalVelocity(double height, double mass)
    {
        return Math.Sqrt(Constants.G * mass / height);
    }
}