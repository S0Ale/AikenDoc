namespace AikenDocument;

/// <summary>
/// Represents an element of an Aiken question.
/// </summary>
public abstract class AikenElement(string txt){
    /// <summary>
    /// The text of the element.
    /// </summary>
    public string Text{ get; } = txt ?? "";
}