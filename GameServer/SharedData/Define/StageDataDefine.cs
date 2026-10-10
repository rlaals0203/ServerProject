using System.Text.Json;

public class StageDataDefine
{
    private readonly Dictionary<string, StageData> _stageData = new();

    public StageDataDefine()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "StageData.json");

        if (!File.Exists(path))
        {
            Console.WriteLine($"Warning: StageData.json not found at {path}");
            return;
        }

        string json = File.ReadAllText(path);
        Dictionary<string, StageData> data = JsonSerializer.Deserialize<Dictionary<string, StageData>>(json);

        if (data == null)
        {
            return;
        }

        foreach (var stage in data)
        {
            _stageData[stage.Key] = stage.Value;
        }
    }

    public StageData GetStageById(string stageId)
    {
        _stageData.TryGetValue(stageId, out var stageData);
        return stageData;
    }
}
