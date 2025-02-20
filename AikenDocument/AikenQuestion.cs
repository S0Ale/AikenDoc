namespace AikenDocument;

/// <summary>
/// Represents an Aiken question.
/// </summary>
public class AikenQuestion(string txt) : AikenElement(txt){
    /// <summary>
    /// The options of the question.
    /// </summary>
    public List<AikenOption> Options{ get; } = [];

    /// <summary>
    /// Sets the correct option of the question.
    /// </summary>
    /// <param name="optionLetter">The letter of the correct option.</param>
    /// <exception cref="ArgumentException">Thrown when an invalid letter is provided.</exception>
    public void SetCorrectOption(string optionLetter){
        foreach (var option in Options){
            option.IsCorrect = false;
        }

        foreach (var option in Options.Where(option => option.Letter == optionLetter)){
            option.IsCorrect = true;
            return;
        }
    }
    
    /// <summary>
    /// The correct answer of the question.
    /// </summary>
    public string CorrectAnswer => Options.FirstOrDefault(option => option.IsCorrect)?.Letter ?? "";
}