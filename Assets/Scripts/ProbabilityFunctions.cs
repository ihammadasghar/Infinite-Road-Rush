using System;

class ProbabilityFunctions
{
    static Random random = new Random();

    public static int getRandomAmountOfObstacles(int upperLimit, int len){
        int lowerLimit = upperLimit - len;

        // Create a Random object
        Random random = new Random();

        // Generate a random value between 0 and 1
        int randomValue = (int)(len*random.NextDouble());

        return lowerLimit + randomValue;

    }

    //  the probability of returning True is determined by a linear function of the form probability = seconds_passed / max_seconds, 
    //  where max_seconds is a parameter defining the maximum number of seconds for the probability to reach 1.0
    public static bool shouldChangeToNight(double nightProbability)
    {
        // Generate a random value between 0 and 1
        double randomValue = random.NextDouble();

        // Check if the random value is less than the calculated probability
        return randomValue < nightProbability;
    }

    // This function has a bias towards generating cordinates in the middle of the interval
    public static int getRandomCordinate(int low, int high, double biasStrength = 5.0)
    {
        // Ensure that the interval is valid
        if (high <= low)
        {
            throw new ArgumentException("Invalid interval: 'high' must be greater than 'low'.");
        }

        // Calculate the mean of the interval
        double mean = (low + high) / 2.0;

        // Calculate the standard deviation based on the bias strength
        double stdDev = (high - low) / biasStrength;

        // Generate a random number with a normal distribution
        double randomValue = normal(mean, stdDev);

        // Clip the result to ensure it falls within the specified interval
        double clippedValue = Math.Max(low, Math.Min(high, randomValue));

        // Round to the nearest integer
        return (int)Math.Round(clippedValue);
    }

    // Picks number from 0 to X
    // More chance of picking lower numbers using Geometric Distribution
    public static int pickDiscreteNumber(int x)
    {
        // Ensure x is greater than 0
        if (x <= 0)
        {
            throw new ArgumentException("Invalid value for 'x': 'x' must be greater than 0.");
        }

        // Generate a random number with a geometric distribution
        double p = 0.5;
        int randomValue = geometric(p);

        // Clip the result to ensure it falls within the specified range
        return Math.Min(x - 1, randomValue);
    }

    // The max speed changes depending on time
    // Bias towards higher speed using the inverse of geometric distribution
    public static float getEnemySpeed(int upperLimit, int len, double biasStrength = 3.0){
        int lowerLimit = upperLimit - len;

        double m = (upperLimit + lowerLimit)/2.0;

        // Calculate the standard deviation based on the bias strength
        double stdDev = len / biasStrength;

        float randomValue = (float)normal(m, stdDev);

        return randomValue;
    }

    static double normal(double mean, double stdDev)
    {
        double u1 = 1.0 - random.NextDouble();
        double u2 = 1.0 - random.NextDouble();
        double z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
        
        // Scale and shift the result to have the desired mean and standard deviation
        return mean + stdDev * z;
    }

    static int geometric(double p){
        return (int)Math.Floor(Math.Log(random.NextDouble()) / Math.Log(1.0 - p));
    }
}
