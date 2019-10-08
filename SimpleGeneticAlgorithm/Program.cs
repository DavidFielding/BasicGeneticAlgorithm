using System;

namespace SimpleGeneticAlgorithm
{
    class Program
    {        
        //--------------------------------------------------------------------------\\
        //--------------------------------------------------------------------------\\
        static void Main(string[] args)
        {
            const int generations = 50;
            const int popSize = 50;
            const int nOfBits = 50;
            const int mutationThreshold = 1; //percentage mutation chance

            Population parent = new Population("Parent", popSize, nOfBits, mutationThreshold);
            Population offspring = new Population("Offspring", popSize, nOfBits, mutationThreshold);

            parent.GeneratePopulation();
            //parent.DisplayPopulation(0);
            //parent.DisplayFitnessStats();

            offspring.GeneratePopulation();
            //offspring.DisplayPopulation();
            //offspring.DisplayFitnessStats();

            for (int i = 1; i <= generations; i++)
            {
                parent.TournamentSelection(offspring, false);

                offspring.SinglePointCrossover();
                offspring.Mutate(false);

                offspring.EvaluateFitness();
                offspring.DisplayFitnessStats(i, true);

                offspring.SwitchPopulationTo(parent);
            }
        }
        //--------------------------------------------------------------------------//
        //--------------------------------------------------------------------------//
    }
}
