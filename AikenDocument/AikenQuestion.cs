namespace AikenDocument;

public class AikenQuestion{
    public string? CorrectAnswer { get; set; }
    
    public string Text{ get; set; } = "";
    public List<AikenOption> Options{ get; } = [];

    /// <summary>
    /// Sets the correct option of the question
    /// </summary>
    public void SetCorrectOption(string optionLetter){
        foreach (var option in Options){
            option.IsCorrect = false;
        }
        
        var index = optionLetter.ToUpper()[0] - 'A';
        if (index >= 0 && index < Options.Count){
            Options[index].IsCorrect = true;
        }else throw new ArgumentException("Lettera dell'opzione non valida.");
    }
}