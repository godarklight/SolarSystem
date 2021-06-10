using System;

public class Integrator<T> where T : Integratable<T>
{
    private static T StepEuler(Func<double, T, T> derivative, double deltaTime, T initial)
    {
        T k1 = derivative(0, initial);
        T thisStep = k1.Multiply(deltaTime);
        return initial.Add(thisStep);
    }

    private static T StepMidpoint(Func<double, T, T> derivative, double deltaTime, T initial)
    {
        T inner = derivative(0, initial).Multiply(0.5d * deltaTime);
        T k1 = derivative(0.5 * deltaTime, initial.Add(inner));
        T thisStep = k1.Multiply(deltaTime);
        return initial.Add(thisStep);
    }

    private static T StepRalston(Func<double, T, T> derivative, double deltaTime, T initial)
    {
        T k1 = derivative(0, initial);
        T k2 = derivative(2/3d * deltaTime, initial.Add(k1.Multiply(2/3d * deltaTime)));
        T thisStep = k1.Multiply(1/4d);
        thisStep = thisStep.Add(k2.Multiply(3/4d));
        thisStep = thisStep.Multiply(deltaTime);
        return initial.Add(thisStep);
    }

    private static T StepRK4(Func<double, T, T> derivative, double deltaTime, T initial)
    {
        T k1 = derivative(0d, initial);
        T k2 = derivative(deltaTime / 2d, initial.Add(k1.Multiply(0.5d * deltaTime)));
        T k3 = derivative(deltaTime / 2d, initial.Add(k2.Multiply(0.5d * deltaTime)));
        T k4 = derivative(deltaTime, initial.Add(k3.Multiply(deltaTime)));
        T thisStep = k1.Multiply(1 / 6d);
        thisStep = thisStep.Add(k2.Multiply(2 / 6d));
        thisStep = thisStep.Add(k3.Multiply(2 / 6d));
        thisStep = thisStep.Add(k4.Multiply(1 / 6d));
        thisStep = thisStep.Multiply(deltaTime);
        return initial.Add(thisStep);
    }

    public static T Integrate(Func<double, T, T> derivative, double deltaTime, T initial)
    {
        return StepRK4(derivative, deltaTime, initial);
    }
}