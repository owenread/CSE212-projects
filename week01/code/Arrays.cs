public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // 1. Create an array of size numberOfMultiples
        double[] result = new double[length];
        // 2. Loop from 0 to numberOfMultiples
        for (int i = 0; i < length; i++)
        {
            // 3. For each index, calculate start * (index + 1)
            // 4. Store it in the array
            result[i] = number * (i + 1);

        }
        // 5. Return the array
        return result; // replace this return statement with your own
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // 1. Get a count of how many data items we have to work with in the list
        int count = data.Count;

        // 2.  get the tail (last 'amount' items) and the front (the rest)
        List<int> tail = data.GetRange(count - amount, amount);
        List<int> front = data.GetRange(0, count - amount);

        // 3. combining tail + front in the new order
        List<int> rotated = new List<int>(count);
        // 4. Create a new variable that hold the new list order that we will use to replace the data in the original list
        // and add the tal (last 'amount' items) to the front of the list.
        rotated.AddRange(tail);
        // 5. Add the front (the rest) to the end of the temporary list. 
        rotated.AddRange(front);

        // 6. clear the original list and repopulate it with the data from out temporary, rotated list. 
        data.Clear();
        data.AddRange(rotated);
    }
}

