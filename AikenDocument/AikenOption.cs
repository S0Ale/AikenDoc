namespace AikenDocument;

public class AikenOption{
    public const string Pattern = @"^[A-E][\.\)]\s+.+$";

    public string Text{ get; set; } = "";
    public bool IsCorrect { get; set; }
}