using System;

public class Individual
{

    public int fitness;
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
}

