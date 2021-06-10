public struct StateVector : Integratable<StateVector>
{
    public Vector3d pos;
    public Vector3d vel;

    public StateVector Add(StateVector rhs)
    {
        StateVector retVal;
        retVal.pos = pos.Add(rhs.pos);
        retVal.vel = vel.Add(rhs.vel);
        return retVal;
    }

    public StateVector Subtract(StateVector rhs)
    {
        StateVector retVal;
        retVal.pos = pos.Subtract(rhs.pos);
        retVal.vel = vel.Subtract(rhs.vel);
        return retVal;
    }

    public StateVector Multiply(StateVector rhs)
    {
        StateVector retVal;
        retVal.pos = pos.Multiply(rhs.pos);
        retVal.vel = vel.Multiply(rhs.vel);
        return retVal;
    }

    public StateVector Multiply(double rhs)
    {
        StateVector retVal;
        retVal.pos = pos.Multiply(rhs);
        retVal.vel = vel.Multiply(rhs);
        return retVal;
    }

    public StateVector Divide(StateVector rhs)
    {
        StateVector retVal;
        retVal.pos = pos.Divide(rhs.pos);
        retVal.vel = vel.Divide(rhs.vel);
        return retVal;
    }

    public StateVector Divide(double rhs)
    {
        StateVector retVal;
        retVal.pos = pos.Divide(rhs);
        retVal.vel = vel.Divide(rhs);
        return retVal;
    }
}