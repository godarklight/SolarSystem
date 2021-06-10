using System;
using System.Collections.Generic;
public class ForceCalculator
{
    private Body body;
    private List<Body> bodies;
    Integrator<StateVector> integrator = new Integrator<StateVector>();

    public ForceCalculator(Body body, List<Body> bodies)
    {
        this.body = body;
        this.bodies = bodies;
    }

    public StateVector GetAccelerationAtState(double deltaTime, StateVector state)
    {
        StateVector retVal = new StateVector();
        Vector3d updatedPos = state.pos + state.vel.Multiply(deltaTime);
        foreach (Body foreignBody in bodies)
        {
            if (foreignBody == body)
            {
                continue;
            }
            Vector3d vectorToBody = foreignBody.state.pos - updatedPos;
            double radius = vectorToBody.length;
            double force = Constants.G * foreignBody.mass / (radius * radius * radius);
            Vector3d newForce = vectorToBody * force;
            retVal.vel = retVal.vel + newForce;
        }
        retVal.pos = state.vel + retVal.vel;
        return retVal;
    }
}