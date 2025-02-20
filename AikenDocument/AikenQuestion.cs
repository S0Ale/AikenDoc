namespace AikenDocument;

public class AikenQuestion{
    public string Text { get; set; }
    public List<AikenOption> Options { get; private set; }

    public AikenQuestion()
    {
        Options = new List<AikenOption>();
    }

    /// <summary>
    /// Imposta l'opzione corretta specificando la lettera (es. "A", "B", ...)
    /// </summary>
    public void SetCorrectOption(string optionLetter)
    {
        // Reset di tutte le opzioni come non corrette
        foreach (var option in Options)
        {
            option.IsCorrect = false;
        }

        // Imposta l'opzione corretta
        int index = optionLetter.ToUpper()[0] - 'A';
        if (index >= 0 && index < Options.Count)
        {
            Options[index].IsCorrect = true;
        }
        else
        {
            throw new ArgumentException("Lettera dell'opzione non valida.");
        }
    }
}