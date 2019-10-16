using System;
//this is just a test to see if I can use Git Bash correctly.
public class Individual
{
    public double fitness;
    public bool[] gene;
    public int crossOverPoint;
    int numberOfBits;

    public Individual(int bits)
    {
        this.fitness = 0;
        this.gene = new bool[bits];
        this.numberOfBits = bits;
        this.crossOverPoint = 0;
    }

    public void FitnessCountingOnes()
    {
        this.fitness = 0;

        for (int i = 0; i < this.numberOfBits; i++)
        {
            if (this.gene[i]) { this.fitness++; }
        }
    }

    public void FitnessXsquared()
    {
        this.fitness = 0;

        for (int i = 0; i < this.numberOfBits; i++)
        {
            if (this.gene[i])
            {
                this.fitness += (int)Math.Pow(2, numberOfBits - i-1);
            }
        }

        this.fitness = (int)Math.Pow(this.fitness, 2);
    }

    public void FitnessBinaryEncoded()
    {
        //f(x,y) = 0.26.( x2 + y2) – 0.48.x.y Where - 15 ≤ x,y ≤ 15
        //4 bits for x and 4 for y, as well as a sign bit for each, so total of 10 bits necessary.
        //alternatively, simply use 5 bits each and add an offset of -16 to enable negative numbers.
        this.fitness = 0;

        if (numberOfBits < 10)
        {
            Console.WriteLine("There are insufficient genes.");
        }
        else if(numberOfBits > 10)
        {
            Console.WriteLine("There are too many genes.");
        }
        else
        {
            int x;
            int y;

            bool[] xAr = new bool[5];
            bool[] yAr = new bool[5];

            Array.Copy(this.gene, xAr, 5);
            Array.ConstrainedCopy(this.gene,5, yAr, 0, 5);

            x = Number.Binary.ToInt(xAr)-16;
            y = Number.Binary.ToInt(yAr)-16;

            this.fitness = 0.26f*(Math.Pow(x, 2) + Math.Pow(y, 2)) - 0.48f*x*y;

            //Console.WriteLine("{0}, {1}", x, y);


            //for(int i = 0; i < 10; i++)
            //{
            //    Console.Write(gene[i]);
            //}
            //Console.WriteLine();

            //for (int i = 0; i < 5; i++)
            //{
            //    Console.Write(xAr[i]);
            //}
            //Console.WriteLine();

            //for (int i = 0; i < 5; i++)
            //{
            //    Console.Write(yAr[i]);
            //}
            //Console.WriteLine();

            //x = Number.Binary.ToInt();
            //y = Number.Binary.ToInt();

        }
    }
}

