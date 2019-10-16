using System;

public namespace Utility
{
	public Utility()
	{
	}

    public int BinToInt(bool[] array)
    {
        int temp;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i])
            {
                temp += (int)Math.Pow(2, array.Length - i - 1);
            }
        }

        return temp;
    }
}
