using System;

public class Genes<T>
{
    //Basic gene for each member in the population 
    public T[] _Genes { get; private set; }
    public float Fitness { get; private set; }
    private Random random;
    private Func<T> getRandomGene;
    private Func<int, float> fitnessFunction;

    public Genes(int size ,Random random,Func<T> getRandomGene, Func<int ,float> fitnessFunction,bool shouldInitGenes = true)
    {
        _Genes = new T[size];
        this.random = random;
        this.getRandomGene = getRandomGene;
        this.fitnessFunction = fitnessFunction;

        //Making sure the genes are random
        if (shouldInitGenes)
        {
            for (int i = 0; i < _Genes.Length; i++)
            {
                _Genes[i] = getRandomGene();
            }
        }
    }

    public float CalculateFitness(int index)
    {
        Fitness = fitnessFunction(index);
        return Fitness;
    }

    public Genes<T> Crossover(Genes<T> otherParent)
    {
        Genes<T> child = new Genes<T>(_Genes.Length, random, getRandomGene, fitnessFunction, shouldInitGenes: false);
        //Coin flip to see which parent is getting picked 
        for (int i = 0; i < _Genes.Length; i++)
        {
            child._Genes[i] = random.NextDouble() < 0.5 ? _Genes[i] : otherParent._Genes[i];
        }

        return child;
    }


    public void Mutate(float mutataionRate)
    {
        for (int i = 0; i < _Genes.Length; i++)
        {
            if (random.NextDouble() < mutataionRate)
            {
                _Genes[i] = getRandomGene();
            }
        }
    }
}
