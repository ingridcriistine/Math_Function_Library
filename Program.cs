using static System.Console;

Function x3 = new PowFunction(new Linear(), new ConstFunction(3));
Function x2 = new MultFunction(
    new ConstFunction(-4),
    new PowFunction(new Linear(), new ConstFunction(2))
);
Function x1 = new MultFunction(new ConstFunction(-6), new Linear());
Function poli = new SumFuncition(
    new SumFuncition(x3, x2),
    new SumFuncition(x1, new ConstFunction(12))
);

var newton = new DivFunction(poli, poli.Derive());

double x = 4;
for (int i = 0; i < 4; i++)
{
    x = x - newton[x];
}

WriteLine(x);
