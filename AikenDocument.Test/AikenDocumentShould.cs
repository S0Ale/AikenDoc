namespace AikenDocument.Test;

[TestFixture]
internal class AikenDocumentShould{
    [SetUp]
    public void Setup(){ }
    
    [Test]
    public void Throw_FileDoesNotExist(){
        var doc = new AikenDocument();
        Assert.Throws<FileNotFoundException>(() => doc.Load("InvalidPath"));
    }
    
    [Test]
    public void Throw_NoTxtFile(){
        var doc = new AikenDocument();
        Assert.Throws<FormatException>(() => doc.Load(TestService.GetAssetsPath("NoTxt.csv")));
    }
    
    [Test]
    public void Throw_NoAnswerFound(){
        var doc = new AikenDocument();
        Assert.Throws<FormatException>(() => doc.Load(TestService.GetAssetsPath("NoAnswer.txt")));
    }
    
    [Test]
    public void Throw_InvalidAnswer(){
        var doc = new AikenDocument();
        Assert.Throws<FormatException>(() => doc.Load(TestService.GetAssetsPath("InvalidAnswer.txt")));
    }
    
    [Test]
    public void Throw_MultipleAnswers(){
        var doc = new AikenDocument();
        Assert.Throws<FormatException>(() => doc.Load(TestService.GetAssetsPath("MultipleAnswers.txt")));
    }
    
    [Test]
    public void Throw_InvalidOption(){
        var doc = new AikenDocument();
        Assert.Throws<FormatException>(() => doc.Load(TestService.GetAssetsPath("InvalidOption.txt")));
    }
    
    [Test]
    public void Success_SingleQuestion(){
        var doc = new AikenDocument();
        doc.Load(TestService.GetAssetsPath("SingleQuestion.txt"));
        Assert.That(doc.QuestionCount, Is.EqualTo(1));
    }
    
    [Test]
    public void Success_MultipleQuestions(){
        var doc = new AikenDocument();
        doc.Load(TestService.GetAssetsPath("MultipleQuestions.txt"));
        Assert.That(doc.QuestionCount, Is.EqualTo(3));
    }
}