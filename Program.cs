using static System.Console;

Function x = new Linear();

var poli = (x ^ 3) + -4 * (x ^ 2) + -6 * x + 12;

var newton = poli / poli.Derive();

double m = 4;
int count = 0;
var aux = 0d;
while (Math.Round(aux, 3) != Math.Round(m, 3))
{
    aux = m;
    m = m - newton[m];
    count++;
}

WriteLine($"Foram {count} tentativar valor encontrado: {m}");
