namespace AikenDocument.Test;

[TestFixture]
internal class AikenDocumentShould{
    [SetUp]
    public void Setup(){ }

    [Test]
    public void Success_CorrectFileLoad(){
        var doc = new AikenDocument();
        doc.Load(TestService.GetAssetsPath("TestQuestion.txt"));
        Assert.That(doc.QuestionCount, Is.EqualTo(1));
    }
}