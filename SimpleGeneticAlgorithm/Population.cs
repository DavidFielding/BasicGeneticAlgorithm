﻿using System;

public class Population
{

    Random random = new Random();

    public Individual[] population;

    public string name;
    public int popSize;
    public int nOfBits;
    int mutationThreshold;

    public Population(string populationName, int pop, int nBits, int threshold)
    {
        popSize = pop;
        population = new Individual[popSize];
        name = populationName;
        nOfBits = nBits;
        mutationThreshold = threshold;
    }

    public void GeneratePopulation()
    {
        for (int p = 0; p < popSize; p++)
        {
            population[p] = new Individual(nOfBits);

            for (int n = 0; n < nOfBits; n++)
            {
                population[p].gene[n] = this.RandomBinary();
            }
        }

        EvaluateFitness();
    }

    public void EvaluateFitness()
    {
        for (int p = 0; p < popSize; p++)
        {
            population[p].FitnessCountingOnes();
        }
    }


    public void DisplayPopulation(int generation)
    {
        Console.WriteLine("{0}) ", generation);

        for (int p = 0; p < popSize; p++)
        {
            for (int n = 0; n < nOfBits; n++)
            {
                int temp = 0;
                if (population[p].gene[n]) { temp = 1; }
                else { temp = 0; }
                Console.Write(temp);
            }
            Console.WriteLine(", fitness: " + population[p].fitness);
        }
        Console.WriteLine();
    }


    public void DisplayFitnessStats(int generation, bool numbersOnly)
    {
        float sum = 0;    //for the average
        int min = nOfBits;
        int max = 0;
        int tempFitness;

        for (int p = 0; p < popSize; p++)
        {
            tempFitness = population[p].fitness;
            sum += tempFitness;
            min = (tempFitness < min) ? tempFitness : min;
            max = (tempFitness > max) ? tempFitness : max;
        }

        if (numbersOnly)
        {
            Console.WriteLine("{4},{0},{1},{2}", min, max, sum / popSize, this.name, generation);
        }
        else
        {
            Console.WriteLine("{4}. {3} population:\t Min) {0}\t Max) {1}\t Mean) {2}.", min, max, sum / popSize, this.name, generation);
        }
    }

    public void TournamentSelection(Population offspringPopulation, bool display)
    {
        int parent1;
        int parent2;
        int newParent;

        for (int p = 0; p < popSize; p++)
        {
            parent1 = random.Next() % popSize;
            parent2 = random.Next() % popSize;

            if (display) { Console.WriteLine("tournament selection: {0}, {1}.", parent1, parent2); }

            if (population[parent1].fitness >= population[parent2].fitness)
            {
                newParent = parent1;
            }
            else
            {
                newParent = parent2;
            }

            for(int g = 0; g< nOfBits; g++)
            {
                offspringPopulation.population[p].gene[g] = population[newParent].gene[g];
            }
            offspringPopulation.population[p].fitness = population[p].fitness;
        }
    }

    public void SinglePointCrossover()
    {
        int parent1;
        int parent2;
        int randomPoint;
        bool temp;

        for (int p = 0; p < popSize; p++)
        {
            randomPoint = random.Next() % nOfBits;

            parent1 = random.Next() % popSize;
            parent2 = random.Next() % popSize;

            for (int j = randomPoint; j < nOfBits; j++)
            {
                temp = population[parent1].gene[j];
                population[parent1].gene[j] = population[parent2].gene[j];
                population[parent2].gene[j] = temp;

                population[parent1].crossOverPoint = randomPoint;
                population[parent2].crossOverPoint = randomPoint;
            }
        }
    }

    public void Mutate(bool display)
    {
        int randomMutationValue;

        for (int p = 0; p < popSize; p++)
        {
            for (int g = 0; g < nOfBits; g++)
            {
                randomMutationValue = random.Next() % 100;

                if (randomMutationValue < mutationThreshold)
                {
                    population[p].gene[g] ^= true;
                    if (display) { Console.Write("v"); }
                }
                else
                {
                    if (display) { Console.Write(" "); }
                }
            }
        }
        if (display) { Console.WriteLine(); }
    }

    private bool RandomBinary()
    {
        return (random.Next() % 2) != 0;
    }

    public void SwitchPopulationTo(Population newPopulation)
    {
        for (int p = 0; p < popSize; p++)
        {
            for(int g = 0; g < nOfBits; g++)
            {
                newPopulation.population[p].gene[g] = population[p].gene[g];
            }
            newPopulation.population[p].fitness = population[p].fitness;
        }
    }
}