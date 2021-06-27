using System.Collections;
using System.Collections.Generic;
using System;

public class GeneticAlgorithm<T>
{
    public List<Genes<T>> Population { get; private set; }
    public int Generation { get; private set; } // Gen Number that were on 
    public float BestFitness { get; private set; } // 
    public T[] BestGenes { get; private set; }
    public int Elitism { get; private set; }

    public int elitism;
    public float mutationRate;

    private List<Genes<T>> newPopulation;
    private Random random;
    private float fitnessSum;
    private int dnaSize;
    private Func<T> getRandomGene;
    private Func<int, float> fitnessFunction;

    public GeneticAlgorithm(int populationSize, int dnaSize, Random random, Func<T> getRandomGene, Func<int, float> fitnessFunction,
      int elitism, float mutationRate = 0.01f)
    {
        Generation = 1;
        Elitism = elitism;
        mutationRate = mutationRate;
        Population = new List<Genes<T>>(populationSize); // so that is doesnt have to resize
        newPopulation = new List<Genes<T>>(populationSize);
        this.random = random;
        this.dnaSize = dnaSize;
        this.getRandomGene = getRandomGene;
        this.fitnessFunction = fitnessFunction;

        BestGenes = new T[dnaSize];

        for (int i = 0; i < populationSize; i++)
        {
            Population.Add(new Genes<T>(dnaSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));
        }
    }

    public void NewGeneration(int numNewDNA = 0, bool crossoverNewDNA = false)
    {
        int finalCount = Population.Count + numNewDNA;

        if (finalCount <= 0)
        {
            return;
        }

        if (Population.Count > 0)
        {
            CalculateFitness();
            Population.Sort(CompareDNA);
        }
        newPopulation.Clear();
        //crossover
        for (int i = 0; i < Population.Count; i++)
        {
            if (i < Elitism && i < Population.Count)
            {
                newPopulation.Add(Population[i]);
            }
            else if(i < Population.Count || crossoverNewDNA)
            {
                Genes<T> parent1 = ChooseParent();
                Genes<T> parent2 = ChooseParent();

                Genes<T> child = parent1.Crossover(parent2);

                child.Mutate(mutationRate);

                newPopulation.Add(child);
            }
            else
            {
                newPopulation.Add(new Genes<T>(dnaSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));
            }

        }

        List<Genes<T>> tmpList = Population;
        Population = newPopulation;
        newPopulation = tmpList;
        //create new population;
       
        Generation++;
    }

    private int CompareDNA(Genes<T> a, Genes<T> b)
    {
        if (a.Fitness > b.Fitness)
        {
            return -1;
        }
        else if (a.Fitness < b.Fitness)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    private void CalculateFitness()
    {
        fitnessSum = 0;
        Genes<T> best = Population[0];
        //calcuating he fitness of our game takes all the weights of each individual and finds the average
        for (int i = 0; i < Population.Count; i++)
        {
            fitnessSum += Population[i].CalculateFitness(i);
            //if this fitness is better than the one before 
            if (Population[i].Fitness > best.Fitness)
            {
                best = Population[i];
            }
        }

        //save the best fitness we find 
        BestFitness = best.Fitness;
        best._Genes.CopyTo(BestGenes, 0);
    }
    private Genes<T> ChooseParent()
    {
        double randomNumber = random.NextDouble() * fitnessSum;

        for (int i = 0; i < Population.Count; i++)
        {
            
            if (randomNumber < Population[i].Fitness)
            {
                return Population[i];
            }

            randomNumber -= Population[i].Fitness;
        }
        return null;
    }
}
