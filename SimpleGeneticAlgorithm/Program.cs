using System;
using Number;

namespace SimpleGeneticAlgorithm
{
    class Program
    {        
        //--------------------------------------------------------------------------\\
        //--------------------------------------------------------------------------\\
        static void Main(string[] args)
        {
            const int generations = 50;
            const int popSize = 10;
            const int nOfBits = 10;
            const int mutationThreshold = 1; //percentage mutation chance with a change

            Population parent = new Population("Parent", popSize, nOfBits, mutationThreshold);
            Population offspring = new Population("Offspring", popSize, nOfBits, mutationThreshold);

            parent.GeneratePopulation();
            //parent.DisplayPopulation(0);
            //parent.DisplayFitnessStats(1, false); 

            offspring.GeneratePopulation();
            //offspring.DisplayPopulation(1);
            //offspring.DisplayFitnessStats();
            Console.WriteLine("Generations: {0} popSize: {1} nOfBits: {2} mutationThresh: {3}.", generations, popSize, nOfBits, mutationThreshold);
            Console.WriteLine(",Min,Max,Average");

            for (int i = 1; i <= generations; i++)
            {
                parent.TournamentSelection(offspring, false);
                //parent.RouletteWheelSelection(offspring);

                offspring.SinglePointCrossover();
                offspring.Mutate(false);

                offspring.EvaluateFitness();
                //offspring.DisplayPopulation(i);
                offspring.DisplayFitnessStats(i, true);

                offspring.SwitchPopulationTo(parent);
            }
            //Utility.Binary.ToInt()
            //asdf
        }
        //--------------------------------------------------------------------------//
        //--------------------------------------------------------------------------//
    }
}
