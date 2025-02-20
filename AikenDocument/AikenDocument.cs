namespace AikenDocument;

/// <summary>
/// Represents a document containing a list of Aiken questions
/// </summary>
public class AikenDocument{
    private List<AikenQuestion> Questions { get; set; } = [];

    /// <summary>
    /// Loads an Aiken file from the specified path
    /// </summary>
    public void Load(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("Il file specificato non esiste.");

        var lines = File.ReadAllLines(filePath);
        ParseLines(lines);
    }

    /// <summary>
    /// Analyses the lines of the Aiken file and populates the list of questions
    /// </summary>
    private void ParseLines(string[] lines)
    {
        AikenQuestion? currentQuestion = null;

        foreach (var line in lines)
        {
            // Ignores empty lines
            if (string.IsNullOrWhiteSpace(line))
                continue;
            
            // If the line does not start with an option (A., B., C., D., ...) it is the text of a question
            if (!line.StartsWith("A. ") && !line.StartsWith("B. ") &&
                !line.StartsWith("C. ") && !line.StartsWith("D. "))
            {
                currentQuestion = new AikenQuestion { Text = line.Trim() };
                Questions.Add(currentQuestion);
            }
            // Otherwise it is an answer option
            else if (currentQuestion != null)
            {
                var option = new AikenOption
                {
                    Text = line.Substring(3).Trim(),
                    IsCorrect = false
                };
                currentQuestion.Options.Add(option);
            }
        }
    }

    /// <summary>
    /// Finds a question by text
    /// </summary>
    public AikenQuestion? FindQuestionByText(string text)
    {
        return Questions.FirstOrDefault(q => q.Text.Contains(text, StringComparison.OrdinalIgnoreCase));
    }
}