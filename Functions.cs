public abstract class Function()
{
    public abstract double this[double x] { get; }

    public static Function operator +(Function x, double y) =>
        new SumFuncition(x, new ConstFunction(y));

    public static Function operator *(Function x, double y) =>
        new MultFunction(x, new ConstFunction(y));

    public static implicit operator Function(double x) => new ConstFunction(x);

    public abstract Function Derive();
}

public class Linear() : Function
{
    public override double this[double x] => x;

    public override Function Derive() => new ConstFunction(1);
}

public class SumFuncition(Function g, Function h) : Function
{
    public override double this[double x] => g[x] + h[x];

    public override Function Derive() => new SumFuncition(g.Derive(), h.Derive());
}

public class MultFunction(Function g, Function h) : Function
{
    public override double this[double x] => g[x] * h[x];

    public override Function Derive() =>
        new SumFuncition(new MultFunction(g, h.Derive()), new MultFunction(g.Derive(), h));
}

public class ConstFunction(double c) : Function
{
    public override double this[double x] => c;

    public override Function Derive() => new ConstFunction(0);
}

public class CompositionFunction(Function f, Function g) : Function
{
    public override double this[double x] => f[g[x]];

    public override Function Derive() =>
        new MultFunction(new CompositionFunction(f.Derive(), g), g.Derive());
}

public class PowFunction(Function f, Function g) : Function
{
    public override double this[double x] => Math.Pow(f[x], g[x]);

    public override Function Derive() =>
        new MultFunction(
            this,
            new SumFuncition(
                new MultFunction(g.Derive(), new LogFunction(g)),
                new MultFunction(g, new DivFunction(f.Derive(), f))
            )
        );
}

public class CosFunction() : Function
{
    public override double this[double x] => Math.Cos(x);

    public override Function Derive() => new SinFunctionNeg();
}

public class SinFunctionNeg() : Function
{
    public override double this[double x] => -Math.Sin(x);

    public override Function Derive() => throw new NotImplementedException();
}

public class LogFunction(Function f) : Function
{
    public override double this[double x] => Math.Log(f[x]);

    public override Function Derive() => throw new NotImplementedException();
}

public class DivFunction(Function f, Function g) : Function
{
    public override double this[double x] => f[x] / g[x];

    public override Function Derive()
    {
        throw new NotImplementedException();
    }
}
