using System.Text.Json;

public class UnitDataDefine
{
    public Dictionary<string, UnitStatData> UnitData = new();
    public string StarterUnitId = "1";

    public Dictionary<string, UnitStatData> GetUnitData()
    {
        return UnitData;
    }

    public UnitDataDefine()
    {
        LoadUnitData();
    }

    private void LoadUnitData()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "UnitData.json");

        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IncludeFields = true;
            var deserializedData = JsonSerializer.Deserialize<Dictionary<string, UnitStatData>>(jsonString, options);
            
            if (deserializedData != null)
            {
                UnitData = deserializedData;
            }
        }
        else
        {
            Console.WriteLine($"Warning: UnitData.json not found at {path}");
        }
    }

    public UnitStatData GetUnitById(string unitId)
    {
        if (UnitData.TryGetValue(unitId, out UnitStatData unitData))
        {
            return unitData;
        }

        return null;
    }
}