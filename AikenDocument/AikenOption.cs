namespace AikenDocument;

/// <summary>
/// Represents an option of an Aiken question.
/// </summary>
public class AikenOption(string txt, string letter) : AikenElement(txt){
    /// <summary>
    /// The pattern that an option must match.
    /// </summary>
    public const string Pattern = @"^[A-E][\.\)]\s+.+$";

    /// <summary>
    /// The letter of the option.
    /// </summary>
    public string Letter{ get; } = letter;
    
    /// <summary>
    /// Indicates whether the option is the correct answer.
    /// </summary>
    public bool IsCorrect{ get; set; }
}