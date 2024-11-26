using Newtonsoft.Json;
using System;
using System.IO;

public class AppConfig
{
    public string DATABASE_FILE { get; set; }
    public int NUM_SIMULATORS { get; set; }
    public int POPULATION_SIZE { get; set; }
    public int OFFSPRING_SIZE { get; set; }
    public int NUM_GENERATIONS { get; set; }
    public int CMAES_NUM_GENERATIONS { get; set; }
    public double CMAES_INITIAL_STD { get; set; }
    public int CMAES_POP_SIZE { get; set; }
    public float[] CMAES_BOUNDS { get; set; }
    public int GRID_SIZE { get; set; }
    public int ROUND_LENGTH { get; set; }
    public int GENERATION_INDEX { get; set; }
}

public class ConfigManager
{
    public static AppConfig LoadConfig(string path)
    {
        using (StreamReader file = File.OpenText(path))
        {
            JsonSerializer serializer = new JsonSerializer();
            AppConfig config = (AppConfig)serializer.Deserialize(file, typeof(AppConfig));
            return config;
        }
    }

    public static void SaveConfig(string path, AppConfig config)
    {
        using (StreamWriter file = File.CreateText(path))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            serializer.Serialize(file, config);
        }
    }
}