using System.Text;

namespace AikenDoc;

/// <summary>
/// Represents an Aiken question.
/// </summary>
public class AikenQuestion(string txt) : AikenElement(txt), ICloneable{
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
        if (Options.All(option => option.Letter != optionLetter))
            throw new FormatException($"The provided letter is not a valid option for this question \"{Text}\"");
        
        foreach (var option in Options) option.IsCorrect = false;
        
        foreach (var option in Options.Where(option => option.Letter == optionLetter)){
            option.IsCorrect = true;
            return;
        }
    }

    /// <summary>
    /// Returns the text of the question and its options.
    /// </summary>
    /// <returns>All the question text.</returns>
    public string GetAllText(){
        var builder = new StringBuilder();
        // Add question text with a new line
        builder = builder.AppendLine(Text);
        foreach (var option in Options)
            builder = builder.AppendLine($"{option.Letter}) {option.Text}");
        
        builder = builder.Append($"ANSWER: {CorrectAnswer}");
        
        return builder.ToString();
    }

    /// <summary>
    /// Adds a new option to the question.
    /// </summary>
    /// <param name="text">The option's text.</param>
    /// <param name="letter">The option's letter.</param>
    public void AddOption(string text, string letter){
        Options.Add(new AikenOption(text, letter));
        if (Options.Count == 1) Options[0].IsCorrect = true;
    }
    
    /// <summary>
    /// The correct answer of the question.
    /// </summary>
    public string CorrectAnswer => Options.FirstOrDefault(option => option.IsCorrect)?.Letter ?? "";

    /// <summary>
    /// Creates a new instance of the <see cref="AikenQuestion"/> class.
    /// </summary>
    /// <returns>A new instance of <see cref="AikenQuestion"/>.</returns>
    public object Clone(){
        var question = new AikenQuestion(Text);
        foreach (var option in Options) question.Options.Add((AikenOption)option.Clone());
        return question;
    }
}