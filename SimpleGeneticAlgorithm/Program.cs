using System;

namespace SimpleGeneticAlgorithm
{
    class Program
    {

        //--------------------------------------------------------------------------\\
        //--------------------------------------------------------------------------\\


        static void Main(string[] args)
        {
            int generations = 1;
            Population population = new Population("Population");

            population.GeneratePopulation();
            population.EvaluateFitness();
            //population.DisplayAverageParentFitness();
            //population.DisplayParentPopulation();

            for (int i = 1; i <= generations; i++)
            {
                population.TournamentSelection();
                population.SinglePointCrossoverOffspring();
                population.DisplayParentPopulation();
                population.MutateOffspring();
                population.EvaluateFitness();
                population.DisplayOffspringPopulation();
                population.generation = i;
                population.DisplayAverageFitness();
                population.SwitchGenerations();
            }
        }

        //--------------------------------------------------------------------------//
        //--------------------------------------------------------------------------//

        class Population
        {
            const int popSize = 1;
            const int nOfBits = 50;
            const int mutationThreshold = 10; //percentage mutation chance

            Random random = new Random();

            public Individual[] parent;
            public Individual[] offspring;

            public string name;
            public int generation;

            public Population(string s)
            {
                this.parent = new Individual[popSize];
                this.offspring = new Individual[popSize];
                this.name = s;
            }

            public void GeneratePopulation()
            {
                for (int p = 0; p < popSize; p++)
                {
                    parent[p] = new Individual(nOfBits);
                    offspring[p] = new Individual(nOfBits);

                    for (int n = 0; n < nOfBits; n++)
                    {
                        parent[p].gene[n] = this.RandomBinary();
                    }
                }
            }

            public void EvaluateFitness()
            {
                for (int p = 0; p < popSize; p++)
                {
                    parent[p].FitnessCountingOnes();
                    offspring[p].FitnessCountingOnes();
                }
            }


            public void DisplayParentPopulation()
            {
                for (int p = 0; p < popSize; p++)
                {
                    for (int n = 0; n < nOfBits; n++)
                    {
                        //Console.Write(population[p].gene[n]);
                        int temp = 0;
                        if (parent[p].gene[n]) { temp = 1; }
                        else { temp = 0; }
                        Console.Write(temp);
                    }
                    Console.WriteLine(", fitness: " + parent[p].fitness);
                }
            }

            public void DisplayOffspringPopulation()
            {
                int temp;

                for (int p = 0; p < popSize; p++)
                {
                    for (int n = 0; n < nOfBits; n++)
                    {
                        if(offspring[p].crossOverPoint == n) { Console.Write("|"); }

                        temp = 0;
                        if (offspring[p].gene[n]) { temp = 1; }
                        else { temp = 0; }
                        Console.Write(temp);
                    }
                    Console.WriteLine(", fitness: " + offspring[p].fitness);

                }
            }

            public void DisplayAverageFitness()
            {
                int sum = 0;
                int sum2 = 0;

                for (int p = 0; p < popSize; p++)
                {
                    sum += parent[p].fitness;
                    sum2 += offspring[p].fitness;
                }

                Console.WriteLine("{0}) Parent fitness: {1}, Offspring fitness: {2}", this.generation, (float)sum / popSize, (float)sum2 / popSize);
            }

            //float AverageFitness(Population[] populations)
            //{


            //    return 1f;
            //}

            public void TournamentSelection()
            {
                int parent1;
                int parent2;

                for (int p = 0; p < popSize; p++)
                {
                    parent1 = random.Next() % popSize;
                    parent2 = random.Next() % popSize;

                    if (parent[parent1].fitness >= parent[parent2].fitness)
                    {
                        offspring[p] = parent[parent1];
                    }
                    else
                    {
                        offspring[p] = parent[parent2];
                    }
                }
            }

            public void SwitchGenerations()
            {
                for (int p = 0; p < popSize; p++)
                {
                    parent[p] = offspring[p];
                }
            }

            public void SinglePointCrossoverOffspring()
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

                    for(int j = randomPoint; j < nOfBits; j++)
                    {
                        temp = offspring[parent1].gene[j];
                        offspring[parent1].gene[j] = offspring[parent2].gene[j];
                        offspring[parent2].gene[j] = temp;

                        offspring[parent1].crossOverPoint = randomPoint;
                        offspring[parent2].crossOverPoint = randomPoint;
                    }
                }
            }

            public void MutateOffspring()
            {
                int randomMutationValue;

                for (int p = 0; p < popSize; p++)
                {
                    for(int g = 0; g < nOfBits; g++)
                    {
                        randomMutationValue = random.Next()%100;

                        if (randomMutationValue < mutationThreshold)
                        {                            
                            offspring[p].gene[g] ^= true;
                            Console.Write("v");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                }
                Console.WriteLine();
            }

            private bool RandomBinary()
            {
                return (random.Next() % 2) != 0;
            }

        }
     

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

                for(int i = 0; i < this.numberOfBits; i++)
                {
                    if (this.gene[i]) { this.fitness++; }
                }
            }
        }
    }
}
