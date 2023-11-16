#include <stdio.h>
#include <math.h>

char a;
int a, b, c, d, radio;
float x, y, z;
char w;

void main()
{
    printf("\nHola mundo");
    printf("\n============");
    printf("\nRadio = %i", radio);
    scanf("%i", &radio);

    radio = (3 + 5) * 8 - (10 - 4) / 2;
    a++; b++; c++; d++;
    z = (a + b) * c;

    if ((3 + 5) * 8 > z)
    {
        printf("\nEs mayor");
    }

    if (radio % 2 == 0)
    {
        printf("\nRadio es par");
    }
    else
    {
        printf("\nRadio es impar");
    }

    
}
