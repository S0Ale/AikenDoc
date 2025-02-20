namespace AikenDocument.Test;

internal abstract class TestService{
    public const string AssetsDir = "./assets";
    
    public static string GetAssetsPath(string fileName){
        return $"{AssetsDir}/{fileName}";
    }
    
    public static void RemoveAsset(string fileName){
        // Remove the file if it exists
        if (File.Exists($"{AssetsDir}/{fileName}"))
            File.Delete($"{AssetsDir}/{fileName}");
    }
    
    public static string GetTextFromFile(string fileName){
        return File.ReadAllText($"{AssetsDir}/{fileName}");
    }
}