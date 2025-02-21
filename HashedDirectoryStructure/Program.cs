using System.Text;

namespace HashedDirectoryStructure;

internal class Program
{
    //const int MIN_COUNT = 94;
    //const int MAX_COUNT = 215;
    const int MIN_COUNT = 800;
    const int MAX_COUNT = 1200;

    static void Main(string[] args)
    {
        TestNewGuid();

        Console.ReadKey();
    }

    private static void TestNewGuid()
    {        
        // Dictionary to count occurrences of each path
        Dictionary<string, int> counter = new Dictionary<string, int>();
        //int iterations = 10_000_000; // 10 million iterations
        int iterations = 65_536_000; // 65.536 million iterations

        for (int i = 1; i <= iterations; i++)
        {
            // Generate a random UUID string
            string fileName = Guid.NewGuid().ToString();
            int hash = GetHashCodeString(fileName);

            int mask = 255;
            int firstDir = hash & mask;
            int secondDir = (hash >> 8) & mask;

            // Build the path similar to Java's File.separator and String.format("%02x", value)
            string path = new StringBuilder()
                .Append(Path.DirectorySeparatorChar)
                .Append(firstDir.ToString("x2"))
                .Append(Path.DirectorySeparatorChar)
                .Append(secondDir.ToString("x2"))
                .ToString();

            // Increment the count for this path
            if (counter.ContainsKey(path))
            {
                counter[path]++;
            }
            else
            {
                counter[path] = 1;
            }
        }

        int count = 0;
        foreach (var key in counter.Keys)
        {
            Console.WriteLine($"{++count}. {key}: {counter[key]}");

            if (counter[key] < MIN_COUNT)
            {
                Console.ReadKey();
                // throw new ArgumentOutOfRangeException($"cont < {MIN_COUNT}");
            }
            else if(counter[key] >= MAX_COUNT)
            {
                Console.ReadKey();
                // throw new ArgumentOutOfRangeException($"cont >= {MAX_COUNT}");
            }
        }

        Console.WriteLine();

        return;
    }

    private static int GetHashCodeString(string s)
    {
        int h = 0;
        if (!string.IsNullOrEmpty(s))
        {
            for (int i = 0; i < s.Length; i++)
            {
                h = (h << 5) - h + s[i];
            }
        }
        return h;
    }
}
