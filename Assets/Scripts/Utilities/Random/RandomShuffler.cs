using System;
using System.Linq;

public class RandomShuffler<T> : IShuffler<T>
{
    public T[] shuffle(T[] elements)
    {
        var rnd =  new Random();
        var randomList = Enumerable.Range(0, elements.Length).OrderBy(e => rnd.Next()).ToList();
        T[] shuffledElements = new T[elements.Length];
        int ind = 0;
        foreach(var i in randomList)
        {
            shuffledElements[ind] = elements[i];
            ind += 1;
        }
        return shuffledElements;
    }
}