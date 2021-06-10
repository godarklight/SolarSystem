public interface Integratable<T>
{
    T Add(T rhs);
    T Multiply(T rhs);
    T Multiply(double rhs);
    T Divide(double rhs);
}