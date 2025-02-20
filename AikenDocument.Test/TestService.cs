namespace AikenDocument.Test;

internal abstract class TestService{
    private const string AssetsDir = "./assets";
    
    public static string GetAssetsPath(string fileName){
        return $"{AssetsDir}/{fileName}";
    }
    
    public static string GetTextFromFile(string fileName){
        return File.ReadAllText($"{AssetsDir}/{fileName}");
    }
}