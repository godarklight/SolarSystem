using System;
using System.Collections.Generic;
public class Body
{
    public ForceCalculator forceCalculator;
    public StateVector state = new StateVector();
    public StateVector newState = new StateVector();
    public double mass;

    public Body(List<Body> bodies)
    {
        forceCalculator = new ForceCalculator(this, bodies);
    }

    public void Integrate(double deltaTime)
    {
        newState = Integrator<StateVector>.Integrate(forceCalculator.GetAccelerationAtState, deltaTime, state);
    }

    public void Apply()
    {
        StateVector temp = state;
        state = newState;
        newState = temp;
    }
}